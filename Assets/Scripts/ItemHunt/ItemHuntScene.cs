using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemHuntScene : SceneBase
{
	[SerializeField]
	private Text HuntCountDownTimerText = null;

	[SerializeField]
	private HuntedItemDialogController CuHuntedItemDialog = null;
	public HuntedItemDialogController HuntedItemDialog => CuHuntedItemDialog;

	// Start is called before the first frame update
	IEnumerator Start() {
		while (EntryPoint.IsInitialized == false) {
			yield return null;
		}

		// データキャリア
		ItemHuntDataCarrier.Instance.Initialize();
		ItemHuntDataCarrier.Instance.Scene = this;
		
		// ステートマシン
		StateMachineManager.Instance.Init();
		var stm = StateMachineManager.Instance;
		stm.CreateStateMachineMap(StateMachineName.ItemHunt);
		stm.AddState(StateMachineName.ItemHunt, (int)ItemHuntState.Initialize, new ItemHuntInitializeState());
		stm.AddState(StateMachineName.ItemHunt, (int)ItemHuntState.SetHuntTimer, new ItemHuntSetHuntTimerState());
		stm.AddState(StateMachineName.ItemHunt, (int)ItemHuntState.UserWait, new ItemHuntUserWaitState());
		stm.AddState(StateMachineName.ItemHunt, (int)ItemHuntState.LotItemHunt, new ItemHuntLotItemHuntState());
		stm.AddState(StateMachineName.ItemHunt, (int)ItemHuntState.End, new ItemHuntEndState());
		
		stm.ChangeState(StateMachineName.ItemHunt, (int)ItemHuntState.Initialize);
			
		FadeManager.Instance.FadeOut(FadeManager.Type.Mask, 0.5f, null);
	}

	// Update is called once per frame
	void Update()
	{
		StateMachineManager.Instance.Update(StateMachineName.ItemHunt, Time.deltaTime);
		
		UpdateTimer();
	}
	
	private void UpdateTimer() {
		ItemHuntDataCarrier.Instance.HuntTimerPassTime -= Time.deltaTime;
		if (ItemHuntDataCarrier.Instance.HuntTimerPassTime <= 0f) {
			ItemHuntDataCarrier.Instance.HuntTimerPassTime = 0f;
		}
		float pt = ItemHuntDataCarrier.Instance.HuntTimerPassTime;
		int m = ((int)pt)/60;
		int s = ((int)pt)%60;
		int ms = (int)((pt*100)%100f);
		HuntCountDownTimerText.text = string.Format("{0:D2}:{1:D2}:{2:D2}", m, s, ms);

		var stm = StateMachineManager.Instance;
		// ユーザー入力待機状態でなければ、処理しない
		if (stm.GetNextState(StateMachineName.ItemHunt) == (int)ItemHuntState.UserWait) {
			if (ItemHuntDataCarrier.Instance.HuntTimerPassTime <= 0f) {
				StateMachineManager.Instance.ChangeState(StateMachineName.ItemHunt, (int)ItemHuntState.LotItemHunt);
			}
		}
	}
	
	void OnDestroy()
	{
		StateMachineManager.Instance.Release(StateMachineName.ItemHunt);
		if (ItemHuntDataCarrier.IsNull() == false) {
			ItemHuntDataCarrier.Instance.Release();
			ItemHuntDataCarrier.DestroyInstance();
		}
	}

	public void OnClickOpenHuntedItemDialogButton()
	{
		CuHuntedItemDialog.Open();
	}

	public void OnClickGoHomeButton()
	{
		var stm = StateMachineManager.Instance;
		// ユーザー入力待機状態でなければ、処理しない
		if (stm.GetNextState(StateMachineName.ItemHunt) == (int)ItemHuntState.UserWait) {
			ItemHuntDataCarrier.Instance.NextSceneName = LocalSceneManager.SceneName.Home;
			StateMachineManager.Instance.ChangeState(StateMachineName.ItemHunt, (int)ItemHuntState.End);
		}
	}
}
