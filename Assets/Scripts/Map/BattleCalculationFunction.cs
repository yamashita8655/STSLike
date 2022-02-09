using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCalculationFunction {
	public static void PlayerValueChange(ActionPack pack) {
		if (pack.Effect == EnumSelf.EffectType.Damage) {
			BattleCalculationFunction.PlayerCalcDamageNormalDamage(pack);
		} else if (pack.Effect == EnumSelf.EffectType.Heal) {
			BattleCalculationFunction.PlayerCalcHeal(pack);
		} else if (pack.Effect == EnumSelf.EffectType.Shield) {
			BattleCalculationFunction.PlayerCalcShield(pack);
		} else if (pack.Effect == EnumSelf.EffectType.ShieldDamage) {
			BattleCalculationFunction.PlayerCalcShieldDamage(pack);
		} else if (
			(pack.Effect == EnumSelf.EffectType.Strength) ||
			(pack.Effect == EnumSelf.EffectType.Poison) ||
			(pack.Effect == EnumSelf.EffectType.Regenerate)
		) {
			BattleCalculationFunction.PlayerUpdatePower(pack);
		} else if (
			(pack.Effect == EnumSelf.EffectType.ReverseHeal) ||
			(pack.Effect == EnumSelf.EffectType.Vulnerable) ||
			(pack.Effect == EnumSelf.EffectType.Weakness)
		) {
			BattleCalculationFunction.PlayerUpdateTurnPower(pack);
		}
	}

	public static void EnemyValueChange(ActionPack pack) {
		if (pack.Effect == EnumSelf.EffectType.Damage) {
			BattleCalculationFunction.EnemyCalcDamageNormalDamage(pack);
		} else if (pack.Effect == EnumSelf.EffectType.Heal) {
			BattleCalculationFunction.EnemyCalcHeal(pack);
		} else if (pack.Effect == EnumSelf.EffectType.Shield) {
			BattleCalculationFunction.EnemyCalcShield(pack);
		} else if (pack.Effect == EnumSelf.EffectType.ShieldDamage) {
			BattleCalculationFunction.EnemyCalcShieldDamage(pack);
		} else if (
			(pack.Effect == EnumSelf.EffectType.Strength) ||
			(pack.Effect == EnumSelf.EffectType.Poison) ||
			(pack.Effect == EnumSelf.EffectType.Regenerate)
		) {
			BattleCalculationFunction.EnemyUpdatePower(pack);
		} else if (
			(pack.Effect == EnumSelf.EffectType.DiceMinusOne) ||
			(pack.Effect == EnumSelf.EffectType.Weakness) ||
			(pack.Effect == EnumSelf.EffectType.Vulnerable) ||
			(pack.Effect == EnumSelf.EffectType.Patient) ||
			(pack.Effect == EnumSelf.EffectType.ReverseHeal)
		) {
			BattleCalculationFunction.EnemyUpdateTurnPower(pack);
		}
	}
	
	public static void PlayerTurnStartValueChange() {
		// 再生などのバフをチェック
		PlayerStatus status = MapDataCarrier.Instance.CuPlayerStatus;
		Power power = status.GetPower();

		// 再生
		int val = power.GetValue(EnumSelf.PowerType.Regenerate);
		if (val > 0) {
			bool isReverse = false;
			if (MapDataCarrier.Instance.CuPlayerStatus.GetTurnPowerValue(EnumSelf.TurnPowerType.ReverseHeal) > 0) {
				isReverse = true;
			}

			if (isReverse == true) {
				status.AddNowHp(-val);
			} else {
				status.AddNowHp(val);
			}
		}
		
		// 毒
		val = power.GetValue(EnumSelf.PowerType.Poison);
		if (val > 0) {
			status.AddNowHp(-val);
		}
	}
	
	public static void PlayerTurnEndValueChange() {
		var status = MapDataCarrier.Instance.CuPlayerStatus;
		for (int i = 0; i < (int)EnumSelf.TurnPowerType.Max; i++) {
			if (i == (int)EnumSelf.TurnPowerType.Patient) {
				continue;
			}
			status.AddTurnPower((EnumSelf.TurnPowerType)i, -1);
			int turn = status.GetTurnPowerValue((EnumSelf.TurnPowerType)i);
			MapDataCarrier.Instance.TurnPowerObjects[(int)i].GetComponent<TurnPowerController>().SetTurn(turn);
		}
	}
	
	public static void EnemyTurnStartValueChange() {
		// 再生などのバフをチェック
		EnemyStatus status = MapDataCarrier.Instance.CuEnemyStatus;
		Power power = status.GetPower();

		// 再生
		int val = power.GetValue(EnumSelf.PowerType.Regenerate);
		if (val > 0) {
			bool isReverse = false;
			if (MapDataCarrier.Instance.CuEnemyStatus.GetTurnPowerValue(EnumSelf.TurnPowerType.ReverseHeal) > 0) {
				isReverse = true;
			}

			if (isReverse == true) {
				status.AddNowHp(-val);
			} else {
				status.AddNowHp(val);
			}
		}
		
		// 毒
		val = power.GetValue(EnumSelf.PowerType.Poison);
		if (val > 0) {
			status.AddNowHp(-val);

			// TODO このダメージ時の判定処理、2毒とDamage処理時で2重化してるから、どっかで統一の設計をする
			// 敵の状態異常Patiantの数値があれば、その数値も減らす
			if (status.GetTurnPowerValue(EnumSelf.TurnPowerType.Patient) > 0) {
				status.AddTurnPower(EnumSelf.TurnPowerType.Patient, val);
				EnemyUpdateTurnPower(EnumSelf.TurnPowerType.Patient);
				// もし、0以下になったら、Patiant解除
				if (status.GetTurnPowerValue(EnumSelf.TurnPowerType.Patient) <= 0) {
					// Patientは今後ユニークで増えていくので、それに該当するIDを決め打ちで指定する
					MasterEnemyAITable.Data data = MasterEnemyAITable.Instance.GetData(91);
					status.UpdateAIData(data);
					// 敵の行動開始前なので、行動を抽選しなおす
					status.LotActionData();
				}
			}
		}
	}

	public static void EnemyTurnEndValueChange() {
		var status = MapDataCarrier.Instance.CuEnemyStatus;
		for (int i = 0; i < (int)EnumSelf.TurnPowerType.Max; i++) {
			if (i == (int)EnumSelf.TurnPowerType.Patient) {
				continue;
			}
			status.AddTurnPower((EnumSelf.TurnPowerType)i, -1);
			int turn = status.GetTurnPowerValue((EnumSelf.TurnPowerType)i);
			MapDataCarrier.Instance.EnemyTurnPowerObjects[(int)i].GetComponent<TurnPowerController>().SetTurn(turn);
		}
	}

	// Player用
	public static void PlayerCalcDamageNormalDamage(ActionPack pack) {
		var player = MapDataCarrier.Instance.CuPlayerStatus;
		var enemy = MapDataCarrier.Instance.CuEnemyStatus;
		int shield = 0;
		int powerStrength = player.GetPower().GetValue(EnumSelf.PowerType.Strength);
		int overDamage = 0;
		int damage = pack.Value+powerStrength;
		
		// 脱力しているかどうか
		if (player.GetTurnPowerValue(EnumSelf.TurnPowerType.Weakness) > 0) {
			// 与ダメが25％下がる
			damage = (int)((float)damage * 0.75f);
		}

		if (pack.Target == EnumSelf.TargetType.Opponent) {
			// 相手弱体しているか
			if (enemy.GetTurnPowerValue(EnumSelf.TurnPowerType.Vulnerable) > 0) {
				// 与ダメが50％上がる
				damage = (int)((float)damage * 1.5f);
			}

			shield = enemy.GetNowShield();
			enemy.AddNowShield(-damage);
			overDamage = shield - damage;
			if (overDamage < 0) {
				enemy.AddNowHp(overDamage);
			}

			// 敵の状態異常Patiantの数値があれば、その数値も減らす
			if (enemy.GetTurnPowerValue(EnumSelf.TurnPowerType.Patient) > 0) {
				enemy.AddTurnPower(EnumSelf.TurnPowerType.Patient, overDamage);
				EnemyUpdateTurnPower(EnumSelf.TurnPowerType.Patient);
				// もし、0以下になったら、Patiant解除
				if (enemy.GetTurnPowerValue(EnumSelf.TurnPowerType.Patient) <= 0) {
					// Patientは今後ユニークで増えていくので、それに該当するIDを決め打ちで指定する
					MasterEnemyAITable.Data data = MasterEnemyAITable.Instance.GetData(91);
					enemy.UpdateAIData(data);
					// 敵の行動開始前なので、行動を抽選しなおす
					enemy.LotActionData();
				}
			}

		} else if (pack.Target == EnumSelf.TargetType.Self) {
			shield = player.GetNowShield();
			player.AddNowShield(-damage);
			overDamage = shield - damage;
			if (overDamage < 0) {
				player.AddNowHp(overDamage);
			}
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
				MapDataCarrier.Instance.CuEnemyStatus.AddNowHp(-heal);
			} else {
				MapDataCarrier.Instance.CuEnemyStatus.AddNowHp(heal);
			}
		} else if (pack.Target == EnumSelf.TargetType.Self) {
			bool isReverse = false;
			if (MapDataCarrier.Instance.CuPlayerStatus.GetTurnPowerValue(EnumSelf.TurnPowerType.ReverseHeal) > 0) {
				isReverse = true;
			}

			if (isReverse == true) {
				MapDataCarrier.Instance.CuPlayerStatus.AddNowHp(-heal);
			} else {
				MapDataCarrier.Instance.CuPlayerStatus.AddNowHp(heal);
			}
		}
	}
	
	public static void PlayerCalcShield(ActionPack pack) {
		int shield = pack.Value;
		if (pack.Target == EnumSelf.TargetType.Opponent) {
			MapDataCarrier.Instance.CuEnemyStatus.AddNowShield(shield);
		} else if (pack.Target == EnumSelf.TargetType.Self) {
			MapDataCarrier.Instance.CuPlayerStatus.AddNowShield(shield);
		}
	}
	
	public static void PlayerUpdatePower(ActionPack pack) {
		int val = pack.Value;
		EnumSelf.PowerType pType = ConvertEffectType2PowerType(pack.Effect);
		if (pack.Target == EnumSelf.TargetType.Opponent) {
			MapDataCarrier.Instance.CuEnemyStatus.AddPower(pType, val);
			MapDataCarrier.Instance.EnemyPowerObjects[(int)pType].GetComponent<PowerController>().SetValue(val);
		} else if (pack.Target == EnumSelf.TargetType.Self) {
			MapDataCarrier.Instance.CuPlayerStatus.AddPower(pType, val);
			MapDataCarrier.Instance.PowerObjects[(int)pType].GetComponent<PowerController>().SetValue(val);
		}
	}
	
	public static void PlayerUpdateTurnPower(ActionPack pack) {
		int val = pack.Value;
		EnumSelf.TurnPowerType pType = ConvertEffectType2TurnPowerType(pack.Effect);
		if (pack.Target == EnumSelf.TargetType.Opponent) {
			MapDataCarrier.Instance.CuEnemyStatus.AddTurnPower(pType, val);
			int turn = MapDataCarrier.Instance.CuEnemyStatus.GetTurnPowerValue(pType);
			MapDataCarrier.Instance.EnemyTurnPowerObjects[(int)pType].GetComponent<TurnPowerController>().SetTurn(turn);
		} else if (pack.Target == EnumSelf.TargetType.Self) {
			MapDataCarrier.Instance.CuPlayerStatus.AddTurnPower(pType, val);
			int turn = MapDataCarrier.Instance.CuPlayerStatus.GetTurnPowerValue(pType);
			MapDataCarrier.Instance.TurnPowerObjects[(int)pType].GetComponent<TurnPowerController>().SetTurn(turn);
		}
	}
	
	public static void PlayerUpdateTurnPower(EnumSelf.TurnPowerType type) {
		int turn = MapDataCarrier.Instance.CuPlayerStatus.GetTurnPowerValue(type);
		MapDataCarrier.Instance.TurnPowerObjects[(int)type].GetComponent<TurnPowerController>().SetTurn(turn);
	}
	
	// Enemy用
	public static void EnemyCalcDamageNormalDamage(ActionPack pack) {
		int shield = 0;
		int powerStrength = MapDataCarrier.Instance.CuEnemyStatus.GetPower().GetValue(EnumSelf.PowerType.Strength);
		int overDamage = 0;
		int damage = pack.Value+powerStrength;

		// 脱力しているかどうか
		if (MapDataCarrier.Instance.CuEnemyStatus.GetTurnPowerValue(EnumSelf.TurnPowerType.Weakness) > 0) {
			// 与ダメが25％下がる
			damage = (int)((float)damage * 0.75f);
		}
		

		if (pack.Target == EnumSelf.TargetType.Opponent) {
			// 弱体しているかどうか
			if (MapDataCarrier.Instance.CuPlayerStatus.GetTurnPowerValue(EnumSelf.TurnPowerType.Vulnerable) > 0) {
				// 与ダメが50％上がる
				damage = (int)((float)damage * 1.5f);
			}

			shield = MapDataCarrier.Instance.CuPlayerStatus.GetNowShield();
			MapDataCarrier.Instance.CuPlayerStatus.AddNowShield(-damage);
			overDamage = shield - damage;
			if (overDamage < 0) {
				MapDataCarrier.Instance.CuPlayerStatus.AddNowHp(overDamage);
			}
		} else if (pack.Target == EnumSelf.TargetType.Self) {
			shield = MapDataCarrier.Instance.CuEnemyStatus.GetNowShield();
			MapDataCarrier.Instance.CuEnemyStatus.AddNowShield(-damage);
			overDamage = shield - damage;
			if (overDamage < 0) {
				MapDataCarrier.Instance.CuEnemyStatus.AddNowHp(overDamage);
			}
		}
	}
	
	public static void EnemyCalcShieldDamage(ActionPack pack) {
		if (pack.Target == EnumSelf.TargetType.Opponent) {
			MapDataCarrier.Instance.CuPlayerStatus.AddNowShield(-pack.Value);
		} else if (pack.Target == EnumSelf.TargetType.Self) {
			MapDataCarrier.Instance.CuEnemyStatus.AddNowShield(-pack.Value);
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
				MapDataCarrier.Instance.CuPlayerStatus.AddNowHp(-heal);
			} else {
				MapDataCarrier.Instance.CuPlayerStatus.AddNowHp(heal);
			}
		} else if (pack.Target == EnumSelf.TargetType.Self) {
			bool isReverse = false;
			if (MapDataCarrier.Instance.CuEnemyStatus.GetTurnPowerValue(EnumSelf.TurnPowerType.ReverseHeal) > 0) {
				isReverse = true;
			}

			if (isReverse == true) {
				MapDataCarrier.Instance.CuEnemyStatus.AddNowHp(-heal);
			} else {
				MapDataCarrier.Instance.CuEnemyStatus.AddNowHp(heal);
			}
		}
	}
	
	public static void EnemyCalcShield(ActionPack pack) {
		int shield = pack.Value;
		if (pack.Target == EnumSelf.TargetType.Opponent) {
			MapDataCarrier.Instance.CuPlayerStatus.AddNowShield(shield);
		} else if (pack.Target == EnumSelf.TargetType.Self) {
			MapDataCarrier.Instance.CuEnemyStatus.AddNowShield(shield);
		}
	}
	
	public static void EnemyUpdatePower(ActionPack pack) {
		int val = pack.Value;
		EnumSelf.PowerType pType = ConvertEffectType2PowerType(pack.Effect);
		if (pack.Target == EnumSelf.TargetType.Opponent) {
			MapDataCarrier.Instance.CuPlayerStatus.AddPower(pType, val);
			MapDataCarrier.Instance.PowerObjects[(int)pType].GetComponent<PowerController>().SetValue(val);
		} else if (pack.Target == EnumSelf.TargetType.Self) {
			MapDataCarrier.Instance.CuEnemyStatus.AddPower(pType, val);
			MapDataCarrier.Instance.EnemyPowerObjects[(int)pType].GetComponent<PowerController>().SetValue(val);
		}
	}

	// こっちは、アクションパックによる付与
	public static void EnemyUpdateTurnPower(ActionPack pack) {
		int val = pack.Value;
		EnumSelf.TurnPowerType pType = ConvertEffectType2TurnPowerType(pack.Effect);
		if (pack.Target == EnumSelf.TargetType.Opponent) {
			MapDataCarrier.Instance.CuPlayerStatus.AddTurnPower(pType, val);
			int turn = MapDataCarrier.Instance.CuPlayerStatus.GetTurnPowerValue(pType);
			MapDataCarrier.Instance.TurnPowerObjects[(int)pType].GetComponent<TurnPowerController>().SetTurn(turn);
		} else if (pack.Target == EnumSelf.TargetType.Self) {
			MapDataCarrier.Instance.CuEnemyStatus.AddTurnPower(pType, val);
			int turn = MapDataCarrier.Instance.CuEnemyStatus.GetTurnPowerValue(pType);
			MapDataCarrier.Instance.EnemyTurnPowerObjects[(int)pType].GetComponent<TurnPowerController>().SetTurn(turn);
		}
	}
	
	// こっちは、数値が他のタイミングで変動した際の更新
	public static void EnemyUpdateTurnPower(EnumSelf.TurnPowerType type) {
		int turn = MapDataCarrier.Instance.CuEnemyStatus.GetTurnPowerValue(type);
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
		} else if (type == EnumSelf.EffectType.Vulnerable) {
			pType = EnumSelf.TurnPowerType.Vulnerable;
		} else if (type == EnumSelf.EffectType.Patient) {
			pType = EnumSelf.TurnPowerType.Patient;
		}

		return pType;
	}
	
	public static EnumSelf.PowerType ConvertEffectType2PowerType(EnumSelf.EffectType type) {
		EnumSelf.PowerType pType = EnumSelf.PowerType.None;
		if (type == EnumSelf.EffectType.Strength) {
			pType = EnumSelf.PowerType.Strength;
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
		}

		return type;
	}
}
