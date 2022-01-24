using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeScene : SceneBase
{
	// Start is called before the first frame update
	IEnumerator Start() {
		while (EntryPoint.IsInitialized == false) {
			yield return null;
		}

		// データキャリア
		HomeDataCarrier.Instance.Initialize();
		HomeDataCarrier.Instance.Scene = this;
		
		// ステートマシン
		StateMachineManager.Instance.Init();
		var stm = StateMachineManager.Instance;
		stm.CreateStateMachineMap(StateMachineName.Home);
		stm.AddState(StateMachineName.Home, (int)HomeState.Initialize, new HomeInitializeState());
		stm.AddState(StateMachineName.Home, (int)HomeState.UserWait, new HomeUserWaitState());
		stm.AddState(StateMachineName.Home, (int)HomeState.End, new HomeEndState());
		
		stm.ChangeState(StateMachineName.Home, (int)HomeState.Initialize);
			
		FadeManager.Instance.FadeIn(0.5f, null);
	}

	// Update is called once per frame
	void Update()
	{
		StateMachineManager.Instance.Update(StateMachineName.Home, Time.deltaTime);
	}
	
	void OnDestroy()
	{
		StateMachineManager.Instance.Release(StateMachineName.Home);
		if (HomeDataCarrier.IsNull() == false) {
			HomeDataCarrier.Instance.Release();
			HomeDataCarrier.DestroyInstance();
		}
	}
	
	public void OnClickGoToMap()
	{
		// ユーザー入力待機状態でなければ、処理しない
		var stm = StateMachineManager.Instance;
		if (stm.GetNextState(StateMachineName.Home) != (int)HomeState.UserWait) {
			return;
		}

		HomeDataCarrier.Instance.NextSceneName = LocalSceneManager.SceneName.Map;
		
		StateMachineManager.Instance.ChangeState(StateMachineName.Home, (int)HomeState.End);
	}
}
