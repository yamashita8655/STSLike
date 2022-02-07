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
		scene.DungeonResultRoot.SetActive(false);
		scene.ArtifactRoot.SetActive(false);

		scene.CarryArtifactDetailController.Close();
		scene.CarryCardDetailController.Close();

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
		status.SetActionData(0, MasterAction2Table.Instance.GetData(22));
		status.SetActionData(1, MasterAction2Table.Instance.GetData(23));
		status.SetActionData(2, MasterAction2Table.Instance.GetData(24));
		status.SetActionData(3, MasterAction2Table.Instance.GetData(24));
		status.SetActionData(4, MasterAction2Table.Instance.GetData(27));
		status.SetActionData(5, MasterAction2Table.Instance.GetData(27));


		status.SetMaxDiceCount(3);
		MapDataCarrier.Instance.CuPlayerStatus = status;

		scene.PlayerNowHpText.text = status.GetNowHp().ToString();
		scene.PlayerMaxHpText.text = status.GetMaxHp().ToString();

		// TODO フロア設定
		MapDataCarrier.Instance.MaxFloor = MapDataCarrier.Instance.DungeonData.FloorCount;
		MapDataCarrier.Instance.NowFloor = 1;

		scene.NowFloorText.text = MapDataCarrier.Instance.NowFloor.ToString();
		scene.MaxFloorText.text = MapDataCarrier.Instance.MaxFloor.ToString();

		MapDataCarrier.Instance.IsClear = false;

		// バトルで使用する状態異常オブジェクトの初期化をしてしまう
		// なぜなら、バトル毎に生成する物ではない為
		for (int i = 0; i < (int)EnumSelf.PowerType.Max; i++) {
			int index = i;
			ResourceManager.Instance.RequestExecuteOrder(
				Const.PowerControllerPath,
				ExecuteOrder.Type.GameObject,
				scene.gameObject,
				(rawObject) => {
					GameObject obj = GameObject.Instantiate(rawObject) as GameObject;
					obj.GetComponent<PowerController>().Initialize((EnumSelf.PowerType)index, 0, scene.PowerRoot);
					MapDataCarrier.Instance.PowerObjects.Add(obj);
				}
			);
		}
		
		for (int i = 0; i < (int)EnumSelf.TurnPowerType.Max; i++) {
			int index = i;
			ResourceManager.Instance.RequestExecuteOrder(
				Const.TurnPowerControllerPath,
				ExecuteOrder.Type.GameObject,
				scene.gameObject,
				(rawObject) => {
					GameObject obj = GameObject.Instantiate(rawObject) as GameObject;
					obj.GetComponent<TurnPowerController>().Initialize((EnumSelf.TurnPowerType)index, 0, scene.TurnPowerRoot);
					MapDataCarrier.Instance.TurnPowerObjects.Add(obj);
				}
			);
		}
		
		for (int i = 0; i < (int)EnumSelf.PowerType.Max; i++) {
			int index = i;
			ResourceManager.Instance.RequestExecuteOrder(
				Const.PowerControllerPath,
				ExecuteOrder.Type.GameObject,
				scene.gameObject,
				(rawObject) => {
					GameObject obj = GameObject.Instantiate(rawObject) as GameObject;
					obj.GetComponent<PowerController>().Initialize((EnumSelf.PowerType)index, 0, scene.EnemyPowerRoot);
					MapDataCarrier.Instance.EnemyPowerObjects.Add(obj);
				}
			);
		}
		
		for (int i = 0; i < (int)EnumSelf.TurnPowerType.Max; i++) {
			int index = i;
			ResourceManager.Instance.RequestExecuteOrder(
				Const.TurnPowerControllerPath,
				ExecuteOrder.Type.GameObject,
				scene.gameObject,
				(rawObject) => {
					GameObject obj = GameObject.Instantiate(rawObject) as GameObject;
					obj.GetComponent<TurnPowerController>().Initialize((EnumSelf.TurnPowerType)index, 0, scene.EnemyTurnPowerRoot);
					MapDataCarrier.Instance.EnemyTurnPowerObjects.Add(obj);
				}
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
		StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.UpdateDifficult);
	}

	/// <summary>
	/// ステート解放時処理.
	/// </summary>
	override public void OnRelease()
	{
	}
}
