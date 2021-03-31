using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBattleInitializeState : StateBase {

	/// <summary>
	/// 初期化前処理.
	/// </summary>
	override public bool OnBeforeInit()
	{
		var scene = MapDataCarrier.Instance.Scene as MapScene;
		scene.BattleRoot.SetActive(true);
		scene.MapRoot.SetActive(false);

		MapDataCarrier.Instance.DiceValueList.Clear();

		// プレイヤーのアクション設定
		PlayerStatus status = MapDataCarrier.Instance.CuPlayerStatus;
		for (int i = 0; i < scene.PlayerActionNameStrings.Length; i++) {
			MasterActionTable.Data pdata = status.GetActionData(i);
			scene.PlayerActionNameStrings[i].text = pdata.Name;
			scene.PlayerActionValueStrings[i].text = pdata.Value1.ToString();
		}

		// 敵出現
		int enemyId = 1;
		MasterEnemyTable.Data data = MasterEnemyTable.Instance.GetData(enemyId);
		EnemyStatus enemy = new EnemyStatus();
		enemy.SetMaxHp(data.MHp);
		enemy.SetNowHp(data.Hp);
		// TODO とりあえず、一個目決め打ちで
		MasterActionTable.Data actionData = MasterActionTable.Instance.GetData(data.ActionId1);
		enemy.AddActionData(actionData);
		MapDataCarrier.Instance.CuEnemyStatus = enemy;

		scene.EnemyNowHpText.text = data.Hp.ToString();
		scene.EnemyMaxHpText.text = data.MHp.ToString();
		scene.EnemyNameText.text = data.Name;

		// TODO とりあえず、敵の行動はここで一回決め打ちして動かさない
		scene.EnemyActionText.text = actionData.Name;
		scene.EnemyActionValueText.text = actionData.Value1.ToString();

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
