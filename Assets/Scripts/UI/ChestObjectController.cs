using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestObjectController : MonoBehaviour
{
	[SerializeField]
	private Image ChestImage = null;
	
	[SerializeField]
	private Text RewardText = null;

	private int Rarity = 0;
	
    public void Initialize(int rarity) {
		ResourceManager.Instance.RequestExecuteOrder(
			string.Format(Const.ChestImagePath, rarity.ToString()),
			ExecuteOrder.Type.Sprite,
			this.gameObject,
			(rawSprite) => {
				ChestImage.sprite = rawSprite as Sprite;
			}
		);

		Rarity = rarity;

		RewardText.gameObject.SetActive(false);
	}

	public void UpdateRewardText(int val) {
		RewardText.text = val.ToString();
		RewardText.gameObject.SetActive(true);
	}
	
	public int GetRarity() => Rarity;
}
