using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegularSettingCardContentItem : MonoBehaviour
{
	[SerializeField]
	private Image RarityFrameImage = null;
	
	[SerializeField]
	private Image CardImage = null;
	
	[SerializeField]
	private Text CardName = null;
	
	[SerializeField]
	private Text CardDetail = null;
	
	[SerializeField]
	private Image LockImage = null;
	
	[SerializeField]
	private Button RootButton = null;
	
	[SerializeField]
	private Text CostText = null;
	
	private Action<RegularSettingCardContentItem> Callback = null;

	private MasterAction2Table.Data Data = null;

	public void Initialize(MasterAction2Table.Data data, Action<RegularSettingCardContentItem> callback) {
		Data = data;
		ResourceManager.Instance.RequestExecuteOrder(
			string.Format(Const.RarityFrameImagePath, data.Rarity.ToString()),
			ExecuteOrder.Type.Sprite,
			this.gameObject,
			(rawSprite) => {
				RarityFrameImage.sprite = rawSprite as Sprite;
			}
		);
		Callback = callback;

		UpdateDisplay();
	}

	public void UpdateDisplay() {
		if (PlayerPrefsManager.Instance.IsFindCard(Data.Id) == false) {
			ResourceManager.Instance.RequestExecuteOrder(
				Const.NotFoundImagePath,
				ExecuteOrder.Type.Sprite,
				this.gameObject,
				(rawSprite) => {
					CardImage.sprite = rawSprite as Sprite;
				}
			);
			CardName.text = "?";
			CardDetail.text = "未発見のカード";
			CostText.text = "";
			LockImage.gameObject.SetActive(false);
			RootButton.interactable = false;
		} else {

			ResourceManager.Instance.RequestExecuteOrder(
				Data.ImagePath,
				ExecuteOrder.Type.Sprite,
				this.gameObject,
				(rawSprite) => {
					CardImage.sprite = rawSprite as Sprite;
				}
			);
			CardName.text = Data.Name;

			System.Object[] arguments = new System.Object[Data.ActionPackList.Count];
			for (int i = 0; i < Data.ActionPackList.Count; i++) {
				arguments[i] = Data.ActionPackList[i].Value;
			}
			CardDetail.text = string.Format(Data.Detail, arguments);


			if (PlayerPrefsManager.Instance.IsUnlockCard(Data.Id) == false) {
				// TODO ここで、アンロックしていない時のボタンとか表示を行う
				LockImage.gameObject.SetActive(true);
				CostText.text = Data.UnlockCost.ToString();
			} else {
				LockImage.gameObject.SetActive(false);
				CostText.text = Data.EquipCost.ToString();
			}
			RootButton.interactable = true;
		}
	}
	
	public void OnClick() {
		if (Callback != null) {
			Callback(this);
		}
	}
	
	public MasterAction2Table.Data GetData() {
		return Data;
	}
}
