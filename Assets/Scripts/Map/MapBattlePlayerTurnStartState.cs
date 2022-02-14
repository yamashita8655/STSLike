using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBattlePlayerTurnStartState : StateBase {

	// TODO ここで、仮に死亡判定やダメージ判定などが必要になったら、
	// バリューチェンジ→チェックのフローを介す必要がある

	/// <summary>
	/// 初期化前処理.
	/// </summary>
	override public bool OnBeforeInit()
	{
		var scene = MapDataCarrier.Instance.Scene as MapScene;
		MapDataCarrier.Instance.CuPlayerStatus.SetNowShield(0);
		scene.PlayerShieldText.text = "";

		BattleCalculationFunction.PlayerTurnStartValueChange();

		scene.UpdateParameterText();
		
		// TODO もう少し呼び出す回数最適化できそうな気がする
		for (int i = 0; i < 6; i++) {
			scene.UpdatePlayerValueObject(i);
		}
		// 自分の弱体が終わると、敵の表示も変える必要があるので、ここでも更新する
		scene.UpdateEnemyValueObject();
		return true;
	}

	/// <summary>
	/// メイン更新処理.
	/// </summary>
	/// <param name="delta">経過時間</param>
	override public void OnUpdateMain(float delta)
	{
		StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleUpdateAttackButtonDisplay);
	}

	/// <summary>
	/// ステート解放時処理.
	/// </summary>
	override public void OnRelease()
	{
	}
}
