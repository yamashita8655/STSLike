using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBattleValueChangeState : StateBase {

	/// <summary>
	/// 初期化前処理.
	/// </summary>
	override public bool OnBeforeInit()
	{
		var scene = MapDataCarrier.Instance.Scene as MapScene;

		int select = MapDataCarrier.Instance.SelectAttackIndex;

		MasterActionTable.Data data = MapDataCarrier.Instance.CuPlayerStatus.GetActionData(select);

		if (data.Type1 == Enum.ActionType.AddDamage) {
			MapDataCarrier.Instance.CuEnemyStatus.AddNowHp(-data.Value1);
		} else if (data.Type1 == Enum.ActionType.ContinuousDamage) {
			MapDataCarrier.Instance.CuEnemyStatus.AddNowHp(-data.Value1);
			MapDataCarrier.Instance.ContinuousCount++;
		} else if (data.Type1 == Enum.ActionType.Heal) {
			MapDataCarrier.Instance.CuPlayerStatus.AddNowHp(data.Value1);
		}

		scene.PlayerNowHpText.text = MapDataCarrier.Instance.CuPlayerStatus.GetNowHp().ToString();
		scene.EnemyNowHpText.text = MapDataCarrier.Instance.CuEnemyStatus.GetNowHp().ToString();

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
