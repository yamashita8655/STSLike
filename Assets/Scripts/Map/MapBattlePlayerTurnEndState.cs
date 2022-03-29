using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBattlePlayerTurnEndState : StateBase {

	/// <summary>
	/// 初期化前処理.
	/// </summary>
	override public bool OnBeforeInit()
	{
		var scene = MapDataCarrier.Instance.Scene as MapScene;
		scene.TurnEndButtonObject.SetActive(false);
		BattleCalculationFunction.PlayerTurnEndValueChange();
	
		var player = MapDataCarrier.Instance.CuPlayerStatus;

		// ターン終了時、攻撃していなければ、鉄壁1獲得
		if (player.GetParameterListFlag(EnumSelf.ParameterType.NoAttackGetTurnShieldPreserve) == true) {
			if (player.GetTurnUseAttackCardCount() <= 0) {
				BattleCalculationFunction.PlayerUpdateTurnPower(EnumSelf.TurnPowerType.TurnShieldPreserve, 1);
			}
		}

		scene.UpdateParameterText();

		// 手札を捨て札に捨てる
		var trashList = MapDataCarrier.Instance.TrashList;

		var ctrls = MapDataCarrier.Instance.BattleCardButtonControllers;
		for (int i = 0; i < ctrls.Count; i++) {
            if (ctrls[i].gameObject.activeSelf == true) {
                trashList.Add(ctrls[i].GetData());
            }
            ctrls[i].gameObject.SetActive(false);
		}
		scene.TrashCountText.text = trashList.Count.ToString();
		
		//for (int i = 0; i < 6; i++) {
		//	scene.UpdatePlayerValueObject(i);
		//}
		scene.UpdateEnemyValueObject();
		return true;
	}

	/// <summary>
	/// メイン更新処理.
	/// </summary>
	/// <param name="delta">経過時間</param>
	override public void OnUpdateMain(float delta)
	{
		StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleEnemyTurnStart);
	}

	/// <summary>
	/// ステート解放時処理.
	/// </summary>
	override public void OnRelease()
	{
	}
}
