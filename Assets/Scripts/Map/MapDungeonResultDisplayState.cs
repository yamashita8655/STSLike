using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDungeonResultDisplayState : StateBase {

	private List<ChestObjectController> Ctrls = new List<ChestObjectController>();

    /// <summary>
    /// メイン前処理.
    /// 戻り値は、同一フレーム内で次の処理に移行してよければfalse、1フレーム飛ばして欲しい場合はfalse.
    /// </summary>
    override public bool OnBeforeMain()
    {
		var scene = MapDataCarrier.Instance.Scene as MapScene;
		scene.DungeonResultRoot.SetActive(true);
		
		int currentPoint = PlayerPrefsManager.Instance.GetPoint();
		int rewardPoint = MapDataCarrier.Instance.DungeonData.RewardPoint;

		if (MapDataCarrier.Instance.IsClear == true) {
			rewardPoint = MapDataCarrier.Instance.DungeonData.RewardPoint;
		} else {
			rewardPoint = MapDataCarrier.Instance.DungeonData.RewardPoint / 10;
		}

		scene.CurrentPointText.text = currentPoint.ToString();
		scene.GetPointText.text = rewardPoint.ToString();

		currentPoint += rewardPoint;

		PlayerPrefsManager.Instance.SavePoint(currentPoint);

		if (MapDataCarrier.Instance.IsClear == true) {
			scene.ResultText.text = "Clear!!";
		} else {
			scene.ResultText.text = "Death...";
		}

		// チェスト表示適用
		// TODO デバッグ加算
		//MapDataCarrier.Instance.ChestList[0] = 0;
		//MapDataCarrier.Instance.ChestList[1] = 0;
		//MapDataCarrier.Instance.ChestList[2] = 0;
		var list = MapDataCarrier.Instance.ChestList;
		int loadCount = list[0] + list[1] + list[2];
		int loadedCount = 0;
		for (int i = 0; i < list.Count; i++) {
			int count = list[i];
			for (int i2 = 0; i2 < count; i2++) {
				int rarity = i;
				ResourceManager.Instance.RequestExecuteOrder(
					Const.ChestObjectPath,
					ExecuteOrder.Type.GameObject,
					scene.gameObject,
					(rawObject) => {
						GameObject obj = GameObject.Instantiate(rawObject) as GameObject;
						var ctrl = obj.GetComponent<ChestObjectController>();
						ctrl.Initialize(rarity);
						obj.transform.SetParent(scene.ChestRoot.transform);
						obj.transform.localPosition = Vector3.zero;
						obj.transform.localScale = Vector3.one;
						Ctrls.Add(ctrl);
						loadedCount++;
						if (loadedCount == loadCount) {
							scene.StartCoroutine(CoLotChestReward());
						}
					}
				);
			}
		}

		return false;
    }

	private IEnumerator CoLotChestReward() {
		yield return new WaitForSeconds(1f);
		int addPoint = 0;
		for (int i = 0; i < Ctrls.Count; i++) {
			int rarity = Ctrls[i].GetRarity();
			// rarityは0からの添え字だが、マスターは1からなので、補正加算
			var data = MasterChestRewardLotTable.Instance.GetData(rarity+1);
			var ratioList = data.RewardRatios;
			int index = BattleCalculationFunction.LotRarity(ratioList);
			
			int val = data.RewardPoints[index-1];
			Ctrls[i].UpdateRewardText(val);
			addPoint += val;
		}
		int currentPoint = PlayerPrefsManager.Instance.GetPoint();
		currentPoint += addPoint;
		PlayerPrefsManager.Instance.SavePoint(currentPoint);
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
