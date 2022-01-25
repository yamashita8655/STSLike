using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class MenuScene : SceneBase
{
	// Start is called before the first frame update
	IEnumerator Start() {
		while (EntryPoint.IsInitialized == false) {
			yield return null;
		}

		// データキャリア
		MenuDataCarrier.Instance.Initialize();
		MenuDataCarrier.Instance.Scene = this;
		
		// ステートマシン
		InitializeStateMachine();

		StateMachineManager.Instance.ChangeState(StateMachineName.Menu, (int)MenuState.Initialize);
		FadeManager.Instance.FadeIn(0.5f, null);
	}

	// Update is called once per frame
	void Update()
	{
		StateMachineManager.Instance.Update(StateMachineName.Menu, Time.deltaTime);
	}
	
	void OnDestroy()
	{
		StateMachineManager.Instance.Release(StateMachineName.Menu);
		if (MenuDataCarrier.IsNull() == false) {
			MenuDataCarrier.Instance.Release();
			MenuDataCarrier.DestroyInstance();
		}
	}
}
