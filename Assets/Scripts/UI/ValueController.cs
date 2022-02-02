using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValueController : MonoBehaviour
{
	[SerializeField]
	private Image EffectImage = null;
	
	[SerializeField]
	private Text  EffectValue = null;
	
	public void Initialize(EnumSelf.EffectType type, int val, GameObject attachRoot) {

		string path = ConvertEffectType2Path(type);

		ResourceManager.Instance.RequestExecuteOrder(
			path,
			ExecuteOrder.Type.Sprite,
			this.gameObject,
			(rawSprite) => {
				EffectImage.sprite = rawSprite as Sprite;
			}
		);

		EffectValue.text = val.ToString();

		transform.SetParent(attachRoot.transform);
		transform.localPosition = Vector3.zero;
		transform.localScale = Vector3.one;
	}

	private string ConvertEffectType2Path(EnumSelf.EffectType type) {
		string ret = "";
		if (type == EnumSelf.EffectType.Damage) {
			ret = Const.DamageImagePath;
		} else if (type == EnumSelf.EffectType.Shield) {
			ret = Const.ShieldImagePath;
		} else if (type == EnumSelf.EffectType.Heal) {
			ret = Const.HealImagePath;
		} else if (type == EnumSelf.EffectType.Strength) {
			ret = Const.PowerImagePath;
		} else if (type == EnumSelf.EffectType.DiceMinusOne) {
			ret = Const.DebugImagePath;
		}

		return ret;
	}

}
