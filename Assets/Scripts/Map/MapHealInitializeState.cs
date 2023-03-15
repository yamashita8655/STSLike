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

		MapDataCarrier.Instance.SelectHealIndex = -1;

		var player = MapDataCarrier.Instance.CuPlayerStatus;
			
		MasterHealTable.Data data = MasterHealTable.Instance.GetData(1);
		scene.HealTexts[0].text = data.Name;
		data = MasterHealTable.Instance.GetData(2);
		scene.HealTexts[1].text = data.Name;
		data = MasterHealTable.Instance.GetData(3);
		scene.HealTexts[2].text = data.Name;
		if (MapDataCarrier.Instance.OriginalDeckList.Count == 1) {
			scene.HealButtons[2].interactable = false;
		} else {
			scene.HealButtons[2].interactable = true;
		}

		scene.HealDetailText.text = "";
		
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
