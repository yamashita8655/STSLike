using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCalculationFunction {
	public static void PlayerValueChange(ActionPack pack) {
		if (
			(pack.Effect == EnumSelf.EffectType.Damage) ||
			(pack.Effect == EnumSelf.EffectType.DamageSuction) ||
			(pack.Effect == EnumSelf.EffectType.DamageShieldSuction) ||
			(pack.Effect == EnumSelf.EffectType.DamageGainMaxHp) ||
			(pack.Effect == EnumSelf.EffectType.DamageMultiStrength) ||
			(pack.Effect == EnumSelf.EffectType.DamageDiscardCount) ||
			(pack.Effect == EnumSelf.EffectType.TrueDamage) ||
			(pack.Effect == EnumSelf.EffectType.DamageFinish) ||
			(pack.Effect == EnumSelf.EffectType.DamageDice) ||
			(pack.Effect == EnumSelf.EffectType.DamageTotalSelfTrueDamage) ||
			(pack.Effect == EnumSelf.EffectType.ShieldBash)
		) {
			BattleCalculationFunction.PlayerCalcDamageNormalDamage(pack);
		//} else if (pack.Effect == EnumSelf.EffectType.TrueDamage) {
		//	BattleCalculationFunction.PlayerCalcTrueDamage(pack);
		} else if (pack.Effect == EnumSelf.EffectType.SelfTrueDamage) {
			BattleCalculationFunction.PlayerCalcSelfTrueDamage(pack);
		} else if (pack.Effect == EnumSelf.EffectType.RemovePower) {
			BattleCalculationFunction.PlayerRemovePower(pack);
		} else if (pack.Effect == EnumSelf.EffectType.Heal) {
			BattleCalculationFunction.PlayerCalcHeal(pack);
		} else if (
			(pack.Effect == EnumSelf.EffectType.Shield) ||
			(pack.Effect == EnumSelf.EffectType.StrengthShield) ||
			(pack.Effect == EnumSelf.EffectType.ShieldDouble)
		) {
			BattleCalculationFunction.PlayerCalcShield(pack);
		} else if (pack.Effect == EnumSelf.EffectType.ShieldDamage) {
			BattleCalculationFunction.PlayerCalcShieldDamage(pack);
		} else if (pack.Effect == EnumSelf.EffectType.Death) {
			BattleCalculationFunction.PlayerDeath(pack);
		} else if (
			(pack.Effect == EnumSelf.EffectType.Strength) ||
			(pack.Effect == EnumSelf.EffectType.FastStrength) ||
			(pack.Effect == EnumSelf.EffectType.Toughness) ||
			(pack.Effect == EnumSelf.EffectType.Poison) ||
			(pack.Effect == EnumSelf.EffectType.AddMaxDiceCost) ||
			(pack.Effect == EnumSelf.EffectType.HealCharge) ||
			(pack.Effect == EnumSelf.EffectType.Regenerate)
		) {
			BattleCalculationFunction.PlayerUpdatePower(pack);
		} else if (
			(pack.Effect == EnumSelf.EffectType.ReverseHeal) ||
			(pack.Effect == EnumSelf.EffectType.Vulnerable) ||
			(pack.Effect == EnumSelf.EffectType.Weakness) ||
			(pack.Effect == EnumSelf.EffectType.AutoShield) ||
			(pack.Effect == EnumSelf.EffectType.Thorn) ||
			(pack.Effect == EnumSelf.EffectType.RotBody) ||
			(pack.Effect == EnumSelf.EffectType.Versak) ||
			(pack.Effect == EnumSelf.EffectType.DiceMinusOne) ||
			(pack.Effect == EnumSelf.EffectType.ShieldWeakness) ||
			(pack.Effect == EnumSelf.EffectType.TurnRegenerate) ||
			(pack.Effect == EnumSelf.EffectType.ReactiveShield) ||
			(pack.Effect == EnumSelf.EffectType.SubStrength) ||
			(pack.Effect == EnumSelf.EffectType.ShieldPreserve) ||
			(pack.Effect == EnumSelf.EffectType.TurnShieldPreserve) ||
			(pack.Effect == EnumSelf.EffectType.Invincible) ||
			(pack.Effect == EnumSelf.EffectType.DoubleAttack) ||
			(pack.Effect == EnumSelf.EffectType.Cost6DoubleAttack) ||
			(pack.Effect == EnumSelf.EffectType.Critical) ||
			(pack.Effect == EnumSelf.EffectType.NonDraw) ||
			(pack.Effect == EnumSelf.EffectType.DemonPower) ||
			(pack.Effect == EnumSelf.EffectType.AddShieldTrueDamage) ||
			(pack.Effect == EnumSelf.EffectType.TurnThorn) ||
			(pack.Effect == EnumSelf.EffectType.SupportShoot) ||
			(pack.Effect == EnumSelf.EffectType.AfterImage) ||
			(pack.Effect == EnumSelf.EffectType.Activity) ||
			(pack.Effect == EnumSelf.EffectType.Resist) ||
			(pack.Effect == EnumSelf.EffectType.AddHandCurseDamage) ||
			(pack.Effect == EnumSelf.EffectType.DiscardCurseHeal) ||
			(pack.Effect == EnumSelf.EffectType.DiscardShield) ||
			(pack.Effect == EnumSelf.EffectType.DiscardDamage) ||
			(pack.Effect == EnumSelf.EffectType.CurseReturn) ||
			(pack.Effect == EnumSelf.EffectType.SelfHarm) ||
			(pack.Effect == EnumSelf.EffectType.AddSelfTrueDamageStrength) ||
			(pack.Effect == EnumSelf.EffectType.AddSelfTrueDamageHealCharge) ||
			(pack.Effect == EnumSelf.EffectType.DrawSelfTrueDamage)
		) {
			BattleCalculationFunction.PlayerUpdateTurnPower(pack);
		} else if (pack.Effect == EnumSelf.EffectType.DoubleStrength) {
			BattleCalculationFunction.PlayerDoubleStrength(pack);
		} else if (pack.Effect == EnumSelf.EffectType.DebugDisaster) {
			BattleCalculationFunction.PlayerDebugDisaster(pack);
		}
	}

	public static void EnemyValueChange(ActionPack pack) {
		if (
			(pack.Effect == EnumSelf.EffectType.Damage) ||
			(pack.Effect == EnumSelf.EffectType.DamageSuction) ||
			(pack.Effect == EnumSelf.EffectType.DamageGainMaxHp) ||
			(pack.Effect == EnumSelf.EffectType.DamageMultiStrength) ||
			(pack.Effect == EnumSelf.EffectType.TrueDamage) ||
			(pack.Effect == EnumSelf.EffectType.ShieldBash)
		) {
			BattleCalculationFunction.EnemyCalcDamageNormalDamage(pack);
		//} else if (pack.Effect == EnumSelf.EffectType.TrueDamage) {
		//	BattleCalculationFunction.EnemyCalcTrueDamage(pack);
		} else if (pack.Effect == EnumSelf.EffectType.RemovePower) {
			BattleCalculationFunction.EnemyRemovePower(pack);
		} else if (pack.Effect == EnumSelf.EffectType.Heal) {
			BattleCalculationFunction.EnemyCalcHeal(pack);
		} else if (
			(pack.Effect == EnumSelf.EffectType.Shield) ||
			(pack.Effect == EnumSelf.EffectType.StrengthShield) ||
			(pack.Effect == EnumSelf.EffectType.ShieldDouble)
		) {
			BattleCalculationFunction.EnemyCalcShield(pack);
		} else if (pack.Effect == EnumSelf.EffectType.ShieldDamage) {
			BattleCalculationFunction.EnemyCalcShieldDamage(pack);
		} else if (pack.Effect == EnumSelf.EffectType.Death) {
			BattleCalculationFunction.EnemyDeath(pack);
		} else if (
			(pack.Effect == EnumSelf.EffectType.Draw) ||
			(pack.Effect == EnumSelf.EffectType.DamageShieldSuction) ||
			(pack.Effect == EnumSelf.EffectType.Critical) ||
			(pack.Effect == EnumSelf.EffectType.DoubleAttack) ||
			(pack.Effect == EnumSelf.EffectType.GainDiceCost) ||
			(pack.Effect == EnumSelf.EffectType.AddMaxDiceCost) ||
			(pack.Effect == EnumSelf.EffectType.Hand2DeckTop) ||
			(pack.Effect == EnumSelf.EffectType.Hand2Discard) ||
			(pack.Effect == EnumSelf.EffectType.Hand2Trash) ||
			(pack.Effect == EnumSelf.EffectType.Hand2Erase) ||
			(pack.Effect == EnumSelf.EffectType.HandCurseDiscard) ||
			(pack.Effect == EnumSelf.EffectType.Cost6DoubleAttack) ||
			(pack.Effect == EnumSelf.EffectType.NonDraw) ||
			(pack.Effect == EnumSelf.EffectType.DamageGainMaxHp) ||
			(pack.Effect == EnumSelf.EffectType.HealCharge) ||
			(pack.Effect == EnumSelf.EffectType.DoubleStrength) ||
			(pack.Effect == EnumSelf.EffectType.AddShieldTrueDamage) ||
			(pack.Effect == EnumSelf.EffectType.AfterImage) ||
			(pack.Effect == EnumSelf.EffectType.Activity) ||
			(pack.Effect == EnumSelf.EffectType.Resist) ||
			(pack.Effect == EnumSelf.EffectType.DamageFinish) ||
			(pack.Effect == EnumSelf.EffectType.DamageDice) ||
			(pack.Effect == EnumSelf.EffectType.AddHandCurseDamage) ||
			(pack.Effect == EnumSelf.EffectType.DiscardCurseHeal) ||
			(pack.Effect == EnumSelf.EffectType.DiscardShield) ||
			(pack.Effect == EnumSelf.EffectType.DiscardDamage) ||
			(pack.Effect == EnumSelf.EffectType.CurseReturn) ||
			(pack.Effect == EnumSelf.EffectType.SelfHarm) ||
			(pack.Effect == EnumSelf.EffectType.DamageDiscardCount) ||
			(pack.Effect == EnumSelf.EffectType.SelfTrueDamage) ||
			(pack.Effect == EnumSelf.EffectType.DamageTotalSelfTrueDamage) ||
			(pack.Effect == EnumSelf.EffectType.AddSelfTrueDamageStrength) ||
			(pack.Effect == EnumSelf.EffectType.AddSelfTrueDamageHealCharge) ||
			(pack.Effect == EnumSelf.EffectType.DrawSelfTrueDamage) ||
			(pack.Effect == EnumSelf.EffectType.SupportShoot)
		) {
			LogManager.Instance.LogError($"EnemyValueChange:pack.Effect is {pack.Effect} 敵に {pack.Effect}は設定しても効果がない");
		} else if (
			(pack.Effect == EnumSelf.EffectType.Strength) ||
			(pack.Effect == EnumSelf.EffectType.FastStrength) ||
			(pack.Effect == EnumSelf.EffectType.Toughness) ||
			(pack.Effect == EnumSelf.EffectType.Poison) ||
			(pack.Effect == EnumSelf.EffectType.Regenerate)
		) {
			BattleCalculationFunction.EnemyUpdatePower(pack);
		} else if (
			(pack.Effect == EnumSelf.EffectType.DiceMinusOne) ||
			(pack.Effect == EnumSelf.EffectType.Weakness) ||
			(pack.Effect == EnumSelf.EffectType.ShieldWeakness) ||
			(pack.Effect == EnumSelf.EffectType.TurnRegenerate) ||
			(pack.Effect == EnumSelf.EffectType.Vulnerable) ||
			(pack.Effect == EnumSelf.EffectType.Patient) ||
			(pack.Effect == EnumSelf.EffectType.AutoShield) ||
			(pack.Effect == EnumSelf.EffectType.Thorn) ||
			(pack.Effect == EnumSelf.EffectType.RotBody) ||
			(pack.Effect == EnumSelf.EffectType.Versak) ||
			(pack.Effect == EnumSelf.EffectType.ReactiveShield) ||
			(pack.Effect == EnumSelf.EffectType.SubStrength) ||
			(pack.Effect == EnumSelf.EffectType.ShieldPreserve) ||
			(pack.Effect == EnumSelf.EffectType.TurnShieldPreserve) ||
			(pack.Effect == EnumSelf.EffectType.Invincible) ||
			(pack.Effect == EnumSelf.EffectType.DemonPower) ||
			(pack.Effect == EnumSelf.EffectType.TurnThorn) ||
			(pack.Effect == EnumSelf.EffectType.ReverseHeal)
		) {
			BattleCalculationFunction.EnemyUpdateTurnPower(pack);
		}
	}
	
	public static void PlayerInitiativeValueChange() {
		PlayerStatus player = MapDataCarrier.Instance.CuPlayerStatus;
		if (player.GetParameterListFlag(EnumSelf.ParameterType.HeroSword) == true) {
			if (player.GetNowHp() == player.GetMaxHp()) {
				PlayerUpdatePower(EnumSelf.PowerType.Strength, 3);
				PlayerUpdatePower(EnumSelf.PowerType.Toughness, 3);
			}
		}
		
		if (player.GetParameterListFlag(EnumSelf.ParameterType.HeroShield) == true) {
			if (player.GetNowHp() <= 20) {
				PlayerUpdateTurnPower(EnumSelf.TurnPowerType.TurnRegenerate, 10);
			}
		}
		
	}
	
	public static void PlayerTurnStartValueChange() {
		// 再生などのバフをチェック
		PlayerStatus player = MapDataCarrier.Instance.CuPlayerStatus;
		Power power = player.GetPower();
		
		// 鉄壁でも堅牢でもなかったら、シールドを0にする
		if (
			(player.GetTurnPowerValue(EnumSelf.TurnPowerType.TurnShieldPreserve) == 0) &&
			(player.GetTurnPowerValue(EnumSelf.TurnPowerType.ShieldPreserve) == 0) 
		) {
			if (player.GetParameterListFlag(EnumSelf.ParameterType.ReduseShieldLimit15) == true) {
				// シールド減少値を15に変更
				player.AddNowShield(-15);
			} else {
				player.SetNowShield(0);
			}
		}

		// 反撃は、ターン開始時に0にする
		if (player.GetTurnPowerValue(EnumSelf.TurnPowerType.TurnThorn) > 0) {
			int turnThorn = player.GetTurnPowerValue(EnumSelf.TurnPowerType.TurnThorn);
			PlayerUpdateTurnPower(EnumSelf.TurnPowerType.TurnThorn, -turnThorn);
		}
		
		// アーティファクト系
		if (player.GetParameterListFlag(EnumSelf.ParameterType.SupportFire) == true) {
			// シールドにも影響する3ダメージを与える
			PlayerCalcDamageNormalDamage(3);
		}
		
		if (player.GetParameterListFlag(EnumSelf.ParameterType.ShieldOne) == true) {
			if (MapDataCarrier.Instance.BattleTurnCount == 1) {
				PlayerCalcShield(6);
			}
		}
		
		if (player.GetParameterListFlag(EnumSelf.ParameterType.ShieldTwo) == true) {
			if (MapDataCarrier.Instance.BattleTurnCount == 2) {
				PlayerCalcShield(8);
			}
		}
		
		if (player.GetParameterListFlag(EnumSelf.ParameterType.ShieldThree) == true) {
			if (MapDataCarrier.Instance.BattleTurnCount == 3) {
				PlayerCalcShield(12);
			}
		}
		
		if (player.GetParameterListFlag(EnumSelf.ParameterType.AddVersak) == true) {
			if (IsOnlyDamageAll() == true) {
				PlayerUpdateTurnPower(EnumSelf.TurnPowerType.Versak, 1);
			}
		}
		
		if (player.GetParameterListFlag(EnumSelf.ParameterType.AddDoubleAttack6) == true) {
			PlayerUpdateTurnPower(EnumSelf.TurnPowerType.Cost6DoubleAttack, 1);
		}
		
		if (player.GetParameterListFlag(EnumSelf.ParameterType.DyingAddVersak) == true) {
			if (player.GetNowHp() <= 20) {
				PlayerUpdateTurnPower(EnumSelf.TurnPowerType.Versak, 1);
			}
		}
		
		// 超再生
		int val = power.GetValue(EnumSelf.PowerType.Regenerate);
		if (val > 0) {
			bool isReverse = false;
			if (MapDataCarrier.Instance.CuPlayerStatus.GetTurnPowerValue(EnumSelf.TurnPowerType.ReverseHeal) > 0) {
				isReverse = true;
			}

			if (isReverse == true) {
				PlayerUpdateHp(-val);
			} else {
				PlayerUpdateHp(val);
			}
		}

		// 再生
		val = player.GetTurnPowerValue(EnumSelf.TurnPowerType.TurnRegenerate);
		if (val > 0) {
			bool isReverse = false;
			if (player.GetTurnPowerValue(EnumSelf.TurnPowerType.ReverseHeal) > 0) {
				isReverse = true;
			}

			if (isReverse == true) {
				PlayerUpdateHp(-val);
			} else {
				PlayerUpdateHp(val);
			}
		}
		
		// 毒
		val = power.GetValue(EnumSelf.PowerType.Poison);
		if (val > 0) {
			PlayerUpdateHp(-val);
		}
		
		// 敵のバフチェック
		EnemyStatus enemy = MapDataCarrier.Instance.CuEnemyStatus;
		val = enemy.GetTurnPowerValue(EnumSelf.TurnPowerType.AutoShield);
		if (val > 0) {
			enemy.AddNowShield(val);
		}
		
		val = player.GetTurnPowerValue(EnumSelf.TurnPowerType.DemonPower);
		if (val > 0) {
			PlayerUpdatePower(EnumSelf.PowerType.Strength, val);
		}
		
		// ターンスタート時に数値を減らす物
		if (player.GetTurnPowerValue(EnumSelf.TurnPowerType.TurnShieldPreserve) > 0) {
			PlayerUpdateTurnPower(EnumSelf.TurnPowerType.TurnShieldPreserve, -1);
		}

		PlayerUpdateTurnPower(EnumSelf.TurnPowerType.Weakness, -1);
		PlayerUpdateTurnPower(EnumSelf.TurnPowerType.Vulnerable, -1);
		PlayerUpdateTurnPower(EnumSelf.TurnPowerType.ShieldWeakness, -1);
	}
	
	public static void PlayerTurnEndValueChange() {
		var status = MapDataCarrier.Instance.CuPlayerStatus;

		for (int i = 0; i < (int)EnumSelf.TurnPowerType.Max; i++) {
			if (
				(i == (int)EnumSelf.TurnPowerType.Patient) || 
				(i == (int)EnumSelf.TurnPowerType.Thorn) || 
				(i == (int)EnumSelf.TurnPowerType.ReactiveShield) ||
				(i == (int)EnumSelf.TurnPowerType.ShieldPreserve) ||
				(i == (int)EnumSelf.TurnPowerType.TurnShieldPreserve) ||
				(i == (int)EnumSelf.TurnPowerType.Weakness) ||
				(i == (int)EnumSelf.TurnPowerType.Vulnerable) ||
				(i == (int)EnumSelf.TurnPowerType.ShieldWeakness) ||
				(i == (int)EnumSelf.TurnPowerType.Invincible) ||
				(i == (int)EnumSelf.TurnPowerType.Critical) ||
				(i == (int)EnumSelf.TurnPowerType.DemonPower) ||
				(i == (int)EnumSelf.TurnPowerType.AddShieldTrueDamage) ||
				(i == (int)EnumSelf.TurnPowerType.TurnThorn) ||
				(i == (int)EnumSelf.TurnPowerType.SupportShoot) || 
				(i == (int)EnumSelf.TurnPowerType.AfterImage) || 
				(i == (int)EnumSelf.TurnPowerType.Activity) || 
				(i == (int)EnumSelf.TurnPowerType.Resist) || 
				(i == (int)EnumSelf.TurnPowerType.AddHandCurseDamage) || 
				(i == (int)EnumSelf.TurnPowerType.DiscardCurseHeal) || 
				(i == (int)EnumSelf.TurnPowerType.DiscardShield) || 
				(i == (int)EnumSelf.TurnPowerType.DiscardDamage) || 
				(i == (int)EnumSelf.TurnPowerType.CurseReturn) || 
				(i == (int)EnumSelf.TurnPowerType.SelfHarm) || 
				(i == (int)EnumSelf.TurnPowerType.AddSelfTrueDamageStrength) || 
				(i == (int)EnumSelf.TurnPowerType.AddSelfTrueDamageHealCharge) || 
				(i == (int)EnumSelf.TurnPowerType.DrawSelfTrueDamage) || 
				(i == (int)EnumSelf.TurnPowerType.AutoShield)
			) {
				continue;
			} else if (
				(i == (int)EnumSelf.TurnPowerType.RotBody)
			) {
				if (status.GetTurnPowerValue((EnumSelf.TurnPowerType)i) > 0) {
					status.SetTurnPowerValue((EnumSelf.TurnPowerType)i, 1);
				}
			} else if (
				(i == (int)EnumSelf.TurnPowerType.DoubleAttack) ||
				(i == (int)EnumSelf.TurnPowerType.NonDraw) ||
				(i == (int)EnumSelf.TurnPowerType.Cost6DoubleAttack)
			) {
					status.SetTurnPowerValue((EnumSelf.TurnPowerType)i, 0);
			} else if (
				(i == (int)EnumSelf.TurnPowerType.SubStrength)
			) {
				if (status.GetTurnPowerValue((EnumSelf.TurnPowerType)i) > 0) {
					int substrength = status.GetTurnPowerValue(EnumSelf.TurnPowerType.SubStrength);
					status.SetTurnPowerValue(EnumSelf.TurnPowerType.SubStrength, 0);
					PlayerUpdatePower(EnumSelf.PowerType.Strength, -substrength);
				}
			} else {
				status.AddTurnPower((EnumSelf.TurnPowerType)i, -1);
			}
			int turn = status.GetTurnPowerValue((EnumSelf.TurnPowerType)i);
			MapDataCarrier.Instance.TurnPowerObjects[(int)i].GetComponent<TurnPowerController>().SetTurn(turn);
		}
		
		// ターン終了時、シールドがなければ、シールド6獲得
		if (status.GetParameterListFlag(EnumSelf.ParameterType.ZeroTurnEndShield) == true) {
			if (status.GetNowShield() <= 0) {
				status.AddNowShield(6);
			}
		}
	}

	public static void EnemyTurnStartValueChange() {
		// 再生などのバフをチェック
		EnemyStatus enemy = MapDataCarrier.Instance.CuEnemyStatus;
		Power power = enemy.GetPower();
		
		// 鉄壁でも堅牢でもなければ、シールドを消滅させる
		if (
			(enemy.GetTurnPowerValue(EnumSelf.TurnPowerType.TurnShieldPreserve) == 0) &&
			(enemy.GetTurnPowerValue(EnumSelf.TurnPowerType.ShieldPreserve) == 0)
		) {
			enemy.SetNowShield(0);
		}
		
		// 反撃は、ターン開始時に0にする
		if (enemy.GetTurnPowerValue(EnumSelf.TurnPowerType.TurnThorn) > 0) {
			int turnThorn = enemy.GetTurnPowerValue(EnumSelf.TurnPowerType.TurnThorn);
			EnemyUpdateTurnPower(EnumSelf.TurnPowerType.TurnThorn, -turnThorn);
		}
		
		// 超再生
		int val = power.GetValue(EnumSelf.PowerType.Regenerate);
		if (val > 0) {
			bool isReverse = false;
			if (enemy.GetTurnPowerValue(EnumSelf.TurnPowerType.ReverseHeal) > 0) {
				isReverse = true;
			}

			if (isReverse == true) {
				EnemyUpdateHp(-val);
			} else {
				EnemyUpdateHp(val);
			}
		}
		
		// 再生
		val = enemy.GetTurnPowerValue(EnumSelf.TurnPowerType.TurnRegenerate);
		if (val > 0) {
			bool isReverse = false;
			if (enemy.GetTurnPowerValue(EnumSelf.TurnPowerType.ReverseHeal) > 0) {
				isReverse = true;
			}

			if (isReverse == true) {
				EnemyUpdateHp(-val);
			} else {
				EnemyUpdateHp(val);
			}
		}
		
		// 毒
		val = power.GetValue(EnumSelf.PowerType.Poison);
		if (val > 0) {
			EnemyUpdateHp(-val);

		}
		
		// プレイヤーのバフチェック
		// タイミング的に、敵のターンでもプレイヤーにシールドを付けたりする
		PlayerStatus player = MapDataCarrier.Instance.CuPlayerStatus;
		val = player.GetTurnPowerValue(EnumSelf.TurnPowerType.AutoShield);
		if (val > 0) {
			player.AddNowShield(val);
		}
		
		// 敵の効果
		val = enemy.GetTurnPowerValue(EnumSelf.TurnPowerType.DemonPower);
		if (val > 0) {
			EnemyUpdatePower(EnumSelf.PowerType.Strength, val);
		}
		
		// ターンスタート時に数値を減らす物
		if (enemy.GetTurnPowerValue(EnumSelf.TurnPowerType.TurnShieldPreserve) > 0) {
			EnemyUpdateTurnPower(EnumSelf.TurnPowerType.TurnShieldPreserve, -1);
		}
	}

	public static void EnemyTurnEndValueChange() {
		var status = MapDataCarrier.Instance.CuEnemyStatus;
		
		for (int i = 0; i < (int)EnumSelf.TurnPowerType.Max; i++) {
			if (
				(i == (int)EnumSelf.TurnPowerType.Patient) ||
				(i == (int)EnumSelf.TurnPowerType.Thorn) ||
				(i == (int)EnumSelf.TurnPowerType.ReactiveShield) ||
				(i == (int)EnumSelf.TurnPowerType.ShieldPreserve) ||
				(i == (int)EnumSelf.TurnPowerType.TurnShieldPreserve) ||
				(i == (int)EnumSelf.TurnPowerType.Invincible) ||
				(i == (int)EnumSelf.TurnPowerType.DemonPower) ||
				(i == (int)EnumSelf.TurnPowerType.TurnThorn) ||
				(i == (int)EnumSelf.TurnPowerType.AutoShield)
			) {
				continue;
			} else if (
				(i == (int)EnumSelf.TurnPowerType.RotBody)
			) {
				if (status.GetTurnPowerValue((EnumSelf.TurnPowerType)i) > 0) {
					status.SetTurnPowerValue((EnumSelf.TurnPowerType)i, 1);
				}
			} else if (
				(i == (int)EnumSelf.TurnPowerType.SubStrength)
			) {
				if (status.GetTurnPowerValue((EnumSelf.TurnPowerType)i) > 0) {
					int substrength = status.GetTurnPowerValue(EnumSelf.TurnPowerType.SubStrength);
					status.SetTurnPowerValue(EnumSelf.TurnPowerType.SubStrength, 0);
					EnemyUpdatePower(EnumSelf.PowerType.Strength, -substrength);
				}
			} else {
				status.AddTurnPower((EnumSelf.TurnPowerType)i, -1);
			}
			int turn = status.GetTurnPowerValue((EnumSelf.TurnPowerType)i);
			MapDataCarrier.Instance.EnemyTurnPowerObjects[(int)i].GetComponent<TurnPowerController>().SetTurn(turn);
		}
	}

	// Player用
	public static void PlayerCalcDamageNormalDamage(ActionPack pack) {
		var player = MapDataCarrier.Instance.CuPlayerStatus;
		var enemy = MapDataCarrier.Instance.CuEnemyStatus;

		// TrueDamageは、全てのバフデバフ効果の影響を受けない
		// ただし、攻撃したという事実を残したいので、この関数内で統一した
		if (pack.Effect == EnumSelf.EffectType.TrueDamage) {
			int damage = pack.Value;
			if (pack.Target == EnumSelf.TargetType.Opponent) {
				EnemyCalcTrueDamage(-damage);
			} else if (pack.Target == EnumSelf.TargetType.Self) {
				PlayerCalcTrueDamage(-damage);
			}
		} else {
			int shield = 0;
			int overDamage = 0;
			int damage = CalcPlayerDamageValue(pack);

			// ダメージ計算したら、空元気は解除する
			player.ResetPower(EnumSelf.PowerType.FastStrength);
			PlayerUpdatePower(EnumSelf.PowerType.FastStrength, 0);
			
			// 必殺も解除する
			if (player.GetTurnPowerValue(EnumSelf.TurnPowerType.Critical) > 0) {
				// 最初の攻撃だったら、解除する
				if (pack.DamageNumberCount == 0) {
					PlayerUpdateTurnPower(EnumSelf.TurnPowerType.Critical, -1);
				}
			}

			if (pack.Target == EnumSelf.TargetType.Opponent) {
				// 相手がリアクティブシールド状態か
				if (enemy.GetTurnPowerValue(EnumSelf.TurnPowerType.ReactiveShield) > 0) {
					enemy.AddNowShield(enemy.GetTurnPowerValue(EnumSelf.TurnPowerType.ReactiveShield));
				}

				// 朽ちた体状態だったら、その数値分ダメージを加算して、数値を1増やす
				if (enemy.GetTurnPowerValue(EnumSelf.TurnPowerType.RotBody) > 0) {
					int rotbodyVal = enemy.GetTurnPowerValue(EnumSelf.TurnPowerType.RotBody);
					damage += rotbodyVal;
					EnemyUpdateTurnPower(EnumSelf.TurnPowerType.RotBody, 1);
				}

				shield = enemy.GetNowShield();
				enemy.AddNowShield(-damage);
				overDamage = shield - damage;
				if (overDamage < 0) {
					if (player.GetParameterListFlag(EnumSelf.ParameterType.AssassinRod) == true) {
						if (overDamage >= -4) {
							overDamage = -4;
						}
					}
					
					// 攻撃のみで反応させたいので、EnemyUpdateHp内ではなく、こちらで判定
					if (player.GetParameterListFlag(EnumSelf.ParameterType.DamageAddStrength1) == true) {
						PlayerUpdatePower(EnumSelf.PowerType.Strength, 1);
						PlayerUpdateTurnPower(EnumSelf.TurnPowerType.SubStrength, 1);
					}

					EnemyUpdateHp(overDamage);
					if (pack.Effect == EnumSelf.EffectType.DamageSuction) {
						PlayerUpdateHp(-overDamage);
					}
					
					if (pack.Effect == EnumSelf.EffectType.DamageShieldSuction) {
						player.AddNowShield(-overDamage);
					}
				}

				// 相手が棘状態か
				if (enemy.GetTurnPowerValue(EnumSelf.TurnPowerType.Thorn) > 0) {
					int thornDamage = enemy.GetTurnPowerValue(EnumSelf.TurnPowerType.Thorn);
					shield = player.GetNowShield();
					player.AddNowShield(-thornDamage);
					overDamage = shield - thornDamage;
					if (overDamage < 0) {
						PlayerUpdateHp(overDamage);
					}
				}
				
				// 相手が反撃状態か
				if (enemy.GetTurnPowerValue(EnumSelf.TurnPowerType.TurnThorn) > 0) {
					int turnThornDamage = enemy.GetTurnPowerValue(EnumSelf.TurnPowerType.TurnThorn);
					shield = player.GetNowShield();
					player.AddNowShield(-turnThornDamage);
					overDamage = shield - turnThornDamage;
					if (overDamage < 0) {
						PlayerUpdateHp(overDamage);
					}
				}

			} else if (pack.Target == EnumSelf.TargetType.Self) {
				LogManager.Instance.LogError("PlayerCalcDamageNormalDamage:Effect:Damage,Target:Self,自分を対象にしたDamageは未実装予定");
			}
		}
		
		// ここの関数が通ったら、攻撃を使ったとみなす
		player.AddTurnUseAttackCount(1);

	}
	
	// Player用
	public static void PlayerCalcSelfTrueDamage(ActionPack pack) {
		var player = MapDataCarrier.Instance.CuPlayerStatus;

		int damage = pack.Value;

		if (pack.Target == EnumSelf.TargetType.Self) {
			PlayerCalcSelfTrueDamage(damage);
		} else if (pack.Target == EnumSelf.TargetType.Opponent) {
			LogManager.Instance.LogError($"PlayerCalcTrueDamage:{EnumSelf.TargetType.Opponent}、相手を対象にしたSelfTrueDamageは未実装予定");
		}
	}
	
	public static void PlayerCalcSelfTrueDamage(int addValue) {
		var player = MapDataCarrier.Instance.CuPlayerStatus;
		player.AddTotalSelfTrueDamage(addValue);
		PlayerUpdateHp(-addValue);

		int addSelfTrueDamageStrengthCount = player.GetTurnPowerValue(EnumSelf.TurnPowerType.AddSelfTrueDamageStrength);
		if (addSelfTrueDamageStrengthCount > 0) {
			PlayerUpdatePower(EnumSelf.PowerType.Strength, addSelfTrueDamageStrengthCount);
		}
		
		int addSelfTrueDamageHealChargeCount = player.GetTurnPowerValue(EnumSelf.TurnPowerType.AddSelfTrueDamageHealCharge);
		if (addSelfTrueDamageHealChargeCount > 0) {
			PlayerUpdatePower(EnumSelf.PowerType.HealCharge, addSelfTrueDamageHealChargeCount);
		}
		
		int drawSelfTrueDamageCount = player.GetTurnPowerValue(EnumSelf.TurnPowerType.DrawSelfTrueDamage);
		if (drawSelfTrueDamageCount > 0) {
			var scene = MapDataCarrier.Instance.Scene as MapScene;
			scene.DrawCard(drawSelfTrueDamageCount);
		}
	}
	
	// こっちは、アクションパック以外で、シールドとのダメージ計算が必要な物
	// アクションパックではないので、被ダメ時に反応する諸々は、こちらでは反応しない
	public static void PlayerCalcDamageNormalDamage(int val) {
		var player = MapDataCarrier.Instance.CuPlayerStatus;
		var enemy = MapDataCarrier.Instance.CuEnemyStatus;
		int shield = 0;
		int overDamage = 0;
		int damage = val;
			
		shield = enemy.GetNowShield();
		enemy.AddNowShield(-damage);
		overDamage = shield - damage;
		if (overDamage < 0) {
			EnemyUpdateHp(overDamage);
		}
	}
	
	public static void PlayerCalcTrueDamage(int val) {
		PlayerUpdateHp(val);
	}
	
	public static void PlayerRemovePower(ActionPack pack) {
		var player = MapDataCarrier.Instance.CuPlayerStatus;
		var enemy = MapDataCarrier.Instance.CuEnemyStatus;
		
		if (pack.Target == EnumSelf.TargetType.Opponent) {
			enemy.ResetPowerAll();
			enemy.ResetTurnPower();
		} else if (pack.Target == EnumSelf.TargetType.Self) {
			player.ResetPowerAll();
			player.ResetTurnPower();
		}
		
		// 表示更新
		for (int i = 0; i < (int)EnumSelf.PowerType.Max; i++) {
			PlayerUpdatePower((EnumSelf.PowerType)i, 0);
			EnemyUpdatePower((EnumSelf.PowerType)i, 0);
		}
		
		for (int i = 0; i < (int)EnumSelf.TurnPowerType.Max; i++) {
			PlayerUpdateDisplayTurnPower((EnumSelf.TurnPowerType)i);
			EnemyUpdateDisplayTurnPower((EnumSelf.TurnPowerType)i);
		}
	}
	
	public static void PlayerCalcShieldDamage(ActionPack pack) {
		if (pack.Target == EnumSelf.TargetType.Opponent) {
			MapDataCarrier.Instance.CuEnemyStatus.AddNowShield(-pack.Value);
		} else if (pack.Target == EnumSelf.TargetType.Self) {
			MapDataCarrier.Instance.CuPlayerStatus.AddNowShield(-pack.Value);
		}
	}
	
	public static void PlayerCalcHeal(ActionPack pack) {
		int heal = pack.Value;


		if (pack.Target == EnumSelf.TargetType.Opponent) {
			bool isReverse = false;
			if (MapDataCarrier.Instance.CuEnemyStatus.GetTurnPowerValue(EnumSelf.TurnPowerType.ReverseHeal) > 0) {
				isReverse = true;
			}

			if (isReverse == true) {
				EnemyUpdateHp(-heal);
			} else {
				EnemyUpdateHp(heal);
			}
		} else if (pack.Target == EnumSelf.TargetType.Self) {
			bool isReverse = false;
			if (MapDataCarrier.Instance.CuPlayerStatus.GetTurnPowerValue(EnumSelf.TurnPowerType.ReverseHeal) > 0) {
				isReverse = true;
			}

			if (isReverse == true) {
				PlayerUpdateHp(-heal);
			} else {
				PlayerUpdateHp(heal);
			}
		}
	}
	
	public static void PlayerCalcShield(ActionPack pack) {
		PlayerStatus player = MapDataCarrier.Instance.CuPlayerStatus;
		EnemyStatus enemy = MapDataCarrier.Instance.CuEnemyStatus;

		int shield = CalcPlayerShieldValue(pack);

		if (pack.Target == EnumSelf.TargetType.Opponent) {
			if (pack.Effect == EnumSelf.EffectType.ShieldDouble) {
				shield = enemy.GetNowShield();
			}
			EnemyCalcShield(shield);
			//enemy.AddNowShield(shield);
		} else if (pack.Target == EnumSelf.TargetType.Self) {
			if (pack.Effect == EnumSelf.EffectType.ShieldDouble) {
				shield = player.GetNowShield();
			}
			PlayerCalcShield(shield);
			//player.AddNowShield(shield);
		}

		player.AddTurnUseShieldCount(1);
		if ((player.GetTurnUseShieldCount()%3) == 0) {
			if (player.GetParameterListFlag(EnumSelf.ParameterType.Use3ShieldAddToughness) == true) {
				PlayerUpdatePower(EnumSelf.PowerType.Toughness, 1);
			}
		}
	}
	
	// こっちは、数値を単純に加算する時使用。アーティファクト効果とか。
	public static void PlayerCalcShield(int val) {
		PlayerStatus player = MapDataCarrier.Instance.CuPlayerStatus;
		if (player.GetTurnPowerValue(EnumSelf.TurnPowerType.AddShieldTrueDamage) > 0) {
			if (val > 0) {
				int addShieldTrueDamage = player.GetTurnPowerValue(EnumSelf.TurnPowerType.AddShieldTrueDamage);
				EnemyCalcTrueDamage(-addShieldTrueDamage);
			}
		}
		player.AddNowShield(val);
	}
	
	public static void PlayerDeath(ActionPack pack) {
		PlayerStatus player = MapDataCarrier.Instance.CuPlayerStatus;
		EnemyStatus enemy = MapDataCarrier.Instance.CuEnemyStatus;

		if (pack.Target == EnumSelf.TargetType.Opponent) {
			enemy.SetNowHp(0);
		} else if (pack.Target == EnumSelf.TargetType.Self) {
			player.SetNowHp(0);
		}
	}
	
	public static void PlayerUpdatePower(ActionPack pack) {
		PlayerStatus player = MapDataCarrier.Instance.CuPlayerStatus;
		EnemyStatus enemy = MapDataCarrier.Instance.CuEnemyStatus;

		int val = pack.Value;
		EnumSelf.PowerType pType = ConvertEffectType2PowerType(pack.Effect);
		if (pack.Target == EnumSelf.TargetType.Opponent) {
			EnemyUpdatePower(pType, val);
			//enemy.AddPower(pType, val);
			//int nowVal = enemy.GetPower().GetValue(pType);
			//MapDataCarrier.Instance.EnemyPowerObjects[(int)pType].GetComponent<PowerController>().SetValue(nowVal);
		} else if (pack.Target == EnumSelf.TargetType.Self) {
			PlayerUpdatePower(pType, val);
			//player.AddPower(pType, val);
			//int nowVal = player.GetPower().GetValue(pType);
			//MapDataCarrier.Instance.PowerObjects[(int)pType].GetComponent<PowerController>().SetValue(nowVal);
		}
	}
	
	//// こっちは、数値が他のタイミングで変動した際の更新
	//public static void PlayerUpdatePower(EnumSelf.PowerType type) {
	//	PlayerStatus player = MapDataCarrier.Instance.CuPlayerStatus;
	//	int nowVal = player.GetPower().GetValue(type);
	//	MapDataCarrier.Instance.PowerObjects[(int)type].GetComponent<PowerController>().SetValue(nowVal);
	//}
	
	public static void PlayerUpdatePower(EnumSelf.PowerType type, int addValue) {
		PlayerStatus player = MapDataCarrier.Instance.CuPlayerStatus;
		if (type == EnumSelf.PowerType.Strength) {
			if (player.GetParameterListFlag(EnumSelf.ParameterType.AddAnotherStrength1) == true) {
				addValue += 1;
			}
		}
		
		if (type == EnumSelf.PowerType.HealCharge) {
			if (player.GetParameterListFlag(EnumSelf.ParameterType.ExtraAddHealCharge1) == true) {
				addValue += 1;
			}
		}

		player.AddPower(type, addValue);
		int nowVal = player.GetPower().GetValue(type);
		MapDataCarrier.Instance.PowerObjects[(int)type].GetComponent<PowerController>().SetValue(nowVal);
	}
	
	public static void PlayerUpdateTurnPower(ActionPack pack) {
		var player = MapDataCarrier.Instance.CuPlayerStatus;
		var enemy = MapDataCarrier.Instance.CuEnemyStatus;

		int val = pack.Value;
		EnumSelf.TurnPowerType pType = ConvertEffectType2TurnPowerType(pack.Effect);
		if (pack.Target == EnumSelf.TargetType.Opponent) {
			EnemyUpdateTurnPower(pType, val);
		} else if (pack.Target == EnumSelf.TargetType.Self) {
			PlayerUpdateTurnPower(pType, val);
		}
	}
	
	public static void PlayerUpdateTurnPower(EnumSelf.TurnPowerType type, int val) {
		var player = MapDataCarrier.Instance.CuPlayerStatus;
		if (type == EnumSelf.TurnPowerType.Weakness) {
			if (player.GetParameterListFlag(EnumSelf.ParameterType.AntiWeakness) == true) {
				return;
			}
		}
		
		if (type == EnumSelf.TurnPowerType.ShieldWeakness) {
			if (player.GetParameterListFlag(EnumSelf.ParameterType.AntiShieldWeakness) == true) {
				return;
			}
		}
		player.AddTurnPower(type, val);
		PlayerUpdateDisplayTurnPower(type);
	}
	
	public static void PlayerUpdateDisplayTurnPower(EnumSelf.TurnPowerType type) {
		var player = MapDataCarrier.Instance.CuPlayerStatus;
		int turn = MapDataCarrier.Instance.CuPlayerStatus.GetTurnPowerValue(type);
		MapDataCarrier.Instance.TurnPowerObjects[(int)type].GetComponent<TurnPowerController>().SetTurn(turn);
	}
	
	public static void PlayerDebugDisaster(ActionPack pack) {
		PlayerStatus player = MapDataCarrier.Instance.CuPlayerStatus;
		EnemyStatus enemy = MapDataCarrier.Instance.CuEnemyStatus;
		if (pack.Target == EnumSelf.TargetType.Opponent) {
			for (int i = 0; i < (int)EnumSelf.PowerType.Max; i++) {
				EnemyUpdatePower((EnumSelf.PowerType)i, 10);
			}
			
			for (int i = 0; i < (int)EnumSelf.TurnPowerType.Max; i++) {
				// PatientはAI変わっちゃうので、含めない
				if (i == (int)EnumSelf.TurnPowerType.Patient) {
					continue;
				}
				EnemyUpdateTurnPower((EnumSelf.TurnPowerType)i, 10);
			}
		} else if (pack.Target == EnumSelf.TargetType.Self) {
			for (int i = 0; i < (int)EnumSelf.PowerType.Max; i++) {
				PlayerUpdatePower((EnumSelf.PowerType)i, 10);
			}
			
			for (int i = 0; i < (int)EnumSelf.TurnPowerType.Max; i++) {
				// PatientはAI変わっちゃうので、含めない
				if (i == (int)EnumSelf.TurnPowerType.Patient) {
					continue;
				}
				PlayerUpdateTurnPower((EnumSelf.TurnPowerType)i, 10);
			}
		}
		
		// 
		for (int i = 0; i < (int)EnumSelf.PowerType.Max; i++) {
			PlayerUpdatePower((EnumSelf.PowerType)i, 0);
			EnemyUpdatePower((EnumSelf.PowerType)i, 0);
		}
	}
	
	// Enemy用
	public static void EnemyCalcDamageNormalDamage(ActionPack pack) {
		var player = MapDataCarrier.Instance.CuPlayerStatus;
		var enemy = MapDataCarrier.Instance.CuEnemyStatus;

		if (pack.Effect == EnumSelf.EffectType.TrueDamage) {
			int damage = pack.Value;
			if (pack.Target == EnumSelf.TargetType.Opponent) {
				PlayerCalcTrueDamage(-damage);
			} else if (pack.Target == EnumSelf.TargetType.Self) {
				EnemyCalcTrueDamage(-damage);
			}
		} else {
			int shield = 0;
			int overDamage = 0;
			int damage = CalcEnemyDamageValue(pack);
			
			// 空元気解除
			enemy.ResetPower(EnumSelf.PowerType.FastStrength);
			EnemyUpdatePower(EnumSelf.PowerType.FastStrength, 0);

			if (pack.Target == EnumSelf.TargetType.Opponent) {
				// 相手がリアクティブシールド状態か
				if (player.GetTurnPowerValue(EnumSelf.TurnPowerType.ReactiveShield) > 0) {
					player.AddNowShield(enemy.GetTurnPowerValue(EnumSelf.TurnPowerType.ReactiveShield));
				}

				// 朽ちた体状態だったら、その数値分ダメージを加算して、数値を1増やす
				if (player.GetTurnPowerValue(EnumSelf.TurnPowerType.RotBody) > 0) {
					int rotbodyVal = player.GetTurnPowerValue(EnumSelf.TurnPowerType.RotBody);
					damage += rotbodyVal;
					PlayerUpdateTurnPower(EnumSelf.TurnPowerType.RotBody, 1);
				}

				shield = player.GetNowShield();
				player.AddNowShield(-damage);
				overDamage = shield - damage;
				if (overDamage < 0) {
					PlayerUpdateHp(overDamage);
					if (pack.Effect == EnumSelf.EffectType.DamageSuction) {
						EnemyUpdateHp(-overDamage);
					}
				}
				
				// 相手が棘状態か
				if (player.GetTurnPowerValue(EnumSelf.TurnPowerType.Thorn) > 0) {
					int thornDamage = player.GetTurnPowerValue(EnumSelf.TurnPowerType.Thorn);
					shield = enemy.GetNowShield();
					enemy.AddNowShield(-thornDamage);
					overDamage = shield - thornDamage;
					if (overDamage < 0) {
						EnemyUpdateHp(overDamage);
					}
				}
				
				// 相手が反撃状態か
				if (player.GetTurnPowerValue(EnumSelf.TurnPowerType.TurnThorn) > 0) {
					int turnThornDamage = player.GetTurnPowerValue(EnumSelf.TurnPowerType.TurnThorn);
					shield = enemy.GetNowShield();
					enemy.AddNowShield(-turnThornDamage);
					overDamage = shield - turnThornDamage;
					if (overDamage < 0) {
						EnemyUpdateHp(overDamage);
					}
				}
			} else if (pack.Target == EnumSelf.TargetType.Self) {
				LogManager.Instance.LogError("EnemyCalcDamageNormalDamage:Effect:Damage,Target:Self,自分を対象にしたDamageは未実装予定");
			}
		}
	}
	
	//public static void EnemyCalcTrueDamage(ActionPack pack) {
	//	int damage = pack.Value;
	//	
	//	if (pack.Target == EnumSelf.TargetType.Opponent) {
	//		//PlayerUpdateHp(-damage);
	//		PlayerCalcTrueDamage(-damage);
	//	} else if (pack.Target == EnumSelf.TargetType.Self) {
	//		//EnemyUpdateHp(-damage);
	//		EnemyCalcTrueDamage(-damage);
	//	}
	//}
	
	public static void EnemyCalcTrueDamage(int val) {
		EnemyUpdateHp(val);
	}
	
	public static void EnemyRemovePower(ActionPack pack) {
		var player = MapDataCarrier.Instance.CuPlayerStatus;
		var enemy = MapDataCarrier.Instance.CuEnemyStatus;
		
		if (pack.Target == EnumSelf.TargetType.Opponent) {
			player.ResetPowerAll();
			player.ResetTurnPower();
		} else if (pack.Target == EnumSelf.TargetType.Self) {
			enemy.ResetPowerAll();
			enemy.ResetTurnPower();
		}
		
		// 表示更新
		for (int i = 0; i < (int)EnumSelf.PowerType.Max; i++) {
			PlayerUpdatePower((EnumSelf.PowerType)i, 0);
			EnemyUpdatePower((EnumSelf.PowerType)i, 0);
		}
		
		for (int i = 0; i < (int)EnumSelf.TurnPowerType.Max; i++) {
			PlayerUpdateDisplayTurnPower((EnumSelf.TurnPowerType)i);
			EnemyUpdateDisplayTurnPower((EnumSelf.TurnPowerType)i);
		}
	}
	
	public static void EnemyCalcShieldDamage(ActionPack pack) {
		PlayerStatus player = MapDataCarrier.Instance.CuPlayerStatus;
		EnemyStatus enemy = MapDataCarrier.Instance.CuEnemyStatus;

		if (pack.Target == EnumSelf.TargetType.Opponent) {
			player.AddNowShield(-pack.Value);
		} else if (pack.Target == EnumSelf.TargetType.Self) {
			enemy.AddNowShield(-pack.Value);
		}
	}
	
	public static void EnemyCalcHeal(ActionPack pack) {
		int heal = pack.Value;
		if (pack.Target == EnumSelf.TargetType.Opponent) {
			bool isReverse = false;
			if (MapDataCarrier.Instance.CuPlayerStatus.GetTurnPowerValue(EnumSelf.TurnPowerType.ReverseHeal) > 0) {
				isReverse = true;
			}

			if (isReverse == true) {
				PlayerUpdateHp(-heal);
			} else {
				PlayerUpdateHp(heal);
			}
		} else if (pack.Target == EnumSelf.TargetType.Self) {
			bool isReverse = false;
			if (MapDataCarrier.Instance.CuEnemyStatus.GetTurnPowerValue(EnumSelf.TurnPowerType.ReverseHeal) > 0) {
				isReverse = true;
			}

			if (isReverse == true) {
				EnemyUpdateHp(-heal);
			} else {
				EnemyUpdateHp(heal);
			}
		}
	}
	
	public static void EnemyCalcShield(ActionPack pack) {
		int shield = CalcEnemyShieldValue(pack);
		if (pack.Target == EnumSelf.TargetType.Opponent) {
			//MapDataCarrier.Instance.CuPlayerStatus.AddNowShield(shield);
			PlayerCalcShield(shield);
		} else if (pack.Target == EnumSelf.TargetType.Self) {
			//MapDataCarrier.Instance.CuEnemyStatus.AddNowShield(shield);
			EnemyCalcShield(shield);
		}
	}
	
	public static void EnemyCalcShield(int val) {
		EnemyStatus enemy = MapDataCarrier.Instance.CuEnemyStatus;
		enemy.AddNowShield(val);
	}
	
	public static void EnemyDeath(ActionPack pack) {
		PlayerStatus player = MapDataCarrier.Instance.CuPlayerStatus;
		EnemyStatus enemy = MapDataCarrier.Instance.CuEnemyStatus;

		if (pack.Target == EnumSelf.TargetType.Opponent) {
			player.SetNowHp(0);
		} else if (pack.Target == EnumSelf.TargetType.Self) {
			enemy.SetNowHp(0);
		}
	}
	
//	public static void EnemyCurse(ActionPack pack) {
//		PlayerStatus player = MapDataCarrier.Instance.CuPlayerStatus;
//		EnemyStatus enemy = MapDataCarrier.Instance.CuEnemyStatus;
//
//		if (pack.Target == EnumSelf.TargetType.Opponent) {
//			int actionId = pack.Value;
//			MasterAction2Table.Data data = MasterAction2Table.Instance.GetData(actionId);
//
//			// 呪いは山札に加える
//			var deckList = MapDataCarrier.Instance.BattleDeckList;
//			int random = UnityEngine.Random.Range(0, deckList.Count+1);
//			deckList.Insert(random, data);
//			//deckList.Add(data);
//		} else {
//			LogManager.Instance.LogError("EnemyCurse:Target:Self は、サポートしていない");
//		}
//	}
	
	// こっちは、アクションパックによる付与
	public static void EnemyUpdatePower(ActionPack pack) {
		PlayerStatus player = MapDataCarrier.Instance.CuPlayerStatus;
		EnemyStatus enemy = MapDataCarrier.Instance.CuEnemyStatus;

		int val = pack.Value;
		EnumSelf.PowerType pType = ConvertEffectType2PowerType(pack.Effect);
		if (pack.Target == EnumSelf.TargetType.Opponent) {
			PlayerUpdatePower(pType, val);
			//player.AddPower(pType, val);
			//int nowVal = player.GetPower().GetValue(pType);
			//MapDataCarrier.Instance.PowerObjects[(int)pType].GetComponent<PowerController>().SetValue(nowVal);
		} else if (pack.Target == EnumSelf.TargetType.Self) {
			EnemyUpdatePower(pType, val);
			//enemy.AddPower(pType, val);
			//int nowVal = enemy.GetPower().GetValue(pType);
			//MapDataCarrier.Instance.EnemyPowerObjects[(int)pType].GetComponent<PowerController>().SetValue(nowVal);
		}
	}
	
	//// こっちは、数値が他のタイミングで変動した際の更新
	//public static void EnemyUpdatePower(EnumSelf.PowerType type) {
	//	EnemyStatus enemy = MapDataCarrier.Instance.CuEnemyStatus;
	//	int nowVal = enemy.GetPower().GetValue(type);
	//	MapDataCarrier.Instance.EnemyPowerObjects[(int)type].GetComponent<PowerController>().SetValue(nowVal);
	//}
	
	// こっちは、数値が他のタイミングで変動した際の更新
	public static void EnemyUpdatePower(EnumSelf.PowerType type, int addValue) {
		EnemyStatus enemy = MapDataCarrier.Instance.CuEnemyStatus;
		enemy.AddPower(type, addValue);
		int nowVal = enemy.GetPower().GetValue(type);
		MapDataCarrier.Instance.EnemyPowerObjects[(int)type].GetComponent<PowerController>().SetValue(nowVal);
	}

	// こっちは、アクションパックによる付与
	public static void EnemyUpdateTurnPower(ActionPack pack) {
		int val = pack.Value;
		EnumSelf.TurnPowerType pType = ConvertEffectType2TurnPowerType(pack.Effect);
		if (pack.Target == EnumSelf.TargetType.Opponent) {
			PlayerUpdateTurnPower(pType, val);
		} else if (pack.Target == EnumSelf.TargetType.Self) {
			EnemyUpdateTurnPower(pType, val);
		}
	}
	
	// こっちは、数値が他のタイミングで変動した際の更新
	public static void EnemyUpdateTurnPower(EnumSelf.TurnPowerType type, int val) {
		var enemy = MapDataCarrier.Instance.CuEnemyStatus;
		enemy.AddTurnPower(type, val);
		EnemyUpdateDisplayTurnPower(type);
	}
	
	public static void EnemyUpdateDisplayTurnPower(EnumSelf.TurnPowerType type) {
		var enemy = MapDataCarrier.Instance.CuEnemyStatus;
		int turn = enemy.GetTurnPowerValue(type);
		MapDataCarrier.Instance.EnemyTurnPowerObjects[(int)type].GetComponent<TurnPowerController>().SetTurn(turn);
	}

	// 共通
	public static EnumSelf.TurnPowerType ConvertEffectType2TurnPowerType(EnumSelf.EffectType type) {
		EnumSelf.TurnPowerType pType = EnumSelf.TurnPowerType.None;
		if (type == EnumSelf.EffectType.DiceMinusOne) {
			pType = EnumSelf.TurnPowerType.DiceMinusOne;
		} else if (type == EnumSelf.EffectType.ReverseHeal) {
			pType = EnumSelf.TurnPowerType.ReverseHeal;
		} else if (type == EnumSelf.EffectType.Weakness) {
			pType = EnumSelf.TurnPowerType.Weakness;
		} else if (type == EnumSelf.EffectType.ShieldWeakness) {
			pType = EnumSelf.TurnPowerType.ShieldWeakness;
		} else if (type == EnumSelf.EffectType.TurnRegenerate) {
			pType = EnumSelf.TurnPowerType.TurnRegenerate;
		} else if (type == EnumSelf.EffectType.Vulnerable) {
			pType = EnumSelf.TurnPowerType.Vulnerable;
		} else if (type == EnumSelf.EffectType.Patient) {
			pType = EnumSelf.TurnPowerType.Patient;
		} else if (type == EnumSelf.EffectType.AutoShield) {
			pType = EnumSelf.TurnPowerType.AutoShield;
		} else if (type == EnumSelf.EffectType.Thorn) {
			pType = EnumSelf.TurnPowerType.Thorn;
		} else if (type == EnumSelf.EffectType.RotBody) {
			pType = EnumSelf.TurnPowerType.RotBody;
		} else if (type == EnumSelf.EffectType.Versak) {
			pType = EnumSelf.TurnPowerType.Versak;
		} else if (type == EnumSelf.EffectType.ReactiveShield) {
			pType = EnumSelf.TurnPowerType.ReactiveShield;
		} else if (type == EnumSelf.EffectType.SubStrength) {
			pType = EnumSelf.TurnPowerType.SubStrength;
		} else if (type == EnumSelf.EffectType.ShieldPreserve) {
			pType = EnumSelf.TurnPowerType.ShieldPreserve;
		} else if (type == EnumSelf.EffectType.TurnShieldPreserve) {
			pType = EnumSelf.TurnPowerType.TurnShieldPreserve;
		} else if (type == EnumSelf.EffectType.Invincible) {
			pType = EnumSelf.TurnPowerType.Invincible;
		} else if (type == EnumSelf.EffectType.DoubleAttack) {
			pType = EnumSelf.TurnPowerType.DoubleAttack;
		} else if (type == EnumSelf.EffectType.Cost6DoubleAttack) {
			pType = EnumSelf.TurnPowerType.Cost6DoubleAttack;
		} else if (type == EnumSelf.EffectType.Critical) {
			pType = EnumSelf.TurnPowerType.Critical;
		} else if (type == EnumSelf.EffectType.NonDraw) {
			pType = EnumSelf.TurnPowerType.NonDraw;
		} else if (type == EnumSelf.EffectType.DemonPower) {
			pType = EnumSelf.TurnPowerType.DemonPower;
		} else if (type == EnumSelf.EffectType.AddShieldTrueDamage) {
			pType = EnumSelf.TurnPowerType.AddShieldTrueDamage;
		} else if (type == EnumSelf.EffectType.TurnThorn) {
			pType = EnumSelf.TurnPowerType.TurnThorn;
		} else if (type == EnumSelf.EffectType.SupportShoot) {
			pType = EnumSelf.TurnPowerType.SupportShoot;
		} else if (type == EnumSelf.EffectType.AfterImage) {
			pType = EnumSelf.TurnPowerType.AfterImage;
		} else if (type == EnumSelf.EffectType.Activity) {
			pType = EnumSelf.TurnPowerType.Activity;
		} else if (type == EnumSelf.EffectType.Resist) {
			pType = EnumSelf.TurnPowerType.Resist;
		} else if (type == EnumSelf.EffectType.AddHandCurseDamage) {
			pType = EnumSelf.TurnPowerType.AddHandCurseDamage;
		} else if (type == EnumSelf.EffectType.DiscardCurseHeal) {
			pType = EnumSelf.TurnPowerType.DiscardCurseHeal;
		} else if (type == EnumSelf.EffectType.DiscardShield) {
			pType = EnumSelf.TurnPowerType.DiscardShield;
		} else if (type == EnumSelf.EffectType.SelfHarm) {
			pType = EnumSelf.TurnPowerType.SelfHarm;
		} else if (type == EnumSelf.EffectType.CurseReturn) {
			pType = EnumSelf.TurnPowerType.CurseReturn;
		} else if (type == EnumSelf.EffectType.DiscardDamage) {
			pType = EnumSelf.TurnPowerType.DiscardDamage;
		} else if (type == EnumSelf.EffectType.AddSelfTrueDamageStrength) {
			pType = EnumSelf.TurnPowerType.AddSelfTrueDamageStrength;
		} else if (type == EnumSelf.EffectType.DrawSelfTrueDamage) {
			pType = EnumSelf.TurnPowerType.DrawSelfTrueDamage;
		} else if (type == EnumSelf.EffectType.AddSelfTrueDamageHealCharge) {
			pType = EnumSelf.TurnPowerType.AddSelfTrueDamageHealCharge;
		}

		return pType;
	}
	
	public static EnumSelf.PowerType ConvertEffectType2PowerType(EnumSelf.EffectType type) {
		EnumSelf.PowerType pType = EnumSelf.PowerType.None;
		if (type == EnumSelf.EffectType.Strength) {
			pType = EnumSelf.PowerType.Strength;
		} else if (type == EnumSelf.EffectType.FastStrength) {
			pType = EnumSelf.PowerType.FastStrength;
		} else if (type == EnumSelf.EffectType.Toughness) {
			pType = EnumSelf.PowerType.Toughness;
		} else if (type == EnumSelf.EffectType.Regenerate) {
			pType = EnumSelf.PowerType.Regenerate;
		} else if (type == EnumSelf.EffectType.Poison) {
			pType = EnumSelf.PowerType.Poison;
		} else if (type == EnumSelf.EffectType.AddMaxDiceCost) {
			pType = EnumSelf.PowerType.AddMaxDiceCost;
		} else if (type == EnumSelf.EffectType.HealCharge) {
			pType = EnumSelf.PowerType.HealCharge;
		}

		return pType;
	}
	
	static public EnumSelf.EnemyActionType ConvertString2EnemyActionType(string typeString) {
		EnumSelf.EnemyActionType type = EnumSelf.EnemyActionType.None;

		if (typeString == "Random") {
			type = EnumSelf.EnemyActionType.Random;
		} else if (typeString == "Rotation") {
			type = EnumSelf.EnemyActionType.Rotation;
		}

		return type;
	}
	
	static public EnumSelf.AIChangeType ConvertString2AIChangeType(string typeString) {
		EnumSelf.AIChangeType type = EnumSelf.AIChangeType.None;
		
		if (typeString == "TurnProgress") {
			type = EnumSelf.AIChangeType.TurnProgress;
		} else if (typeString == "HpBorder") {
			type = EnumSelf.AIChangeType.HpBorder;
		} else if (typeString == "LotHpBorder") {
			type = EnumSelf.AIChangeType.LotHpBorder;
		}

		return type;
	}

	static public int LotRarity(List<int> lotWeightList) {
		int allWeight = 0;
		for (int i = 0; i < lotWeightList.Count; i++) {
			allWeight += lotWeightList[i];
		}

		int weight = UnityEngine.Random.Range(0, allWeight);

		int startWeight = 0;
		int endWeight = lotWeightList[0]-1;

		int rarity = 0;
		for (int i = 0; i < lotWeightList.Count; i++) {
			if ((startWeight <= weight) && (weight <= endWeight)) {
				rarity = i+1;
				break;
			}

			startWeight = endWeight+1;
			endWeight = startWeight + lotWeightList[i+1]-1;
		}

		return rarity;
	}
	
	// 敵味方のHP増減はここで統一
	// HP増減の内容（毒なのかダメージなのか）を知る必要が出てきた場合は、引数で渡してここで処理する
	// また、HP変動時に判定する物も、ここで処理を統一する
	static public void PlayerUpdateHp(int val) {
		var player = MapDataCarrier.Instance.CuPlayerStatus;

		if (val < 0) {
			// 全ての減算値を、1減らす（＝つまり、1増やす）
			if (player.GetParameterListFlag(EnumSelf.ParameterType.GodBless) == true) {
				val = val + 1;
			}
		}
			
		if (val < 0) {
			if (player.GetTurnPowerValue(EnumSelf.TurnPowerType.Invincible) > 0) {
				val = 0;
				PlayerUpdateTurnPower(EnumSelf.TurnPowerType.Invincible, -1);
			}
		}

		player.AddNowHp(val);

		if (val < 0) {
			// 0未満であれば、Hp減少という判断
			if (player.GetTurnPowerValue(EnumSelf.TurnPowerType.AutoShield) > 0) {
				PlayerUpdateTurnPower(EnumSelf.TurnPowerType.AutoShield, -1);
			}

		} else if (val > 0) {
			// 0より大ければ、Hp増加という判断
		}
		
	}
	
	static public void EnemyUpdateHp(int val) {
		var enemy = MapDataCarrier.Instance.CuEnemyStatus;

		if (val < 0) {
			if (enemy.GetTurnPowerValue(EnumSelf.TurnPowerType.Invincible) > 0) {
				val = 0;
				EnemyUpdateTurnPower(EnumSelf.TurnPowerType.Invincible, -1);
			}
		}

		enemy.AddNowHp(val);

		if (val < 0) {
			// 0未満であれば、Hp減少という判断
			// 敵の状態異常Patiantの数値があれば、その数値も減らす
			if (enemy.GetTurnPowerValue(EnumSelf.TurnPowerType.Patient) > 0) {
				EnemyUpdateTurnPower(EnumSelf.TurnPowerType.Patient, val);
				
				if (enemy.GetTurnPowerValue(EnumSelf.TurnPowerType.Patient) <= 0) {
					// Patientは今後ユニークで増えていくので、それに該当するIDを決め打ちで指定する
					MasterEnemyAITable.Data data = MasterEnemyAITable.Instance.GetData(2991);
					enemy.UpdateAIData(data);
					// 敵の行動開始前なので、行動を抽選しなおす
					enemy.LotActionData();
				}
			}
			
			if (enemy.GetTurnPowerValue(EnumSelf.TurnPowerType.AutoShield) > 0) {
				EnemyUpdateTurnPower(EnumSelf.TurnPowerType.AutoShield, -1);
			}

			// 他の条件とのかみ合わせ次第で、条件判定を考える
			// 現状あり得ないが、この書き方だと、PatientとHp減少が噛み合うと、HP減少が優先される
			enemy.CheckAIForHpBorder();
			
		} else if (val > 0) {
			// 0より大ければ、Hp増加という判断
		}
	}

	static public bool IsCurse(int id) {
		bool res = false;
		if ((5000 <= id) && (id <= 5999)) {
			res = true;
		}
		return res;
	}

	// 選択した効果が、相手ダメージと自分シールドのみの効果かどうか
	static public bool IsOnlyDamageAndShield(MasterAction2Table.Data data) {
		bool res = false;

		var list = data.ActionPackList;

		bool findOpponentDamage = false;
		bool findSelfShield = false;
		bool findOther = false;

		for (int i = 0; i < list.Count; i++) {
			ActionPack pack = list[i];
			// ここのダメージ判定は、PlayerValueChangeのPlayerCalcDamageNormalDamageの判定と合わせておく
			if (
				(pack.Effect == EnumSelf.EffectType.Damage) ||
				(pack.Effect == EnumSelf.EffectType.DamageSuction) ||
				(pack.Effect == EnumSelf.EffectType.DamageShieldSuction) ||
				(pack.Effect == EnumSelf.EffectType.DamageGainMaxHp) ||
				(pack.Effect == EnumSelf.EffectType.DamageMultiStrength) ||
				(pack.Effect == EnumSelf.EffectType.DamageDiscardCount) ||
				(pack.Effect == EnumSelf.EffectType.TrueDamage) ||
				(pack.Effect == EnumSelf.EffectType.DamageFinish) ||
				(pack.Effect == EnumSelf.EffectType.DamageDice) ||
				(pack.Effect == EnumSelf.EffectType.DamageTotalSelfTrueDamage) ||
				(pack.Effect == EnumSelf.EffectType.ShieldBash)
			) {
				if (pack.Target == EnumSelf.TargetType.Opponent) {
					findOpponentDamage = true;
				}
			} else if (
				(pack.Effect == EnumSelf.EffectType.Shield) ||
				(pack.Effect == EnumSelf.EffectType.StrengthShield)
			) {

				if (pack.Target == EnumSelf.TargetType.Self) {
					findSelfShield = true;
				}
			} else {
				findOther = true;
				break;
			}
		}

		if ((findOpponentDamage == true) && (findSelfShield == true)) {
			res = true;
		}

		if (findOther == true) {
			res = false;
		}

		return res;
	}
	
	static public bool IsOnlyDamageAndShieldAll() {
		bool res = true;
		
		PlayerStatus player = MapDataCarrier.Instance.CuPlayerStatus;

		var cloneList = player.GetBackUpActionDataCloseList();

		for (int i = 0; i < cloneList.Count; i++) {
			bool check = IsOnlyDamageAndShield(cloneList[i]);
			if (check == false) {
				res = false;
				break;
			}
		}

		return res;
	}
	
	static public bool IsOnlyDamage(MasterAction2Table.Data data) {
		bool res = false;

		var list = data.ActionPackList;

		bool findOpponentDamage = false;
		bool findOther = false;

		for (int i = 0; i < list.Count; i++) {
			ActionPack pack = list[i];
			if (
				(pack.Effect == EnumSelf.EffectType.Damage) ||
				(pack.Effect == EnumSelf.EffectType.DamageSuction) ||
				(pack.Effect == EnumSelf.EffectType.DamageShieldSuction) ||
				(pack.Effect == EnumSelf.EffectType.DamageGainMaxHp) ||
				(pack.Effect == EnumSelf.EffectType.DamageMultiStrength) ||
				(pack.Effect == EnumSelf.EffectType.DamageDiscardCount) ||
				(pack.Effect == EnumSelf.EffectType.TrueDamage) ||
				(pack.Effect == EnumSelf.EffectType.DamageFinish) ||
				(pack.Effect == EnumSelf.EffectType.DamageDice) ||
				(pack.Effect == EnumSelf.EffectType.DamageTotalSelfTrueDamage) ||
				(pack.Effect == EnumSelf.EffectType.ShieldBash)
			) {
				if (pack.Target == EnumSelf.TargetType.Opponent) {
					findOpponentDamage = true;
				}
			} else {
				findOther = true;
				break;
			}
		}

		if (findOpponentDamage == true) {
			res = true;
		}
		
		if (findOther == true) {
			res = false;
		}

		return res;
	}
	
	static public bool IsOnlyDamageAll() {
		bool res = true;
		
		PlayerStatus player = MapDataCarrier.Instance.CuPlayerStatus;

		var cloneList = player.GetBackUpActionDataCloseList();

		for (int i = 0; i < cloneList.Count; i++) {
			bool check = IsOnlyDamage(cloneList[i]);
			if (check == false) {
				res = false;
				break;
			}
		}

		return res;
	}

	static public int CalcPlayerDamageValue(ActionPack pack) {
		var player = MapDataCarrier.Instance.CuPlayerStatus;
		var enemy = MapDataCarrier.Instance.CuEnemyStatus;
		
		int val = 0;
				
		bool isNormalDamage = true;// Valの値をそのまま使う状態かどうか
		// シールドバッシュかどうか
		if (pack.Effect == EnumSelf.EffectType.ShieldBash) {
			val = player.GetNowShield();
		} else if (pack.Effect == EnumSelf.EffectType.DamageFinish) {
			int count = player.GetTurnUseAttackCount();
			val = count * pack.Value;
		} else if (pack.Effect == EnumSelf.EffectType.DamageDice) {
			// ここに来る際には、もうダイスコストが引かれてしまっている為
			// 払ったコスト分を加算して、調整する
			var data = MasterAction2Table.Instance.GetData(pack.ExecuteActionId);
			val = MapDataCarrier.Instance.CurrentTotalDiceCost + data.DiceCost;
		} else if (pack.Effect == EnumSelf.EffectType.DamageTotalSelfTrueDamage) {
			val = player.GetTotalSelfTrueDamage() * pack.Value;
		} else {
			if (player.GetParameterListFlag(EnumSelf.ParameterType.ApprenticeKnight) == true) {
				MasterAction2Table.Data data = MasterAction2Table.Instance.GetData(pack.ExecuteActionId);
				if (IsOnlyDamageAndShield(data) == true) {
					val += (pack.Value + 3);
					isNormalDamage = false;
				}
			}
			
			if (player.GetParameterListFlag(EnumSelf.ParameterType.KnightMaster) == true) {
				if (IsOnlyDamageAndShieldAll() == true) {
					val += (pack.Value * 2);
					isNormalDamage = false;
				}
			}

			if (pack.Effect == EnumSelf.EffectType.DamageMultiStrength) {
				isNormalDamage = false;
			}
			
			if (pack.Effect == EnumSelf.EffectType.DamageDiscardCount) {
				int discardCount = MapDataCarrier.Instance.DiscardList.Count;
				val = pack.Value * discardCount;
				isNormalDamage = false;
			}

			if (isNormalDamage == true) {
				val = pack.Value;
			}
		}

		if (player.GetParameterListFlag(EnumSelf.ParameterType.Under1CostGainDamage4) == true) {
			MasterAction2Table.Data data = MasterAction2Table.Instance.GetData(pack.ExecuteActionId);
			if (data.DiceCost <= 1) {
				val += 4;
			}
		}

		// 必殺があるかどうか
		if (player.GetTurnPowerValue(EnumSelf.TurnPowerType.Critical) > 0) {
			// 最初の攻撃だったら、加算する
			if (pack.DamageNumberCount == 0) {
				val += pack.Value;
			}
		}

		// 筋力があるかどうか
		int strength = player.GetPower().GetValue(EnumSelf.PowerType.Strength);

		if (pack.Effect == EnumSelf.EffectType.DamageMultiStrength) {
			val += (strength * pack.Value);
		} else {
			val += strength;
		}
		
		// 空元気があるかどうか
		int faststrength = player.GetPower().GetValue(EnumSelf.PowerType.FastStrength);
		val += faststrength;

		// バーサク状態か
		if (player.GetTurnPowerValue(EnumSelf.TurnPowerType.Versak) > 0) {
			val *= 2;
		}

		// 与ダメ減少状態か
		if (player.GetTurnPowerValue(EnumSelf.TurnPowerType.Weakness) > 0) {
			val = val - (val * 25 / 100);
		}
		
		// 相手が被ダメ上昇状態か
		if (enemy.GetTurnPowerValue(EnumSelf.TurnPowerType.Vulnerable) > 0) {
			if (player.GetParameterListFlag(EnumSelf.ParameterType.VulnerableUp) == true) {
				// 与ダメが75％上がる
				val = val + (val * 75 / 100);
			} else {
				val = val + (val * 50 / 100);
			}
		}

		return val;
	}
	
	static public int CalcPlayerShieldValue(ActionPack pack) {
		var player = MapDataCarrier.Instance.CuPlayerStatus;
		int powerToughness = player.GetPower().GetValue(EnumSelf.PowerType.Toughness);
		int val = 0;

		bool isNormalShield = true;// Valの値をそのまま使うかどうか
		if (player.GetParameterListFlag(EnumSelf.ParameterType.ApprenticeKnight) == true) {
			MasterAction2Table.Data data = MasterAction2Table.Instance.GetData(pack.ExecuteActionId);
			if (IsOnlyDamageAndShield(data) == true) {
				val += pack.Value + 3;
				isNormalShield = false;
			}
		}
		
		if (player.GetParameterListFlag(EnumSelf.ParameterType.KnightMaster) == true) {
			if (IsOnlyDamageAndShieldAll() == true) {
				val += pack.Value * 2;
				isNormalShield = false;
			}
		}

		if (pack.Effect == EnumSelf.EffectType.StrengthShield) {
			int strength = player.GetPower().GetValue(EnumSelf.PowerType.Strength);
			val += strength;
			isNormalShield = false;
		}

		if (isNormalShield == true) {
			val = pack.Value;
		}
		
		val = val + powerToughness;

		if (player.GetTurnPowerValue(EnumSelf.TurnPowerType.ShieldWeakness) > 0) {
			val = val - (val * 25 / 100);
		}
				
		return val;
	}

	static public int CalcEnemyDamageValue(ActionPack pack) {
		var player = MapDataCarrier.Instance.CuPlayerStatus;
		var enemy = MapDataCarrier.Instance.CuEnemyStatus;

		int val = 0;
		
		if (pack.Effect == EnumSelf.EffectType.ShieldBash) {
			val = enemy.GetNowShield();
		} else {
			val = pack.Value;
		}
		int strength = enemy.GetPower().GetValue(EnumSelf.PowerType.Strength);
		if (pack.Effect == EnumSelf.EffectType.DamageMultiStrength) {
			val += (strength * pack.Value);
		} else {
			val += strength;
		}

		// 空元気があるかどうか
		int faststrength = enemy.GetPower().GetValue(EnumSelf.PowerType.FastStrength);
		val += faststrength;

		if (enemy.GetTurnPowerValue(EnumSelf.TurnPowerType.Versak) > 0) {
			val *= 2;
		}

		if (enemy.GetTurnPowerValue(EnumSelf.TurnPowerType.Weakness) > 0) {
			if (player.GetParameterListFlag(EnumSelf.ParameterType.WeaknessUp) == true) {
				val = val - (val * 40 / 100);
			} else {
				val = val - (val * 25 / 100);
			}
		}
				
		if (player.GetTurnPowerValue(EnumSelf.TurnPowerType.Vulnerable) > 0) {
			val = val + (val * 50 / 100);
		}
		return val;
	}
	
	static public int CalcEnemyShieldValue(ActionPack pack) {
		var enemy = MapDataCarrier.Instance.CuEnemyStatus;
		int powerToughness = enemy.GetPower().GetValue(EnumSelf.PowerType.Toughness);
		bool isNormalShield = true;// Valの値をそのまま使うかどうか
		int val = 0;

		if (pack.Effect == EnumSelf.EffectType.StrengthShield) {
			int strength = enemy.GetPower().GetValue(EnumSelf.PowerType.Strength);
			val += strength;
			isNormalShield = false;
		}

		if (isNormalShield == true) {
			val = pack.Value;
		}
		
		val = val + powerToughness;

		if (enemy.GetTurnPowerValue(EnumSelf.TurnPowerType.ShieldWeakness) > 0) {
			val = val - (val * 25 / 100);
		}

		return val;
	}
	
	// 選択した効果に相手へのDamage効果が含まれているか
	static public bool IsOpponentDamage(MasterAction2Table.Data data) {
		var list = data.ActionPackList;

		bool findOpponentDamage = false;

		for (int i = 0; i < list.Count; i++) {
			ActionPack pack = list[i];
			if (
				(pack.Effect == EnumSelf.EffectType.Damage) ||
				(pack.Effect == EnumSelf.EffectType.DamageSuction) ||
				(pack.Effect == EnumSelf.EffectType.DamageShieldSuction) ||
				(pack.Effect == EnumSelf.EffectType.DamageGainMaxHp) ||
				(pack.Effect == EnumSelf.EffectType.DamageMultiStrength) ||
				(pack.Effect == EnumSelf.EffectType.DamageDiscardCount) ||
				(pack.Effect == EnumSelf.EffectType.TrueDamage) ||
				(pack.Effect == EnumSelf.EffectType.DamageFinish) ||
				(pack.Effect == EnumSelf.EffectType.DamageDice) ||
				(pack.Effect == EnumSelf.EffectType.DamageTotalSelfTrueDamage) ||
				(pack.Effect == EnumSelf.EffectType.ShieldBash)
			) {
				if (pack.Target == EnumSelf.TargetType.Opponent) {
					findOpponentDamage = true;
					break;
				}
			}
		}

		return findOpponentDamage;
	}
	
	public static void PlayerDoubleStrength(ActionPack pack) {
		var player = MapDataCarrier.Instance.CuPlayerStatus;

		if (pack.Target == EnumSelf.TargetType.Opponent) {
			LogManager.Instance.LogError("PlayerDoubleStrength:Effect:DoubleStrength,Target:Opponent,相手を対象にしたDoubleStrengthは未実装予定");
		} else if (pack.Target == EnumSelf.TargetType.Self) {
			int strength = player.GetPower().GetValue(EnumSelf.PowerType.Strength);
			int addValue = strength;

			PlayerUpdatePower(EnumSelf.PowerType.Strength, addValue);

			PlayerUpdateTurnPower(EnumSelf.TurnPowerType.SubStrength, addValue);
		}
	}
}
