/*
 * @file StateMachineManager_define.cs
 * ステートマシンの種類を記載する定義クラス.
 * @author 山下
 */

using UnityEngine;
using System.Collections;

/// <summary>
///	ステートマシンの種類を記載する定義クラス.
/// </summary>
public enum StateMachineName : int
{
	Test,
	Home,
	ItemHunt,
	Equip,
	Debug,
	Map,
	Max,
};

/// <summary>
/// </summary>
public enum HomeState : int
{
	Initialize = 0,
	UserWait,
	End
};

/// <summary>
/// </summary>
public enum MapState : int {
	Initialize = 0,
	UpdateMap,
	UpdateDifficult,
	UserWait,

	BattleInitialize,
	BattleUpdateAttackButtonDisplay,
	BattleDiceRollUserWait,
	BattleDiceRoll,
	BattleAttackSelectUserWait,
	BattleAttackResult,
	BattleCheck,
	BattleEnemyAttack,
	BattleEnemyAttackResult,
	BattleWin,
	BattleLose,
	BattleEnd,

	End
};

/// <summary>
/// </summary>
public enum ItemHuntState : int
{
	Initialize = 0,
	SetHuntTimer,
	UserWait,
	LotItemHunt,
	End
};

/// <summary>
/// </summary>
public enum EquipState : int
{
	Initialize = 0,
	UserWait,
	UpdateInventory,
	End
};

/// <summary>
/// </summary>
public enum DebugState : int
{
	Initialize = 0,
	UserWait,
	ReloadGround,
	End
};
