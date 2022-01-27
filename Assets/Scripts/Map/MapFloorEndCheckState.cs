using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapFloorEndCheckState : StateBase {

	/// <summary>
	/// 初期化前処理.
	/// </summary>
	override public bool OnBeforeInit()
	{
		var scene = MapDataCarrier.Instance.Scene as MapScene;

		return true;
	}

	/// <summary>
	/// メイン更新処理.
	/// </summary>
	/// <param name="delta">経過時間</param>
	override public void OnUpdateMain(float delta)
	{
		if (MapDataCarrier.Instance.NowFloor == MapDataCarrier.Instance.MaxFloor) {
			//StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.End);
			StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.DungeonResultDisplay);
			MapDataCarrier.Instance.IsClear = true;
		} else {
			StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.UpdateDifficult);
		}
	}

	/// <summary>
	/// ステート解放時処理.
	/// </summary>
	override public void OnRelease()
	{
	}
}
