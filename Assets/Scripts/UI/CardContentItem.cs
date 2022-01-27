using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardContentItem : MonoBehaviour
{
	[SerializeField]
	private Image RarityFrameImage = null;
	
	[SerializeField]
	private Image CardImage = null;
	
	[SerializeField]
	private Text CardName = null;
	
	[SerializeField]
	private Text CardDetail = null;

	private readonly string FramePath = "Image/UI/Card/cardframe{0}";

	private Action<MasterActionTable.Data> Callback = null;

	private MasterActionTable.Data Data = null;

	public void Initialize(MasterActionTable.Data data, Action<MasterActionTable.Data> callback) {

		Data = data;

		ResourceManager.Instance.RequestExecuteOrder(
			string.Format(FramePath, data.Rarity.ToString()),
			ExecuteOrder.Type.Sprite,
			this.gameObject,
			(rawSprite) => {
				RarityFrameImage.sprite = rawSprite as Sprite;
			}
		);
		
		//ResourceManager.Instance.RequestExecuteOrder(
		//	string.Format(FramePath, data.Rarity.ToString()),
		//	ExecuteOrder.Type.Sprite,
		//	this.gameObject,
		//	(rawSprite) => {
		//		CardImage.sprite = rawSprite as Sprite;
		//	}
		//);

		CardName.text = data.Name;
		CardDetail.text = string.Format(data.Detail, data.Value1, data.Value2);

		Callback = callback;
	}
	
	public void OnClick() {
		if (Callback != null) {
			Callback(Data);
		}
	}
}
