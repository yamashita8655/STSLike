using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBattleValueChangeState : StateBase {

	/// <summary>
	/// 初期化前処理.
	/// </summary>
	override public bool OnBeforeInit()
	{
		var scene = MapDataCarrier.Instance.Scene as MapScene;
		var player = MapDataCarrier.Instance.CuPlayerStatus;

		MasterAction2Table.Data data = MapDataCarrier.Instance.SelectBattleCardData;

		int count = MapDataCarrier.Instance.ActionPackCount;
		ActionPack pack = data.ActionPackList[count]; 
		
		BattleCalculationFunction.PlayerValueChange(pack);

		// TODO もしかしたら、こういう副次的な効果も、全てアクションパックに含めた方がいいのかもしれない
		if (player.GetParameterListFlag(EnumSelf.ParameterType.UseCurseShield) == true) {
			if (BattleCalculationFunction.IsCurse(data.Id) == true) {
				BattleCalculationFunction.PlayerCalcShield(4);
			}
		}

		// TODO 戦闘のパラメータに関係しない効果は、こっちで処理する
		bool showHandCardSelect = false;
		if (pack.Effect == EnumSelf.EffectType.Draw) {
			if (player.GetTurnPowerValue(EnumSelf.TurnPowerType.NonDraw) == 0) {
				// ドロー不可デバフがついてなければ、処理できる。
				scene.DrawCard(pack.Value);
			}
		}
		
		if (pack.Effect == EnumSelf.EffectType.GainDiceCost) {
			MapDataCarrier.Instance.CurrentTotalDiceCost += pack.Value;
			scene.UpdateCurrentTotalDiceCostText();
		}
		
		if (
			(pack.Effect == EnumSelf.EffectType.Hand2DeckTop) ||
			(pack.Effect == EnumSelf.EffectType.Hand2Discard) ||
			(pack.Effect == EnumSelf.EffectType.Hand2Erase) ||
			(pack.Effect == EnumSelf.EffectType.Hand2Trash)
		) {

			if (MapDataCarrier.Instance.GetHandCount() > 0) {
				showHandCardSelect = true;
			}
		}

		scene.UpdateParameterText();

		if (showHandCardSelect == true) {
			StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleHandSelectInitialize);
		} else {
			StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleCheck);
		}

		return true;
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
