// このファイルは/Tools/CreateStateMachineDefinition/create_statemachinedefine.pyで自動生成されるので、編集禁止です。
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class MenuScene : SceneBase
{
	private void InitializeStateMachine() {
		// ステートマシン
		StateMachineManager.Instance.Init();
		var stm = StateMachineManager.Instance;
		stm.CreateStateMachineMap(StateMachineName.Menu);
		stm.AddState(StateMachineName.Menu, (int)MenuState.Initialize, new MenuInitializeState());
		stm.AddState(StateMachineName.Menu, (int)MenuState.UserWait, new MenuUserWaitState());
		stm.AddState(StateMachineName.Menu, (int)MenuState.End, new MenuEndState());

	}

}
