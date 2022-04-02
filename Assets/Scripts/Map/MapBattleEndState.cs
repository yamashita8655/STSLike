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
		MapDataCarrier.Instance.EventBattleFloorAdd = 0;
		
		var player = MapDataCarrier.Instance.CuPlayerStatus;
		if (player.GetParameterListFlag(EnumSelf.ParameterType.FirstAidKit) == true) {
			if (player.GetNowHp() <= (player.GetMaxHp()/2)) {
				player.AddNowHp(12);
				scene.UpdateParameterText();
			}
		}
		
		if (player.GetParameterListFlag(EnumSelf.ParameterType.SeekersAmulet) == true) {
			player.AddMaxHp(1);
			player.AddNowHp(1);
			scene.UpdateParameterText();
		}
		
		if (player.GetPower().GetValue(EnumSelf.PowerType.HealCharge) > 0) {
			player.AddNowHp(player.GetPower().GetValue(EnumSelf.PowerType.HealCharge));
			scene.UpdateParameterText();
		}

		
		return true;
	}

	/// <summary>
	/// メイン更新処理.
	/// </summary>
	/// <param name="delta">経過時間</param>
	override public void OnUpdateMain(float delta)
	{
		if (MapDataCarrier.Instance.CuEnemyStatus.IsEscape() == true) {
			StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.ResultEnd);
		} else {
			StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.ResultInitialize);
		}
	}

	/// <summary>
	/// ステート解放時処理.
	/// </summary>
	override public void OnRelease()
	{
	}
}
