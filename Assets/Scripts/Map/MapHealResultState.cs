using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapHealResultState : StateBase {

	/// <summary>
	/// 初期化前処理.
	/// </summary>
	override public bool OnBeforeInit()
	{
		var scene = MapDataCarrier.Instance.Scene as MapScene;
		
		int healIndex = MapDataCarrier.Instance.SelectHealIndex;
		// TODO とりあえず、回復決め打ちになっているので、
		// ちゃんと効果タイプを見た内容を反映させる
		MasterHealTable.Data data = MapDataCarrier.Instance.HealList[healIndex];

		int healValue = data.Value1;
		PlayerStatus status = MapDataCarrier.Instance.CuPlayerStatus;
		status.AddNowHp(healValue);
		scene.PlayerNowHpText.text = status.GetNowHp().ToString();

		return true;
	}

	/// <summary>
	/// メイン更新処理.
	/// </summary>
	/// <param name="delta">経過時間</param>
	override public void OnUpdateMain(float delta)
	{
		StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.HealEnd);
	}

	/// <summary>
	/// ステート解放時処理.
	/// </summary>
	override public void OnRelease()
	{
	}
}
