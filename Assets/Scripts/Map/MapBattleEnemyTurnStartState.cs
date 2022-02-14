using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBattleEnemyTurnStartState : StateBase {

	/// <summary>
	/// 初期化前処理.
	/// </summary>
	override public bool OnBeforeInit()
	{
		var scene = MapDataCarrier.Instance.Scene as MapScene;
		MapDataCarrier.Instance.CuEnemyStatus.SetNowShield(0);
		scene.EnemyShieldText.text = "";

		BattleCalculationFunction.EnemyTurnStartValueChange();

		scene.UpdateParameterText();
		
		// TODO これ、もう少し呼び出す回数最適化できそうな気がするが…
		scene.UpdateEnemyValueObject();

		return true;
	}

	/// <summary>
	/// メイン更新処理.
	/// </summary>
	/// <param name="delta">経過時間</param>
	override public void OnUpdateMain(float delta)
	{
		StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleCheck);
	}

	/// <summary>
	/// ステート解放時処理.
	/// </summary>
	override public void OnRelease()
	{
	}
}
