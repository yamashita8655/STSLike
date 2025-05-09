﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBattlePlayerTurnEndState : StateBase {

	/// <summary>
	/// 初期化前処理.
	/// </summary>
	override public bool OnBeforeInit()
	{
		var scene = MapDataCarrier.Instance.Scene as MapScene;
		scene.TurnEndButtonObject.SetActive(false);
		BattleCalculationFunction.PlayerTurnEndValueChange();

		scene.UpdateParameterText();
		
		for (int i = 0; i < 6; i++) {
			scene.UpdatePlayerValueObject(i);
		}
		scene.UpdateEnemyValueObject();
		return true;
	}

	/// <summary>
	/// メイン更新処理.
	/// </summary>
	/// <param name="delta">経過時間</param>
	override public void OnUpdateMain(float delta)
	{
		StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleEnemyTurnStart);
	}

	/// <summary>
	/// ステート解放時処理.
	/// </summary>
	override public void OnRelease()
	{
	}
}
