using System;
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
		var enemy = MapDataCarrier.Instance.CuEnemyStatus;
		// トロフィー
		if (enemy.IsDead()) {
			PlayerPrefsManager.Instance.SetDoubleKO(1);
		}

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
