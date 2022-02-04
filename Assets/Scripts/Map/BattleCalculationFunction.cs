using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCalculationFunction {
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
	
	public static void PlayerCalcHeal(ActionPack pack) {
		int heal = pack.Value;
		if (pack.Target == EnumSelf.TargetType.Opponent) {
			MapDataCarrier.Instance.CuEnemyStatus.AddNowHp(heal);
		} else if (pack.Target == EnumSelf.TargetType.Self) {
			MapDataCarrier.Instance.CuPlayerStatus.AddNowHp(heal);
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
	
	public static void EnemyCalcHeal(ActionPack pack) {
		int heal = pack.Value;
		if (pack.Target == EnumSelf.TargetType.Opponent) {
			MapDataCarrier.Instance.CuPlayerStatus.AddNowHp(heal);
		} else if (pack.Target == EnumSelf.TargetType.Self) {
			MapDataCarrier.Instance.CuEnemyStatus.AddNowHp(heal);
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
