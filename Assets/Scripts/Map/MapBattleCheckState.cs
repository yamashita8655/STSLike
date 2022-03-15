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
			}  else {
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
		
		var player = MapDataCarrier.Instance.CuPlayerStatus;

		// 自分が負ける判定が先に来る
		if (IsDead) {
			StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleLose);
		} else if (IsWin) {
			MasterAction2Table.Data data = MapDataCarrier.Instance.SelectBattleCardData;
			// 敵のターンで敵が死んだときに、NULLになる
			if (data != null) {
				int count = MapDataCarrier.Instance.ActionPackCount;
				ActionPack pack = data.ActionPackList[count]; 
				if (pack.Effect == EnumSelf.EffectType.DamageGainMaxHp) {
					// TODO 固定値決めうち、シートにはダメージ値しか設定できない為。
					// TODO 効果量の所も配列設定にして、場合によっては追加情報を見るのもいいかも…
					player.AddMaxHp(3);
					player.AddNowHp(3);
					scene.UpdateParameterText();
				}
			}
			StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleWin);
		} else {
            if (StateMachineManager.Instance.GetPrevState(StateMachineName.Map) == (int)MapState.BattleInitiativeValueChange) {// プレイヤーのイニシアチブ
				MapDataCarrier.Instance.InitiativeActionPackCount++;
				if (MapDataCarrier.Instance.InitiativeActionPackCount < MapDataCarrier.Instance.MaxInitiativeActionPackCount) {
					StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleInitiativeValueChange);
				} else {
					player.RemoveInitiativeFirstActionData();
					StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattlePlayerInitiative);
				}
            } else if (StateMachineManager.Instance.GetPrevState(StateMachineName.Map) == (int)MapState.BattlePlayerTurnStart) {// プレイヤーのターンスタート時のHP変動
				StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleUpdateAttackButtonDisplay);
            } else if (StateMachineManager.Instance.GetPrevState(StateMachineName.Map) == (int)MapState.BattleEnemyInitiativeValueChange) {// 敵のイニシアチブ
				MapDataCarrier.Instance.EnemyInitiativeActionPackCount++;

				if (MapDataCarrier.Instance.EnemyInitiativeActionPackCount < MapDataCarrier.Instance.EnemyMaxInitiativeActionPackCount) {
					StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleEnemyInitiativeValueChange);
				} else {
					MapDataCarrier.Instance.CuEnemyStatus.RemoveInitiativeFirstActionData();
					StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleEnemyInitiative);
				}
            } else if (StateMachineManager.Instance.GetPrevState(StateMachineName.Map) == (int)MapState.BattleEnemyTurnStart) {// 敵のターンスタート時のHP変動
				StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleEnemyAttackResult);
			} else if (
				(StateMachineManager.Instance.GetPrevState(StateMachineName.Map) == (int)MapState.BattleValueChange) ||
				(StateMachineManager.Instance.GetPrevState(StateMachineName.Map) == (int)MapState.BattleHandSelectEnd)
			) {// プレイヤーのターンの場合

				// アクションパックの処理が全て終わっていなかったら、次のアクションパックの処理を行う
				MapDataCarrier.Instance.ActionPackCount++;
				if (MapDataCarrier.Instance.ActionPackCount < MapDataCarrier.Instance.MaxActionPackCount) {
					StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleValueChange);
				} else {
					// 何らかの効果で、ダブル発動時のカードの場合はカードは削除するので、捨て札やデッキに登録しない
					if (MapDataCarrier.Instance.IsDoubleAttackCard == true) {
						MapDataCarrier.Instance.DoubleAttackBattleCardData = null;
						MapDataCarrier.Instance.IsDoubleAttackCard = false;
					} else if (MapDataCarrier.Instance.IsCost6DoubleAttackCard == true) {
						MapDataCarrier.Instance.Cost6DoubleAttackBattleCardData = null;
						MapDataCarrier.Instance.IsCost6DoubleAttackCard = false;
					} else {
						// 捨て札に使ったカードを登録させる
						var data = MapDataCarrier.Instance.SelectBattleCardData;
						if (data.UseType == EnumSelf.UseType.Erase) {
							// Eraseなら、捨て札にも破棄札にもならず、その戦闘中は2度と使えないカード
						} else if (data.UseType == EnumSelf.UseType.Discard) {
							scene.AddDiscard(data);
						} else if (data.UseType == EnumSelf.UseType.Repeat) {
							var trashList = MapDataCarrier.Instance.TrashList;
                    		trashList.Add(data);
						}
					}
					scene.UpdateCardListCountText();
						
					if (
						(MapDataCarrier.Instance.DoubleAttackBattleCardData == null) &&
						(MapDataCarrier.Instance.Cost6DoubleAttackBattleCardData == null)
					) {
						MapDataCarrier.Instance.SelectBattleCardData = null;
						// 効果終了後時に、手札0枚だったら、補充する
						if (player.GetParameterListFlag(EnumSelf.ParameterType.ZeroHand1Draw) == true) {
							if (MapDataCarrier.Instance.GetHandCount() == 0) {
								scene.DrawCard(1);
							}
						}

						// 効果終了後に、10枚使用したかどうか判定
						if (player.GetParameterListFlag(EnumSelf.ParameterType.Use10Card1Draw) == true) {
							if ((player.GetBattleUseCardCount() % 10) == 0) {
								scene.DrawCard(1);
							}
						}
						
						// 効果終了後に、10枚使用したかどうか判定
						if (player.GetParameterListFlag(EnumSelf.ParameterType.Use10CardGain3Activity) == true) {
							if ((player.GetBattleUseCardCount() % 10) == 0) {
								BattleCalculationFunction.PlayerUpdateTurnPower(EnumSelf.TurnPowerType.Activity, 3);
							}
						}

						StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleCheckAfter);
					} else {
						StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleAttackResult);
					}
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
				MapDataCarrier.Instance.EnemyActionPackCount++;

				// アクションパックの処理が全て終わっていなかったら、次のアクションパックの処理を行う
				if (MapDataCarrier.Instance.EnemyActionPackCount < MapDataCarrier.Instance.EnemyMaxActionPackCount) {
					StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleEnemyValueChange);
				} else {
					scene.UpdateCardListCountText();
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

			} else if (StateMachineManager.Instance.GetPrevState(StateMachineName.Map) == (int)MapState.BattleCheckAfter) {// プレイヤーのバトルチェック後のフロー。
				StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleAttackSelectUserWait);
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
