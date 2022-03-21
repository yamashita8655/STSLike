using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapHealDetailUpdateState : StateBase {

	/// <summary>
	/// 初期化前処理.
	/// </summary>
	override public bool OnBeforeInit()
	{
		var scene = MapDataCarrier.Instance.Scene as MapScene;

		int index = MapDataCarrier.Instance.SelectHealIndex;
		int difficult = MapDataCarrier.Instance.SelectDifficultNumber;
		
		var player = MapDataCarrier.Instance.CuPlayerStatus;


		if (index == 0) {
			// TODO とりあえず1決め打ち
			MasterHealTable.Data data = MasterHealTable.Instance.GetData(1);

			int healRatio = data.Values[difficult];
			int healVal = player.GetMaxHp() * healRatio / 100;

			scene.HealDetailText.text = string.Format(
				MasterStringTable.Instance.GetString(data.Detail),
				healVal.ToString()
			);
		
			// 回復時のバフを持っていたら、表記を足す
			if (player.GetParameterListFlag(EnumSelf.ParameterType.RestUp1) == true) {
				scene.HealDetailText.text += string.Format(
					MasterStringTable.Instance.GetString("Map_HealValue"),
					8
				);
			}
			if (player.GetParameterListFlag(EnumSelf.ParameterType.RestUp2) == true) {
				scene.HealDetailText.text += string.Format(
					MasterStringTable.Instance.GetString("Map_HealValue"),
					15	
				);
			}
			if (player.GetParameterListFlag(EnumSelf.ParameterType.RestUp3) == true) {
				scene.HealDetailText.text += string.Format(
					MasterStringTable.Instance.GetString("Map_HealValue"),
					21	
				);
			}
		}

		if (index == 1) {
			MasterHealTable.Data data = MasterHealTable.Instance.GetData(2);
			int addMaxHpVal = data.Values[difficult];
			scene.HealDetailText.text = string.Format(
				MasterStringTable.Instance.GetString(data.Detail),
				addMaxHpVal.ToString(),
				MapDataCarrier.Instance.AddDiceCost
			);
		}
		
		if (index == 2) {
			MasterHealTable.Data data = MasterHealTable.Instance.GetData(3);
			int removeCount = data.Values[difficult];
			scene.HealDetailText.text = string.Format(
				MasterStringTable.Instance.GetString(data.Detail),
				removeCount.ToString()
			);
		}

		return true;
	}

	/// <summary>
	/// メイン更新処理.
	/// </summary>
	/// <param name="delta">経過時間</param>
	override public void OnUpdateMain(float delta)
	{
		StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.HealUserWait);
	}

	/// <summary>
	/// ステート解放時処理.
	/// </summary>
	override public void OnRelease()
	{
	}
}
