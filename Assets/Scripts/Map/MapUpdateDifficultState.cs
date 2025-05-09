﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapUpdateDifficultState : StateBase {

	private readonly int Level1Ratio = 220;
	private readonly int Level2Ratio = 220;
	private readonly int Level3Ratio = 330;
	private readonly int Level4Ratio = 150;
	private readonly int Level5Ratio = 80;
	//private readonly int Level1Ratio = 0;
	//private readonly int Level2Ratio = 0;
	//private readonly int Level3Ratio = 0;
	//private readonly int Level4Ratio = 0;
	//private readonly int Level5Ratio = 200;

    /// <summary>
    /// メイン前処理.
    /// </summary>
    override public bool OnBeforeMain()
    {
		var scene = MapDataCarrier.Instance.Scene as MapScene;
		PlayerStatus player = MapDataCarrier.Instance.CuPlayerStatus;
		player.SetMaxShield(0);
		player.SetNowShield(0);
		scene.PlayerShieldText.text = "";

		for (int i = 0; i < MapDataCarrier.Instance.HandDifficultList.Count; i++) {
			if (MapDataCarrier.Instance.HandDifficultList[i] == -1) {
				int difficult = LotteryDiffucult();
				MapDataCarrier.Instance.HandDifficultList[i] = difficult;
				scene.DifficultImages[i].sprite = scene.LevelSprites[difficult];
			}
		}
		
		// モモンガの尻尾処理
		// 本来なら、獲得した時に変えた方がいいんだけど、そうするとフローがごちゃごちゃになるので
		// モモンガ取得→そのフロー中にここで、モモンガ処理→モモンガ使用済みと交換
		// というフローで強引に解決
		// データ保存、途中復帰する際に、データ保存タイミングを気を付ける事
		if (player.GetParameterListFlag(EnumSelf.ParameterType.Momonga) == true) {
			scene.RemoveArtifactObject(EnumSelf.ParameterType.Momonga);
			// TODO 使用済みモモンガの数値決め打ち
			MasterArtifactTable.Data data = MasterArtifactTable.Instance.GetData(1029);
			scene.AddArtifactObject(data);

			// 全て5に上書き
			for (int i = 0; i < MapDataCarrier.Instance.HandDifficultList.Count; i++) {
				MapDataCarrier.Instance.HandDifficultList[i] = 4;
				scene.DifficultImages[i].sprite = scene.LevelSprites[4];
			}
		}

		return false;
    }
	
	private int LotteryDiffucult() {
		int seedEnd = (
			Level1Ratio +
			Level2Ratio +
			Level3Ratio +
			Level4Ratio +
			Level5Ratio
		);

		int seed = UnityEngine.Random.Range(0, seedEnd);
		int difficult = 0;

		int startBorder = 0;
		int endBorder = Level1Ratio;

		if (startBorder <= seed && seed < endBorder) {
			difficult = 0;
		}

		startBorder = endBorder;
		endBorder += Level2Ratio;
		if (startBorder <= seed && seed < endBorder) {
			difficult = 1;
		}
		
		startBorder = endBorder;
		endBorder += Level3Ratio;
		if (startBorder <= seed && seed < endBorder) {
			difficult = 2;
		}
		
		startBorder = endBorder;
		endBorder += Level4Ratio;
		if (startBorder <= seed && seed < endBorder) {
			difficult = 3;
		}
		
		startBorder = endBorder;
		endBorder += Level5Ratio;
		if (startBorder <= seed && seed < endBorder) {
			difficult = 4;
		}

		return difficult;
	}

    /// <summary>
    /// メイン更新処理.
    /// </summary>
    /// <param name="delta">経過時間</param>
    override public void OnUpdateMain(float delta)
    {
		StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.UpdateMap);
    }

    /// <summary>
    /// ステート解放時処理.
    /// </summary>
    override public void OnRelease()
    {
    }
}
