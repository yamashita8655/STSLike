using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBattleAttackResultState : StateBase {

	/// <summary>
	/// 初期化前処理.
	/// </summary>
	override public bool OnBeforeInit()
	{
		var scene = MapDataCarrier.Instance.Scene as MapScene;

		int select = MapDataCarrier.Instance.SelectAttackIndex;

		MasterActionTable.Data data = MapDataCarrier.Instance.CuPlayerStatus.GetActionData(select);

		MapDataCarrier.Instance.ContinuousCount = 0;
		MapDataCarrier.Instance.MaxContinuousCount = 0;
		
		// 数値の初期化などはここで一度行い、実際の数値の増減ループは
		// ValueChange-BattleCheck間で行う
		if (data.Type1 == Enum.ActionType.ContinuousDamage) {
			MapDataCarrier.Instance.MaxContinuousCount = data.Value2;
		}

		return true;
	}

	/// <summary>
	/// メイン更新処理.
	/// </summary>
	/// <param name="delta">経過時間</param>
	override public void OnUpdateMain(float delta)
	{
		StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleValueChange);
	}

	/// <summary>
	/// ステート解放時処理.
	/// </summary>
	override public void OnRelease()
	{
	}
}
