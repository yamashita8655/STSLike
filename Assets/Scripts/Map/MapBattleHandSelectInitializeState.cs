using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBattleHandSelectInitializeState : StateBase {

    /// <summary>
    /// メイン前処理.
    /// 戻り値は、同一フレーム内で次の処理に移行してよければfalse、1フレーム飛ばして欲しい場合はfalse.
    /// </summary>
    override public bool OnBeforeMain()
    {
		var scene = MapDataCarrier.Instance.Scene as MapScene;
		var player = MapDataCarrier.Instance.CuPlayerStatus;

		MasterAction2Table.Data data = MapDataCarrier.Instance.SelectBattleCardData;

		var ctrls = MapDataCarrier.Instance.BattleCardButtonControllers;
		for (int i = 0; i < ctrls.Count; i++) {
			ctrls[i].UpdateToggleActive(true);
		}

		int count = MapDataCarrier.Instance.ActionPackCount;
		ActionPack pack = data.ActionPackList[count]; 

		int maxCount = pack.Value;
		int handCount = MapDataCarrier.Instance.GetHandCount();
		if (maxCount >= handCount) {
			maxCount = handCount;
		}
		scene.HandCardSelectText.text = string.Format("説明文\n0/{0}", maxCount.ToString());

		scene.HandCardSelectDecideButton.interactable = false;

		scene.HandCardSelectRoot.SetActive(true);
		scene.HandCardSelectDecideButton.gameObject.SetActive(true);
		return true;
    }

    /// <summary>
    /// メイン更新処理.
    /// </summary>
    /// <param name="delta">経過時間</param>
    override public void OnUpdateMain(float delta)
    {
		StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleHandSelectUserWait);
    }

    /// <summary>
    /// ステート解放時処理.
    /// </summary>
    override public void OnRelease()
    {
    }
}
