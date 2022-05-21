using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDungeonResultDisplayState : StateBase {

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

		scene.AdmobButton.interactable = true;
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

		// TODO デバッグ加算
		//MapDataCarrier.Instance.ChestList[0] = 1;
		//MapDataCarrier.Instance.ChestList[1] = 1;
		//MapDataCarrier.Instance.ChestList[2] = 1;
		
		var list = MapDataCarrier.Instance.ChestList;
		int loadCount = list[0] + list[1] + list[2];
		if (MapDataCarrier.Instance.IsClear == true) {
			if (loadCount > 0) {
				scene.AdmobChestButton.gameObject.SetActive(false);
				StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.DungeonResultCreateChest);
			} else {
				scene.AdmobChestButton.gameObject.SetActive(false);
				StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.DungeonResultUserWait);
			}
		} else {
			scene.AdmobChestButton.interactable = true;
			if (loadCount > 0) {
				scene.AdmobChestButton.gameObject.SetActive(true);
				StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.DungeonResultCreateChest);
			} else {
				scene.AdmobChestButton.gameObject.SetActive(false);
				StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.DungeonResultUserWait);
			}
		}

		return false;
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
