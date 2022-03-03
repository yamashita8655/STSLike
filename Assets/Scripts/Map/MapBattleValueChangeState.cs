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
		if (pack.Effect == EnumSelf.EffectType.Draw) {
			scene.DrawCard(pack.Value);
		}
		
		if (pack.Effect == EnumSelf.EffectType.GainDiceCost) {
			MapDataCarrier.Instance.CurrentTotalDiceCost += pack.Value;
			scene.UpdateCurrentTotalDiceCostText();
		}

		MapDataCarrier.Instance.ActionPackCount++;

		scene.UpdateParameterText();

		return true;
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
