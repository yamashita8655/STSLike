using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapScene : SceneBase
{
	[SerializeField]
	private Sprite[] CuMapSprites = null;
	public Sprite[] MapSprites => CuMapSprites;

	[SerializeField]
	private Sprite[] CuLevelSprites = null;
	public Sprite[] LevelSprites => CuLevelSprites;
	
	[SerializeField]
	private Image[] CuMapImages = null;
	public Image[] MapImages => CuMapImages;
	
	[SerializeField]
	private Image[] CuDifficultImages = null;
	public Image[] DifficultImages => CuDifficultImages;

	// Start is called before the first frame update
	IEnumerator Start() {
		while (EntryPoint.IsInitialized == false) {
			yield return null;
		}

		// データキャリア
		MapDataCarrier.Instance.Initialize();
		MapDataCarrier.Instance.Scene = this;
		
		// ステートマシン
		StateMachineManager.Instance.Init();
		var stm = StateMachineManager.Instance;
		stm.CreateStateMachineMap(StateMachineName.Map);
		stm.AddState(StateMachineName.Map, (int)MapState.Initialize, new MapInitializeState());
		stm.AddState(StateMachineName.Map, (int)MapState.UpdateMap, new MapUpdateMapState());
		stm.AddState(StateMachineName.Map, (int)MapState.UpdateDifficult, new MapUpdateDifficultState());
		stm.AddState(StateMachineName.Map, (int)MapState.UserWait, new MapUserWaitState());
		stm.AddState(StateMachineName.Map, (int)MapState.End, new MapEndState());
		
		stm.ChangeState(StateMachineName.Map, (int)MapState.Initialize);
			
		FadeManager.Instance.FadeOut(FadeManager.Type.Mask, 0.5f, null);
	}

	// Update is called once per frame
	void Update()
	{
		StateMachineManager.Instance.Update(StateMachineName.Map, Time.deltaTime);
	}
	
	void OnDestroy()
	{
		StateMachineManager.Instance.Release(StateMachineName.Map);
		if (MapDataCarrier.IsNull() == false) {
			MapDataCarrier.Instance.Release();
			MapDataCarrier.DestroyInstance();
		}
	}
	
	public void OnClickGoToItemHunt()
	{
		// ユーザー入力待機状態でなければ、処理しない
		var stm = StateMachineManager.Instance;
		if (stm.GetNextState(StateMachineName.Map) != (int)MapState.UserWait) {
			return;
		}

		MapDataCarrier.Instance.NextSceneName = LocalSceneManager.SceneName.ItemHunt;
		
		StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.End);
	}

	public void OnClickDifficultButton(int index) {
		MapDataCarrier.Instance.CurrentMapNumber++;
		MapDataCarrier.Instance.HandDifficultList[index] = -1;
		StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.UpdateDifficult);
	}
}
