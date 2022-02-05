using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBattleValueChangeState : StateBase {

	/// <summary>
	/// 初期化前処理.
	/// </summary>
	override public bool OnBeforeInit()
	{
		var scene = MapDataCarrier.Instance.Scene as MapScene;

		int select = MapDataCarrier.Instance.SelectAttackIndex;

		MasterAction2Table.Data data = MapDataCarrier.Instance.CuPlayerStatus.GetActionData(select);

		int count = MapDataCarrier.Instance.ActionPackCount;
		ActionPack pack = data.ActionPackList[count]; 
		
		BattleCalculationFunction.PlayerValueChange(pack);

		//if (pack.Effect == EnumSelf.EffectType.Damage) {
		//	BattleCalculationFunction.PlayerCalcDamageNormalDamage(pack);
		//} else if (pack.Effect == EnumSelf.EffectType.Heal) {
		//	BattleCalculationFunction.PlayerCalcHeal(pack);
		//} else if (pack.Effect == EnumSelf.EffectType.Shield) {
		//	BattleCalculationFunction.PlayerCalcShield(pack);
		//}

		MapDataCarrier.Instance.ActionPackCount++;

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
	//	int overDamage = 0;
	//	if (pack.Target == EnumSelf.TargetType.Opponent) {
	//		shield = MapDataCarrier.Instance.CuEnemyStatus.GetNowShield();
	//		MapDataCarrier.Instance.CuEnemyStatus.AddNowShield(-pack.Value);
	//		overDamage = shield - pack.Value;
	//		if (overDamage < 0) {
	//			MapDataCarrier.Instance.CuEnemyStatus.AddNowHp(overDamage);
	//		}
	//	} else if (pack.Target == EnumSelf.TargetType.Self) {
	//		shield = MapDataCarrier.Instance.CuPlayerStatus.GetNowShield();
	//		MapDataCarrier.Instance.CuPlayerStatus.AddNowShield(-pack.Value);
	//		overDamage = shield - pack.Value;
	//		if (overDamage < 0) {
	//			MapDataCarrier.Instance.CuPlayerStatus.AddNowHp(overDamage);
	//		}
	//	}
	//}
	//
	//private void CalcHeal(ActionPack pack) {
	//	int heal = pack.Value;
	//	if (pack.Target == EnumSelf.TargetType.Opponent) {
	//		MapDataCarrier.Instance.CuEnemyStatus.AddNowHp(heal);
	//	} else if (pack.Target == EnumSelf.TargetType.Self) {
	//		MapDataCarrier.Instance.CuPlayerStatus.AddNowHp(heal);
	//	}
	//}
	//
	//private void CalcShield(ActionPack pack) {
	//	int shield = pack.Value;
	//	if (pack.Target == EnumSelf.TargetType.Opponent) {
	//		MapDataCarrier.Instance.CuEnemyStatus.AddNowShield(shield);
	//	} else if (pack.Target == EnumSelf.TargetType.Self) {
	//		MapDataCarrier.Instance.CuPlayerStatus.AddNowShield(shield);
	//	}
	//}
	
/*
	/// <summary>
	/// 初期化前処理.
	/// </summary>
	override public bool OnBeforeInit()
	{
		var scene = MapDataCarrier.Instance.Scene as MapScene;

		int select = MapDataCarrier.Instance.SelectAttackIndex;

		MasterActionTable.Data data = MapDataCarrier.Instance.CuPlayerStatus.GetActionData(select);

		if (data.Type1 == EnumSelf.ActionType.AddDamage) {
			CalcDamageNormalDamage(data);
		} else if (data.Type1 == EnumSelf.ActionType.ContinuousDamage) {
			CalcDamageNormalDamage(data);
			MapDataCarrier.Instance.ContinuousCount++;
		} else if (data.Type1 == EnumSelf.ActionType.Heal) {
			MapDataCarrier.Instance.CuPlayerStatus.AddNowHp(data.Value1);
		} else if (data.Type1 == EnumSelf.ActionType.AddShield) {
			MapDataCarrier.Instance.CuPlayerStatus.AddNowShield(data.Value1);
		}

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

	private void CalcDamageNormalDamage(MasterActionTable.Data data) {
		int shield = MapDataCarrier.Instance.CuEnemyStatus.GetNowShield();
		MapDataCarrier.Instance.CuEnemyStatus.AddNowShield(-data.Value1);
		int overDamage = shield - data.Value1;

		if (overDamage < 0) {
			MapDataCarrier.Instance.CuEnemyStatus.AddNowHp(overDamage);
		}
	}
	*/
}
