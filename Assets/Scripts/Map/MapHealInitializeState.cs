using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapHealInitializeState : StateBase {

	/// <summary>
	/// 初期化前処理.
	/// </summary>
	override public bool OnBeforeInit()
	{
		var scene = MapDataCarrier.Instance.Scene as MapScene;
		
		scene.HealRoot.SetActive(true);
		scene.MapRoot.SetActive(false);

		scene.HealDecideButton.interactable = false;
			
		// TODO Healカウント3決め打ち
		MapDataCarrier.Instance.HealList.Clear();
		// かつ、一個目は回復固定
		MasterHealTable.Data data = MasterHealTable.Instance.GetData(1);
		MapDataCarrier.Instance.HealList.Add(data);
		scene.HealTexts[0].text = data.Name;

		for (int i = 1; i < 3; i++) {
			// TODO 2~7決め打ち
			int id = UnityEngine.Random.Range(2, 8);
			data = MasterHealTable.Instance.GetData(id);
			MapDataCarrier.Instance.HealList.Add(data);
			scene.HealTexts[i].text = data.Name;
		}

		return true;
	}

	/// <summary>
	/// メイン更新処理.
	/// </summary>
	/// <param name="delta">経過時間</param>
	override public void OnUpdateMain(float delta)
	{
		StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.HealDisplay);
	}

	/// <summary>
	/// ステート解放時処理.
	/// </summary>
	override public void OnRelease()
	{
	}
}
