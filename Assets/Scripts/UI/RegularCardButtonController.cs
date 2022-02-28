using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegularCardButtonController : MonoBehaviour
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

    private MasterAction2Table.Data Data = null;

	private Action<MasterAction2Table.Data> ClickCallback = null;
	private Action<RegularCardButtonController> DetailClickCallback = null;

	private List<ValueController> ValueControllers = new List<ValueController>();

	public void Initialize(
		MasterAction2Table.Data data,
		Action<MasterAction2Table.Data> clickCallback,
		Action<RegularCardButtonController> detailClickCallback
	) {
		Data = data;
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
					if (loadCount == loadedCount) {
						UpdateDisplay();
					}
				}
			);
		}
	}

	private void UpdateDisplay() {
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

		Cost.text = Data.DiceCost.ToString();

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
			ClickCallback(Data);
		}
	}
	
	public void OnDetailClick() {
		if (DetailClickCallback != null) {
			DetailClickCallback(this);
		}
	}

	public MasterAction2Table.Data GetData() {
		return Data;
	}
}
