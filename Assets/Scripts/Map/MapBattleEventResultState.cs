using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBattleEventResultState : StateBase {

    /// <summary>
    /// メイン前処理.
    /// 戻り値は、同一フレーム内で次の処理に移行してよければfalse、1フレーム飛ばして欲しい場合はfalse.
    /// </summary>
    override public bool OnBeforeMain()
    {
		var scene = MapDataCarrier.Instance.Scene as MapScene;

		// とりあえず0番目決め打ち
		int dice = MapDataCarrier.Instance.DiceValueList[0];

		if (dice == 0) {
			StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleInitialize);
		} else if (dice == 1) {
			StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.ArtifactInitialize);
		} else if (dice == 2) {
			StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.HealInitialize);
		} else if (dice == 3) {
			// 戦闘をすっ飛ばすので、敵データをイベント用の設定だけした敵を指定しておく(エラー回避用)
			MapDataCarrier.Instance.CuEnemyStatus = new EnemyStatus(MasterEnemyTable.Instance.GetData(99999));
			StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.ResultInitialize);
		} else if (dice == 4) {
			// 10個下の階層の敵から抽選
			MapDataCarrier.Instance.EventBattleFloorAdd = -10;
			StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleInitialize);

		} else if (dice == 5) {
			// 10個上の階層の敵から抽選
			MapDataCarrier.Instance.EventBattleFloorAdd = 10;
			StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleInitialize);
		}

		scene.EventRoot.SetActive(false);
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
