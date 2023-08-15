using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class HomeScene : SceneBase
{
	[SerializeField]
	private GameObject CuContinueObject = null;
	public GameObject ContinueObject => CuContinueObject;
	
	[SerializeField]
	private Text CuTitleText = null;
	public Text TitleText => CuTitleText;
	
	[SerializeField]
	private Text CuTouchStartText = null;
	public Text TouchStartText => CuTouchStartText;

	// Start is called before the first frame update
	IEnumerator Start() {
		while (EntryPoint.IsInitialized == false) {
			yield return null;
		}

		// データキャリア
		HomeDataCarrier.Instance.Initialize();
		HomeDataCarrier.Instance.Scene = this;
		
		// ステートマシン
		InitializeStateMachine();
		
		StateMachineManager.Instance.ChangeState(StateMachineName.Home, (int)HomeState.Initialize);
			
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

		var dungeonState = PlayerPrefsManager.Instance.GetDungeonState();
		if (string.IsNullOrEmpty(dungeonState) == false)
		{
			ContinueObject.SetActive(true);
		}
		else
		{
			HomeDataCarrier.Instance.NextSceneName = LocalSceneManager.SceneName.Menu;
			StateMachineManager.Instance.ChangeState(StateMachineName.Home, (int)HomeState.End);
		}
	}
	
	public void OnClickContinueButton()
	{
		// ユーザー入力待機状態でなければ、処理しない
		var stm = StateMachineManager.Instance;
		if (stm.GetNextState(StateMachineName.Home) != (int)HomeState.UserWait) {
			return;
		}

		ContinueObject.SetActive(false);
		HomeDataCarrier.Instance.NextSceneName = LocalSceneManager.SceneName.Map;
		
		StateMachineManager.Instance.ChangeState(StateMachineName.Home, (int)HomeState.End);
	}
	
	public void OnClickCancelButton()
	{
		// ユーザー入力待機状態でなければ、処理しない
		var stm = StateMachineManager.Instance;
		if (stm.GetNextState(StateMachineName.Home) != (int)HomeState.UserWait) {
			return;
		}

		ContinueObject.SetActive(false);

		// 多分、空のデータでも大丈夫のはず
		HomeDataCarrier.Instance.NextSceneName = LocalSceneManager.SceneName.Menu;
		MapData data = new MapData();
		HomeDataCarrier.Instance.Data = (SceneDataBase)data;

		// データ初期化
		PlayerPrefsManager.Instance.SetDungeonState(string.Empty);
		
		StateMachineManager.Instance.ChangeState(StateMachineName.Home, (int)HomeState.End);
	}
}
