using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBattleStartState : StateBase {

    /// <summary>
    /// メイン前処理.
    /// 戻り値は、同一フレーム内で次の処理に移行してよければfalse、1フレーム飛ばして欲しい場合はfalse.
    /// </summary>
    override public bool OnBeforeMain()
    {
		var scene = MapDataCarrier.Instance.Scene as MapScene;
		PlayerStatus player = MapDataCarrier.Instance.CuPlayerStatus;

		if (player.GetParameterListFlag(EnumSelf.ParameterType.GameStart3Draw) == true) {
			scene.DrawCard(3);
		}
		
		if (player.GetParameterListFlag(EnumSelf.ParameterType.HeroSword) == true) {
			if (player.GetNowHp() == player.GetMaxHp()) {
				BattleCalculationFunction.PlayerUpdatePower(EnumSelf.PowerType.Strength, 3);
				BattleCalculationFunction.PlayerUpdatePower(EnumSelf.PowerType.Toughness, 3);
			}
		}
		
		if (player.GetParameterListFlag(EnumSelf.ParameterType.HeroShield) == true) {
			if (player.GetNowHp() <= 20) {
				BattleCalculationFunction.PlayerUpdateTurnPower(EnumSelf.TurnPowerType.TurnRegenerate, 10);
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
		StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattlePlayerInitiative);
    }

    /// <summary>
    /// ステート解放時処理.
    /// </summary>
    override public void OnRelease()
    {
    }
}
