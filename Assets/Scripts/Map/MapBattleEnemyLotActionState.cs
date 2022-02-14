using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBattleEnemyLotActionState : StateBase {

    /// <summary>
    /// メイン前処理.
    /// 戻り値は、同一フレーム内で次の処理に移行してよければfalse、1フレーム飛ばして欲しい場合はfalse.
    /// </summary>
    override public bool OnBeforeMain()
    {
		var scene = MapDataCarrier.Instance.Scene as MapScene;

		// 抽選をして、初めて行動が確定する
		MapDataCarrier.Instance.CuEnemyStatus.LotActionData();

		// 抽選時に、HP以下かどうかでAIが変わるなら、それで上書き
		MapDataCarrier.Instance.CuEnemyStatus.CheckAIForLotHpBorder();

		scene.UpdateEnemyValueObject();

		return false;
    }

    /// <summary>
    /// メイン更新処理.
    /// </summary>
    /// <param name="delta">経過時間</param>
    override public void OnUpdateMain(float delta)
    {
		StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattlePlayerTurnStart);
    }

    /// <summary>
    /// ステート解放時処理.
    /// </summary>
    override public void OnRelease()
    {
    }
}
