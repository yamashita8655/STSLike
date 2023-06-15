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
		BattleCalculationFunction.EnemyTurnEndValueChange();
		// ここで、ターン経過によるAI変更を行う必要があるかのチェックを行う
		MapDataCarrier.Instance.CuEnemyStatus.CheckAIForTurnProgress();

		scene.UpdateParameterText();
		//for (int i = 0; i < 6; i++) {
		//	scene.UpdatePlayerValueObject(i);
		//}
		var ctrls = MapDataCarrier.Instance.BattleCardButtonControllers;
		for (int i = 0; i < ctrls.Count; i++) {
			if (ctrls[i].gameObject.activeSelf == true) {
				ctrls[i].UpdateDisplay();
			}
		}
		// LotActionでやってるし、ここでやる必要なさそう
		//scene.UpdateEnemyValueObject();
		return true;
	}

	/// <summary>
	/// メイン更新処理.
	/// </summary>
	/// <param name="delta">経過時間</param>
	override public void OnUpdateMain(float delta)
	{
		StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleEnemyLotAction);
	}

	/// <summary>
	/// ステート解放時処理.
	/// </summary>
	override public void OnRelease()
	{
	}
}
