using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBattleEnemyValueChangeState : StateBase {

	/// <summary>
	/// 初期化前処理.
	/// </summary>
	override public bool OnBeforeInit()
	{
		var scene = MapDataCarrier.Instance.Scene as MapScene;

		EnemyStatus enemy = MapDataCarrier.Instance.CuEnemyStatus;
		MasterAction2Table.Data data = enemy.GetActionData();

		int count = MapDataCarrier.Instance.EnemyActionPackCount;
		ActionPack pack = data.ActionPackList[count]; 

		BattleCalculationFunction.EnemyValueChange(pack);

		//if (pack.Effect == EnumSelf.EffectType.Damage) {
		//	BattleCalculationFunction.EnemyCalcDamageNormalDamage(pack);
		//} else if (pack.Effect == EnumSelf.EffectType.Heal) {
		//	BattleCalculationFunction.EnemyCalcHeal(pack);
		//} else if (pack.Effect == EnumSelf.EffectType.Shield) {
		//	BattleCalculationFunction.EnemyCalcShield(pack);
		//} else if (
		//	(pack.Effect == EnumSelf.EffectType.Strength) ||
		//	(pack.Effect == EnumSelf.EffectType.Regenerate)
		//) {
		//	BattleCalculationFunction.EnemyUpdatePower(pack);
		//} else if (pack.Effect == EnumSelf.EffectType.DiceMinusOne) {
		//	BattleCalculationFunction.EnemyUpdateTurnPower(pack);
		//}

		MapDataCarrier.Instance.EnemyActionPackCount++;

		scene.UpdateParameterText();

		return true;
	}

	/// <summary>
	/// メイン更新処理.
	/// </summary>
	/// <param name="delta">経過時間</param>
	override public void OnUpdateMain(float delta)
	{
		StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleCheck);
	}

	/// <summary>
	/// ステート解放時処理.
	/// </summary>
	override public void OnRelease()
	{
	}
	
	//private void CalcDamageNormalDamage(ActionPack pack) {
	//	int shield = 0;
	//	int powerStrength = MapDataCarrier.Instance.CuEnemyStatus.GetPower().GetParameter(EnumSelf.PowerType.Strength);
	//	int overDamage = 0;
	//		int damage = pack.Value+powerStrength;
	//	if (pack.Target == EnumSelf.TargetType.Opponent) {
	//		shield = MapDataCarrier.Instance.CuPlayerStatus.GetNowShield();
	//		MapDataCarrier.Instance.CuPlayerStatus.AddNowShield(-damage);
	//		overDamage = shield - damage;
	//		if (overDamage < 0) {
	//			MapDataCarrier.Instance.CuPlayerStatus.AddNowHp(overDamage);
	//		}
	//	} else if (pack.Target == EnumSelf.TargetType.Self) {
	//		shield = MapDataCarrier.Instance.CuEnemyStatus.GetNowShield();
	//		MapDataCarrier.Instance.CuEnemyStatus.AddNowShield(-damage);
	//		overDamage = shield - damage;
	//		if (overDamage < 0) {
	//			MapDataCarrier.Instance.CuEnemyStatus.AddNowHp(overDamage);
	//		}
	//	}
	//}
	//
	//private void CalcHeal(ActionPack pack) {
	//	int heal = pack.Value;
	//	if (pack.Target == EnumSelf.TargetType.Opponent) {
	//		MapDataCarrier.Instance.CuPlayerStatus.AddNowHp(heal);
	//	} else if (pack.Target == EnumSelf.TargetType.Self) {
	//		MapDataCarrier.Instance.CuEnemyStatus.AddNowHp(heal);
	//	}
	//}
	//
	//private void CalcShield(ActionPack pack) {
	//	int shield = pack.Value;
	//	if (pack.Target == EnumSelf.TargetType.Opponent) {
	//		MapDataCarrier.Instance.CuPlayerStatus.AddNowShield(shield);
	//	} else if (pack.Target == EnumSelf.TargetType.Self) {
	//		MapDataCarrier.Instance.CuEnemyStatus.AddNowShield(shield);
	//	}
	//}
	//
	//private void UpdatePower(ActionPack pack) {
	//	int val = pack.Value;
	//	EnumSelf.PowerType pType = ConvertEffectType2PowerType(pack.Effect);
	//	if (pack.Target == EnumSelf.TargetType.Opponent) {
	//		MapDataCarrier.Instance.CuPlayerStatus.AddPower(pType, val);
	//	} else if (pack.Target == EnumSelf.TargetType.Self) {
	//		MapDataCarrier.Instance.CuEnemyStatus.AddPower(pType, val);
	//	}
	//}

	//private EnumSelf.PowerType ConvertEffectType2PowerType(EnumSelf.EffectType type) {
	//	EnumSelf.PowerType pType = EnumSelf.PowerType.None;
	//	if (type == EnumSelf.EffectType.Strength) {
	//		pType = EnumSelf.PowerType.Strength;
	//	} else if (type == EnumSelf.EffectType.Regenerate) {
	//		pType = EnumSelf.PowerType.Regenerate;
	//	}

	//	return pType;
	//}
	//
	//private void UpdateTurnPower(ActionPack pack) {
	//	int val = pack.Value;
	//	EnumSelf.TurnPowerType pType = ConvertEffectType2TurnPowerType(pack.Effect);
	//	if (pack.Target == EnumSelf.TargetType.Opponent) {
	//		MapDataCarrier.Instance.CuPlayerStatus.AddTurnPower(pType, val);
	//	} else if (pack.Target == EnumSelf.TargetType.Self) {
	//		MapDataCarrier.Instance.CuEnemyStatus.AddTurnPower(pType, val);
	//	}
	//}

	//private EnumSelf.TurnPowerType ConvertEffectType2TurnPowerType(EnumSelf.EffectType type) {
	//	EnumSelf.TurnPowerType pType = EnumSelf.TurnPowerType.None;
	//	if (type == EnumSelf.EffectType.DiceMinusOne) {
	//		pType = EnumSelf.TurnPowerType.DiceMinusOne;
	//	}

	//	return pType;
	//}
}
