using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapHealResultState : StateBase {

	/// <summary>
	/// 初期化前処理.
	/// </summary>
	override public bool OnBeforeInit()
	{
		var scene = MapDataCarrier.Instance.Scene as MapScene;
		
		int index = MapDataCarrier.Instance.SelectHealIndex;
		int difficult = MapDataCarrier.Instance.SelectDifficultNumber;

		PlayerStatus player = MapDataCarrier.Instance.CuPlayerStatus;
		
		if (index == 0) {
			MasterHealTable.Data data = MasterHealTable.Instance.GetData(1);
			
			int healRatio = data.Values[difficult];
			int healVal = player.GetMaxHp() * healRatio / 100;

			// 回復時のバフを持っていたら、表記を足す
			if (player.GetParameterListFlag(EnumSelf.ParameterType.RestUp1) == true) {
				healVal += 8;
			}
			if (player.GetParameterListFlag(EnumSelf.ParameterType.RestUp2) == true) {
				healVal += 15;
			}
			if (player.GetParameterListFlag(EnumSelf.ParameterType.RestUp3) == true) {
				healVal += 21;
			}
			
			player.AddNowHp(healVal);
			scene.UpdateParameterText();
		}

		if (index == 1) {
			MasterHealTable.Data data = MasterHealTable.Instance.GetData(2);
			int addMaxHpVal = data.Values[difficult];
			player.AddMaxHp(addMaxHpVal);
			player.AddNowHp(addMaxHpVal);
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
		StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.HealEnd);
	}

	/// <summary>
	/// ステート解放時処理.
	/// </summary>
	override public void OnRelease()
	{
	}
}
