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
			(type == EnumSelf.EffectType.ShieldDouble) ||
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
			(type == EnumSelf.EffectType.DamageShieldSuction) ||
			(type == EnumSelf.EffectType.DamageGainMaxHp) ||
			(type == EnumSelf.EffectType.DamageMultiStrength) ||
			(type == EnumSelf.EffectType.DamageFinish) ||
			(type == EnumSelf.EffectType.DamageDice) ||
			(type == EnumSelf.EffectType.ShieldBash)
		) {
			ret = Const.DamageImagePath;
		} else if (type == EnumSelf.EffectType.TrueDamage) {
			ret = Const.TrueDamageImagePath;
		} else if (
			(type == EnumSelf.EffectType.Shield) ||
			(type == EnumSelf.EffectType.StrengthShield) ||
			(type == EnumSelf.EffectType.ShieldDouble)
		) {
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
		} else if (type == EnumSelf.EffectType.Draw) {
			ret = Const.DrawImagePath;
		} else if (type == EnumSelf.EffectType.GainDiceCost) {
			ret = Const.GainDiceCostImagePath;
		} else if (type == EnumSelf.EffectType.Hand2DeckTop) {
			ret = Const.Hand2DeckTopImagePath;
		} else if (type == EnumSelf.EffectType.Hand2Discard) {
			ret = Const.Hand2DiscardImagePath;
		} else if (type == EnumSelf.EffectType.Hand2Trash) {
			ret = Const.Hand2TrashImagePath;
		} else if (type == EnumSelf.EffectType.Hand2Erase) {
			ret = Const.Hand2EraseImagePath;
		} else if (
			(type == EnumSelf.EffectType.Strength) ||
			(type == EnumSelf.EffectType.FastStrength) ||
			(type == EnumSelf.EffectType.Toughness) ||
			(type == EnumSelf.EffectType.AutoShield) ||
			(type == EnumSelf.EffectType.Thorn) ||
			(type == EnumSelf.EffectType.Versak) ||
			(type == EnumSelf.EffectType.ShieldPreserve) ||
			(type == EnumSelf.EffectType.TurnShieldPreserve) ||
			(type == EnumSelf.EffectType.Invincible) ||
			(type == EnumSelf.EffectType.DoubleAttack) ||
			(type == EnumSelf.EffectType.Cost6DoubleAttack) ||
			(type == EnumSelf.EffectType.TurnRegenerate) ||
			(type == EnumSelf.EffectType.Critical) ||
			(type == EnumSelf.EffectType.AddMaxDiceCost) ||
			(type == EnumSelf.EffectType.HealCharge) ||
			(type == EnumSelf.EffectType.DoubleStrength) ||
			(type == EnumSelf.EffectType.DemonPower) ||
			(type == EnumSelf.EffectType.AddShieldTrueDamage) ||
			(type == EnumSelf.EffectType.TurnThorn) ||
			(type == EnumSelf.EffectType.SupportShoot) ||
			(type == EnumSelf.EffectType.AfterImage) ||
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
			(type == EnumSelf.EffectType.NonDraw) ||
			(type == EnumSelf.EffectType.ReverseHeal)
		) {
			ret = Const.DebuffImagePath;
		}

		return ret;
	}

}
