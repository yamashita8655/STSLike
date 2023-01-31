using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapResultInitializeState : StateBase {

	/// <summary>
	/// 初期化前処理.
	/// </summary>
	override public bool OnBeforeInit()
	{
		var scene = MapDataCarrier.Instance.Scene as MapScene;
		scene.TreasureDecideButton.interactable = false;
		
		scene.TreasureDetailCardName.gameObject.SetActive(false);
		scene.TreasureDetailCardImage.gameObject.SetActive(false);
		scene.TreasureDetailCardDetail.gameObject.SetActive(false);
		scene.TreasureDiceCostText.gameObject.SetActive(false);

		MapDataCarrier.Instance.SelectTreasureIndex = -1;
				
		scene.TreasureRarityFrameImage.sprite = null;

		int difficult = MapDataCarrier.Instance.SelectDifficultNumber;

		// 戦乙女のお守りを持っていたら（UpgradeRewardがON）difficultを1上げる(=戦闘報酬の中身が良くなる)
		if (MapDataCarrier.Instance.CuPlayerStatus.GetParameterListFlag(EnumSelf.ParameterType.UpgradeReward) == true) {
			difficult++;
			if (difficult >= 4) {
				difficult = 4;
			}
		}

		// TODO とりあえず1番目のレシオセットを固定で使う
		MasterCardLotTable.Data lotData = MasterCardLotTable.Instance.GetData("1");
		//MasterCardLotTable.Data lotData = MasterCardLotTable.Instance.GetData("999");
		List<int> weightList = lotData.LotList[difficult];
			
		// TODO treasureカウント3決め打ち
		MapDataCarrier.Instance.TreasureList.Clear();

		var enemyData = MapDataCarrier.Instance.CuEnemyStatus.GetEnemyData();
		List<int> addDropActionIds = enemyData.AddDropActionIds;

		for (int i = 0; i < 3; i++) {
			int rarity = BattleCalculationFunction.LotRarity(weightList);

			List<int> cardList = null;
			cardList = MasterAction2Table.Instance.GetBattleRarityCardCloneList(rarity);

			// 難易度5（添え字だと4）の場合は、該当するレアリティの追加カードをロットテーブルに追加する
			if (difficult == 4) {
				for (int i2 = 0; i2 < addDropActionIds.Count; i2++) {
					var cardData = MasterAction2Table.Instance.GetData(addDropActionIds[i2]);
					if (cardData.Rarity == rarity) {
						cardList.Add(cardData.Id);
					}
				}
			}
			
			int id = 0;
			while (true) {
				int index = UnityEngine.Random.Range(0, cardList.Count);
				id = cardList[index];
				bool isSame = false;
				for (int i2 = 0; i2 < MapDataCarrier.Instance.TreasureList.Count; i2++) {
					if (MapDataCarrier.Instance.TreasureList[i2].Id == id) {
						isSame = true;
						break;
					}
				}
				if (isSame == false) {
					break;
				}
			}
			MasterAction2Table.Data data = MasterAction2Table.Instance.GetData(id);
			MapDataCarrier.Instance.TreasureList.Add(data);
			scene.TreasureNameTexts[i].text = data.Name;

			int index2 = i;
		
			ResourceManager.Instance.RequestExecuteOrder(
				string.Format(Const.RarityFrameImagePath, data.Rarity),
				ExecuteOrder.Type.Sprite,
				scene.gameObject,
				(rawSprite) => {
					scene.TreasureButtonRarityFrameImages[index2].sprite = rawSprite as Sprite;
				}
			);

			// 見つけたIDリストに加える
			PlayerPrefsManager.Instance.SaveFindCardId(id);
		}
		
		PlayerPrefsManager.Instance.SaveTreasureList(MapDataCarrier.Instance.TreasureList);

		PlayerPrefsManager.Instance.SetDungeonState("RewardWait");

		return true;
	}

	/// <summary>
	/// メイン更新処理.
	/// </summary>
	/// <param name="delta">経過時間</param>
	override public void OnUpdateMain(float delta)
	{
		StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.ResultTreasureDisplay);
	}

	/// <summary>
	/// ステート解放時処理.
	/// </summary>
	override public void OnRelease()
	{
	}
}
