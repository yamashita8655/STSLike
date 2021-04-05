using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBattleEnemyTurnEndState : StateBase {

	// TODO ターン終了時処理の実装が必要にあったら、フローを考え直す
	// と言っても、ここでダメージが発生したら、多分バリューチェンジ→チェックになると思う

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
		//StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleEnemyAttackResult);
	}

	/// <summary>
	/// ステート解放時処理.
	/// </summary>
	override public void OnRelease()
	{
	}
}
