﻿using System;
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
		var player = MapDataCarrier.Instance.CuPlayerStatus;

		int select = MapDataCarrier.Instance.SelectAttackIndex;

		MasterAction2Table.Data data = player.GetActionData(select);

		int count = MapDataCarrier.Instance.ActionPackCount;
		ActionPack pack = data.ActionPackList[count]; 
		
		BattleCalculationFunction.PlayerValueChange(pack);

		// TODO もしかしたら、こういう副次的な効果も、全てアクションパックに含めた方がいいのかもしれない
		if (player.GetParameterListFlag(EnumSelf.ParameterType.UseCurseShield) == true) {
			if (BattleCalculationFunction.IsCurse(data.Id) == true) {
				BattleCalculationFunction.PlayerCalcShield(4);
			}
		}

		MapDataCarrier.Instance.ActionPackCount++;

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
