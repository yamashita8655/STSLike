using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeInitializeState : StateBase {

	/// <summary>
	/// 初期化前処理.
	/// </summary>
	override public bool OnBeforeInit()
	{
		var scene = HomeDataCarrier.Instance.Scene as HomeScene;

		scene.TitleText.text = MasterStringTable.Instance.GetString("Home_Title");
		scene.TouchStartText.text = MasterStringTable.Instance.GetString("Home_TapStart");

		return true;
	}

	/// <summary>
	/// メイン更新処理.
	/// </summary>
	/// <param name="delta">経過時間</param>
	override public void OnUpdateMain(float delta)
	{
		StateMachineManager.Instance.ChangeState(StateMachineName.Home, (int)HomeState.UserWait);
	}

	/// <summary>
	/// ステート解放時処理.
	/// </summary>
	override public void OnRelease()
	{
	}
}
