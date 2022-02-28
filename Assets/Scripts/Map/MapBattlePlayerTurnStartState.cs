using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBattlePlayerTurnStartState : StateBase {

	// TODO ここで、仮に死亡判定やダメージ判定などが必要になったら、
	// バリューチェンジ→チェックのフローを介す必要がある

	/// <summary>
	/// 初期化前処理.
	/// </summary>
	override public bool OnBeforeInit()
	{
		var scene = MapDataCarrier.Instance.Scene as MapScene;
		MapDataCarrier.Instance.SelectAttackIndex = -1;
		MapDataCarrier.Instance.BattleTurnCount++;
		
		MapDataCarrier.Instance.CurrentTotalDiceCost = 0;
		scene.UpdateCurrentTotalDiceCostText();

		BattleCalculationFunction.PlayerTurnStartValueChange();

		// カードを引く
		//デッキドロー数 = 6+n;
		//デッキドローカウント = 0;
		//while (デッキドローカウント < デッキドロー数) {
		//	if (デッキが0じゃなければ) {
		//		if (手札が20以上ではない) {
		//			デッキの先頭を取り出して、ハンドに追加
		//		} else {
		//			デッキの先頭を取り出して、トラッシュに追加
		//		}
		//		デッキドローカウント++;
		//	} else {
		//		if (トラッシュが0枚じゃない) {
		//			トラッシュを山札に入れて、シャッフルして、もう一度この処理を行う
		//		} else {
		//			引けるものが無いので、ここで処理終わりにしてもいいけど、
		//			フローをちゃんとする為に、デッキドローカウント++;
		//		}
		//	}
		//}

		int drawCount = Const.DrawCount;// 何か補正で引く枚数が増えたら、ここ調整する
		scene.DrawCard(drawCount);
		//int drewCount = 0;

		//var deckList = MapDataCarrier.Instance.BattleDeckList;
		//var trashList = MapDataCarrier.Instance.TrashList;

		//while (drewCount < drawCount) {
		//	MasterAction2Table.Data drawCard = null;
		//	if (deckList.Count > 0) {
		//		drawCard = deckList[0];
		//		deckList.RemoveAt(0);
        //        var ctrl = MapDataCarrier.Instance.GetNonActiveBattleCardController();
        //        if (ctrl != null) {
		//			ctrl.gameObject.SetActive(true);
		//			ctrl.SetData(drawCard);
		//			ctrl.UpdateDisplay();
		//			ctrl.UpdateInteractable(MapDataCarrier.Instance.CurrentTotalDiceCost);
		//		} else {
		//			trashList.Add(drawCard);
		//		}
		//		drewCount++;
		//	} else {
		//		if (trashList.Count > 0) {
		//			MapDataCarrier.Instance.DeckShuffle();
		//		} else {
		//			drewCount++;
		//		}
		//	}
		//}

		//scene.TrashCountText.text = trashList.Count.ToString();
		//scene.DeckCountText.text = deckList.Count.ToString();

		scene.UpdateParameterText();
		
		//// TODO もう少し呼び出す回数最適化できそうな気がする
		//for (int i = 0; i < 6; i++) {
		//	scene.UpdatePlayerValueObject(i);
		//}
		// 自分の弱体が終わると、敵の表示も変える必要があるので、ここでも更新する
		scene.UpdateEnemyValueObject();
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
