using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapArtifactInitializeState : StateBase {

    /// <summary>
    /// メイン前処理.
    /// 戻り値は、同一フレーム内で次の処理に移行してよければfalse、1フレーム飛ばして欲しい場合はfalse.
    /// </summary>
    override public bool OnBeforeMain()
    {
		var scene = MapDataCarrier.Instance.Scene as MapScene;
		
		scene.ArtifactRoot.SetActive(true);
		scene.MapRoot.SetActive(false);

		scene.ArtifactDecideButton.interactable = false;
		
		scene.ArtifactNameText.gameObject.SetActive(false);
		scene.ArtifactImage.gameObject.SetActive(false);
		scene.ArtifactDetailText.gameObject.SetActive(false);
		
		MapDataCarrier.Instance.SelectArtifactIndex = -1;
		
		int difficult = MapDataCarrier.Instance.SelectDifficultNumber;

		// TODO とりあえず1番目のレシオセットを固定で使う
		MasterArtifactLotTable.Data lotData = MasterArtifactLotTable.Instance.GetData("1");

		List<int> weightList = lotData.LotList[difficult];
			
		// TODO Artifactカウント3決め打ち
		MapDataCarrier.Instance.ArtifactList.Clear();
		
		for (int i = 0; i < 3; i++) {
			int id = 0;
			bool noArtifact = false;
			int rarity = BattleCalculationFunction.LotRarity(weightList);
			List<int> artifactCloneList = null;
			while (true) {
				artifactCloneList = new List<int>(MapDataCarrier.Instance.RarityNoAcquiredArtifactList[rarity-1]);
				// リストが空じゃなかった場合も、こっちはカードと違って重複が許されないので、
				// 予め、抽選リストから今回抽選済みのアーティファクトを削除しておく
				for (int i2 = 0; i2 < i; i2++) {
					for (int i3 = 0; i3 < artifactCloneList.Count; i3++) {
						if (MapDataCarrier.Instance.ArtifactList[i2].Id == artifactCloneList[i3]) {
							artifactCloneList.RemoveAt(i3);
							break;
						}
					}
				}

				if (artifactCloneList.Count == 0) {
					rarity--;
					if (rarity == 0) {
						noArtifact = true;
						break;
					}
				} else {
					break;
				}
			}

			if (noArtifact == true) {
				// アーティファクト抽選テーブルに、アーティファクトが存在しない（つまり、全て獲得済み）であれば、
				// 強制的に、2000番（やり込み証明書）を獲得する
				id = 2000;
			} else {
				int index = UnityEngine.Random.Range(0, artifactCloneList.Count);
				id = artifactCloneList[index];
			}
				
			MasterArtifactTable.Data data = MasterArtifactTable.Instance.GetData(id);
			MapDataCarrier.Instance.ArtifactList.Add(data);
			scene.ArtifactTexts[i].text = data.Name;
			int index2 = i;
			ResourceManager.Instance.RequestExecuteOrder(
				string.Format(Const.RarityFrameImagePath, data.Rarity),
				ExecuteOrder.Type.Sprite,
				scene.gameObject,
				(rawSprite) => {
					scene.ArtifactRarityImages[index2].sprite = rawSprite as Sprite;
				}
			);
			
			// 見つけたIDリストに加える
			PlayerPrefsManager.Instance.SaveFindArtifactId(id);
		}

		PlayerPrefsManager.Instance.SetDungeonState("RewardWait");
		return true;
    }

	private int LotArtifactId() {
		int id = 0;

		return id;
	}

    /// <summary>
    /// メイン更新処理.
    /// </summary>
    /// <param name="delta">経過時間</param>
    override public void OnUpdateMain(float delta)
    {
		StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.ArtifactDisplay);
    }

    /// <summary>
    /// ステート解放時処理.
    /// </summary>
    override public void OnRelease()
    {
    }
}
