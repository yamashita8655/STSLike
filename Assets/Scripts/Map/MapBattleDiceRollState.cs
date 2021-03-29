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

		for (int i = 0; i < scene.DiceImages.Length; i++) {
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
		}

		return true;
	}

	/// <summary>
	/// メイン更新処理.
	/// </summary>
	/// <param name="delta">経過時間</param>
	override public void OnUpdateMain(float delta)
	{
		StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleAttackSelectUserWait);
	}

	/// <summary>
	/// ステート解放時処理.
	/// </summary>
	override public void OnRelease()
	{
	}
}
