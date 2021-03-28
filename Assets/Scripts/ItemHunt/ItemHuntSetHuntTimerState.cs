using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHuntSetHuntTimerState : StateBase {

	//private readonly float HuntCountDownTimer = 5f;
	private readonly float HuntCountDownTimer = 1f;

	/// <summary>
	/// 初期化前処理.
	/// </summary>
	override public bool OnBeforeInit()
	{
		Debug.Log("ItemHuntSetHuntTimerState");
		var scene = ItemHuntDataCarrier.Instance.Scene as ItemHuntScene;
		ItemHuntDataCarrier.Instance.HuntTimerPassTime = HuntCountDownTimer;
		return true;
	}

	/// <summary>
	/// メイン更新処理.
	/// </summary>
	/// <param name="delta">経過時間</param>
	override public void OnUpdateMain(float delta)
	{
		StateMachineManager.Instance.ChangeState(StateMachineName.ItemHunt, (int)ItemHuntState.UserWait);
	}

	/// <summary>
	/// ステート解放時処理.
	/// </summary>
	override public void OnRelease()
	{
	}
}
