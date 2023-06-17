using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInitializeState : StateBase {

	private MapScene Scene = null;
	/// <summary>
	/// 初期化前処理.
	/// </summary>
	override public bool OnBeforeInit()
	{
		// TODO デバッグ、セーブ情報を表示する為の参照処理
		var debugHandList = PlayerPrefsManager.Instance.GetHandDifficultList();
		int SelectMapType = PlayerPrefsManager.Instance.GetSelectMapType();
		var nowFloor = PlayerPrefsManager.Instance.GetNowFloor();
		var mapTypeList = PlayerPrefsManager.Instance.GetMapTypeList();
		//var dungeonId = PlayerPrefsManager.Instance.GetDungeonId();
		var artifactList = PlayerPrefsManager.Instance.GetArtifactList();
		var originalDeckList = PlayerPrefsManager.Instance.GetOriginalDeckList();
		var diceCost = PlayerPrefsManager.Instance.GetDiceCost();
		var chestList = PlayerPrefsManager.Instance.GetChestList();
		var enemyId = PlayerPrefsManager.Instance.GetEnemyId();
		var treasureList = PlayerPrefsManager.Instance.GetTreasureList();
		var lotArtifactList = PlayerPrefsManager.Instance.GetLotArtifactList();
		var nowHp = PlayerPrefsManager.Instance.GetSaveNowHp();
		var maxHp = PlayerPrefsManager.Instance.GetSaveMaxHp();

		Scene = MapDataCarrier.Instance.Scene as MapScene;

		Scene.BattleRoot.SetActive(false);
		Scene.ResultRoot.SetActive(false);
		Scene.ChangeRoot.SetActive(false);
		Scene.HealRoot.SetActive(false);
		Scene.MapRoot.SetActive(true);
		Scene.DungeonResultRoot.SetActive(false);
		Scene.ArtifactRoot.SetActive(false);
		Scene.EventRoot.SetActive(false);

		Scene.CarryArtifactDetailController.Close();
		Scene.CarryCardDetailController.Close();
		Scene.EraseCardDetailController.Close();
		
		Scene.HandCardRoot.SetActive(false);
		Scene.HandCardSelectRoot.SetActive(false);
		Scene.CardInfoRoot.SetActive(false);
		Scene.HandCardSelectDecideButton.gameObject.SetActive(false);
		Scene.HandCardSelectDecideButton.interactable = false;
		
		MapDataCarrier.Instance.HandDifficultList.Clear();
		
		// データ初期化
		var dungeonState = PlayerPrefsManager.Instance.GetDungeonState();
		
		// 未獲得アーティファクトリストを取得しておく
		for (int i = 1; i < 6; i++) {
			MapDataCarrier.Instance.RarityNoAcquiredArtifactList[i-1] = MasterArtifactTable.Instance.GetRarityArtifactCloneList(i);
		}
			
		PlayerStatus status = new PlayerStatus();

		if (string.IsNullOrEmpty(dungeonState) == false)
		{
			string dungeonId = PlayerPrefsManager.Instance.GetDungeonId();
			MapDataCarrier.Instance.DungeonData = MasterDungeonTable.Instance.GetData(dungeonId);

			// プレイヤーパラメータ初期化
			status = new PlayerStatus();
			MapDataCarrier.Instance.CuPlayerStatus = status;
			status.SetMaxHp(PlayerPrefsManager.Instance.GetSaveMaxHp());
			status.SetNowHp(PlayerPrefsManager.Instance.GetSaveNowHp());
			
			// TODO フロア設定
			MapDataCarrier.Instance.MaxFloor = MapDataCarrier.Instance.DungeonData.FloorCount;
			MapDataCarrier.Instance.NowFloor = PlayerPrefsManager.Instance.GetNowFloor();
		
			MapDataCarrier.Instance.AddDiceCost = PlayerPrefsManager.Instance.GetDiceCost();

			MapDataCarrier.Instance.ChestList = PlayerPrefsManager.Instance.GetChestList();

			MapDataCarrier.Instance.OriginalDeckList.AddRange(PlayerPrefsManager.Instance.GetOriginalDeckList().ToArray());

			var regularArtifacts = PlayerPrefsManager.Instance.GetArtifactList();
			for (int i = 0; i < regularArtifacts.Count; i++) {
				Scene.AddArtifactObject(regularArtifacts[i]);
				MapDataCarrier.Instance.RemoveRarityNoAcquiredArtifactList(regularArtifacts[i].Id);
			}
			
			MapDataCarrier.Instance.HandDifficultList.AddRange(PlayerPrefsManager.Instance.GetHandDifficultList().ToArray());
			MapDataCarrier.Instance.SelectDifficultIndex = PlayerPrefsManager.Instance.GetSelectDifficultIndex();
		}
		else
		{
			MapData sceneData = (MapData)LocalSceneManager.Instance.SceneData;
			MapDataCarrier.Instance.DungeonData = sceneData.Data;
		
			// 初回時
			PlayerPrefsManager.Instance.SaveDungeonId(MapDataCarrier.Instance.DungeonData.Id);
		
			for (int i = 0; i < Scene.DifficultImages.Length; i++) {
				MapDataCarrier.Instance.HandDifficultList.Add(-1);
			}

			// プレイヤーパラメータ初期化
			status = new PlayerStatus();
			MapDataCarrier.Instance.CuPlayerStatus = status;
			status.SetMaxHp(80);
			status.SetNowHp(80);
			PlayerPrefsManager.Instance.SaveSaveNowHp(status.GetNowHp());
			PlayerPrefsManager.Instance.SaveSaveMaxHp(status.GetMaxHp());
		
			// TODO フロア設定
			MapDataCarrier.Instance.MaxFloor = MapDataCarrier.Instance.DungeonData.FloorCount;
			MapDataCarrier.Instance.NowFloor = 1;
			
			MapDataCarrier.Instance.AddDiceCost = 0;
		
			// 初期デッキ構築
			MapDataCarrier.Instance.OriginalDeckList.Add(MasterAction2Table.Instance.GetData(1));
			MapDataCarrier.Instance.OriginalDeckList.Add(MasterAction2Table.Instance.GetData(1));
			MapDataCarrier.Instance.OriginalDeckList.Add(MasterAction2Table.Instance.GetData(1));
			MapDataCarrier.Instance.OriginalDeckList.Add(MasterAction2Table.Instance.GetData(1));
			MapDataCarrier.Instance.OriginalDeckList.Add(MasterAction2Table.Instance.GetData(1));
			MapDataCarrier.Instance.OriginalDeckList.Add(MasterAction2Table.Instance.GetData(101));
			MapDataCarrier.Instance.OriginalDeckList.Add(MasterAction2Table.Instance.GetData(101));
			MapDataCarrier.Instance.OriginalDeckList.Add(MasterAction2Table.Instance.GetData(101));
			MapDataCarrier.Instance.OriginalDeckList.Add(MasterAction2Table.Instance.GetData(101));
			MapDataCarrier.Instance.OriginalDeckList.Add(MasterAction2Table.Instance.GetData(101));
			
			//// TODO デバッグ用カード追加
			//MapDataCarrier.Instance.OriginalDeckList.Add(MasterAction2Table.Instance.GetData(19));
			//MapDataCarrier.Instance.OriginalDeckList.Add(MasterAction2Table.Instance.GetData(19));
			//MapDataCarrier.Instance.OriginalDeckList.Add(MasterAction2Table.Instance.GetData(19));
			//MapDataCarrier.Instance.OriginalDeckList.Add(MasterAction2Table.Instance.GetData(19));
			//MapDataCarrier.Instance.OriginalDeckList.Add(MasterAction2Table.Instance.GetData(19));
			//MapDataCarrier.Instance.OriginalDeckList.Add(MasterAction2Table.Instance.GetData(19));
			//MapDataCarrier.Instance.OriginalDeckList.Add(MasterAction2Table.Instance.GetData(406));
			//MapDataCarrier.Instance.OriginalDeckList.Add(MasterAction2Table.Instance.GetData(407));
			//MapDataCarrier.Instance.OriginalDeckList.Add(MasterAction2Table.Instance.GetData(408));
			//MapDataCarrier.Instance.OriginalDeckList.Add(MasterAction2Table.Instance.GetData(408));
			//MapDataCarrier.Instance.OriginalDeckList.Add(MasterAction2Table.Instance.GetData(409));
			//MapDataCarrier.Instance.OriginalDeckList.Add(MasterAction2Table.Instance.GetData(409));
			//MapDataCarrier.Instance.OriginalDeckList.Add(MasterAction2Table.Instance.GetData(410));
			//MapDataCarrier.Instance.OriginalDeckList.Add(MasterAction2Table.Instance.GetData(410));

			// レギュラーカード設定
			var regularCardIds = PlayerPrefsManager.Instance.GetRegularSettingCardIds();
			for (int i = 0; i < regularCardIds.Count; i++) {
				MapDataCarrier.Instance.OriginalDeckList.Add(MasterAction2Table.Instance.GetData(regularCardIds[i]));
			}
			
			PlayerPrefsManager.Instance.SaveOriginalDeckList(MapDataCarrier.Instance.OriginalDeckList);

			//// ここに、最初からアーティファクトを持たせて、効果を発揮できるようにする
			//data = MasterArtifactTable.Instance.GetData(1021);
			//Scene.AddArtifactObject(data);
			//MapDataCarrier.Instance.RemoveRarityNoAcquiredArtifactList(1021);

			//data = MasterArtifactTable.Instance.GetData(1022);
			//Scene.AddArtifactObject(data);
			//MapDataCarrier.Instance.RemoveRarityNoAcquiredArtifactList(1022);

			//data = MasterArtifactTable.Instance.GetData(1023);
			//Scene.AddArtifactObject(data);
			//MapDataCarrier.Instance.RemoveRarityNoAcquiredArtifactList(1023);

			//data = MasterArtifactTable.Instance.GetData(1040);
			//Scene.AddArtifactObject(data);
			//MapDataCarrier.Instance.RemoveRarityNoAcquiredArtifactList(1040);

			//data = MasterArtifactTable.Instance.GetData(1045);
			//Scene.AddArtifactObject(data);
			//MapDataCarrier.Instance.RemoveRarityNoAcquiredArtifactList(1045);
			//
			//data = MasterArtifactTable.Instance.GetData(1046);
			//Scene.AddArtifactObject(data);
			//MapDataCarrier.Instance.RemoveRarityNoAcquiredArtifactList(1046);
			
			// レギュラーアーティファクト設定
			MasterArtifactTable.Data data = null;

			var regularIds = PlayerPrefsManager.Instance.GetRegularSettingArtifactIds();
			for (int i = 0; i < regularIds.Count; i++) {
				data = MasterArtifactTable.Instance.GetData(regularIds[i]);
				Scene.AddArtifactObject(data);
				MapDataCarrier.Instance.RemoveRarityNoAcquiredArtifactList(regularIds[i]);
			}

			// 初期チェスト数を保存しておく。タイミングがここしかない為
			PlayerPrefsManager.Instance.SaveChestList(MapDataCarrier.Instance.ChestList);
		}

		Scene.UpdateOriginalDeckCountText();

		// 初期装備
/*		for (int i = 0; i < 6; i++) {
			int id = PlayerPrefsManager.Instance.GetRegularSettingCardId(i);
			status.SetActionData(i, MasterAction2Table.Instance.GetData(id));
		}*/
		// TODO 初期装備は、テストの為色々変えている
		//status.SetActionData(0, MasterAction2Table.Instance.GetData(101));
		//status.SetActionData(1, MasterAction2Table.Instance.GetData(101));
		//status.SetActionData(2, MasterAction2Table.Instance.GetData(101));
		//status.SetActionData(3, MasterAction2Table.Instance.GetData(101));
		//status.SetActionData(4, MasterAction2Table.Instance.GetData(9989));
		//status.SetActionData(5, MasterAction2Table.Instance.GetData(9989));

		status.SetMaxDiceCount(3);

		Scene.PlayerNowHpText.text = status.GetNowHp().ToString();
		Scene.PlayerMaxHpText.text = status.GetMaxHp().ToString();

		Scene.NowFloorText.text = MapDataCarrier.Instance.NowFloor.ToString();
		Scene.MaxFloorText.text = MapDataCarrier.Instance.MaxFloor.ToString();

		MapDataCarrier.Instance.IsClear = false;

		// バトルで使用する状態異常オブジェクトの初期化をしてしまう
		// なぜなら、バトル毎に生成する物ではない為
		//LoadPlayerPowerObjects();

		Scene.StartCoroutine(CoObjectLoad());

		SetText();
		Scene.UpdateChestCountDisplay();
		
		return true;
	}

	private IEnumerator CoObjectLoad() {
		int tipNo = UnityEngine.Random.Range(0, 10-1);
		SystemDialogManager.Instance.OpenLoading();
		SystemDialogManager.Instance.SetLoadingText(MasterStringTable.Instance.GetString(($"TIPS{tipNo}")));
		yield return LoadPlayerPowerObjects();
		yield return LoadPlayerTurnPowerObjects();
		yield return LoadEnemyPowerObjects();
		yield return LoadEnemyTurnPowerObjects();
		yield return LoadBattleCardObjects();
		yield return LoadEnemyValueObjects();
		yield return LoadPopupObjects();
		yield return LoadBgImage();
		SystemDialogManager.Instance.CloseLoading();

		FadeManager.Instance.FadeIn(0.5f, null);
		StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.UpdateDifficult);
	}

	
	private IEnumerator LoadPlayerPowerObjects() {
		int loadCount = (int)EnumSelf.PowerType.Max;
		int loadedCount = 0;

		for (int i = 0; i < loadCount; i++) {
			int index = i;
			ResourceManager.Instance.RequestExecuteOrder(
				Const.PowerControllerPath,
				ExecuteOrder.Type.GameObject,
				Scene.gameObject,
				(rawObject) => {
					GameObject obj = GameObject.Instantiate(rawObject) as GameObject;
					obj.GetComponent<PowerController>().Initialize((EnumSelf.PowerType)index, 0, Scene.PowerRoot, Scene.SpawnPopup);
					MapDataCarrier.Instance.PowerObjects.Add(obj);
					loadedCount++;
				}
			);
		}

		while (loadCount < loadedCount) {
			yield return null;
		}
	}
	
	private IEnumerator LoadPlayerTurnPowerObjects() {
		int loadCount = (int)EnumSelf.TurnPowerType.Max;
		int loadedCount = 0;

		for (int i = 0; i < loadCount; i++) {
			int index = i;
			ResourceManager.Instance.RequestExecuteOrder(
				Const.TurnPowerControllerPath,
				ExecuteOrder.Type.GameObject,
				Scene.gameObject,
				(rawObject) => {
					GameObject obj = GameObject.Instantiate(rawObject) as GameObject;
					obj.GetComponent<TurnPowerController>().Initialize((EnumSelf.TurnPowerType)index, 0, Scene.TurnPowerRoot, Scene.SpawnPopup);
					MapDataCarrier.Instance.TurnPowerObjects.Add(obj);
					loadedCount++;
				}
			);
		}
		
		while (loadCount < loadedCount) {
			yield return null;
		}
	}

	private IEnumerator LoadEnemyPowerObjects() {
		int loadCount = (int)EnumSelf.PowerType.Max;
		int loadedCount = 0;

		for (int i = 0; i < loadCount; i++) {
			int index = i;
			ResourceManager.Instance.RequestExecuteOrder(
				Const.PowerControllerPath,
				ExecuteOrder.Type.GameObject,
				Scene.gameObject,
				(rawObject) => {
					GameObject obj = GameObject.Instantiate(rawObject) as GameObject;
					obj.GetComponent<PowerController>().Initialize((EnumSelf.PowerType)index, 0, Scene.EnemyPowerRoot, Scene.SpawnPopup);
					MapDataCarrier.Instance.EnemyPowerObjects.Add(obj);
					loadedCount++;
				}
			);
		}
		
		while (loadCount < loadedCount) {
			yield return null;
		}
	}
	
	private IEnumerator LoadEnemyTurnPowerObjects() {
		int loadCount = (int)EnumSelf.TurnPowerType.Max;
		int loadedCount = 0;

		for (int i = 0; i < loadCount; i++) {
			int index = i;
			ResourceManager.Instance.RequestExecuteOrder(
				Const.TurnPowerControllerPath,
				ExecuteOrder.Type.GameObject,
				Scene.gameObject,
				(rawObject) => {
					GameObject obj = GameObject.Instantiate(rawObject) as GameObject;
					obj.GetComponent<TurnPowerController>().Initialize((EnumSelf.TurnPowerType)index, 0, Scene.EnemyTurnPowerRoot, Scene.SpawnPopup);
					MapDataCarrier.Instance.EnemyTurnPowerObjects.Add(obj);
					loadedCount++;
				}
			);
		}
		
		while (loadCount < loadedCount) {
			yield return null;
		}
	}

	//private IEnumerator LoadPlayerValueObjects() {

	//	int diceCount = 6;
	//	int objectCount = 10;
	//	
	//	int loadCount = diceCount * objectCount;
	//	int loadedCount = 0;

	//	// プレイヤーのValueObjectの初期生成
	//	for (int i = 0; i < diceCount; i++) {
	//		int index1 = i;
	//		// TODO こちらもとりあえず10個決め打ちで作っておく
	//		for (int i2 = 0; i2 < objectCount; i2++) {
	//			ResourceManager.Instance.RequestExecuteOrder(
	//				Const.ValueItemPath,
	//				ExecuteOrder.Type.GameObject,
	//				Scene.gameObject,
	//				(rawObject) => {
	//					GameObject obj = GameObject.Instantiate(rawObject) as GameObject;
	//					obj.GetComponent<ValueController>().Initialize(Scene.PlayerActionValueRoots[index1]);
	//					MapDataCarrier.Instance.ValueObjects[index1].Add(obj);
	//					loadedCount++;
	//				}
	//			);
	//		}
	//	}
	//	
	//	while (loadCount != loadedCount) {
	//		yield return null;
	//	}
	//}
	
	private IEnumerator LoadBattleCardObjects() {

		int cardCount = Const.MaxHand;

		// プレイヤーのバトルカード初期生成
		// 20個作っておいて、表示非表示、表示物更新をして使いまわす
		for (int i = 0; i < cardCount; i++) {
			ResourceManager.Instance.RequestExecuteOrder(
				Const.BattleCardButtonItemPath,
				ExecuteOrder.Type.GameObject,
				Scene.gameObject,
				InitializeBattleCardButtonController
			);
		}
		
		while (MapDataCarrier.Instance.BattleCardButtonControllers.Count < cardCount) {
			yield return null;
		}
	}

	private void InitializeBattleCardButtonController(UnityEngine.Object rawObject) {
		Scene.StartCoroutine(CoInitializeBattleCardButtonController(rawObject));
	}
	
	private IEnumerator CoInitializeBattleCardButtonController(UnityEngine.Object rawObject) {
		GameObject obj = GameObject.Instantiate(rawObject) as GameObject;
		obj.transform.SetParent(Scene.HandRoot.transform);
		obj.transform.localPosition = Vector3.zero;
		obj.transform.localScale = Vector3.one;
		BattleCardButtonController ctrl = obj.GetComponent<BattleCardButtonController>();
		yield return ctrl.Initialize(
			Scene.OnClickAttackButton,
			Scene.OnClickCarryCardDetailButton,
			Scene.UpdateHandSelectToggle
		);
		MapDataCarrier.Instance.BattleCardButtonControllers.Add(ctrl);
	}
	
	private IEnumerator LoadEnemyValueObjects() {
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
				Scene.gameObject,
				(rawObject) => {
					GameObject obj = GameObject.Instantiate(rawObject) as GameObject;
					obj.GetComponent<ValueController>().Initialize(Scene.EnemyActionValueRoot);
					MapDataCarrier.Instance.EnemyValueObjects.Add(obj);
					loadedCount++;
				}
			);
		}
		
		while (loadCount < loadedCount) {
			yield return null;
		}
	}
	
	private IEnumerator LoadPopupObjects() {
		int objectCount = 10;
		
		int loadCount = objectCount;
		int loadedCount = 0;

		for (int i = 0; i < objectCount; i++) {
			int index = i;
			ResourceManager.Instance.RequestExecuteOrder(
				Const.PopupObjectPath,
				ExecuteOrder.Type.GameObject,
				Scene.gameObject,
				(rawObject) => {
					GameObject obj = GameObject.Instantiate(rawObject) as GameObject;
					obj.transform.SetParent(Scene.PopupObjectRoot.transform);
					obj.transform.localPosition = Vector3.zero;
					obj.transform.localScale = Vector3.one;
					var ctrl = obj.GetComponent<PopupAnimationController>();
					ctrl.Initialize();
					MapDataCarrier.Instance.PopupAnimationControllers.Add(ctrl);
					loadedCount++;
				}
			);
		}
		
		while (loadCount < loadedCount) {
			yield return null;
		}
	}
	
	private IEnumerator LoadBgImage() {
		int loadCount = 1;
		int loadedCount = 0;

		ResourceManager.Instance.RequestExecuteOrder(
			MapDataCarrier.Instance.DungeonData.ImagePath,
			ExecuteOrder.Type.Sprite,
			Scene.gameObject,
			(rawSprite) => {
				Scene.BgImage.sprite = rawSprite as Sprite;
				loadedCount++;
			}
		);
		
		while (loadCount < loadedCount) {
			yield return null;
		}
	}

	private void SetText() {
		Scene.CarryCardDetailCloseButtonText.text = MasterStringTable.Instance.GetString("Common_Close");
		Scene.TreasureResultSkipButtonText.text = MasterStringTable.Instance.GetString("Common_Skip");
		Scene.TreasureResultDecideButtonText.text = MasterStringTable.Instance.GetString("Common_Decide");
		Scene.EventDetailText.text = MasterStringTable.Instance.GetString("Map_EventDetail");
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
	{
	}
}
