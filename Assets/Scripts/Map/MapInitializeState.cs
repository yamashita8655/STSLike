using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInitializeState : StateBase {

	/// <summary>
	/// 初期化前処理.
	/// </summary>
	override public bool OnBeforeInit()
	{
		var scene = MapDataCarrier.Instance.Scene as MapScene;
		
		scene.BattleRoot.SetActive(false);
		scene.ResultRoot.SetActive(false);
		scene.ChangeRoot.SetActive(false);
		scene.HealRoot.SetActive(false);
		scene.MapRoot.SetActive(true);

		MapDataCarrier.Instance.HandDifficultList.Clear();
		for (int i = 0; i < scene.DifficultImages.Length; i++) {
			MapDataCarrier.Instance.HandDifficultList.Add(-1);
		}

		// プレイヤーパラメータ初期化
		PlayerStatus status = new PlayerStatus();
		status.SetMaxHp(80);
		status.SetNowHp(80);
		//MasterActionTable.Data data = MasterActionTable.Instance.GetData(1);
		//status.SetActionData(0, data);
		//status.SetActionData(1, data);
		//status.SetActionData(2, data);
		//status.SetActionData(3, data);
		//status.SetActionData(4, data);
		//data = MasterActionTable.Instance.GetData(2);
		//status.SetActionData(5, data);
		// TODO 初期装備は、テストの為色々変えている
		status.SetActionData(0, MasterActionTable.Instance.GetData(1));
		status.SetActionData(1, MasterActionTable.Instance.GetData(2));
		status.SetActionData(2, MasterActionTable.Instance.GetData(18));
		status.SetActionData(3, MasterActionTable.Instance.GetData(19));
		status.SetActionData(4, MasterActionTable.Instance.GetData(20));
		status.SetActionData(5, MasterActionTable.Instance.GetData(21));

		status.SetMaxDiceCount(3);
		MapDataCarrier.Instance.CuPlayerStatus = status;

		scene.PlayerNowHpText.text = status.GetNowHp().ToString();
		scene.PlayerMaxHpText.text = status.GetMaxHp().ToString();

		// TODO フロア設定
		MapDataCarrier.Instance.MaxFloor = MapDataCarrier.Instance.DungeonData.FloorCount;
		MapDataCarrier.Instance.NowFloor = 1;

		scene.NowFloorText.text = MapDataCarrier.Instance.NowFloor.ToString();
		scene.MaxFloorText.text = MapDataCarrier.Instance.MaxFloor.ToString();

		return true;
	}

	/// <summary>
	/// メイン更新処理.
	/// </summary>
	/// <param name="delta">経過時間</param>
	override public void OnUpdateMain(float delta)
	{
		StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.UpdateDifficult);
	}

	/// <summary>
	/// ステート解放時処理.
	/// </summary>
	override public void OnRelease()
	{
	}
}
