using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBattleEndState : StateBase {

	/// <summary>
	/// 初期化前処理.
	/// </summary>
	override public bool OnBeforeInit()
	{
		var scene = MapDataCarrier.Instance.Scene as MapScene;

		var player = MapDataCarrier.Instance.CuPlayerStatus;
		if (player.GetParameterListFlag(EnumSelf.ParameterType.AntiCurse) == true) {
			for (int i = 0; i < 6; i++) {
				// 処理が終わったら、呪いのアクションだった場合は、基に戻す
				MasterAction2Table.Data data = MapDataCarrier.Instance.CuPlayerStatus.GetActionData(i);
				bool IsCurse = BattleCalculationFunction.IsCurse(data.Id);
				if (IsCurse == true) {
					MapDataCarrier.Instance.CuPlayerStatus.ResetActionData(i);
					scene.UpdatePlayerValueObject(i);
				}
			}
		}
		
		return true;
	}

	/// <summary>
	/// メイン更新処理.
	/// </summary>
	/// <param name="delta">経過時間</param>
	override public void OnUpdateMain(float delta)
	{
		//StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.UpdateDifficult);
		StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.ResultInitialize);
	}

	/// <summary>
	/// ステート解放時処理.
	/// </summary>
	override public void OnRelease()
	{
	}
}
