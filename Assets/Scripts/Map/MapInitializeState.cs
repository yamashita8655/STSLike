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
		Scene = MapDataCarrier.Instance.Scene as MapScene;
		
		Scene.BattleRoot.SetActive(false);
		Scene.ResultRoot.SetActive(false);
		Scene.ChangeRoot.SetActive(false);
		Scene.HealRoot.SetActive(false);
		Scene.MapRoot.SetActive(true);
		Scene.DungeonResultRoot.SetActive(false);
		Scene.ArtifactRoot.SetActive(false);

		Scene.CarryArtifactDetailController.Close();
		Scene.CarryCardDetailController.Close();
		Scene.EraseCardDetailController.Close();
		
		Scene.HandCardRoot.SetActive(false);
		Scene.HandCardSelectRoot.SetActive(false);
		Scene.HandCardSelectDecideButton.gameObject.SetActive(false);
		Scene.HandCardSelectDecideButton.interactable = false;

		MapDataCarrier.Instance.HandDifficultList.Clear();
		for (int i = 0; i < Scene.DifficultImages.Length; i++) {
			MapDataCarrier.Instance.HandDifficultList.Add(-1);
		}

		// プレイヤーパラメータ初期化
		PlayerStatus status = new PlayerStatus();
		MapDataCarrier.Instance.CuPlayerStatus = status;
		status.SetMaxHp(80);
		status.SetNowHp(80);
		
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
		
		// TODO デバッグ用カード追加
		MapDataCarrier.Instance.OriginalDeckList.Add(MasterAction2Table.Instance.GetData(601));
		MapDataCarrier.Instance.OriginalDeckList.Add(MasterAction2Table.Instance.GetData(602));
		MapDataCarrier.Instance.OriginalDeckList.Add(MasterAction2Table.Instance.GetData(603));
		MapDataCarrier.Instance.OriginalDeckList.Add(MasterAction2Table.Instance.GetData(604));
		MapDataCarrier.Instance.OriginalDeckList.Add(MasterAction2Table.Instance.GetData(605));
		MapDataCarrier.Instance.OriginalDeckList.Add(MasterAction2Table.Instance.GetData(606));
		MapDataCarrier.Instance.OriginalDeckList.Add(MasterAction2Table.Instance.GetData(607));
		MapDataCarrier.Instance.OriginalDeckList.Add(MasterAction2Table.Instance.GetData(608));
		MapDataCarrier.Instance.OriginalDeckList.Add(MasterAction2Table.Instance.GetData(609));
		

		// レギュラーカード設定
		var regularIds = PlayerPrefsManager.Instance.GetRegularSettingCardIds();
		for (int i = 0; i < regularIds.Count; i++) {
			MapDataCarrier.Instance.OriginalDeckList.Add(MasterAction2Table.Instance.GetData(regularIds[i]));
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

		// 未獲得アーティファクトリストを取得しておく
		for (int i = 1; i < 6; i++) {
			MapDataCarrier.Instance.RarityNoAcquiredArtifactList[i-1] = MasterArtifactTable.Instance.GetRarityArtifactCloneList(i);
		}

		// TODO アーティファクト効果テスト
		// ここに、最初からアーティファクトを持たせて、効果を発揮できるようにする
		MasterArtifactTable.Data data = MasterArtifactTable.Instance.GetData(1046);
		Scene.AddArtifactObject(data);
		MapDataCarrier.Instance.RemoveRarityNoAcquiredArtifactList(1046);

		//data = MasterArtifactTable.Instance.GetData(1022);
		//Scene.AddArtifactObject(data);
		//MapDataCarrier.Instance.RemoveRarityNoAcquiredArtifactList(1022);

		//data = MasterArtifactTable.Instance.GetData(1023);
		//Scene.AddArtifactObject(data);
		//MapDataCarrier.Instance.RemoveRarityNoAcquiredArtifactList(1023);

		//data = MasterArtifactTable.Instance.GetData(4);
		//Scene.AddArtifactObject(data);
		//MapDataCarrier.Instance.RemoveRarityNoAcquiredArtifactList(4);

		//data = MasterArtifactTable.Instance.GetData(1000);
		//Scene.AddArtifactObject(data);
		//MapDataCarrier.Instance.RemoveRarityNoAcquiredArtifactList(1000);

		status.SetMaxDiceCount(3);

		Scene.PlayerNowHpText.text = status.GetNowHp().ToString();
		Scene.PlayerMaxHpText.text = status.GetMaxHp().ToString();

		// TODO フロア設定
		MapDataCarrier.Instance.MaxFloor = MapDataCarrier.Instance.DungeonData.FloorCount;
		MapDataCarrier.Instance.NowFloor = 1;

		Scene.NowFloorText.text = MapDataCarrier.Instance.NowFloor.ToString();
		Scene.MaxFloorText.text = MapDataCarrier.Instance.MaxFloor.ToString();

		MapDataCarrier.Instance.IsClear = false;

		// バトルで使用する状態異常オブジェクトの初期化をしてしまう
		// なぜなら、バトル毎に生成する物ではない為
		//LoadPlayerPowerObjects();

		Scene.StartCoroutine(CoObjectLoad());
		
		return true;
	}

	private IEnumerator CoObjectLoad() {
		yield return LoadPlayerPowerObjects();
		yield return LoadPlayerTurnPowerObjects();
		yield return LoadEnemyPowerObjects();
		yield return LoadEnemyTurnPowerObjects();
		yield return LoadBattleCardObjects();
		yield return LoadEnemyValueObjects();
		yield return LoadBgImage();

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
					obj.GetComponent<PowerController>().Initialize((EnumSelf.PowerType)index, 0, Scene.PowerRoot);
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
					obj.GetComponent<TurnPowerController>().Initialize((EnumSelf.TurnPowerType)index, 0, Scene.TurnPowerRoot);
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
					obj.GetComponent<PowerController>().Initialize((EnumSelf.PowerType)index, 0, Scene.EnemyPowerRoot);
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
					obj.GetComponent<TurnPowerController>().Initialize((EnumSelf.TurnPowerType)index, 0, Scene.EnemyTurnPowerRoot);
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
