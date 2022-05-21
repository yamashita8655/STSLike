using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDungeonResultOpenChestState : StateBase {

    /// <summary>
    /// メイン前処理.
    /// 戻り値は、同一フレーム内で次の処理に移行してよければfalse、1フレーム飛ばして欲しい場合はfalse.
    /// </summary>
    override public bool OnBeforeMain()
    {
		var scene = MapDataCarrier.Instance.Scene as MapScene;
		scene.StartCoroutine(CoLotChestReward());

		return false;
    }
	
	private IEnumerator CoLotChestReward() {
		yield return new WaitForSeconds(1f);
		int addPoint = 0;
		for (int i = 0; i < MapDataCarrier.Instance.ResultChestCtrls.Count; i++) {
			int rarity = MapDataCarrier.Instance.ResultChestCtrls[i].GetRarity();
			// rarityは0からの添え字だが、マスターは1からなので、補正加算
			var data = MasterChestRewardLotTable.Instance.GetData(rarity+1);
			var ratioList = data.RewardRatios;
			int index = BattleCalculationFunction.LotRarity(ratioList);
			
			int val = data.RewardPoints[index-1];
			MapDataCarrier.Instance.ResultChestCtrls[i].UpdateRewardText(val);
			addPoint += val;
		}
		int currentPoint = PlayerPrefsManager.Instance.GetPoint();
		currentPoint += addPoint;
		PlayerPrefsManager.Instance.SavePoint(currentPoint);
		
		StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.DungeonResultUserWait);
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
