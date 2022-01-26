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

		MapDataCarrier.Instance.DiceValueList.Clear();

		// プレイヤーのアクション設定
		PlayerStatus player = MapDataCarrier.Instance.CuPlayerStatus;
		player.SetMaxShield(999999);
		player.SetNowShield(0);
		scene.PlayerShieldText.text = "";
		for (int i = 0; i < scene.PlayerActionNameStrings.Length; i++) {
			MasterActionTable.Data pdata = player.GetActionData(i);
			scene.PlayerActionNameStrings[i].text = pdata.Name;
			scene.PlayerActionValueStrings[i].text = pdata.Value1.ToString();
		}

		// 敵出現
		// TODO ID決め打ち
		//int enemyId = 3;
		int enemyId = LotEnemyId();
		MasterEnemyTable.Data data = MasterEnemyTable.Instance.GetData(enemyId);
		EnemyStatus enemy = new EnemyStatus();
		enemy.SetMaxHp(data.MHp);
		enemy.SetNowHp(data.Hp);
		enemy.SetMaxShield(999999);
		enemy.SetNowShield(0);
		scene.EnemyShieldText.text = "";
		// TODO とりあえず、一個目決め打ちで
		MasterActionTable.Data actionData = MasterActionTable.Instance.GetData(data.ActionId1);
		enemy.AddActionData(actionData);
		MapDataCarrier.Instance.CuEnemyStatus = enemy;

		scene.EnemyNowHpText.text = data.Hp.ToString();
		scene.EnemyMaxHpText.text = data.MHp.ToString();
		scene.EnemyNameText.text = data.Name;

		// TODO 本当は、読み込み待ちした方が良い
		ResourceManager.Instance.RequestExecuteOrder(
			"Image/Map/Enemy/" + data.ImagePath,
			ExecuteOrder.Type.Sprite,
			scene.gameObject,
			(spriteObj) => {
				scene.EnemyImage.sprite = spriteObj as Sprite;
			}
		);

		// TODO とりあえず、敵の行動はここで一回決め打ちして動かさない
		scene.EnemyActionText.text = actionData.Name;
		scene.EnemyActionValueText.text = actionData.Value1.ToString();

		return true;
	}

	private int LotEnemyId() {
		int enemyId = 0;

		// 今の階層から、使用する抽選IDを取得
		int nowFloor = MapDataCarrier.Instance.NowFloor;
		int lotId = 0;
		MasterDungeonTable.Data dungeonData = MapDataCarrier.Instance.DungeonData;
		var lotFloors = dungeonData.LotFloors;
		var enemyLotIds = dungeonData.EnemyLotIds;
		for (int i = 0; i < lotFloors.Count; i++) {
			if (nowFloor <= lotFloors[i]) {
				lotId = enemyLotIds[i];
				break;
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


		int weight = UnityEngine.Random.Range(0, allWeight+1);
		Debug.Log("weight:" + weight);

		int startWeight = 0;
		int endWeight = enemyLotData.LotWeights[0]-1;
		for (int i = 0; i < enemyLotData.LotWeights.Count; i++) {
			Debug.Log("startWeight:" + startWeight);
			Debug.Log("endWeight:" + endWeight);
			if ((startWeight < weight) && (weight < endWeight)) {
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
		StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattlePlayerTurnStart);
	}

	/// <summary>
	/// ステート解放時処理.
	/// </summary>
	override public void OnRelease()
	{
	}
}
