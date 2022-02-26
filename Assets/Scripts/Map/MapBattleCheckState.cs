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
		var player = MapDataCarrier.Instance.CuPlayerStatus;
		var enemy = MapDataCarrier.Instance.CuEnemyStatus;

		IsWin = false;
		IsDead = false;

		if (enemy.IsDead()) {
			IsWin = true;
		}

		if (player.IsDead()) {
			// リバイブ効果を持っていたら、復活させる
			if (player.GetParameterListFlag(EnumSelf.ParameterType.Revive) == true) {
				player.SetNowHp(player.GetMaxHp()/2);
				scene.RemoveArtifactObject(EnumSelf.ParameterType.Revive);
				
				// TODO 使用済みフェニックスののうの数値決め打ち
				MasterArtifactTable.Data data = MasterArtifactTable.Instance.GetData(1001);
				scene.AddArtifactObject(data);
			} else {
				IsDead = true;
			}
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
					MapDataCarrier.Instance.CuPlayerStatus.RemoveInitiativeFirstActionData();
					StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattlePlayerInitiative);
				}
            } else if (StateMachineManager.Instance.GetPrevState(StateMachineName.Map) == (int)MapState.BattlePlayerTurnStart) {// プレイヤーのターンスタート時のHP変動
				StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleUpdateAttackButtonDisplay);
            } else if (StateMachineManager.Instance.GetPrevState(StateMachineName.Map) == (int)MapState.BattleEnemyInitiativeValueChange) {// 敵のイニシアチブ
				if (MapDataCarrier.Instance.EnemyInitiativeActionPackCount < MapDataCarrier.Instance.EnemyMaxInitiativeActionPackCount) {
					StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleEnemyInitiativeValueChange);
				} else {
					MapDataCarrier.Instance.CuEnemyStatus.RemoveInitiativeFirstActionData();
					StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleEnemyInitiative);
				}
            } else if (StateMachineManager.Instance.GetPrevState(StateMachineName.Map) == (int)MapState.BattleEnemyTurnStart) {// 敵のターンスタート時のHP変動
				StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleEnemyAttackResult);
			} else if (StateMachineManager.Instance.GetPrevState(StateMachineName.Map) == (int)MapState.BattleValueChange) {// プレイヤーのターンの場合

				// アクションパックの処理が全て終わっていなかったら、次のアクションパックの処理を行う
				if (MapDataCarrier.Instance.ActionPackCount < MapDataCarrier.Instance.MaxActionPackCount) {
					StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleValueChange);
				} else {
                    //int select = MapDataCarrier.Instance.SelectAttackIndex;
                    //if (select != -1) {
                    //	// 処理が終わったら、呪いのアクションだった場合は、基に戻す
                    //	MasterAction2Table.Data data = MapDataCarrier.Instance.CuPlayerStatus.GetActionData(select);
                    //	bool IsCurse = BattleCalculationFunction.IsCurse(data.Id);
                    //	if (IsCurse == true) {
                    //		MapDataCarrier.Instance.CuPlayerStatus.ResetActionData(select);
                    //	}
                    //}

                    //if (MapDataCarrier.Instance.DiceValueList.Count == 0) {
                    //	StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattlePlayerTurnEnd);
                    //} else {
                    //	StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleAttackSelectUserWait);
                    //}

                    // 捨て札に使ったカードを登録させる
                    var trashList = MapDataCarrier.Instance.TrashList;
                    trashList.Add(MapDataCarrier.Instance.SelectBattleCardButtonController.GetData());
					scene.TrashCountText.text = trashList.Count.ToString();
					
					StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleAttackSelectUserWait);
				}
				
				//for (int i = 0; i < 6; i++) {
				//	scene.UpdatePlayerValueObject(i);
				//}

				var ctrls = MapDataCarrier.Instance.BattleCardButtonControllers;
				for (int i = 0; i < ctrls.Count; i++) {
					if (ctrls[i].gameObject.activeSelf == true) {
						ctrls[i].UpdateDisplay();
					}
				}

				scene.UpdateEnemyValueObject();

			} else if (StateMachineManager.Instance.GetPrevState(StateMachineName.Map) == (int)MapState.BattleEnemyValueChange) {
				// アクションパックの処理が全て終わっていなかったら、次のアクションパックの処理を行う
				if (MapDataCarrier.Instance.EnemyActionPackCount < MapDataCarrier.Instance.EnemyMaxActionPackCount) {
					StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleEnemyValueChange);
				} else {
					StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleEnemyTurnEnd);
				}
				
				var ctrls = MapDataCarrier.Instance.BattleCardButtonControllers;
				for (int i = 0; i < ctrls.Count; i++) {
					if (ctrls[i].gameObject.activeSelf == true) {
						ctrls[i].UpdateDisplay();
					}
				}

				//for (int i = 0; i < 6; i++) {
				//	scene.UpdatePlayerValueObject(i);
				//}
				scene.UpdateEnemyValueObject();

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
