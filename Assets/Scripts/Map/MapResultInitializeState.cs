using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapResultInitializeState : StateBase {

	/// <summary>
	/// 初期化前処理.
	/// </summary>
	override public bool OnBeforeInit()
	{
		var scene = MapDataCarrier.Instance.Scene as MapScene;
		scene.TreasureDecideButton.interactable = false;
			
		// TODO treasureカウント3決め打ち
		MapDataCarrier.Instance.TreasureList.Clear();
		for (int i = 0; i < 3; i++) {
			// TODO 1~7決め打ち
			int id = UnityEngine.Random.Range(1, 8);
			MasterActionTable.Data data = MasterActionTable.Instance.GetData(id);
			MapDataCarrier.Instance.TreasureList.Add(data);
			scene.TreasureNameTexts[i].text = data.Name;
		}

		return true;
	}

	/// <summary>
	/// メイン更新処理.
	/// </summary>
	/// <param name="delta">経過時間</param>
	override public void OnUpdateMain(float delta)
	{
		StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.ResultTreasureDisplay);
	}

	/// <summary>
	/// ステート解放時処理.
	/// </summary>
	override public void OnRelease()
	{
	}
}
