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
	
	private Action<MasterAction2Table.Data> Callback = null;

	private MasterAction2Table.Data Data = null;

	public void Initialize(MasterAction2Table.Data data, Action<MasterAction2Table.Data> callback) {
		Data = data;

		ResourceManager.Instance.RequestExecuteOrder(
			string.Format(Const.RarityFrameImagePath, data.Rarity.ToString()),
			ExecuteOrder.Type.Sprite,
			this.gameObject,
			(rawSprite) => {
				RarityFrameImage.sprite = rawSprite as Sprite;
			}
		);
		
		if (PlayerPrefsManager.Instance.IsFindCard(data.Id) == false) {
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
			LockImage.gameObject.SetActive(false);
			RootButton.interactable = false;
		} else {

			ResourceManager.Instance.RequestExecuteOrder(
				data.ImagePath,
				ExecuteOrder.Type.Sprite,
				this.gameObject,
				(rawSprite) => {
					CardImage.sprite = rawSprite as Sprite;
				}
			);
			CardName.text = data.Name;

			System.Object[] arguments = new System.Object[data.ActionPackList.Count];
			for (int i = 0; i < data.ActionPackList.Count; i++) {
				arguments[i] = data.ActionPackList[i].Value;
			}
			CardDetail.text = string.Format(data.Detail, arguments);


			if (PlayerPrefsManager.Instance.IsUnlockCard(data.Id) == false) {
				// TODO ここで、アンロックしていない時のボタンとか表示を行う
				LockImage.gameObject.SetActive(true);
			} else {
				LockImage.gameObject.SetActive(false);
			}
			RootButton.interactable = true;
		}

		Callback = callback;
	}
	
	public void OnClick() {
		if (Callback != null) {
			Callback(Data);
		}
	}
}
