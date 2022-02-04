using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBattleEnemyInitiativeValueChangeState : StateBase {

    /// <summary>
    /// メイン前処理.
    /// 戻り値は、同一フレーム内で次の処理に移行してよければfalse、1フレーム飛ばして欲しい場合はfalse.
    /// </summary>
    override public bool OnBeforeMain()
    {
		var scene = MapDataCarrier.Instance.Scene as MapScene;

		EnemyStatus enemy = MapDataCarrier.Instance.CuEnemyStatus;
		MasterAction2Table.Data data = enemy.GetInitiativeFirstActionData();

		int count = MapDataCarrier.Instance.EnemyInitiativeActionPackCount;
		ActionPack pack = data.ActionPackList[count]; 

		if (pack.Effect == EnumSelf.EffectType.Damage) {
			BattleCalculationFunction.EnemyCalcDamageNormalDamage(pack);
		} else if (pack.Effect == EnumSelf.EffectType.Heal) {
			BattleCalculationFunction.EnemyCalcHeal(pack);
		} else if (pack.Effect == EnumSelf.EffectType.Shield) {
			BattleCalculationFunction.EnemyCalcShield(pack);
		} else if (
			(pack.Effect == EnumSelf.EffectType.Strength) ||
			(pack.Effect == EnumSelf.EffectType.Regenerate)
		) {
			BattleCalculationFunction.EnemyUpdatePower(pack);
		} else if (pack.Effect == EnumSelf.EffectType.DiceMinusOne) {
			BattleCalculationFunction.EnemyUpdateTurnPower(pack);
		}

		MapDataCarrier.Instance.EnemyInitiativeActionPackCount++;

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
