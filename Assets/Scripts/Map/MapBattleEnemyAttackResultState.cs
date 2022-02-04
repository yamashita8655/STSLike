using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBattleEnemyAttackResultState : StateBase {

	/// <summary>
	/// 初期化前処理.
	/// </summary>
	override public bool OnBeforeInit()
	{
		var scene = MapDataCarrier.Instance.Scene as MapScene;

		EnemyStatus enemy = MapDataCarrier.Instance.CuEnemyStatus;
		MasterAction2Table.Data data = enemy.GetActionData();

		MapDataCarrier.Instance.EnemyActionPackCount = 0;
		MapDataCarrier.Instance.EnemyMaxActionPackCount = data.ActionPackList.Count;
		
		return true;
	}

	/// <summary>
	/// メイン更新処理.
	/// </summary>
	/// <param name="delta">経過時間</param>
	override public void OnUpdateMain(float delta)
	{
		StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleEnemyValueChange);
	}

	/// <summary>
	/// ステート解放時処理.
	/// </summary>
	override public void OnRelease()
	{
	}
}
