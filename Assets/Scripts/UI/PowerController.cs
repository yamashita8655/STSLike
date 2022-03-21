using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerController : MonoBehaviour
{
	[SerializeField]
	private Image PowerImage = null;
	
	[SerializeField]
	private Text PowerValue = null;

	private int Value = 0;
	
	private Action<string> OnClickCallback = null;
	private EnumSelf.PowerType CuPowerType = EnumSelf.PowerType.None;
	
	public void Initialize(EnumSelf.PowerType type, int val, GameObject attachRoot, Action<string> onClickCallback) {
		CuPowerType = type;
		OnClickCallback = onClickCallback;

		string path = ConvertType2Path(type);


		ResourceManager.Instance.RequestExecuteOrder(
			path,
			ExecuteOrder.Type.Sprite,
			this.gameObject,
			(rawSprite) => {
				PowerImage.sprite = rawSprite as Sprite;
			}
		);

		Value = val;

		transform.SetParent(attachRoot.transform);
		transform.localPosition = Vector3.zero;
		transform.localScale = Vector3.one;

		UpdateText();
	}

	public void SetValue(int val) {
		Value = val;
		UpdateText();
	}

	private void UpdateText() {
		PowerValue.text = Value.ToString();
		if (Value == 0) {
			gameObject.SetActive(false);
		} else {
			gameObject.SetActive(true);
		}
	}

	private string ConvertType2Path(EnumSelf.PowerType type) {
		string ret = "";
		if (type == EnumSelf.PowerType.Strength) {
			ret = Const.StrengthImagePath;
		} else if (type == EnumSelf.PowerType.FastStrength) {
			ret = Const.FastStrengthImagePath;
		} else if (type == EnumSelf.PowerType.Toughness) {
			ret = Const.ToughnessImagePath;
		} else if (type == EnumSelf.PowerType.Regenerate) {
			ret = Const.RegenerateImagePath;
		} else if (type == EnumSelf.PowerType.Poison) {
			ret = Const.PoisonImagePath;
		} else if (type == EnumSelf.PowerType.AddMaxDiceCost) {
			ret = Const.AddMaxDiceCostImagePath;
		} else if (type == EnumSelf.PowerType.HealCharge) {
			ret = Const.HealChargeImagePath;
		}

		return ret;
	}
	
	public void OnClick() {
		OnClickCallback($"{CuPowerType}");
	}
}
