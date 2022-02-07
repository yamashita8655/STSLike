using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapResultChangeResultState : StateBase {

	/// <summary>
	/// 初期化前処理.
	/// </summary>
	override public bool OnBeforeInit()
	{
		var scene = MapDataCarrier.Instance.Scene as MapScene;
		
		int treasureIndex = MapDataCarrier.Instance.SelectTreasureIndex;
		MasterAction2Table.Data data = MapDataCarrier.Instance.TreasureList[treasureIndex];

		int changeIndex = MapDataCarrier.Instance.SelectChangeIndex;
		MapDataCarrier.Instance.CuPlayerStatus.SetActionData(changeIndex, data);

		scene.PlayerActionNameStrings[changeIndex].text = data.Name;
		scene.UpdatePlayerValueObject(changeIndex);

		// TODO 見せ方考える
		//scene.PlayerActionValueStrings[changeIndex].text = data.Value1.ToString();

		return true;
	}

	/// <summary>
	/// メイン更新処理.
	/// </summary>
	/// <param name="delta">経過時間</param>
	override public void OnUpdateMain(float delta)
	{
		StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.ResultEnd);
	}

	/// <summary>
	/// ステート解放時処理.
	/// </summary>
	override public void OnRelease()
	{
	}
}
