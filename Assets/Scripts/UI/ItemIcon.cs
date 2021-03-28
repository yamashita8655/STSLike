using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemIcon : MonoBehaviour
{
	[SerializeField]
	private Image BgImage = null;
	
	[SerializeField]
	private Image IconImage = null;

	public MasterEquipItemDataTable.Data Data { get; private set; }
	
	private Action<ItemIcon> ClickCallback = null;

	public void Initialize(Sprite bgSprite, Sprite iconSprite, Action<ItemIcon> clickCallback) {
		BgImage.sprite = bgSprite;
		IconImage.sprite = iconSprite;
		ClickCallback = clickCallback;
	}

	public void OnClick() {
		if (ClickCallback != null) {
			ClickCallback(this);
		}
	}
}
