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
		MasterActionTable.Data data = enemy.GetActionData();

		if (data.Type1 == Enum.ActionType.AddDamage) {
			CalcDamageNormalDamage(data);
		} else if (data.Type1 == Enum.ActionType.ContinuousDamage) {
			CalcDamageNormalDamage(data);
			MapDataCarrier.Instance.EnemyContinuousCount++;
		} else if (data.Type1 == Enum.ActionType.Heal) {
			MapDataCarrier.Instance.CuEnemyStatus.AddNowHp(data.Value1);
		} else if (data.Type1 == Enum.ActionType.AddShield) {
			MapDataCarrier.Instance.CuEnemyStatus.AddNowShield(data.Value1);
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
		int shield = MapDataCarrier.Instance.CuPlayerStatus.GetNowShield();
		MapDataCarrier.Instance.CuPlayerStatus.AddNowShield(-data.Value1);
		int overDamage = shield - data.Value1;

		if (overDamage < 0) {
			MapDataCarrier.Instance.CuPlayerStatus.AddNowHp(overDamage);
		}
	}
}
