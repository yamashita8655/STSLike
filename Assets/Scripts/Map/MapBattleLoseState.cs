﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBattleLoseState : StateBase {

	/// <summary>
	/// 初期化前処理.
	/// </summary>
	override public bool OnBeforeInit()
	{
		var scene = MapDataCarrier.Instance.Scene as MapScene;

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
		StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.DungeonResultDisplay);
	}

	/// <summary>
	/// ステート解放時処理.
	/// </summary>
	override public void OnRelease()
	{
	}
}
