using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBattleUpdateAttackButtonDisplayState : StateBase {

	/// <summary>
	/// 初期化前処理.
	/// </summary>
	override public bool OnBeforeInit()
	{
		var scene = MapDataCarrier.Instance.Scene as MapScene;

		for (int i = 0; i < scene.PlayerActionButtons.Length; i++) {
			scene.PlayerActionButtons[i].interactable = false;
		}

		for (int i = 0; i < MapDataCarrier.Instance.DiceValueList.Count; i++) {
			int val = MapDataCarrier.Instance.DiceValueList[i];
			scene.PlayerActionButtons[val].interactable = true;
		}
		
		// ダイスイメージの更新
		for (int i = 0; i < scene.DiceImages.Length; i++) {
			scene.DiceImages[i].gameObject.SetActive(false);
		}

		for (int i = 0; i < MapDataCarrier.Instance.DiceValueList.Count; i++) {
			scene.DiceImages[i].gameObject.SetActive(true);
			int val = MapDataCarrier.Instance.DiceValueList[i];
			scene.DiceImages[i].sprite = scene.DiceSprites[val];
		}

		return true;
	}

	/// <summary>
	/// メイン更新処理.
	/// </summary>
	/// <param name="delta">経過時間</param>
	override public void OnUpdateMain(float delta)
	{
		if (StateMachineManager.Instance.GetPrevState(StateMachineName.Map) == (int)MapState.BattleCheck) {
			StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleDiceRollUserWait);
		} else if (StateMachineManager.Instance.GetPrevState(StateMachineName.Map) == (int)MapState.BattleDiceRoll) {
			StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleAttackSelectUserWait);
		} else if (StateMachineManager.Instance.GetPrevState(StateMachineName.Map) == (int)MapState.BattleAttackSelectUserWait) {
			StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleAttackResult);
		}
	}

	/// <summary>
	/// ステート解放時処理.
	/// </summary>
	override public void OnRelease()
	{
	}
}
