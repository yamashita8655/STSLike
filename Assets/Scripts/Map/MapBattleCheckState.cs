using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBattleCheckState : StateBase {

	private bool IsWin = false;
	private bool IsDead = false;

	/// <summary>
	/// 初期化前処理.
	/// </summary>
	override public bool OnBeforeInit()
	{
		var scene = MapDataCarrier.Instance.Scene as MapScene;

		IsWin = false;
		IsDead = false;

		if (MapDataCarrier.Instance.CuEnemyStatus.IsDead()) {
			IsWin = true;
		}

		if (MapDataCarrier.Instance.CuPlayerStatus.IsDead()) {
			IsDead = true;
		}

		return true;
	}

	/// <summary>
	/// メイン更新処理.
	/// </summary>
	/// <param name="delta">経過時間</param>
	override public void OnUpdateMain(float delta)
	{
		var scene = MapDataCarrier.Instance.Scene as MapScene;

		// 自分が負ける判定が先に来る
		if (IsDead) {
			StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleLose);
		} else if (IsWin) {
			StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleWin);
		} else {
            if (StateMachineManager.Instance.GetPrevState(StateMachineName.Map) == (int)MapState.BattleInitiativeValueChange) {// プレイヤーのイニシアチブ
				if (MapDataCarrier.Instance.InitiativeActionPackCount < MapDataCarrier.Instance.MaxInitiativeActionPackCount) {
					StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleInitiativeValueChange);
				} else {
					StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattlePlayerInitiative);
				}
            } else if (StateMachineManager.Instance.GetPrevState(StateMachineName.Map) == (int)MapState.BattlePlayerTurnStart) {// プレイヤーのターンスタート時のHP変動
				StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleUpdateAttackButtonDisplay);
            } else if (StateMachineManager.Instance.GetPrevState(StateMachineName.Map) == (int)MapState.BattleEnemyInitiativeValueChange) {// 敵のイニシアチブ
				if (MapDataCarrier.Instance.EnemyInitiativeActionPackCount < MapDataCarrier.Instance.EnemyInitiativeActionPackCount) {
					StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleInitiativeValueChange);
				} else {
					MapDataCarrier.Instance.CuEnemyStatus.RemoveInitiativeFirstActionData();
					StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleEnemyInitiative);
				}
            } else if (StateMachineManager.Instance.GetPrevState(StateMachineName.Map) == (int)MapState.BattleEnemyTurnStart) {// 敵のターンスタート時のHP変動
				StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleEnemyAttackResult);
			} else if (StateMachineManager.Instance.GetPrevState(StateMachineName.Map) == (int)MapState.BattleValueChange) {// プレイヤーのターンの場合

				for (int i = 0; i < 6; i++) {
					scene.UpdatePlayerValueObject(i);
				}
				scene.UpdateEnemyValueObject();

				// アクションパックの処理が全て終わっていなかったら、次のアクションパックの処理を行う
				if (MapDataCarrier.Instance.ActionPackCount < MapDataCarrier.Instance.MaxActionPackCount) {
					StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleValueChange);
				} else {
					if (MapDataCarrier.Instance.DiceValueList.Count == 0) {
						StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattlePlayerTurnEnd);
					} else {
						StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleAttackSelectUserWait);
					}
				}
			} else if (StateMachineManager.Instance.GetPrevState(StateMachineName.Map) == (int)MapState.BattleEnemyValueChange) {
				for (int i = 0; i < 6; i++) {
					scene.UpdatePlayerValueObject(i);
				}
				scene.UpdateEnemyValueObject();

				// アクションパックの処理が全て終わっていなかったら、次のアクションパックの処理を行う
				if (MapDataCarrier.Instance.EnemyActionPackCount < MapDataCarrier.Instance.EnemyMaxActionPackCount) {
					StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleEnemyValueChange);
				} else {
					StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleEnemyTurnEnd);
				}
			}
		}
		//StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleCheck);
	}

	/// <summary>
	/// ステート解放時処理.
	/// </summary>
	override public void OnRelease()
	{
	}
}

//public class MapBattleCheckState : StateBase {
//
//	private bool IsWin = false;
//	private bool IsDead = false;
//
//	/// <summary>
//	/// 初期化前処理.
//	/// </summary>
//	override public bool OnBeforeInit()
//	{
//		var scene = MapDataCarrier.Instance.Scene as MapScene;
//
//		IsWin = false;
//		IsDead = false;
//
//		if (MapDataCarrier.Instance.CuEnemyStatus.IsDead()) {
//			IsWin = true;
//		}
//
//		if (MapDataCarrier.Instance.CuPlayerStatus.IsDead()) {
//			IsDead = true;
//		}
//
//		return true;
//	}
//
//	/// <summary>
//	/// メイン更新処理.
//	/// </summary>
//	/// <param name="delta">経過時間</param>
//	override public void OnUpdateMain(float delta)
//	{
//		if (IsWin) {
//			StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleWin);
//		} else if (IsDead) {
//			StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleLose);
//		} else {
//			// プレイヤーのターンの場合
//			if (StateMachineManager.Instance.GetPrevState(StateMachineName.Map) == (int)MapState.BattleValueChange) {
//
//				// 連続攻撃回数が残っていたら、もう一回ダメージ計算
//				bool isContinuous = false;
//				if (MapDataCarrier.Instance.MaxContinuousCount != 0) {
//					if (MapDataCarrier.Instance.ContinuousCount < MapDataCarrier.Instance.MaxContinuousCount) {
//						StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleValueChange);
//						isContinuous = true;
//					}
//				}
//
//				if (isContinuous == false) {
//					if (MapDataCarrier.Instance.DiceValueList.Count == 0) {
//						StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleEnemyTurnStart);
//					} else {
//						StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleAttackSelectUserWait);
//					}
//				}
//			} else if (StateMachineManager.Instance.GetPrevState(StateMachineName.Map) == (int)MapState.BattleEnemyValueChange) {
//
//				// 連続攻撃回数が残っていたら、もう一回ダメージ計算
//				bool isContinuous = false;
//				if (MapDataCarrier.Instance.EnemyMaxContinuousCount != 0) {
//					if (MapDataCarrier.Instance.EnemyContinuousCount < MapDataCarrier.Instance.EnemyMaxContinuousCount) {
//						StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleEnemyValueChange);
//						isContinuous = true;
//					}
//				}
//
//				if (isContinuous == false) {
//					StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattlePlayerTurnStart);
//				}
//			}
//		}
//		//StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleCheck);
//	}
//
//	/// <summary>
//	/// ステート解放時処理.
//	/// </summary>
//	override public void OnRelease()
//	{
//	}
//}
