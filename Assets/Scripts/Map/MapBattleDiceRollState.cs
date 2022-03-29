using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBattleDiceRollState : StateBase {

	/// <summary>
	/// 初期化前処理.
	/// </summary>
	override public bool OnBeforeInit()
	{
		var scene = MapDataCarrier.Instance.Scene as MapScene;
		scene.DiceRollButton.SetActive(false);
		scene.TurnEndButtonObject.SetActive(true);
		
		MapDataCarrier.Instance.DiceValueList.Clear();

		for (int i = 0; i < scene.DiceImages.Length; i++) {
			scene.DiceImages[i].gameObject.SetActive(false);
		}

		// ダイスのデバフ処理
		PlayerStatus player = MapDataCarrier.Instance.CuPlayerStatus;
		int diceCount = player.GetMaxDiceCount();
		if (player.GetTurnPowerValue(EnumSelf.TurnPowerType.DiceMinusOne) > 0) {
			diceCount--;
			if (diceCount < 0) {
				diceCount = 0;
			}
		}
		for (int i = 0; i < diceCount; i++) {
			scene.DiceImages[i].gameObject.SetActive(true);
			int seed = UnityEngine.Random.Range(0, 6000);
			int dice = 0;

			if (0 <= seed && seed < 1000) {
				dice = 0;
			} else if (1000 <= seed && seed < 2000) {
				dice = 1;
			} else if (2000 <= seed && seed < 3000) {
				dice = 2;
			} else if (3000 <= seed && seed < 4000) {
				dice = 3;
			} else if (4000 <= seed && seed < 5000) {
				dice = 4;
			} else if (5000 <= seed && seed < 6000) {
				dice = 5;
			}

			scene.DiceImages[i].sprite = scene.DiceSprites[dice];

			MapDataCarrier.Instance.DiceValueList.Add(dice);
		}

		int total = 0;
		for (int i = 0; i < MapDataCarrier.Instance.DiceValueList.Count; i++) {
			// ダイスの目は添え字扱いになっているので、数値として扱う為の+1
			total += (MapDataCarrier.Instance.DiceValueList[i]+1);
		}
		
		// ここじゃないと、他の加算値が加算された量のシールドになるので、ここでダイスシールド効果発動
		if (player.GetParameterListFlag(EnumSelf.ParameterType.DiceShield) == true) {
			BattleCalculationFunction.PlayerCalcShield(total);
			scene.UpdateParameterText();
		}
		
		if (player.GetPower().GetValue(EnumSelf.PowerType.AddMaxDiceCost) != 0) {
			total += player.GetPower().GetValue(EnumSelf.PowerType.AddMaxDiceCost);
			// 一応、減少の可能性もあるので、下限チェック
			if (total < 0) {
				total = 0;
			}
		}
		
		if (player.GetTurnPowerValue(EnumSelf.TurnPowerType.Activity) > 0) {
			int val = player.GetTurnPowerValue(EnumSelf.TurnPowerType.Activity);
			total += val;
			BattleCalculationFunction.PlayerUpdateTurnPower(EnumSelf.TurnPowerType.Activity, -val);
		}

		total += MapDataCarrier.Instance.AddDiceCost;
		
		MapDataCarrier.Instance.CurrentTotalDiceCost = total;
		scene.UpdateCurrentTotalDiceCostText();

		return true;
	}

	/// <summary>
	/// メイン更新処理.
	/// </summary>
	/// <param name="delta">経過時間</param>
	override public void OnUpdateMain(float delta)
	{
		StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleUpdateAttackButtonDisplay);
	}

	/// <summary>
	/// ステート解放時処理.
	/// </summary>
	override public void OnRelease()
	{
	}
}
