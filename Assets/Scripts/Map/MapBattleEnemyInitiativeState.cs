using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBattleEnemyInitiativeState : StateBase {

    /// <summary>
    /// メイン前処理.
    /// 戻り値は、同一フレーム内で次の処理に移行してよければfalse、1フレーム飛ばして欲しい場合はfalse.
    /// </summary>
    override public bool OnBeforeMain()
    {
		var scene = MapDataCarrier.Instance.Scene as MapScene;
		MapDataCarrier.Instance.EnemyInitiativeActionPackCount = 0;

		EnemyStatus enemy = MapDataCarrier.Instance.CuEnemyStatus;
		MasterAction2Table.Data data = enemy.GetInitiativeFirstActionData();

		if (data != null) {
			MapDataCarrier.Instance.EnemyMaxInitiativeActionPackCount = data.ActionPackList.Count;
			StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleEnemyInitiativeValueChange);
		} else {
			StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleEnemyLotAction);
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
