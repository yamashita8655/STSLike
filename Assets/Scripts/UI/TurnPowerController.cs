using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnPowerController : MonoBehaviour
{
	[SerializeField]
	private Image TurnPowerImage = null;
	
	[SerializeField]
	private Text TurnValue = null;

	public void Initialize(EnumSelf.TurnPowerType type, int turn, GameObject attachRoot) {

		string path = ConvertType2Path(type);

		ResourceManager.Instance.RequestExecuteOrder(
			path,
			ExecuteOrder.Type.Sprite,
			this.gameObject,
			(rawSprite) => {
				TurnPowerImage.sprite = rawSprite as Sprite;
			}
		);

		transform.SetParent(attachRoot.transform);
		transform.localPosition = Vector3.zero;
		transform.localScale = Vector3.one;

		UpdateTurn(turn);
	}

	public void UpdateTurn(int turn) {
		TurnValue.text = turn.ToString();
	}

	private string ConvertType2Path(EnumSelf.TurnPowerType type) {
		string ret = "";
		if (type == EnumSelf.TurnPowerType.DiceMinusOne) {
			ret = Const.DiceMinusOneImagePath;
		} else if (type == EnumSelf.TurnPowerType.ReverseHeal) {
			ret = Const.ReverseHealImagePath;
		}

		return ret;
	}

}
