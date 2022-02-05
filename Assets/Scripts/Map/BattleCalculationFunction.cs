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
		} else if (pack.Effect == EnumSelf.EffectType.ReverseHeal) {
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
			(pack.Effect == EnumSelf.EffectType.Regenerate)
		) {
			BattleCalculationFunction.EnemyUpdatePower(pack);
		} else if (
			(pack.Effect == EnumSelf.EffectType.DiceMinusOne) ||
			(pack.Effect == EnumSelf.EffectType.ReverseHeal)
		) {
			BattleCalculationFunction.EnemyUpdateTurnPower(pack);
		}
	}
	
	public static void PlayerTurnEndValueChange() {
		var status = MapDataCarrier.Instance.CuPlayerStatus;
		for (int i = 0; i < (int)EnumSelf.TurnPowerType.Max; i++) {
			status.AddTurnPower((EnumSelf.TurnPowerType)i, -1);
		}
	}
	
	public static void EnemyTurnStartValueChange() {
		// 再生などのバフをチェック
		EnemyStatus status = MapDataCarrier.Instance.CuEnemyStatus;
		Power power = status.GetPower();
		int val = power.GetParameter(EnumSelf.PowerType.Regenerate);
		if (val > 0) {
			bool isReverse = false;
			if (MapDataCarrier.Instance.CuEnemyStatus.GetTurnPowerCount(EnumSelf.TurnPowerType.ReverseHeal) > 0) {
				isReverse = true;
			}

			if (isReverse == true) {
				status.AddNowHp(-val);
			} else {
				status.AddNowHp(val);
			}
		}
	}

	public static void EnemyTurnEndValueChange() {
		var status = MapDataCarrier.Instance.CuEnemyStatus;
		for (int i = 0; i < (int)EnumSelf.TurnPowerType.Max; i++) {
			status.AddTurnPower((EnumSelf.TurnPowerType)i, -1);
		}
	}

	// Player用
	public static void PlayerCalcDamageNormalDamage(ActionPack pack) {
		int shield = 0;
		int overDamage = 0;
		if (pack.Target == EnumSelf.TargetType.Opponent) {
			shield = MapDataCarrier.Instance.CuEnemyStatus.GetNowShield();
			MapDataCarrier.Instance.CuEnemyStatus.AddNowShield(-pack.Value);
			overDamage = shield - pack.Value;
			if (overDamage < 0) {
				MapDataCarrier.Instance.CuEnemyStatus.AddNowHp(overDamage);
			}
		} else if (pack.Target == EnumSelf.TargetType.Self) {
			shield = MapDataCarrier.Instance.CuPlayerStatus.GetNowShield();
			MapDataCarrier.Instance.CuPlayerStatus.AddNowShield(-pack.Value);
			overDamage = shield - pack.Value;
			if (overDamage < 0) {
				MapDataCarrier.Instance.CuPlayerStatus.AddNowHp(overDamage);
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
			if (MapDataCarrier.Instance.CuEnemyStatus.GetTurnPowerCount(EnumSelf.TurnPowerType.ReverseHeal) > 0) {
				isReverse = true;
			}

			if (isReverse == true) {
				MapDataCarrier.Instance.CuEnemyStatus.AddNowHp(-heal);
			} else {
				MapDataCarrier.Instance.CuEnemyStatus.AddNowHp(heal);
			}
		} else if (pack.Target == EnumSelf.TargetType.Self) {
			bool isReverse = false;
			if (MapDataCarrier.Instance.CuPlayerStatus.GetTurnPowerCount(EnumSelf.TurnPowerType.ReverseHeal) > 0) {
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
	
	public static void PlayerUpdateTurnPower(ActionPack pack) {
		int val = pack.Value;
		EnumSelf.TurnPowerType pType = ConvertEffectType2TurnPowerType(pack.Effect);
		if (pack.Target == EnumSelf.TargetType.Opponent) {
			MapDataCarrier.Instance.CuEnemyStatus.AddTurnPower(pType, val);
		} else if (pack.Target == EnumSelf.TargetType.Self) {
			MapDataCarrier.Instance.CuPlayerStatus.AddTurnPower(pType, val);
		}
	}
	
	// Enemy用
	public static void EnemyCalcDamageNormalDamage(ActionPack pack) {
		int shield = 0;
		int powerStrength = MapDataCarrier.Instance.CuEnemyStatus.GetPower().GetParameter(EnumSelf.PowerType.Strength);
		int overDamage = 0;
			int damage = pack.Value+powerStrength;
		if (pack.Target == EnumSelf.TargetType.Opponent) {
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
			if (MapDataCarrier.Instance.CuPlayerStatus.GetTurnPowerCount(EnumSelf.TurnPowerType.ReverseHeal) > 0) {
				isReverse = true;
			}

			if (isReverse == true) {
				MapDataCarrier.Instance.CuPlayerStatus.AddNowHp(-heal);
			} else {
				MapDataCarrier.Instance.CuPlayerStatus.AddNowHp(heal);
			}
		} else if (pack.Target == EnumSelf.TargetType.Self) {
			bool isReverse = false;
			if (MapDataCarrier.Instance.CuEnemyStatus.GetTurnPowerCount(EnumSelf.TurnPowerType.ReverseHeal) > 0) {
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
		} else if (pack.Target == EnumSelf.TargetType.Self) {
			MapDataCarrier.Instance.CuEnemyStatus.AddPower(pType, val);
		}
	}

	
	public static void EnemyUpdateTurnPower(ActionPack pack) {
		int val = pack.Value;
		EnumSelf.TurnPowerType pType = ConvertEffectType2TurnPowerType(pack.Effect);
		if (pack.Target == EnumSelf.TargetType.Opponent) {
			MapDataCarrier.Instance.CuPlayerStatus.AddTurnPower(pType, val);
		} else if (pack.Target == EnumSelf.TargetType.Self) {
			MapDataCarrier.Instance.CuEnemyStatus.AddTurnPower(pType, val);
		}
	}

	// 共通
	public static EnumSelf.TurnPowerType ConvertEffectType2TurnPowerType(EnumSelf.EffectType type) {
		EnumSelf.TurnPowerType pType = EnumSelf.TurnPowerType.None;
		if (type == EnumSelf.EffectType.DiceMinusOne) {
			pType = EnumSelf.TurnPowerType.DiceMinusOne;
		} else if (type == EnumSelf.EffectType.ReverseHeal) {
			pType = EnumSelf.TurnPowerType.ReverseHeal;
		}

		return pType;
	}
	
	public static EnumSelf.PowerType ConvertEffectType2PowerType(EnumSelf.EffectType type) {
		EnumSelf.PowerType pType = EnumSelf.PowerType.None;
		if (type == EnumSelf.EffectType.Strength) {
			pType = EnumSelf.PowerType.Strength;
		} else if (type == EnumSelf.EffectType.Regenerate) {
			pType = EnumSelf.PowerType.Regenerate;
		}

		return pType;
	}
}
