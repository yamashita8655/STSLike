// このファイルは/Tools/CreateStateMachineDefinition/create_statemachinedefine.pyで自動生成されるので、編集禁止です。
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class MapScene : SceneBase
{
	private void InitializeStateMachine() {
		// ステートマシン
		StateMachineManager.Instance.Init();
		var stm = StateMachineManager.Instance;
		stm.CreateStateMachineMap(StateMachineName.Map);
		stm.AddState(StateMachineName.Map, (int)MapState.BattleAttackResult, new MapBattleAttackResultState());
		stm.AddState(StateMachineName.Map, (int)MapState.BattleAttackSelectUserWait, new MapBattleAttackSelectUserWaitState());
		stm.AddState(StateMachineName.Map, (int)MapState.BattleCheck, new MapBattleCheckState());
		stm.AddState(StateMachineName.Map, (int)MapState.BattleCheckAfter, new MapBattleCheckAfterState());
		stm.AddState(StateMachineName.Map, (int)MapState.BattleDiceRoll, new MapBattleDiceRollState());
		stm.AddState(StateMachineName.Map, (int)MapState.BattleDiceRollUserWait, new MapBattleDiceRollUserWaitState());
		stm.AddState(StateMachineName.Map, (int)MapState.BattleEnd, new MapBattleEndState());
		stm.AddState(StateMachineName.Map, (int)MapState.BattleEnemyLotAction, new MapBattleEnemyLotActionState());
		stm.AddState(StateMachineName.Map, (int)MapState.BattleEnemyAttackResult, new MapBattleEnemyAttackResultState());
		stm.AddState(StateMachineName.Map, (int)MapState.BattleEnemyInitiative, new MapBattleEnemyInitiativeState());
		stm.AddState(StateMachineName.Map, (int)MapState.BattleEnemyTurnEnd, new MapBattleEnemyTurnEndState());
		stm.AddState(StateMachineName.Map, (int)MapState.BattleEnemyTurnStart, new MapBattleEnemyTurnStartState());
		stm.AddState(StateMachineName.Map, (int)MapState.BattleEnemyInitiativeValueChange, new MapBattleEnemyInitiativeValueChangeState());
		stm.AddState(StateMachineName.Map, (int)MapState.BattleEnemyValueChange, new MapBattleEnemyValueChangeState());
		stm.AddState(StateMachineName.Map, (int)MapState.BattleInitialize, new MapBattleInitializeState());
		stm.AddState(StateMachineName.Map, (int)MapState.BattleLose, new MapBattleLoseState());
		stm.AddState(StateMachineName.Map, (int)MapState.BattlePlayerInitiative, new MapBattlePlayerInitiativeState());
		stm.AddState(StateMachineName.Map, (int)MapState.BattlePlayerTurnEnd, new MapBattlePlayerTurnEndState());
		stm.AddState(StateMachineName.Map, (int)MapState.BattlePlayerTurnSkip, new MapBattlePlayerTurnSkipState());
		stm.AddState(StateMachineName.Map, (int)MapState.BattlePlayerTurnStart, new MapBattlePlayerTurnStartState());
		stm.AddState(StateMachineName.Map, (int)MapState.BattleUpdateAttackButtonDisplay, new MapBattleUpdateAttackButtonDisplayState());
		stm.AddState(StateMachineName.Map, (int)MapState.BattleInitiativeValueChange, new MapBattleInitiativeValueChangeState());
		stm.AddState(StateMachineName.Map, (int)MapState.BattleValueChange, new MapBattleValueChangeState());
		stm.AddState(StateMachineName.Map, (int)MapState.BattleWin, new MapBattleWinState());
		stm.AddState(StateMachineName.Map, (int)MapState.BattleHandSelectInitialize, new MapBattleHandSelectInitializeState());
		stm.AddState(StateMachineName.Map, (int)MapState.BattleHandSelectUserWait, new MapBattleHandSelectUserWaitState());
		stm.AddState(StateMachineName.Map, (int)MapState.BattleHandSelectEnd, new MapBattleHandSelectEndState());
		stm.AddState(StateMachineName.Map, (int)MapState.FloorEndCheck, new MapFloorEndCheckState());
		stm.AddState(StateMachineName.Map, (int)MapState.HealDetailUpdate, new MapHealDetailUpdateState());
		stm.AddState(StateMachineName.Map, (int)MapState.HealDisplay, new MapHealDisplayState());
		stm.AddState(StateMachineName.Map, (int)MapState.HealEnd, new MapHealEndState());
		stm.AddState(StateMachineName.Map, (int)MapState.HealInitialize, new MapHealInitializeState());
		stm.AddState(StateMachineName.Map, (int)MapState.HealResult, new MapHealResultState());
		stm.AddState(StateMachineName.Map, (int)MapState.HealUserWait, new MapHealUserWaitState());
		stm.AddState(StateMachineName.Map, (int)MapState.ArtifactDetailUpdate, new MapArtifactDetailUpdateState());
		stm.AddState(StateMachineName.Map, (int)MapState.ArtifactDisplay, new MapArtifactDisplayState());
		stm.AddState(StateMachineName.Map, (int)MapState.ArtifactEnd, new MapArtifactEndState());
		stm.AddState(StateMachineName.Map, (int)MapState.ArtifactInitialize, new MapArtifactInitializeState());
		stm.AddState(StateMachineName.Map, (int)MapState.ArtifactResult, new MapArtifactResultState());
		stm.AddState(StateMachineName.Map, (int)MapState.ArtifactUserWait, new MapArtifactUserWaitState());
		stm.AddState(StateMachineName.Map, (int)MapState.Initialize, new MapInitializeState());
		stm.AddState(StateMachineName.Map, (int)MapState.ResultChangeDisplay, new MapResultChangeDisplayState());
		stm.AddState(StateMachineName.Map, (int)MapState.ResultChangeResult, new MapResultChangeResultState());
		stm.AddState(StateMachineName.Map, (int)MapState.ResultChangeUserWait, new MapResultChangeUserWaitState());
		stm.AddState(StateMachineName.Map, (int)MapState.ResultDetailUpdate, new MapResultDetailUpdateState());
		stm.AddState(StateMachineName.Map, (int)MapState.ResultEnd, new MapResultEndState());
		stm.AddState(StateMachineName.Map, (int)MapState.ResultInitialize, new MapResultInitializeState());
		stm.AddState(StateMachineName.Map, (int)MapState.ResultTreasureDisplay, new MapResultTreasureDisplayState());
		stm.AddState(StateMachineName.Map, (int)MapState.ResultTreasureUserWait, new MapResultTreasureUserWaitState());
		stm.AddState(StateMachineName.Map, (int)MapState.UpdateDifficult, new MapUpdateDifficultState());
		stm.AddState(StateMachineName.Map, (int)MapState.UpdateMap, new MapUpdateMapState());
		stm.AddState(StateMachineName.Map, (int)MapState.UserWait, new MapUserWaitState());
		stm.AddState(StateMachineName.Map, (int)MapState.DungeonResultDisplay, new MapDungeonResultDisplayState());
		stm.AddState(StateMachineName.Map, (int)MapState.End, new MapEndState());

	}

}
