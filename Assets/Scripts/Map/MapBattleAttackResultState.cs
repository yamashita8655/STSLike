using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBattleAttackResultState : StateBase {

	/// <summary>
	/// 初期化前処理.
	/// </summary>
	override public bool OnBeforeInit()
	{
		var scene = MapDataCarrier.Instance.Scene as MapScene;

		//int select = MapDataCarrier.Instance.SelectAttackIndex;

		//MasterAction2Table.Data data = MapDataCarrier.Instance.CuPlayerStatus.GetActionData(select);
	
		MasterAction2Table.Data data = null;
		
		if (MapDataCarrier.Instance.Cost6DoubleAttackBattleCardData != null) {
			data = MapDataCarrier.Instance.Cost6DoubleAttackBattleCardData;
			MapDataCarrier.Instance.IsCost6DoubleAttackCard = true;
		} else if (MapDataCarrier.Instance.DoubleAttackBattleCardData != null) {
			data = MapDataCarrier.Instance.DoubleAttackBattleCardData;
			MapDataCarrier.Instance.IsDoubleAttackCard = true;
		}
		
		if (data == null) {
			data = MapDataCarrier.Instance.SelectBattleCardData;
			if (MapDataCarrier.Instance.CuPlayerStatus.GetTurnPowerValue(EnumSelf.TurnPowerType.DoubleAttack) > 0) {
				if (BattleCalculationFunction.IsOpponentDamage(data) == true) {
					MapDataCarrier.Instance.DoubleAttackBattleCardData = data;
					BattleCalculationFunction.PlayerUpdateTurnPower(EnumSelf.TurnPowerType.DoubleAttack, -1);
				}
			}
			
			if (MapDataCarrier.Instance.CuPlayerStatus.GetTurnPowerValue(EnumSelf.TurnPowerType.Cost6DoubleAttack) > 0) {
				if (BattleCalculationFunction.IsOpponentDamage(data) == true) {
					if (data.DiceCost >= 6) {
						MapDataCarrier.Instance.Cost6DoubleAttackBattleCardData = data;
						BattleCalculationFunction.PlayerUpdateTurnPower(EnumSelf.TurnPowerType.Cost6DoubleAttack, -1);
					}
				}
			}
		}

		MapDataCarrier.Instance.ActionPackCount = 0;
		MapDataCarrier.Instance.MaxActionPackCount = data.ActionPackList.Count;
		
		return true;
	}

	/// <summary>
	/// メイン更新処理.
	/// </summary>
	/// <param name="delta">経過時間</param>
	override public void OnUpdateMain(float delta)
	{
		StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleValueChange);
	}

	/// <summary>
	/// ステート解放時処理.
	/// </summary>
	override public void OnRelease()
	{
	}
	
	///// <summary>
	///// 初期化前処理.
	///// </summary>
	//override public bool OnBeforeInit()
	//{
	//	var scene = MapDataCarrier.Instance.Scene as MapScene;

	//	int select = MapDataCarrier.Instance.SelectAttackIndex;

	//	MasterActionTable.Data data = MapDataCarrier.Instance.CuPlayerStatus.GetActionData(select);

	//	MapDataCarrier.Instance.ContinuousCount = 0;
	//	MapDataCarrier.Instance.MaxContinuousCount = 0;
	//	
	//	// 数値の初期化などはここで一度行い、実際の数値の増減ループは
	//	// ValueChange-BattleCheck間で行う
	//	if (data.Type1 == EnumSelf.ActionType.ContinuousDamage) {
	//		MapDataCarrier.Instance.MaxContinuousCount = data.Value2;
	//	}

	//	return true;
	//}

	///// <summary>
	///// メイン更新処理.
	///// </summary>
	///// <param name="delta">経過時間</param>
	//override public void OnUpdateMain(float delta)
	//{
	//	StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleValueChange);
	//}

	///// <summary>
	///// ステート解放時処理.
	///// </summary>
	//override public void OnRelease()
	//{
	//}
}
