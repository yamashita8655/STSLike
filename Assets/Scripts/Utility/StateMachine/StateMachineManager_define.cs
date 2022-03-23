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
	BattleStart = 0,
	BattleAttackResult,
	BattleAttackSelectUserWait,
	BattleCheck,
	BattleCheckAfter,
	BattleDiceRoll,
	BattleDiceRollUserWait,
	BattleEnd,
	BattleEnemyLotAction,
	BattleEnemyAttackResult,
	BattleEnemyInitiative,
	BattleEnemyTurnEnd,
	BattleEnemyTurnStart,
	BattleEnemyInitiativeValueChange,
	BattleEnemyValueChange,
	BattleInitialize,
	BattleLose,
	BattlePlayerInitiative,
	BattlePlayerTurnEnd,
	BattlePlayerTurnSkip,
	BattlePlayerTurnStart,
	BattleUpdateAttackButtonDisplay,
	BattleInitiativeValueChange,
	BattleValueChange,
	BattleWin,
	BattleHandSelectInitialize,
	BattleHandSelectUserWait,
	BattleHandSelectEnd,
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
	RegularCardSettingInitialize,
	RegularCardSettingUserWait,
	RegularCardSettingEnd,
	RegularCardSettingMaxCostUp,
	RegularCardSettingEquipCardDetailOpen,
	RegularCardSettingCardDetailOpen,
	RegularCardSettingCardDetailUnlock,
	RegularCardSettingCardDetailEquip,
	RegularCardSettingCardDetailEquipUserWait,
	RegularCardSettingCardDetailClose,
	RegularArtifactSettingInitialize,
	RegularArtifactSettingUserWait,
	RegularArtifactSettingEnd,
	RegularArtifactSettingMaxCostUp,
	RegularArtifactSettingEquipArtifactDetailOpen,
	RegularArtifactSettingArtifactDetailOpen,
	RegularArtifactSettingArtifactDetailUnlock,
	RegularArtifactSettingArtifactDetailEquip,
	RegularArtifactSettingArtifactDetailEquipUserWait,
	RegularArtifactSettingArtifactDetailClose,
	End,
}

