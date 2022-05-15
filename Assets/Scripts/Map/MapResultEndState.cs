using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapResultEndState : StateBase {

	/// <summary>
	/// 初期化前処理.
	/// </summary>
	override public bool OnBeforeInit()
	{
		var scene = MapDataCarrier.Instance.Scene as MapScene;

		scene.MapRoot.SetActive(true);
		scene.ChangeRoot.SetActive(false);
		scene.ResultRoot.SetActive(false);
		scene.BattleRoot.SetActive(false);
		scene.HandCardRoot.SetActive(false);
		scene.CardInfoRoot.SetActive(false);

		return true;
	}

	/// <summary>
	/// メイン更新処理.
	/// </summary>
	/// <param name="delta">経過時間</param>
	override public void OnUpdateMain(float delta)
	{
		StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.FloorEndCheck);
		//if (MapDataCarrier.Instance.NowFloor == MapDataCarrier.Instance.MaxFloor) {
		//	StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.End);
		//} else {
		//	StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.UpdateDifficult);
		//}
	}

	/// <summary>
	/// ステート解放時処理.
	/// </summary>
	override public void OnRelease()
	{
	}
}
