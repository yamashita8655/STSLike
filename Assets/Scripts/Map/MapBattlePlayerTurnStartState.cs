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
		var player = MapDataCarrier.Instance.CuPlayerStatus;

		MapDataCarrier.Instance.SelectAttackIndex = -1;
		MapDataCarrier.Instance.BattleTurnCount++;
		
		MapDataCarrier.Instance.CurrentTotalDiceCost = 0;
		scene.UpdateCurrentTotalDiceCostText();
		
		MapDataCarrier.Instance.SelectBattleCardData = null;
		MapDataCarrier.Instance.DoubleAttackBattleCardData = null;
		MapDataCarrier.Instance.IsDoubleAttackCard = false;
		MapDataCarrier.Instance.IsCost6DoubleAttackCard = false;

		BattleCalculationFunction.PlayerTurnStartValueChange();

		int drawCount = Const.DrawCount;// 何か補正で引く枚数が増えたら、ここ調整する
		scene.DrawCard(drawCount);

		scene.UpdateParameterText();
		
		// 自分の弱体が終わると、敵の表示も変える必要があるので、ここでも更新する
		scene.UpdateEnemyValueObject();

		// 攻撃使用フラグリセット
		player.SetUseAttack(false);

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
