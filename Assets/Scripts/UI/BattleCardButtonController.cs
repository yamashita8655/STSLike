using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleCardButtonController : MonoBehaviour
{
	[SerializeField]
	private Image RarityFrameImage = null;
	
	[SerializeField]
	private Text Name = null;
	
	[SerializeField]
	private Text Cost = null;
	
	[SerializeField]
	private GameObject ValueObjectRoot = null;
	
	[SerializeField]
	private Button AttackButton = null;

    [SerializeField]
    private Image UseTypeImage = null;
    
	[SerializeField]
    private Toggle SelectToggle = null;

    private MasterAction2Table.Data Data = null;

	private Action<BattleCardButtonController> ClickCallback = null;
	private Action<MasterAction2Table.Data> DetailClickCallback = null;
	
	private int CurrentCost = 0;

	private List<ValueController> ValueControllers = new List<ValueController>();

	public IEnumerator Initialize(
		Action<BattleCardButtonController> clickCallback,
		Action<MasterAction2Table.Data> detailClickCallback,
		UnityEngine.Events.UnityAction<bool> toggleUpdateCallback
	) {

		ClickCallback = clickCallback;
		DetailClickCallback = detailClickCallback;

		// ValueObjectの生成
		// とりあえず、10個生成
		int loadCount = 10;
		int loadedCount = 0;
		for (int i = 0; i < loadCount; i++) {
			ResourceManager.Instance.RequestExecuteOrder(
				Const.ValueItemPath,
				ExecuteOrder.Type.GameObject,
				gameObject,
				(rawObject) => {
					GameObject obj = GameObject.Instantiate(rawObject) as GameObject;
					var ctrl = obj.GetComponent<ValueController>();
					ctrl.Initialize(ValueObjectRoot);
					ValueControllers.Add(ctrl);
					loadedCount++;
				}
			);
		}

		SelectToggle.isOn = false;
		SelectToggle.gameObject.SetActive(false);
		
		SelectToggle.onValueChanged.AddListener(toggleUpdateCallback);
		
		while (loadCount != loadedCount) {
			yield return null;
		}
	}

	public void SetData(MasterAction2Table.Data data) {
		Data = data;
	}

	public void UpdateDisplay() {
		var player = MapDataCarrier.Instance.CuPlayerStatus;
		ResourceManager.Instance.RequestExecuteOrder(
			string.Format(Const.RarityFrameImagePath, Data.Rarity.ToString()),
			ExecuteOrder.Type.Sprite,
			this.gameObject,
			(rawSprite) => {
				RarityFrameImage.sprite = rawSprite as Sprite;
			}
		);

		if (Data.UseType == EnumSelf.UseType.Erase) {
			ResourceManager.Instance.RequestExecuteOrder(
				Const.UseTypeEraseImagePath,
				ExecuteOrder.Type.Sprite,
				this.gameObject,
				(rawSprite) => {
					UseTypeImage.sprite = rawSprite as Sprite;
				}
			);
		} else if (Data.UseType == EnumSelf.UseType.Discard) {
			ResourceManager.Instance.RequestExecuteOrder(
				Const.UseTypeDiscardImagePath,
				ExecuteOrder.Type.Sprite,
				this.gameObject,
				(rawSprite) => {
					UseTypeImage.sprite = rawSprite as Sprite;
				}
			);
		} else {
			UseTypeImage.sprite = null;
		}
		
		Name.text = Data.Name;

		int cost = Data.DiceCost;

		if (Data.CostType == EnumSelf.CostType.None) {
			if (BattleCalculationFunction.IsCurse(Data.Id) == true) {
				if (player.GetParameterListFlag(EnumSelf.ParameterType.AntiCurse) == true) {
					cost = 0;
				}
			}
		} else if (Data.CostType == EnumSelf.CostType.ReduceUseCardSheet) {
			cost = Data.DiceCost - player.GetUseCardCount();
		}

		CurrentCost = cost;
		Cost.text = CurrentCost.ToString();

		if (CurrentCost == Data.DiceCost) {
			Cost.color = Color.black;
		} else if (cost > Data.DiceCost) {
			Cost.color = Color.red;
		} else if (cost < Data.DiceCost) {
			Cost.color = Color.green;
		}

		// 一回全て非表示
		for (int i = 0; i < ValueControllers.Count; i++) {
			ValueControllers[i].Hide();
		}

		var list = Data.ActionPackList;
		int index = 0;
		for (index = 0; index < list.Count; index++) {
			if (index >= 10) {
				LogManager.Instance.LogError("BattleCardButtonController:UpdateDisplay:添え字10以上になってる:" + Data.Name);
			}
			
			int val = list[index].Value;
			int originalVal = list[index].Value;
			if (
				(list[index].Effect == EnumSelf.EffectType.Damage) ||
				(list[index].Effect == EnumSelf.EffectType.DamageSuction) ||
				(list[index].Effect == EnumSelf.EffectType.DamageShieldSuction) ||
				(list[index].Effect == EnumSelf.EffectType.DamageGainMaxHp) ||
				(list[index].Effect == EnumSelf.EffectType.ShieldBash)
			) {
				val = BattleCalculationFunction.CalcPlayerDamageValue(list[index]);
			} else if (
				(list[index].Effect == EnumSelf.EffectType.DamageMultiStrength) ||
				(list[index].Effect == EnumSelf.EffectType.DamageFinish)
			) {
				val = BattleCalculationFunction.CalcPlayerDamageValue(list[index]);
				originalVal = 0;
			} else if (list[index].Effect == EnumSelf.EffectType.DamageDice) {
				val = BattleCalculationFunction.CalcPlayerDamageValue(list[index]);
				originalVal = MapDataCarrier.Instance.CurrentTotalDiceCost;
			} else if (
				(list[index].Effect == EnumSelf.EffectType.Shield) ||
				(list[index].Effect == EnumSelf.EffectType.StrengthShield)
			) {
				val = BattleCalculationFunction.CalcPlayerShieldValue(list[index]);
			}
			ValueControllers[index].UpdateDisplay(
				list[index].Effect,
				originalVal,
				val
			);
		}
	}

	public void UpdateInteractable(int totalCost) {
		if (Data.CostType == EnumSelf.CostType.ReduceUseCardSheet) {
			Debug.Log(CurrentCost);
			Debug.Log(totalCost);
		}

		if (CurrentCost <= totalCost) {
			AttackButton.interactable = true;
		} else {
			AttackButton.interactable = false;
		}
	}

	public void UpdateToggleActive(bool isActive) {
		SelectToggle.gameObject.SetActive(isActive);
	}
	
	public bool ToggleActiveSelf() {
		return SelectToggle.gameObject.activeSelf;
	}
	
	public bool IsSelect() {
		return SelectToggle.isOn;
	}
	
	public void ResetToggle() {
		SelectToggle.isOn = false;
	}
	
	public void OnClick() {
		if (ClickCallback != null) {
			ClickCallback(this);
		}
	}
	
	public void OnDetailClick() {
		if (DetailClickCallback != null) {
			DetailClickCallback(Data);
		}
	}

	public MasterAction2Table.Data GetData() {
		return Data;
	}
}
