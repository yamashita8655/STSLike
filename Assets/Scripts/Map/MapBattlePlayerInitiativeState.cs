using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBattlePlayerInitiativeState : StateBase {

    /// <summary>
    /// メイン前処理.
    /// 戻り値は、同一フレーム内で次の処理に移行してよければfalse、1フレーム飛ばして欲しい場合はfalse.
    /// </summary>
    override public bool OnBeforeMain()
    {
		var scene = MapDataCarrier.Instance.Scene as MapScene;

		BattleCalculationFunction.PlayerInitiativeValueChange();

		
		if (MapDataCarrier.Instance.CuPlayerStatus.GetParameterListFlag(EnumSelf.ParameterType.GameStart3Draw) == true) {
			scene.DrawCard(3);
		}

		MasterAction2Table.Data data = MapDataCarrier.Instance.CuPlayerStatus.GetInitiativeFirstActionData();
		MapDataCarrier.Instance.InitiativeActionPackCount = 0;
		PlayerStatus player = MapDataCarrier.Instance.CuPlayerStatus;
		if (data != null) {
			MapDataCarrier.Instance.MaxInitiativeActionPackCount = data.ActionPackList.Count;
			StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleInitiativeValueChange);
		} else {
			StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleEnemyInitiative);
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
