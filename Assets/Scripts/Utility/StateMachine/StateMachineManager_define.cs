/*
 * @file StateMachineManager_define.cs
 * ステートマシンの種類を記載する定義クラス.
 * このスクリプトは、Tools/CreateStateMachineDefinition.pyで自動生成されます。
 * @author 山下
 */

using UnityEngine;
using System.Collections;

/// <summary>
///	ステートマシンの種類を記載する定義クラス.
/// </summary>
public enum StateMachineName : int
{
	Home = 0,
	Map,
	Menu,
};

public enum HomeState : int
{
	Initialize = 0,
	UserWait,
	End,
}

public enum MapState : int
{
	BattleAttackResult = 0,
	BattleAttackSelectUserWait,
	BattleCheck,
	BattleDiceRoll,
	BattleDiceRollUserWait,
	BattleEnd,
	BattleEnemyLotAction,
	BattleEnemyAttackResult,
	BattleEnemyAttack,
	BattleEnemyTurnEnd,
	BattleEnemyTurnStart,
	BattleEnemyValueChange,
	BattleInitialize,
	BattleLose,
	BattlePlayerTurnEnd,
	BattlePlayerTurnStart,
	BattleUpdateAttackButtonDisplay,
	BattleValueChange,
	BattleWin,
	FloorEndCheck,
	HealDetailUpdate,
	HealDisplay,
	HealEnd,
	HealInitialize,
	HealResult,
	HealUserWait,
	ArtifactDetailUpdate,
	ArtifactDisplay,
	ArtifactEnd,
	ArtifactInitialize,
	ArtifactResult,
	ArtifactUserWait,
	Initialize,
	ResultChangeDisplay,
	ResultChangeResult,
	ResultChangeUserWait,
	ResultDetailUpdate,
	ResultEnd,
	ResultInitialize,
	ResultTreasureDisplay,
	ResultTreasureUserWait,
	UpdateDifficult,
	UpdateMap,
	UserWait,
	DungeonResultDisplay,
	End,
}

public enum MenuState : int
{
	Initialize = 0,
	UserWait,
	End,
}

