using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCalculationFunction {
	public static void PlayerValueChange(ActionPack pack) {
		if (
			(pack.Effect == EnumSelf.EffectType.Damage) ||
			(pack.Effect == EnumSelf.EffectType.DamageSuction) ||
			(pack.Effect == EnumSelf.EffectType.ShieldBash)
		) {
			BattleCalculationFunction.PlayerCalcDamageNormalDamage(pack);
		} else if (pack.Effect == EnumSelf.EffectType.TrueDamage) {
			BattleCalculationFunction.PlayerCalcTrueDamage(pack);
		} else if (pack.Effect == EnumSelf.EffectType.RemovePower) {
			BattleCalculationFunction.PlayerRemovePower(pack);
		} else if (pack.Effect == EnumSelf.EffectType.Heal) {
			BattleCalculationFunction.PlayerCalcHeal(pack);
		} else if (
			(pack.Effect == EnumSelf.EffectType.Shield) ||
			(pack.Effect == EnumSelf.EffectType.ShieldDouble)
		) {
			BattleCalculationFunction.PlayerCalcShield(pack);
		} else if (pack.Effect == EnumSelf.EffectType.ShieldDamage) {
			BattleCalculationFunction.PlayerCalcShieldDamage(pack);
		} else if (pack.Effect == EnumSelf.EffectType.Death) {
			BattleCalculationFunction.PlayerDeath(pack);
		} else if (pack.Effect == EnumSelf.EffectType.Curse) {
			BattleCalculationFunction.PlayerCurse(pack);
		} else if (
			(pack.Effect == EnumSelf.EffectType.Strength) ||
			(pack.Effect == EnumSelf.EffectType.FastStrength) ||
			(pack.Effect == EnumSelf.EffectType.Toughness) ||
			(pack.Effect == EnumSelf.EffectType.Poison) ||
			(pack.Effect == EnumSelf.EffectType.Regenerate)
		) {
			BattleCalculationFunction.PlayerUpdatePower(pack);
		} else if (
			(pack.Effect == EnumSelf.EffectType.ReverseHeal) ||
			(pack.Effect == EnumSelf.EffectType.Vulnerable) ||
			(pack.Effect == EnumSelf.EffectType.AutoShield) ||
			(pack.Effect == EnumSelf.EffectType.Thorn) ||
			(pack.Effect == EnumSelf.EffectType.RotBody) ||
			(pack.Effect == EnumSelf.EffectType.Versak) ||
			(pack.Effect == EnumSelf.EffectType.DiceMinusOne) ||
			(pack.Effect == EnumSelf.EffectType.ShieldWeakness) ||
			(pack.Effect == EnumSelf.EffectType.ReactiveShield) ||
			(pack.Effect == EnumSelf.EffectType.SubStrength) ||
			(pack.Effect == EnumSelf.EffectType.ShieldPreserve) ||
			(pack.Effect == EnumSelf.EffectType.Invincible) ||
			(pack.Effect == EnumSelf.EffectType.Weakness)
		) {
			BattleCalculationFunction.PlayerUpdateTurnPower(pack);
		} else if (pack.Effect == EnumSelf.EffectType.DebugDisaster) {
			BattleCalculationFunction.PlayerDebugDisaster(pack);
		}
	}

	public static void EnemyValueChange(ActionPack pack) {
		if (
			(pack.Effect == EnumSelf.EffectType.Damage) ||
			(pack.Effect == EnumSelf.EffectType.DamageSuction) ||
			(pack.Effect == EnumSelf.EffectType.ShieldBash)
		) {
			BattleCalculationFunction.EnemyCalcDamageNormalDamage(pack);
		} else if (pack.Effect == EnumSelf.EffectType.TrueDamage) {
			BattleCalculationFunction.EnemyCalcTrueDamage(pack);
		} else if (pack.Effect == EnumSelf.EffectType.RemovePower) {
			BattleCalculationFunction.EnemyRemovePower(pack);
		} else if (pack.Effect == EnumSelf.EffectType.Heal) {
			BattleCalculationFunction.EnemyCalcHeal(pack);
		} else if (
			(pack.Effect == EnumSelf.EffectType.Shield) ||
			(pack.Effect == EnumSelf.EffectType.ShieldDouble)
		) {
			BattleCalculationFunction.EnemyCalcShield(pack);
		} else if (pack.Effect == EnumSelf.EffectType.ShieldDamage) {
			BattleCalculationFunction.EnemyCalcShieldDamage(pack);
		} else if (pack.Effect == EnumSelf.EffectType.Death) {
			BattleCalculationFunction.EnemyDeath(pack);
		} else if (pack.Effect == EnumSelf.EffectType.Curse) {
			BattleCalculationFunction.EnemyCurse(pack);
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
			(pack.Effect == EnumSelf.EffectType.Vulnerable) ||
			(pack.Effect == EnumSelf.EffectType.Patient) ||
			(pack.Effect == EnumSelf.EffectType.AutoShield) ||
			(pack.Effect == EnumSelf.EffectType.Thorn) ||
			(pack.Effect == EnumSelf.EffectType.RotBody) ||
			(pack.Effect == EnumSelf.EffectType.Versak) ||
			(pack.Effect == EnumSelf.EffectType.ReactiveShield) ||
			(pack.Effect == EnumSelf.EffectType.SubStrength) ||
			(pack.Effect == EnumSelf.EffectType.ShieldPreserve) ||
			(pack.Effect == EnumSelf.EffectType.Invincible) ||
			(pack.Effect == EnumSelf.EffectType.ReverseHeal)
		) {
			BattleCalculationFunction.EnemyUpdateTurnPower(pack);
		}
	}
	
	public static void PlayerTurnStartValueChange() {
		// 再生などのバフをチェック
		PlayerStatus player = MapDataCarrier.Instance.CuPlayerStatus;
		Power power = player.GetPower();
		
		// 堅牢状態じゃなかったら、シールドを0にする
		if (player.GetTurnPowerValue(EnumSelf.TurnPowerType.ShieldPreserve) == 0) {
			player.SetNowShield(0);
		}

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
				player.AddTurnPower(EnumSelf.TurnPowerType.Versak, 1);
				int turn = player.GetTurnPowerValue(EnumSelf.TurnPowerType.Versak);
				MapDataCarrier.Instance.TurnPowerObjects[(int)EnumSelf.TurnPowerType.Versak].GetComponent<TurnPowerController>().SetTurn(turn);
			}
		}

		// 再生
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
		
		// ターンスタート時に数値を減らす物
		if (player.GetTurnPowerValue(EnumSelf.TurnPowerType.ShieldPreserve) > 0) {
			player.AddTurnPower(EnumSelf.TurnPowerType.ShieldPreserve, -1);
			int turn = player.GetTurnPowerValue(EnumSelf.TurnPowerType.ShieldPreserve);
			MapDataCarrier.Instance.TurnPowerObjects[(int)EnumSelf.TurnPowerType.ShieldPreserve].GetComponent<TurnPowerController>().SetTurn(turn);
		}
	}
	
	public static void PlayerTurnEndValueChange() {
		var status = MapDataCarrier.Instance.CuPlayerStatus;

		for (int i = 0; i < (int)EnumSelf.TurnPowerType.Max; i++) {
			if (
				(i == (int)EnumSelf.TurnPowerType.Patient) || 
				(i == (int)EnumSelf.TurnPowerType.Thorn) || 
				(i == (int)EnumSelf.TurnPowerType.ReactiveShield) ||
				(i == (int)EnumSelf.TurnPowerType.ShieldPreserve) ||
				(i == (int)EnumSelf.TurnPowerType.Invincible) ||
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
					status.AddPower(EnumSelf.PowerType.Strength, -substrength);
					status.SetTurnPowerValue(EnumSelf.TurnPowerType.SubStrength, 0);
					PlayerUpdatePower(EnumSelf.PowerType.Strength);
				}
			} else {
				status.AddTurnPower((EnumSelf.TurnPowerType)i, -1);
			}
			int turn = status.GetTurnPowerValue((EnumSelf.TurnPowerType)i);
			MapDataCarrier.Instance.TurnPowerObjects[(int)i].GetComponent<TurnPowerController>().SetTurn(turn);
		}
		
	}

	public static void EnemyTurnStartValueChange() {
		// 再生などのバフをチェック
		EnemyStatus enemy = MapDataCarrier.Instance.CuEnemyStatus;
		Power power = enemy.GetPower();
		
		// 堅牢状態でなければ、シールドを消滅させる
		if (enemy.GetTurnPowerValue(EnumSelf.TurnPowerType.ShieldPreserve) == 0) {
			enemy.SetNowShield(0);
		}

		// 再生
		int val = power.GetValue(EnumSelf.PowerType.Regenerate);
		if (val > 0) {
			bool isReverse = false;
			if (MapDataCarrier.Instance.CuEnemyStatus.GetTurnPowerValue(EnumSelf.TurnPowerType.ReverseHeal) > 0) {
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
		PlayerStatus player = MapDataCarrier.Instance.CuPlayerStatus;
		val = player.GetTurnPowerValue(EnumSelf.TurnPowerType.AutoShield);
		if (val > 0) {
			player.AddNowShield(val);
		}
		
		// ターンスタート時に数値を減らす物
		if (enemy.GetTurnPowerValue(EnumSelf.TurnPowerType.ShieldPreserve) > 0) {
			enemy.AddTurnPower(EnumSelf.TurnPowerType.ShieldPreserve, -1);
			int turn = enemy.GetTurnPowerValue(EnumSelf.TurnPowerType.ShieldPreserve);
			MapDataCarrier.Instance.EnemyTurnPowerObjects[(int)EnumSelf.TurnPowerType.ShieldPreserve].GetComponent<TurnPowerController>().SetTurn(turn);
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
				(i == (int)EnumSelf.TurnPowerType.Invincible) ||
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
					status.AddPower(EnumSelf.PowerType.Strength, -substrength);
					status.SetTurnPowerValue(EnumSelf.TurnPowerType.SubStrength, 0);
					EnemyUpdatePower(EnumSelf.PowerType.Strength);
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
		//var player = MapDataCarrier.Instance.CuPlayerStatus;
		//var enemy = MapDataCarrier.Instance.CuEnemyStatus;
		//int shield = 0;
		//int powerStrength = player.GetPower().GetValue(EnumSelf.PowerType.Strength);
		//int overDamage = 0;
		//int damage = 0;

		//if (pack.Effect == EnumSelf.EffectType.ShieldBash) {
		//	damage = player.GetNowShield();
		//} else {
		//	damage = pack.Value;
		//}
		//	
		//damage = damage+powerStrength;
		//
		//// 脱力しているかどうか
		//if (player.GetTurnPowerValue(EnumSelf.TurnPowerType.Weakness) > 0) {
		//	// 与ダメが25％下がる
		//	damage = damage - (damage * 25 / 100);
		//}

		//if (pack.Target == EnumSelf.TargetType.Opponent) {
		//	// 相手がリアクティブシールド状態か
		//	if (enemy.GetTurnPowerValue(EnumSelf.TurnPowerType.ReactiveShield) > 0) {
		//		player.AddNowShield(enemy.GetTurnPowerValue(EnumSelf.TurnPowerType.ReactiveShield));
		//	}

		//	// 狂戦士状態か
		//	if (player.GetTurnPowerValue(EnumSelf.TurnPowerType.Versak) > 0) {
		//		// 与ダメが倍になる
		//		damage *= 2;
		//		PlayerUpdateTurnPower(EnumSelf.TurnPowerType.Versak, -1);
		//	}

		//	// 相手弱体しているか
		//	if (enemy.GetTurnPowerValue(EnumSelf.TurnPowerType.Vulnerable) > 0) {
		//		if (player.GetParameterListFlag(EnumSelf.ParameterType.VulnerableUp) == true) {
		//			// 与ダメが75％上がる
		//			damage = damage + (damage * 75 / 100);
		//		} else {
		//			// 与ダメが50％上がる
		//			damage = damage + (damage * 50 / 100);
		//		}
		//	}

		//	// 朽ちた体状態だったら、その数値分ダメージを加算して、数値を1増やす
		//	if (enemy.GetTurnPowerValue(EnumSelf.TurnPowerType.RotBody) > 0) {
		//		int rotbodyVal = enemy.GetTurnPowerValue(EnumSelf.TurnPowerType.RotBody);
		//		damage += rotbodyVal;
		//		EnemyUpdateTurnPower(EnumSelf.TurnPowerType.RotBody, 1);
		//	}

		//	shield = enemy.GetNowShield();
		//	enemy.AddNowShield(-damage);
		//	overDamage = shield - damage;
		//	if (overDamage < 0) {
		//		EnemyUpdateHp(overDamage);
		//		if (pack.Effect == EnumSelf.EffectType.DamageSuction) {
		//			PlayerUpdateHp(-overDamage);
		//		}
		//	}

		//	// 相手が棘状態か
		//	if (enemy.GetTurnPowerValue(EnumSelf.TurnPowerType.Thorn) > 0) {
		//		int thornDamage = enemy.GetTurnPowerValue(EnumSelf.TurnPowerType.Thorn);
		//		shield = player.GetNowShield();
		//		player.AddNowShield(-thornDamage);
		//		overDamage = shield - thornDamage;
		//		if (overDamage < 0) {
		//			PlayerUpdateHp(overDamage);
		//		}
		//	}

		//} else if (pack.Target == EnumSelf.TargetType.Self) {
		//	LogManager.Instance.LogError("PlayerCalcDamageNormalDamage:Effect:Damage,Target:Self,自分を対象にしたDamageは未実装予定");
		//	//shield = player.GetNowShield();
		//	//player.AddNowShield(-damage);
		//	//overDamage = shield - damage;
		//	//if (overDamage < 0) {
		//	//	PlayerUpdateHp(overDamage);
		//	//}
		//}
		var player = MapDataCarrier.Instance.CuPlayerStatus;
		var enemy = MapDataCarrier.Instance.CuEnemyStatus;
		
		int shield = 0;
		int overDamage = 0;
		int damage = CalcPlayerDamageValue(pack);

		// ダメージ計算したら、空元気は解除する
		player.ResetPower(EnumSelf.PowerType.FastStrength);
		PlayerUpdatePower(EnumSelf.PowerType.FastStrength);

		if (pack.Target == EnumSelf.TargetType.Opponent) {
			// 相手がリアクティブシールド状態か
			if (enemy.GetTurnPowerValue(EnumSelf.TurnPowerType.ReactiveShield) > 0) {
				player.AddNowShield(enemy.GetTurnPowerValue(EnumSelf.TurnPowerType.ReactiveShield));
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
				EnemyUpdateHp(overDamage);
				if (pack.Effect == EnumSelf.EffectType.DamageSuction) {
					PlayerUpdateHp(-overDamage);
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

		} else if (pack.Target == EnumSelf.TargetType.Self) {
			LogManager.Instance.LogError("PlayerCalcDamageNormalDamage:Effect:Damage,Target:Self,自分を対象にしたDamageは未実装予定");
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
	
	public static void PlayerCalcTrueDamage(ActionPack pack) {
		int damage = pack.Value;
		
		if (pack.Target == EnumSelf.TargetType.Opponent) {
			EnemyUpdateHp(-damage);
		} else if (pack.Target == EnumSelf.TargetType.Self) {
			PlayerUpdateHp(-damage);
		}
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
			PlayerUpdatePower((EnumSelf.PowerType)i);
			EnemyUpdatePower((EnumSelf.PowerType)i);
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
			enemy.AddNowShield(shield);
		} else if (pack.Target == EnumSelf.TargetType.Self) {
			if (pack.Effect == EnumSelf.EffectType.ShieldDouble) {
				shield = player.GetNowShield();
			}
			player.AddNowShield(shield);
		}
	}
	
	// こっちは、数値を単純に加算する時使用。アーティファクト効果とか。
	public static void PlayerCalcShield(int val) {
		PlayerStatus player = MapDataCarrier.Instance.CuPlayerStatus;
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
	
	public static void PlayerCurse(ActionPack pack) {
		PlayerStatus player = MapDataCarrier.Instance.CuPlayerStatus;
		EnemyStatus enemy = MapDataCarrier.Instance.CuEnemyStatus;

		if (pack.Target == EnumSelf.TargetType.Self) {
			int actionId = pack.Value;
			MasterAction2Table.Data data = MasterAction2Table.Instance.GetData(actionId);
			List<MasterAction2Table.Data> list = player.GetActionDataCloseList();
			List<int> notCurseIndexs = new List<int>();
			for (int i = 0; i < list.Count; i++) {
				if (IsCurse(list[i].Id) == true) {
					continue;
				}
				notCurseIndexs.Add(i);
			}

			if (notCurseIndexs.Count == 0) {
				// 全部呪い状態だったら、上書きしない
			} else {
				int index = UnityEngine.Random.Range(0, notCurseIndexs.Count);
				player.SetCurseActionData(notCurseIndexs[index], data);
			}
		} else {
			LogManager.Instance.LogError("PlayerCurse:Target:Opponent は、サポートしていない");
		}
	}
	
	public static void PlayerUpdatePower(ActionPack pack) {
		PlayerStatus player = MapDataCarrier.Instance.CuPlayerStatus;
		EnemyStatus enemy = MapDataCarrier.Instance.CuEnemyStatus;

		int val = pack.Value;
		EnumSelf.PowerType pType = ConvertEffectType2PowerType(pack.Effect);
		if (pack.Target == EnumSelf.TargetType.Opponent) {
			enemy.AddPower(pType, val);
			int nowVal = enemy.GetPower().GetValue(pType);
			MapDataCarrier.Instance.EnemyPowerObjects[(int)pType].GetComponent<PowerController>().SetValue(nowVal);
		} else if (pack.Target == EnumSelf.TargetType.Self) {
			player.AddPower(pType, val);
			int nowVal = player.GetPower().GetValue(pType);
			MapDataCarrier.Instance.PowerObjects[(int)pType].GetComponent<PowerController>().SetValue(nowVal);
		}
	}
	
	// こっちは、数値が他のタイミングで変動した際の更新
	public static void PlayerUpdatePower(EnumSelf.PowerType type) {
		PlayerStatus player = MapDataCarrier.Instance.CuPlayerStatus;
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
				enemy.AddPower((EnumSelf.PowerType)i, 10);
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
				player.AddPower((EnumSelf.PowerType)i, 10);
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
			PlayerUpdatePower((EnumSelf.PowerType)i);
			EnemyUpdatePower((EnumSelf.PowerType)i);
		}
	}
	
	// Enemy用
	public static void EnemyCalcDamageNormalDamage(ActionPack pack) {
		//var player = MapDataCarrier.Instance.CuPlayerStatus;
		//var enemy = MapDataCarrier.Instance.CuEnemyStatus;

		//int shield = 0;
		//int powerStrength = enemy.GetPower().GetValue(EnumSelf.PowerType.Strength);
		//int overDamage = 0;
		//int damage = 0;

		//if (pack.Effect == EnumSelf.EffectType.ShieldBash) {
		//	damage = enemy.GetNowShield();
		//} else {
		//	damage = pack.Value;
		//}
		//	
		//damage = damage+powerStrength;

		//// 脱力しているかどうか
		//if (enemy.GetTurnPowerValue(EnumSelf.TurnPowerType.Weakness) > 0) {
		//	if (player.GetParameterListFlag(EnumSelf.ParameterType.WeaknessUp) == true) {
		//		// 与ダメが40％下がる
		//		damage = damage - (damage * 40 / 100);
		//	} else {
		//		// 与ダメが25％下がる
		//		damage = damage - (damage * 25 / 100);
		//	}
		//}
		//

		//if (pack.Target == EnumSelf.TargetType.Opponent) {
		//	// 相手がリアクティブシールド状態か
		//	if (player.GetTurnPowerValue(EnumSelf.TurnPowerType.ReactiveShield) > 0) {
		//		enemy.AddNowShield(enemy.GetTurnPowerValue(EnumSelf.TurnPowerType.ReactiveShield));
		//	}

		//	// 狂戦士状態か
		//	if (enemy.GetTurnPowerValue(EnumSelf.TurnPowerType.Versak) > 0) {
		//		// 与ダメが倍になる
		//		damage *= 2;
		//		EnemyUpdateTurnPower(EnumSelf.TurnPowerType.Versak, -1);
		//	}

		//	// 弱体しているかどうか
		//	if (player.GetTurnPowerValue(EnumSelf.TurnPowerType.Vulnerable) > 0) {
		//		// 与ダメが50％上がる
		//		damage = damage + (damage * 50 / 100);
		//	}
		//	
		//	// 朽ちた体状態だったら、その数値分ダメージを加算して、数値を1増やす
		//	if (player.GetTurnPowerValue(EnumSelf.TurnPowerType.RotBody) > 0) {
		//		int rotbodyVal = player.GetTurnPowerValue(EnumSelf.TurnPowerType.RotBody);
		//		damage += rotbodyVal;
		//		PlayerUpdateTurnPower(EnumSelf.TurnPowerType.RotBody, 1);
		//	}

		//	shield = player.GetNowShield();
		//	player.AddNowShield(-damage);
		//	overDamage = shield - damage;
		//	if (overDamage < 0) {
		//		PlayerUpdateHp(overDamage);
		//		if (pack.Effect == EnumSelf.EffectType.DamageSuction) {
		//			EnemyUpdateHp(-overDamage);
		//		}
		//	}
		//	
		//	// 相手が棘状態か
		//	if (player.GetTurnPowerValue(EnumSelf.TurnPowerType.Thorn) > 0) {
		//		int thornDamage = player.GetTurnPowerValue(EnumSelf.TurnPowerType.Thorn);
		//		shield = enemy.GetNowShield();
		//		enemy.AddNowShield(-thornDamage);
		//		overDamage = shield - thornDamage;
		//		if (overDamage < 0) {
		//			EnemyUpdateHp(overDamage);
		//		}
		//	}
		//} else if (pack.Target == EnumSelf.TargetType.Self) {
		//	LogManager.Instance.LogError("EnemyCalcDamageNormalDamage:Effect:Damage,Target:Self,自分を対象にしたDamageは未実装予定");
		//	//shield = enemy.GetNowShield();
		//	//enemy.AddNowShield(-damage);
		//	//overDamage = shield - damage;
		//	//if (overDamage < 0) {
		//	//	EnemyUpdateHp(overDamage);
		//	//}
		//}
		var player = MapDataCarrier.Instance.CuPlayerStatus;
		var enemy = MapDataCarrier.Instance.CuEnemyStatus;

		int shield = 0;
		int overDamage = 0;
		int damage = CalcEnemyDamageValue(pack);
		
		// 空元気解除
		enemy.ResetPower(EnumSelf.PowerType.FastStrength);
		EnemyUpdatePower(EnumSelf.PowerType.FastStrength);

		if (pack.Target == EnumSelf.TargetType.Opponent) {
			// 相手がリアクティブシールド状態か
			if (player.GetTurnPowerValue(EnumSelf.TurnPowerType.ReactiveShield) > 0) {
				enemy.AddNowShield(enemy.GetTurnPowerValue(EnumSelf.TurnPowerType.ReactiveShield));
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
		} else if (pack.Target == EnumSelf.TargetType.Self) {
			LogManager.Instance.LogError("EnemyCalcDamageNormalDamage:Effect:Damage,Target:Self,自分を対象にしたDamageは未実装予定");
		}
	}
	
	public static void EnemyCalcTrueDamage(ActionPack pack) {
		int damage = pack.Value;
		
		if (pack.Target == EnumSelf.TargetType.Opponent) {
			PlayerUpdateHp(-damage);
		} else if (pack.Target == EnumSelf.TargetType.Self) {
			EnemyUpdateHp(-damage);
		}
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
			PlayerUpdatePower((EnumSelf.PowerType)i);
			EnemyUpdatePower((EnumSelf.PowerType)i);
		}
		
		for (int i = 0; i < (int)EnumSelf.TurnPowerType.Max; i++) {
			PlayerUpdateDisplayTurnPower((EnumSelf.TurnPowerType)i);
			EnemyUpdateDisplayTurnPower((EnumSelf.TurnPowerType)i);
		}
	}
	
	public static void EnemyCalcShieldDamage(ActionPack pack) {
		PlayerStatus player = MapDataCarrier.Instance.CuPlayerStatus;
		EnemyStatus enemy = MapDataCarrier.Instance.CuEnemyStatus;
		int powerToughness = enemy.GetPower().GetValue(EnumSelf.PowerType.Toughness);
		int shield = pack.Value+powerToughness;
		

		if (pack.Target == EnumSelf.TargetType.Opponent) {
			if (pack.Effect == EnumSelf.EffectType.ShieldDouble) {
				shield = player.GetNowShield();
			} else {
				// シールド低下しているかどうか
				if (player.GetTurnPowerValue(EnumSelf.TurnPowerType.ShieldWeakness) > 0) {
					// シールドが25％下がる
					shield = shield - (shield * 25 / 100);
				}
			}
			player.AddNowShield(shield);
		} else if (pack.Target == EnumSelf.TargetType.Self) {
			if (pack.Effect == EnumSelf.EffectType.ShieldDouble) {
				shield = enemy.GetNowShield();
			} else {
				// シールド低下しているかどうか
				if (enemy.GetTurnPowerValue(EnumSelf.TurnPowerType.ShieldWeakness) > 0) {
					// シールドが25％下がる
					shield = shield - (shield * 25 / 100);
				}
			}
			enemy.AddNowShield(shield);
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
			MapDataCarrier.Instance.CuPlayerStatus.AddNowShield(shield);
		} else if (pack.Target == EnumSelf.TargetType.Self) {
			MapDataCarrier.Instance.CuEnemyStatus.AddNowShield(shield);
		}
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
	
	public static void EnemyCurse(ActionPack pack) {
		PlayerStatus player = MapDataCarrier.Instance.CuPlayerStatus;
		EnemyStatus enemy = MapDataCarrier.Instance.CuEnemyStatus;

		if (pack.Target == EnumSelf.TargetType.Opponent) {
			int actionId = pack.Value;
			MasterAction2Table.Data data = MasterAction2Table.Instance.GetData(actionId);
			List<MasterAction2Table.Data> list = player.GetActionDataCloseList();
			List<int> notCurseIndexs = new List<int>();
			for (int i = 0; i < list.Count; i++) {
				if (IsCurse(list[i].Id) == true) {
					continue;
				}
				notCurseIndexs.Add(i);
			}

			if (notCurseIndexs.Count == 0) {
				// 全部呪い状態だったら、上書きしない
			} else {
				int index = UnityEngine.Random.Range(0, notCurseIndexs.Count);
				player.SetCurseActionData(notCurseIndexs[index], data);
			}
		} else {
			LogManager.Instance.LogError("EnemyCurse:Target:Self は、サポートしていない");
		}
	}
	
	// こっちは、アクションパックによる付与
	public static void EnemyUpdatePower(ActionPack pack) {
		PlayerStatus player = MapDataCarrier.Instance.CuPlayerStatus;
		EnemyStatus enemy = MapDataCarrier.Instance.CuEnemyStatus;

		int val = pack.Value;
		EnumSelf.PowerType pType = ConvertEffectType2PowerType(pack.Effect);
		if (pack.Target == EnumSelf.TargetType.Opponent) {

			player.AddPower(pType, val);
			int nowVal = player.GetPower().GetValue(pType);
			MapDataCarrier.Instance.PowerObjects[(int)pType].GetComponent<PowerController>().SetValue(nowVal);
		} else if (pack.Target == EnumSelf.TargetType.Self) {
			enemy.AddPower(pType, val);
			int nowVal = enemy.GetPower().GetValue(pType);
			MapDataCarrier.Instance.EnemyPowerObjects[(int)pType].GetComponent<PowerController>().SetValue(nowVal);
		}
	}
	
	// こっちは、数値が他のタイミングで変動した際の更新
	public static void EnemyUpdatePower(EnumSelf.PowerType type) {
		EnemyStatus enemy = MapDataCarrier.Instance.CuEnemyStatus;
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
		} else if (type == EnumSelf.EffectType.Invincible) {
			pType = EnumSelf.TurnPowerType.Invincible;
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
					MasterEnemyAITable.Data data = MasterEnemyAITable.Instance.GetData(91);
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
			if (pack.Effect == EnumSelf.EffectType.Damage) {
				if (pack.Target == EnumSelf.TargetType.Opponent) {
					findOpponentDamage = true;
				}
			} else if (pack.Effect == EnumSelf.EffectType.Shield) {
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
			if (pack.Effect == EnumSelf.EffectType.Damage) {
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
				
		// シールドバッシュかどうか
		if (pack.Effect == EnumSelf.EffectType.ShieldBash) {
			val = player.GetNowShield();
		} else {
			if (player.GetParameterListFlag(EnumSelf.ParameterType.ApprenticeKnight) == true) {
				MasterAction2Table.Data data = MasterAction2Table.Instance.GetData(pack.ExecuteActionId);
				if (IsOnlyDamageAndShield(data) == true) {
					val = pack.Value + 3;
				} else {
					val = pack.Value;
				}
			} else if (player.GetParameterListFlag(EnumSelf.ParameterType.KnightMaster) == true) {
				if (IsOnlyDamageAndShieldAll() == true) {
					val = pack.Value * 2;
				} else {
					val = pack.Value;
				}
			} else {
				val = pack.Value;
			}
		}

		// 筋力があるかどうか
		int strength = player.GetPower().GetValue(EnumSelf.PowerType.Strength);
		val += strength;
		
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

		if (player.GetParameterListFlag(EnumSelf.ParameterType.ApprenticeKnight) == true) {
			MasterAction2Table.Data data = MasterAction2Table.Instance.GetData(pack.ExecuteActionId);
			if (IsOnlyDamageAndShield(data) == true) {
				val = pack.Value + 3;
			} else {
				val = pack.Value;
			}
		} else if (player.GetParameterListFlag(EnumSelf.ParameterType.KnightMaster) == true) {
			if (IsOnlyDamageAndShieldAll() == true) {
				val = pack.Value * 2;
			} else {
				val = pack.Value;
			}
		} else {
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
		val += strength;

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
		int val = pack.Value+powerToughness;
		if (enemy.GetTurnPowerValue(EnumSelf.TurnPowerType.ShieldWeakness) > 0) {
			val = val - (val * 25 / 100);
		}
				
		return val;
	}
}
