using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBattleWinState : StateBase {

	/// <summary>
	/// 初期化前処理.
	/// </summary>
	override public bool OnBeforeInit()
	{
		var scene = MapDataCarrier.Instance.Scene as MapScene;
		var enemyId = MapDataCarrier.Instance.CuEnemyStatus.GetEnemyData().Id;

		// ここで宝箱の抽選を行う
		// まずは、宝箱の獲得抽選
		MasterDungeonTable.Data dungeonData = MapDataCarrier.Instance.DungeonData;
		int nowFloor = MapDataCarrier.Instance.NowFloor;
		int maxFloor = MapDataCarrier.Instance.MaxFloor;
		bool isBoss = false;
		bool isElite = false;
		bool isEnemy = false;
		if (nowFloor == maxFloor) {
			isBoss = true;
		} else {
			if (MapDataCarrier.Instance.SelectDifficultNumber == 4) {
				isElite = true;
			} else {
				isEnemy = true;
			}
		}

		int chestGetRatio = 0;
		if (isBoss == true) {
			chestGetRatio = dungeonData.BossChestDropRatio;
		}
		if (isElite == true) {
			chestGetRatio = dungeonData.EliteChestDropRatio;
		}
		if (isEnemy == true) {
			if ((enemyId / 10000) == 9) {
				chestGetRatio = 100;
			} else {
				chestGetRatio = dungeonData.EnemyChestDropRatio;
			}
		}

		bool getChest = UnityEngine.Random.Range(0, 100+1) <= chestGetRatio ? true : false;

		if (getChest == true) {
			// 宝箱の中身抽選をする
			List<int> ratioList = null;
			if (isBoss == true) {
				ratioList = dungeonData.BossChestRarityLotRatio;
			}
			if (isElite == true) {
				ratioList = dungeonData.EliteChestRarityLotRatio;
			}
			if (isEnemy == true) {
				ratioList = dungeonData.EnemyChestRarityLotRatio;
			}

			int rarity = BattleCalculationFunction.LotRarity(ratioList); 

			// rarityは1から始まりで返ってくるので、1引く
			MapDataCarrier.Instance.ChestList[rarity-1]++;

			scene.UpdateChestCountDisplay();
		}

		// 撃破数保存
		PlayerPrefsManager.Instance.AddEnemyKillCount(enemyId, 1);
		
		return true;
	}

	/// <summary>
	/// メイン更新処理.
	/// </summary>
	/// <param name="delta">経過時間</param>
	override public void OnUpdateMain(float delta)
	{
		StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleEnd);
	}

	/// <summary>
	/// ステート解放時処理.
	/// </summary>
	override public void OnRelease()
	{
	}
}
