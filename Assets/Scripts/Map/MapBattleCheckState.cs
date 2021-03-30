using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBattleCheckState : StateBase {

	private bool IsWin = false;
	private bool IsDead = false;

	/// <summary>
	/// 初期化前処理.
	/// </summary>
	override public bool OnBeforeInit()
	{
		var scene = MapDataCarrier.Instance.Scene as MapScene;

		IsWin = false;
		IsDead = false;

		if (MapDataCarrier.Instance.CuEnemyStatus.IsDead()) {
			IsWin = true;
		}

		if (MapDataCarrier.Instance.CuPlayerStatus.IsDead()) {
			IsDead = true;
		}

		//if (StateMachineManager.Instance.GetPrevState(StateMachineName.Map) == (int)MapState.BattleAttackResult) {
		//	
		//} else if (StateMachineManager.Instance.GetPrevState(StateMachineName.Map) == (int)MapState.BattleAttackResult) {
		//	
		//}

		return true;
	}

	/// <summary>
	/// メイン更新処理.
	/// </summary>
	/// <param name="delta">経過時間</param>
	override public void OnUpdateMain(float delta)
	{
		if (IsWin) {
			Debug.Log("IsWin");
			StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleWin);
		} else if (IsDead) {
			Debug.Log("IsDead");
			StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleLose);
		} else {
			// プレイヤーのターンの場合
			if (StateMachineManager.Instance.GetPrevState(StateMachineName.Map) == (int)MapState.BattleAttackResult) {
				if (MapDataCarrier.Instance.DiceValueList.Count == 0) {
					StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleEnemyAttack);
				} else {
					StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleAttackSelectUserWait);
				}
			} else if (StateMachineManager.Instance.GetPrevState(StateMachineName.Map) == (int)MapState.BattleEnemyAttackResult) {
				StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleDiceRollUserWait);
			}
		}
		//StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleCheck);
	}

	/// <summary>
	/// ステート解放時処理.
	/// </summary>
	override public void OnRelease()
	{
	}
}
