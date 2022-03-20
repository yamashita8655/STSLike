using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegularSettingArtifactContentItem : MonoBehaviour
{
	[SerializeField]
	private Image RarityFrameImage = null;
	
	[SerializeField]
	private Image ArtifactImage = null;
	
	[SerializeField]
	private Text ArtifactName = null;
	
	[SerializeField]
	private Text ArtifactDetail = null;
	
	[SerializeField]
	private Image LockImage = null;
	
	[SerializeField]
	private Button RootButton = null;
	
	[SerializeField]
	private Text CostText = null;
	
	private Action<RegularSettingArtifactContentItem> Callback = null;

	private MasterArtifactTable.Data Data = null;

	public void Initialize(MasterArtifactTable.Data data, Action<RegularSettingArtifactContentItem> callback) {
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
		if (PlayerPrefsManager.Instance.IsFindArtifact(Data.Id) == false) {
			ResourceManager.Instance.RequestExecuteOrder(
				Const.NotFoundImagePath,
				ExecuteOrder.Type.Sprite,
				this.gameObject,
				(rawSprite) => {
					ArtifactImage.sprite = rawSprite as Sprite;
				}
			);
			ArtifactName.text = "?";
			ArtifactDetail.text = "未発見のアーティファクト";
			CostText.text = "";
			LockImage.gameObject.SetActive(false);
			RootButton.interactable = false;
		} else {

			ResourceManager.Instance.RequestExecuteOrder(
				Data.ImagePath,
				ExecuteOrder.Type.Sprite,
				this.gameObject,
				(rawSprite) => {
					ArtifactImage.sprite = rawSprite as Sprite;
				}
			);
			ArtifactName.text = Data.Name;
			ArtifactDetail.text = Data.Detail;


			if (PlayerPrefsManager.Instance.IsUnlockArtifact(Data.Id) == false) {
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
	
	public MasterArtifactTable.Data GetData() {
		return Data;
	}
}
