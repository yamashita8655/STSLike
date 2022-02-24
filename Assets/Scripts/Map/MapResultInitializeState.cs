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
		List<int> weightList = lotData.LotList[difficult];
			
		// TODO treasureカウント3決め打ち
		MapDataCarrier.Instance.TreasureList.Clear();
		for (int i = 0; i < 3; i++) {
			int rarity = BattleCalculationFunction.LotRarity(weightList);
			var cardList = MasterAction2Table.Instance.GetRarityCardCloneList(rarity);
			
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

			// 見つけたIDリストに加える
			PlayerPrefsManager.Instance.SaveFindCardId(id);
		}

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
