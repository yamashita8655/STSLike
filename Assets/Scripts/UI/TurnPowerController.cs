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

	private int RemainTurn = 0;

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

		RemainTurn = turn;

		UpdateTurn();
	}
	
	public void SetTurn(int turn) {
		RemainTurn = turn;
		// TODO 恐らく、ここにセットする前にturnの補正していると思うけど、一応。
		if (RemainTurn < 0) {
			RemainTurn = 0;
		}

		UpdateTurn();
	}

	private void UpdateTurn() {
		TurnValue.text = RemainTurn.ToString();
		if (RemainTurn <= 0) {
			gameObject.SetActive(false);
		} else {
			gameObject.SetActive(true);
		}
	}

	private string ConvertType2Path(EnumSelf.TurnPowerType type) {
		string ret = "";
		if (type == EnumSelf.TurnPowerType.DiceMinusOne) {
			ret = Const.DiceMinusOneImagePath;
		} else if (type == EnumSelf.TurnPowerType.ReverseHeal) {
			ret = Const.ReverseHealImagePath;
		} else if (type == EnumSelf.TurnPowerType.Weakness) {
			ret = Const.WeaknessImagePath;
		}

		return ret;
	}

}
