using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBattleInitializeState : StateBase {

	/// <summary>
	/// 初期化前処理.
	/// </summary>
	override public bool OnBeforeInit()
	{
		var scene = MapDataCarrier.Instance.Scene as MapScene;
		scene.BattleRoot.SetActive(true);
		scene.MapRoot.SetActive(false);
		scene.TurnEndButtonObject.SetActive(false);
		
		scene.HandCardRoot.SetActive(true);

		scene.AddDiceCostText.text = MapDataCarrier.Instance.AddDiceCost.ToString();

		MapDataCarrier.Instance.DiceValueList.Clear();
		MapDataCarrier.Instance.BattleTurnCount = 0;
		MapDataCarrier.Instance.SelectBattleCardData = null;
		MapDataCarrier.Instance.DoubleAttackBattleCardData = null;
		MapDataCarrier.Instance.CuPlayerStatus.SetBattleUseCardCount(0);
		MapDataCarrier.Instance.CuPlayerStatus.SetTotalSelfTrueDamage(0);

		// 手札の非表示
		var handList = MapDataCarrier.Instance.BattleCardButtonControllers;
		for (int i = 0; i < handList.Count; i++) {
			handList[i].gameObject.SetActive(false);
		}

		// オリジナルデッキを、戦闘用デッキにコピー
		MapDataCarrier.Instance.CopyDeck();

		// 前回のトラッシュと破棄をクリア
		MapDataCarrier.Instance.TrashList.Clear();
		MapDataCarrier.Instance.DiscardList.Clear();
		
		// デッキをシャッフル
		MapDataCarrier.Instance.DeckShuffle();

		// プレイヤーのアクション設定
		PlayerStatus player = MapDataCarrier.Instance.CuPlayerStatus;
		player.SetMaxShield(999999);
		player.SetNowShield(0);
		scene.PlayerShieldText.text = "";


		// プレイヤーのイニシアチブ設定
		// 主に、アーティファクトによる、ターン開始時に掛かるバフ系
		var artifactList = MapDataCarrier.Instance.CarryArtifactList;
		for (int i = 0; i < artifactList.Count; i++) {
			if (artifactList[i].GetData().ActionId != 0) {
				MasterAction2Table.Data actionData = MasterAction2Table.Instance.GetData(artifactList[i].GetData().ActionId);
				player.AddInitiativeActionData(actionData);
			}
		}

		// プレイヤーバフの初期化
		player.ResetPowerAll();
		player.ResetTurnPower();
		// バフ表示の初期化
		for (int i = 0; i < (int)EnumSelf.PowerType.Max; i++) {
			MapDataCarrier.Instance.PowerObjects[i].GetComponent<PowerController>().SetValue(0);
		}
		for (int i = 0; i < (int)EnumSelf.TurnPowerType.Max; i++) {
			MapDataCarrier.Instance.TurnPowerObjects[i].GetComponent<TurnPowerController>().SetTurn(0);
		}

		// 敵出現
		int enemyId = LotEnemyId();
		MasterEnemyTable.Data data = MasterEnemyTable.Instance.GetData(enemyId);
		EnemyStatus enemy = new EnemyStatus(data);
		int difficultHpRate = 0;

		int nowFloor = MapDataCarrier.Instance.NowFloor;
		int maxFloor = MapDataCarrier.Instance.MaxFloor;

		// ボス部屋じゃなければ、難易度によるHP補正をかける
		if (nowFloor != maxFloor) {
			if (MapDataCarrier.Instance.SelectDifficultNumber == 0) {
				difficultHpRate = 20;
			} else if (MapDataCarrier.Instance.SelectDifficultNumber == 1) {
				difficultHpRate = 10;
			} else if (MapDataCarrier.Instance.SelectDifficultNumber == 3) {
				difficultHpRate = -33;
			}
		}

		int mHp = data.MHp - (data.MHp * difficultHpRate / 100);
		int hp = data.Hp - (data.Hp * difficultHpRate / 100);
		enemy.SetMaxHp(mHp);
		enemy.SetNowHp(hp);
		enemy.SetMaxShield(999999);
		enemy.SetNowShield(0);
		// 敵バフの初期化
		enemy.ResetPowerAll();
		enemy.ResetTurnPower();
		// バフ表示の初期化
		for (int i = 0; i < (int)EnumSelf.PowerType.Max; i++) {
			MapDataCarrier.Instance.EnemyPowerObjects[i].GetComponent<PowerController>().SetValue(0);
		}
		for (int i = 0; i < (int)EnumSelf.TurnPowerType.Max; i++) {
			MapDataCarrier.Instance.EnemyTurnPowerObjects[i].GetComponent<TurnPowerController>().SetTurn(0);
		}
		scene.EnemyShieldText.text = "";

		// 敵AIの初期化
		MasterEnemyAITable.Data aiData = MasterEnemyAITable.Instance.GetData(data.AIID);
		enemy.UpdateAIData(aiData);

		//for (int i = 0; i < data.ActionIds.Count; i++) {
		//	MasterAction2Table.Data actionData = MasterAction2Table.Instance.GetData(data.ActionIds[i]);
		//	enemy.AddActionData(actionData);
		//}

		// TODO イニシアチブアクションは、別ステートで持った方がいいかも
		string initiativeActionId = data.InitiativeActionId;
		if (initiativeActionId != "NONE") {
			enemy.AddInitiativeActionData(MasterAction2Table.Instance.GetData(int.Parse(initiativeActionId)));
		}

		MapDataCarrier.Instance.CuEnemyStatus = enemy;

		scene.EnemyNowHpText.text = hp.ToString();
		scene.EnemyMaxHpText.text = mHp.ToString();
		scene.EnemyNameText.text = data.Name;


		// TODO 本当は、読み込み待ちした方が良い
		ResourceManager.Instance.RequestExecuteOrder(
			data.ImagePath,
			ExecuteOrder.Type.Sprite,
			scene.gameObject,
			(spriteObj) => {
				scene.EnemyImage.sprite = spriteObj as Sprite;
			}
		);
		
		// 表示の初期化は、内部で敵のバフの状況も見る場合があるので、
		// 敵の情報が出来てから行う
		// TODO 初期化用と、行動後用と、処理分離する必要が出てくるかもしれないね。
		//for (int i = 0; i < 6; i++) {
		//	scene.UpdatePlayerValueObject(i);
		//}

		return true;
	}

	private int LotEnemyId() {
		int enemyId = 0;

		// 今の階層から、使用する抽選IDを取得
		int nowFloor = MapDataCarrier.Instance.NowFloor;
		int maxFloor = MapDataCarrier.Instance.MaxFloor;
		int lotId = 0;


		MasterDungeonTable.Data dungeonData = MapDataCarrier.Instance.DungeonData;

		if (MapDataCarrier.Instance.SelectDifficultNumber == 4) {
			if (nowFloor == maxFloor) {
				// TODO とりあえず、今は5が選択されても、最後の部屋は通常のボスの抽選IDを使う
				lotId = dungeonData.BossLotId;
			} else {
				lotId = dungeonData.EliteLotId;
			}
		} else {
			if (nowFloor == maxFloor) {
				// 最後の部屋なら、ボスの抽選IDを使う
				lotId = dungeonData.BossLotId;
			} else {
				var lotFloors = dungeonData.LotFloors;
				var enemyLotIds = dungeonData.EnemyLotIds;
				for (int i = 0; i < lotFloors.Count; i++) {
					if (nowFloor <= lotFloors[i]) {
						lotId = enemyLotIds[i];
						break;
					}
				}
			}
		}


		Debug.Log("lotId:" + lotId);

		// 抽選番号から、敵のIDを抽選
		MasterEnemyLotTable.Data enemyLotData = MasterEnemyLotTable.Instance.GetData(lotId);
		int allWeight = 0;
		for (int i = 0; i < enemyLotData.LotWeights.Count; i++) {
			allWeight += enemyLotData.LotWeights[i];
		}
		
		Debug.Log("allWeight:" + allWeight);


		int weight = UnityEngine.Random.Range(0, allWeight);
		Debug.Log("weight:" + weight);

		int startWeight = 0;
		int endWeight = enemyLotData.LotWeights[0]-1;
		for (int i = 0; i < enemyLotData.LotWeights.Count; i++) {
			Debug.Log("startWeight:" + startWeight);
			Debug.Log("endWeight:" + endWeight);
			if ((startWeight <= weight) && (weight <= endWeight)) {
				enemyId = enemyLotData.EnemyIds[i];
				break;
			}

			startWeight = endWeight+1;
			endWeight = startWeight + enemyLotData.LotWeights[i+1]-1;
		}

		return enemyId;
	}

	/// <summary>
	/// メイン更新処理.
	/// </summary>
	/// <param name="delta">経過時間</param>
	override public void OnUpdateMain(float delta)
	{
		//StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleEnemyLotAction);
		StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattlePlayerInitiative);
	}

	/// <summary>
	/// ステート解放時処理.
	/// </summary>
	override public void OnRelease()
	{
	}
}
