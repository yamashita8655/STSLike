using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDetailController : MonoBehaviour
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
	private Text Cost = null;

	public void Open(MasterAction2Table.Data data) {
		gameObject.SetActive(true);

		ResourceManager.Instance.RequestExecuteOrder(
			string.Format(Const.RarityFrameImagePath, data.Rarity.ToString()),
			ExecuteOrder.Type.Sprite,
			this.gameObject,
			(rawSprite) => {
				RarityFrameImage.sprite = rawSprite as Sprite;
			}
		);
		
		ResourceManager.Instance.RequestExecuteOrder(
			data.ImagePath,
			ExecuteOrder.Type.Sprite,
			this.gameObject,
			(rawSprite) => {
				CardImage.sprite = rawSprite as Sprite;
			}
		);

		CardName.text = data.Name;
		Cost.text = data.DiceCost.ToString();
		System.Object[] arguments = new System.Object[data.ActionPackList.Count];
		for (int i = 0; i < data.ActionPackList.Count; i++) {
			arguments[i] = data.ActionPackList[i].Value;
		}
		CardDetail.text = string.Format(data.Detail, arguments);
	}
	
	public void Close() {
		gameObject.SetActive(false);
	}
	
	public void OnClickCloseButton() {
		Close();
	}
}
