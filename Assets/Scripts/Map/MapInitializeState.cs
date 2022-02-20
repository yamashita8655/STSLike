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
		MapDataCarrier.Instance.CuPlayerStatus = status;
		status.SetMaxHp(80);
		status.SetNowHp(80);
		// TODO 初期装備は、テストの為色々変えている
		//status.SetActionData(0, MasterAction2Table.Instance.GetData(1));
		//status.SetActionData(1, MasterAction2Table.Instance.GetData(1));
		//status.SetActionData(2, MasterAction2Table.Instance.GetData(2));
		//status.SetActionData(3, MasterAction2Table.Instance.GetData(20));
		//status.SetActionData(4, MasterAction2Table.Instance.GetData(20));
		//status.SetActionData(5, MasterAction2Table.Instance.GetData(21));
		status.SetActionData(0, MasterAction2Table.Instance.GetData(402));
		status.SetActionData(1, MasterAction2Table.Instance.GetData(28));
		status.SetActionData(2, MasterAction2Table.Instance.GetData(22));
		status.SetActionData(3, MasterAction2Table.Instance.GetData(23));
		status.SetActionData(4, MasterAction2Table.Instance.GetData(29));
		status.SetActionData(5, MasterAction2Table.Instance.GetData(26));

		// 未獲得アーティファクトリストを取得しておく
		for (int i = 1; i < 6; i++) {
			MapDataCarrier.Instance.RarityNoAcquiredArtifactList[i-1] = MasterArtifactTable.Instance.GetRarityArtifactCloneList(i);
		}

		// TODO アーティファクト効果テスト
		// ここに、最初からアーティファクトを持たせて、効果を発揮できるようにする
		MasterArtifactTable.Data data = MasterArtifactTable.Instance.GetData(6);
		scene.AddArtifactObject(data);
		MapDataCarrier.Instance.RemoveRarityNoAcquiredArtifactList(6);

		//data = MasterArtifactTable.Instance.GetData(2);
		//scene.AddArtifactObject(data);
		//MapDataCarrier.Instance.RemoveRarityNoAcquiredArtifactList(2);

		//data = MasterArtifactTable.Instance.GetData(3);
		//scene.AddArtifactObject(data);
		//MapDataCarrier.Instance.RemoveRarityNoAcquiredArtifactList(3);

		//data = MasterArtifactTable.Instance.GetData(4);
		//scene.AddArtifactObject(data);
		//MapDataCarrier.Instance.RemoveRarityNoAcquiredArtifactList(4);

		//data = MasterArtifactTable.Instance.GetData(1000);
		//scene.AddArtifactObject(data);
		//MapDataCarrier.Instance.RemoveRarityNoAcquiredArtifactList(1000);

		status.SetMaxDiceCount(3);

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
		LoadPlayerPowerObjects();
		
		return true;
	}

	private void LoadPlayerPowerObjects() {
		var scene = MapDataCarrier.Instance.Scene as MapScene;
		int loadCount = (int)EnumSelf.PowerType.Max;
		int loadedCount = 0;

		for (int i = 0; i < loadCount; i++) {
			int index = i;
			ResourceManager.Instance.RequestExecuteOrder(
				Const.PowerControllerPath,
				ExecuteOrder.Type.GameObject,
				scene.gameObject,
				(rawObject) => {
					GameObject obj = GameObject.Instantiate(rawObject) as GameObject;
					obj.GetComponent<PowerController>().Initialize((EnumSelf.PowerType)index, 0, scene.PowerRoot);
					MapDataCarrier.Instance.PowerObjects.Add(obj);
					loadedCount++;
					if (loadCount == loadedCount) {
						LoadPlayerTurnPowerObjects();
					}
				}
			);
		}
	}
	
	private void LoadPlayerTurnPowerObjects() {
		var scene = MapDataCarrier.Instance.Scene as MapScene;
		int loadCount = (int)EnumSelf.TurnPowerType.Max;
		int loadedCount = 0;

		for (int i = 0; i < loadCount; i++) {
			int index = i;
			ResourceManager.Instance.RequestExecuteOrder(
				Const.TurnPowerControllerPath,
				ExecuteOrder.Type.GameObject,
				scene.gameObject,
				(rawObject) => {
					GameObject obj = GameObject.Instantiate(rawObject) as GameObject;
					obj.GetComponent<TurnPowerController>().Initialize((EnumSelf.TurnPowerType)index, 0, scene.TurnPowerRoot);
					MapDataCarrier.Instance.TurnPowerObjects.Add(obj);
					loadedCount++;
					if (loadCount == loadedCount) {
						LoadEnemyPowerObjects();
					}
				}
			);
		}
	}

	private void LoadEnemyPowerObjects() {
		var scene = MapDataCarrier.Instance.Scene as MapScene;
		int loadCount = (int)EnumSelf.PowerType.Max;
		int loadedCount = 0;

		for (int i = 0; i < loadCount; i++) {
			int index = i;
			ResourceManager.Instance.RequestExecuteOrder(
				Const.PowerControllerPath,
				ExecuteOrder.Type.GameObject,
				scene.gameObject,
				(rawObject) => {
					GameObject obj = GameObject.Instantiate(rawObject) as GameObject;
					obj.GetComponent<PowerController>().Initialize((EnumSelf.PowerType)index, 0, scene.EnemyPowerRoot);
					MapDataCarrier.Instance.EnemyPowerObjects.Add(obj);
					loadedCount++;
					if (loadCount == loadedCount) {
						LoadEnemyTurnPowerObjects();
					}
				}
			);
		}
	}
	
	private void LoadEnemyTurnPowerObjects() {
		var scene = MapDataCarrier.Instance.Scene as MapScene;
		int loadCount = (int)EnumSelf.TurnPowerType.Max;
		int loadedCount = 0;

		for (int i = 0; i < loadCount; i++) {
			int index = i;
			ResourceManager.Instance.RequestExecuteOrder(
				Const.TurnPowerControllerPath,
				ExecuteOrder.Type.GameObject,
				scene.gameObject,
				(rawObject) => {
					GameObject obj = GameObject.Instantiate(rawObject) as GameObject;
					obj.GetComponent<TurnPowerController>().Initialize((EnumSelf.TurnPowerType)index, 0, scene.EnemyTurnPowerRoot);
					MapDataCarrier.Instance.EnemyTurnPowerObjects.Add(obj);
					loadedCount++;
					if (loadCount == loadedCount) {
						LoadPlayerValueObjects();
					}
				}
			);
		}
	}

	private void LoadPlayerValueObjects() {
		var scene = MapDataCarrier.Instance.Scene as MapScene;

		int diceCount = 6;
		int objectCount = 10;
		
		int loadCount = diceCount * objectCount;
		int loadedCount = 0;

		// プレイヤーのValueObjectの初期生成
		for (int i = 0; i < diceCount; i++) {
			int index1 = i;
			// TODO こちらもとりあえず10個決め打ちで作っておく
			for (int i2 = 0; i2 < objectCount; i2++) {
				ResourceManager.Instance.RequestExecuteOrder(
					Const.ValueItemPath,
					ExecuteOrder.Type.GameObject,
					scene.gameObject,
					(rawObject) => {
						GameObject obj = GameObject.Instantiate(rawObject) as GameObject;
						obj.GetComponent<ValueController>().Initialize(scene.PlayerActionValueRoots[index1]);
						MapDataCarrier.Instance.ValueObjects[index1].Add(obj);
						loadedCount++;
						if (loadedCount == loadCount) {
							// どうせ戦闘開始に更新するから、ここで呼び出す必要ないかもしれない
							//for (int i3 = 0; i3 < diceCount; i3++) {
							//	scene.UpdatePlayerValueObject(i3);
							//}
							LoadEnemyValueObjects();
						}
					}
				);
			}
		}
	}
	
	private void LoadEnemyValueObjects() {
		var scene = MapDataCarrier.Instance.Scene as MapScene;
		int objectCount = 10;
		
		int loadCount = objectCount;
		int loadedCount = 0;

		// 敵のValueObjectの初期生成
		// TODO とりあえず、10個あれば足りそうなので、10個決め打ちで作っておく
		for (int i = 0; i < objectCount; i++) {
			int index = i;
			ResourceManager.Instance.RequestExecuteOrder(
				Const.ValueItemPath,
				ExecuteOrder.Type.GameObject,
				scene.gameObject,
				(rawObject) => {
					GameObject obj = GameObject.Instantiate(rawObject) as GameObject;
					obj.GetComponent<ValueController>().Initialize(scene.EnemyActionValueRoot);
					MapDataCarrier.Instance.EnemyValueObjects.Add(obj);
					loadedCount++;
					if (loadedCount == loadCount) {
						//FadeManager.Instance.FadeIn(0.5f, null);
						//StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.UpdateDifficult);
						LoadBgImage();
					}
				}
			);
		}
	}
	
	private void LoadBgImage() {
		var scene = MapDataCarrier.Instance.Scene as MapScene;

		ResourceManager.Instance.RequestExecuteOrder(
			MapDataCarrier.Instance.DungeonData.ImagePath,
			ExecuteOrder.Type.Sprite,
			scene.gameObject,
			(rawSprite) => {
				scene.BgImage.sprite = rawSprite as Sprite;
				FadeManager.Instance.FadeIn(0.5f, null);
				StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.UpdateDifficult);
			}
		);
	}


	/// <summary>
	/// メイン更新処理.
	/// </summary>
	/// <param name="delta">経過時間</param>
	override public void OnUpdateMain(float delta)
	{
	}

	/// <summary>
	/// ステート解放時処理.
	/// </summary>
	override public void OnRelease()
	{ }
}
