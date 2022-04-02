using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBattleEnemyValueChangeState : StateBase {

	/// <summary>
	/// 初期化前処理.
	/// </summary>
	override public bool OnBeforeInit()
	{
		var scene = MapDataCarrier.Instance.Scene as MapScene;

		EnemyStatus enemy = MapDataCarrier.Instance.CuEnemyStatus;
		MasterAction2Table.Data data = enemy.GetActionData();

		int count = MapDataCarrier.Instance.EnemyActionPackCount;
		ActionPack pack = data.ActionPackList[count]; 

		BattleCalculationFunction.EnemyValueChange(pack);

		if (pack.Effect == EnumSelf.EffectType.Escape) {
			enemy.SetEscape();
		}

		// 動的にカード加える系（呪いとか、複製とか）
		scene.CheckAddCard(pack);

		scene.UpdateParameterText();

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
