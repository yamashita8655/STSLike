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

	private MasterAction2Table.Data Data = null;

	private Action<BattleCardButtonController> ClickCallback = null;
	private Action<MasterAction2Table.Data> DetailClickCallback = null;

	private List<ValueController> ValueControllers = new List<ValueController>();

	public IEnumerator Initialize(
		Action<BattleCardButtonController> clickCallback,
		Action<MasterAction2Table.Data> detailClickCallback
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
		
		while (loadCount != loadedCount) {
			yield return null;
		}
	}

	public void SetData(MasterAction2Table.Data data) {
		Data = data;
	}

	public void UpdateDisplay() {
		ResourceManager.Instance.RequestExecuteOrder(
			string.Format(Const.RarityFrameImagePath, Data.Rarity.ToString()),
			ExecuteOrder.Type.Sprite,
			this.gameObject,
			(rawSprite) => {
				RarityFrameImage.sprite = rawSprite as Sprite;
			}
		);
		
		Name.text = Data.Name;
		Cost.text = Data.DiceCost.ToString();

		var list = Data.ActionPackList;
		int index = 0;
		for (index = 0; index < list.Count; index++) {
			if (index >= 10) {
				LogManager.Instance.LogError("BattleCardButtonController:UpdateDisplay:添え字10以上になってる:" + Data.Name);
			}
			
			int val = list[index].Value;
			if (
				(list[index].Effect == EnumSelf.EffectType.Damage) ||
				(list[index].Effect == EnumSelf.EffectType.DamageSuction) ||
				(list[index].Effect == EnumSelf.EffectType.ShieldBash)
			) {
				val = BattleCalculationFunction.CalcPlayerDamageValue(list[index]);
			} else if (list[index].Effect == EnumSelf.EffectType.Shield) {
				val = BattleCalculationFunction.CalcPlayerShieldValue(list[index]);
			}
			ValueControllers[index].UpdateDisplay(
				list[index].Effect,
				list[index].Value,
				val
			);
		}
	}

	public void UpdateInteractable(int totalCost) {
		if (Data.DiceCost <= totalCost) {
			AttackButton.interactable = true;
		} else {
			AttackButton.interactable = false;
		}
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
