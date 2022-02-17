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
	
	public void Initialize(GameObject attachRoot) {
		transform.SetParent(attachRoot.transform);
		transform.localPosition = Vector3.zero;
		transform.localScale = Vector3.one;

		gameObject.SetActive(false);
	}
	
	public void Hide() {
		gameObject.SetActive(false);
	}

	public void UpdateDisplay(EnumSelf.EffectType type, int originalVal, int val) {
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
		if (originalVal > val) {
			EffectValue.color = Color.red;
		} else if (originalVal < val) {
			EffectValue.color = Color.green;
		} else if (originalVal == val) {
			EffectValue.color = Color.white;
		}

		if (
			(type == EnumSelf.EffectType.Warning) ||
			(type == EnumSelf.EffectType.Death) ||
			(type == EnumSelf.EffectType.RemovePower) ||
			(type == EnumSelf.EffectType.Curse) ||
			(type == EnumSelf.EffectType.Stun)
		) {
			EffectValue.text = "";
		}

		gameObject.SetActive(true);
	}

	private string ConvertEffectType2Path(EnumSelf.EffectType type) {
		string ret = "";
		if (
			(type == EnumSelf.EffectType.Damage) ||
			(type == EnumSelf.EffectType.DamageSuction) ||
			(type == EnumSelf.EffectType.ShieldBash)
		) {
			ret = Const.DamageImagePath;
		} else if (type == EnumSelf.EffectType.TrueDamage) {
			ret = Const.TrueDamageImagePath;
		} else if (type == EnumSelf.EffectType.Shield) {
			ret = Const.ShieldImagePath;
		} else if (type == EnumSelf.EffectType.ShieldDamage) {
			ret = Const.ShieldBreakImagePath;
		} else if (type == EnumSelf.EffectType.Heal) {
			ret = Const.HealImagePath;
		} else if (
			(type == EnumSelf.EffectType.Warning) ||
			(type == EnumSelf.EffectType.DebugDisaster)
		) {
			ret = Const.WarningImagePath;
		} else if (type == EnumSelf.EffectType.Stun) {
			ret = Const.StunImagePath;
		} else if (type == EnumSelf.EffectType.Death) {
			ret = Const.DeathImagePath;
		} else if (
			(type == EnumSelf.EffectType.Strength) ||
			(type == EnumSelf.EffectType.AutoShield) ||
			(type == EnumSelf.EffectType.Thorn) ||
			(type == EnumSelf.EffectType.Versak) ||
			(type == EnumSelf.EffectType.Regenerate)
		) {
			ret = Const.PowerImagePath;
		} else if (
			(type == EnumSelf.EffectType.DiceMinusOne) ||
			(type == EnumSelf.EffectType.Poison) ||
			(type == EnumSelf.EffectType.Patient) ||
			(type == EnumSelf.EffectType.Weakness) ||
			(type == EnumSelf.EffectType.ShieldWeakness) ||
			(type == EnumSelf.EffectType.Vulnerable) ||
			(type == EnumSelf.EffectType.RemovePower) ||
			(type == EnumSelf.EffectType.RotBody) ||
			(type == EnumSelf.EffectType.Curse) ||
			(type == EnumSelf.EffectType.ReactiveShield) ||
			(type == EnumSelf.EffectType.SubStrength) ||
			(type == EnumSelf.EffectType.ReverseHeal)
		) {
			ret = Const.DebuffImagePath;
		}

		return ret;
	}

}
