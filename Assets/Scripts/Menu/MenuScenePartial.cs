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
		stm.AddState(StateMachineName.Menu, (int)MenuState.RegularCardSettingInitialize, new MenuRegularCardSettingInitializeState());
		stm.AddState(StateMachineName.Menu, (int)MenuState.RegularCardSettingUserWait, new MenuRegularCardSettingUserWaitState());
		stm.AddState(StateMachineName.Menu, (int)MenuState.RegularCardSettingEnd, new MenuRegularCardSettingEndState());
		stm.AddState(StateMachineName.Menu, (int)MenuState.RegularCardSettingMaxCostUp, new MenuRegularCardSettingMaxCostUpState());
		stm.AddState(StateMachineName.Menu, (int)MenuState.RegularCardSettingCardDetailOpen, new MenuRegularCardSettingCardDetailOpenState());
		stm.AddState(StateMachineName.Menu, (int)MenuState.RegularCardSettingCardDetailUnlock, new MenuRegularCardSettingCardDetailUnlockState());
		stm.AddState(StateMachineName.Menu, (int)MenuState.RegularCardSettingCardDetailEquip, new MenuRegularCardSettingCardDetailEquipState());
		stm.AddState(StateMachineName.Menu, (int)MenuState.RegularCardSettingCardDetailClose, new MenuRegularCardSettingCardDetailCloseState());
		stm.AddState(StateMachineName.Menu, (int)MenuState.End, new MenuEndState());

	}

}
