using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugScene : SceneBase
{
	// Start is called before the first frame update
	IEnumerator Start() {
		while (EntryPoint.IsInitialized == false) {
			yield return null;
		}

		// データキャリア
		DebugDataCarrier.Instance.Initialize();
		DebugDataCarrier.Instance.Scene = this;
		
		// ステートマシン
		StateMachineManager.Instance.Init();
		var stm = StateMachineManager.Instance;
		stm.CreateStateMachineMap(StateMachineName.Debug);
		stm.AddState(StateMachineName.Debug, (int)DebugState.Initialize, new DebugInitializeState());
		stm.AddState(StateMachineName.Debug, (int)DebugState.UserWait, new DebugUserWaitState());
		stm.AddState(StateMachineName.Debug, (int)DebugState.End, new DebugEndState());
		
		stm.ChangeState(StateMachineName.Debug, (int)DebugState.Initialize);
			
		FadeManager.Instance.FadeOut(FadeManager.Type.Mask, 0.5f, null);
	}

	// Update is called once per frame
	void Update()
	{
		StateMachineManager.Instance.Update(StateMachineName.Debug, Time.deltaTime);
	}

	void OnDestroy()
	{
		StateMachineManager.Instance.Release(StateMachineName.Debug);
		if (DebugDataCarrier.IsNull() == false) {
			DebugDataCarrier.Instance.Release();
			DebugDataCarrier.DestroyInstance();
		}
	}
}
