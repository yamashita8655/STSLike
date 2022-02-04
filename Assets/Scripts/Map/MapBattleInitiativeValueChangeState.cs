using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBattleInitiativeValueChangeState : StateBase {

    /// <summary>
    /// メイン前処理.
    /// 戻り値は、同一フレーム内で次の処理に移行してよければfalse、1フレーム飛ばして欲しい場合はfalse.
    /// </summary>
    override public bool OnBeforeMain()
    {
		var scene = MapDataCarrier.Instance.Scene as MapScene;

		MasterAction2Table.Data data = MapDataCarrier.Instance.CuPlayerStatus.GetInitiativeFirstActionData();

		int count = MapDataCarrier.Instance.InitiativeActionPackCount;
		ActionPack pack = data.ActionPackList[count]; 

		if (pack.Effect == EnumSelf.EffectType.Damage) {
			BattleCalculationFunction.PlayerCalcDamageNormalDamage(pack);
		} else if (pack.Effect == EnumSelf.EffectType.Heal) {
			BattleCalculationFunction.PlayerCalcHeal(pack);
		} else if (pack.Effect == EnumSelf.EffectType.Shield) {
			BattleCalculationFunction.PlayerCalcShield(pack);
		}

		MapDataCarrier.Instance.InitiativeActionPackCount++;

		scene.UpdateParameterText();
		return false;
    }

    /// <summary>
    /// メイン更新処理.
    /// </summary>
    /// <param name="delta">経過時間</param>
    override public void OnUpdateMain(float delta)
    {
		StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleCheck);
    }

    /// <summary>
    /// ステート解放時処理.
    /// </summary>
    override public void OnRelease()
    {
    }
}
