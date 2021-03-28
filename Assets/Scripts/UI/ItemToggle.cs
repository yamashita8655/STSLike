using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemToggle : MonoBehaviour
{
	[SerializeField]
	private Image BgImage = null;
	
	[SerializeField]
	private Image IconImage = null;
	
	[SerializeField]
	private Toggle SelectToggle = null;

    public EquipItemBase Data { get; private set; }

    public void Initialize(Sprite bgSprite, Sprite iconSprite, EquipItemBase data) {
		BgImage.sprite = bgSprite;
		IconImage.sprite = iconSprite;
        Data = data;

		SelectToggle.isOn = false;
	}

	public bool IsOn() {
		return SelectToggle.isOn;
	}
}
