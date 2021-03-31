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

	[SerializeField]
	private Sprite[] CuDiceSprites = null;
	public Sprite[] DiceSprites => CuDiceSprites;

	[SerializeField]
	private Image[] CuDiceImages = null;
	public Image[] DiceImages => CuDiceImages;

	[SerializeField]
	private GameObject CuDiceRollButton = null;
	public GameObject DiceRollButton => CuDiceRollButton;
	
	[SerializeField]
	private GameObject CuMapRoot = null;
	public GameObject MapRoot => CuMapRoot;
	
	[SerializeField]
	private GameObject CuBattleRoot = null;
	public GameObject BattleRoot => CuBattleRoot;
	
	[SerializeField]
	private Text CuPlayerNowHpText = null;
	public Text PlayerNowHpText => CuPlayerNowHpText;
	
	[SerializeField]
	private Text CuPlayerMaxHpText = null;
	public Text PlayerMaxHpText => CuPlayerMaxHpText;
	
	[SerializeField]
	private Text CuEnemyNowHpText = null;
	public Text EnemyNowHpText => CuEnemyNowHpText;
	
	[SerializeField]
	private Text CuEnemyMaxHpText = null;
	public Text EnemyMaxHpText => CuEnemyMaxHpText;
	
	[SerializeField]
	private Text[] CuPlayerActionNameStrings = null;
	public Text[] PlayerActionNameStrings => CuPlayerActionNameStrings;
	
	[SerializeField]
	private Text[] CuPlayerActionValueStrings = null;
	public Text[] PlayerActionValueStrings => CuPlayerActionValueStrings;
	
	[SerializeField]
	private Button[] CuPlayerActionButtons = null;
	public Button[] PlayerActionButtons => CuPlayerActionButtons;
	
	[SerializeField]
	private Text CuEnemyNameText = null;
	public Text EnemyNameText => CuEnemyNameText;
	
	[SerializeField]
	private Text CuEnemyActionText = null;
	public Text EnemyActionText => CuEnemyActionText;
	
	[SerializeField]
	private Text CuEnemyActionValueText = null;
	public Text EnemyActionValueText => CuEnemyActionValueText;
	
	[SerializeField]
	private GameObject CuResultRoot = null;
	public GameObject ResultRoot => CuResultRoot;
	
	[SerializeField]
	private GameObject CuChangeRoot = null;
	public GameObject ChangeRoot => CuChangeRoot;
	
	[SerializeField]
	private Button CuTreasureDecideButton = null;
	public Button TreasureDecideButton => CuTreasureDecideButton;
	
	[SerializeField]
	private Text[] CuTreasureNameTexts = null;
	public Text[] TreasureNameTexts => CuTreasureNameTexts;

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

		stm.AddState(StateMachineName.Map, (int)MapState.BattleInitialize, new MapBattleInitializeState());
		stm.AddState(StateMachineName.Map, (int)MapState.BattleUpdateAttackButtonDisplay, new MapBattleUpdateAttackButtonDisplayState());
		stm.AddState(StateMachineName.Map, (int)MapState.BattleDiceRollUserWait, new MapBattleDiceRollUserWaitState());
		stm.AddState(StateMachineName.Map, (int)MapState.BattleDiceRoll, new MapBattleDiceRollState());
		stm.AddState(StateMachineName.Map, (int)MapState.BattleAttackSelectUserWait, new MapBattleAttackSelectUserWaitState());
		stm.AddState(StateMachineName.Map, (int)MapState.BattleAttackResult, new MapBattleAttackResultState());
		stm.AddState(StateMachineName.Map, (int)MapState.BattleCheck, new MapBattleCheckState());
		stm.AddState(StateMachineName.Map, (int)MapState.BattleEnemyAttack, new MapBattleEnemyAttackState());
		stm.AddState(StateMachineName.Map, (int)MapState.BattleEnemyAttackResult, new MapBattleEnemyAttackResultState());
		stm.AddState(StateMachineName.Map, (int)MapState.BattleWin, new MapBattleWinState());
		stm.AddState(StateMachineName.Map, (int)MapState.BattleLose, new MapBattleLoseState());
		stm.AddState(StateMachineName.Map, (int)MapState.BattleEnd, new MapBattleEndState());

		stm.AddState(StateMachineName.Map, (int)MapState.ResultInitialize, new MapResultInitializeState());
		stm.AddState(StateMachineName.Map, (int)MapState.ResultTreasureDisplay, new MapResultTreasureDisplayState());
		stm.AddState(StateMachineName.Map, (int)MapState.ResultTreasureUserWait, new MapResultTreasureUserWaitState());
		stm.AddState(StateMachineName.Map, (int)MapState.ResultDetailUpdate, new MapResultDetailUpdateState());
		stm.AddState(StateMachineName.Map, (int)MapState.ResultChangeDisplay, new MapResultChangeDisplayState());
		stm.AddState(StateMachineName.Map, (int)MapState.ResultChangeUserWait, new MapResultChangeUserWaitState());
		stm.AddState(StateMachineName.Map, (int)MapState.ResultChangeResult, new MapResultChangeResultState());
		stm.AddState(StateMachineName.Map, (int)MapState.ResultEnd, new MapResultEndState());

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
		//StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.UpdateDifficult);
		StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleInitialize);
	}

	// -----以下、バトルシーン用
	public void OnClickDiceRollButton()
	{
		// ユーザー入力待機状態でなければ、処理しない
		var stm = StateMachineManager.Instance;
		if (stm.GetNextState(StateMachineName.Map) != (int)MapState.BattleDiceRollUserWait) {
			return;
		}
		StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleDiceRoll);
	}
	
	public void OnClickAttackButton(int index)
	{
		// ユーザー入力待機状態でなければ、処理しない
		var stm = StateMachineManager.Instance;
		if (stm.GetNextState(StateMachineName.Map) != (int)MapState.BattleAttackSelectUserWait) {
			return;
		}
		MapDataCarrier.Instance.SelectAttackIndex = index;

		// 押されたダイスデータを消す
		for (int i = 0; i < MapDataCarrier.Instance.DiceValueList.Count; i++) {
			if (MapDataCarrier.Instance.DiceValueList[i] == index) {
				MapDataCarrier.Instance.DiceValueList.RemoveAt(i);
				break;
			}
		}

		StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleUpdateAttackButtonDisplay);
	}

	// ここからリザルト
	public void OnClickTreasureButton(int index) {
		// ユーザー入力待機状態でなければ、処理しない
		var stm = StateMachineManager.Instance;
		if (stm.GetNextState(StateMachineName.Map) != (int)MapState.ResultTreasureUserWait) {
			return;
		}
		TreasureDecideButton.interactable = true;
		MapDataCarrier.Instance.SelectTreasureIndex = index;
		StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.ResultDetailUpdate);
	}
	
	public void OnClickSkipButton() {
		// ユーザー入力待機状態でなければ、処理しない
		var stm = StateMachineManager.Instance;
		if (stm.GetNextState(StateMachineName.Map) != (int)MapState.ResultTreasureUserWait) {
			return;
		}
		StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.ResultEnd);
	}
	
	public void OnClickDecideButton() {
		// ユーザー入力待機状態でなければ、処理しない
		var stm = StateMachineManager.Instance;
		if (stm.GetNextState(StateMachineName.Map) != (int)MapState.ResultTreasureUserWait) {
			return;
		}
		StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.ResultChangeDisplay);
	}
	
	public void OnClickBackButton() {
		// ユーザー入力待機状態でなければ、処理しない
		var stm = StateMachineManager.Instance;
		if (stm.GetNextState(StateMachineName.Map) != (int)MapState.ResultChangeUserWait) {
			return;
		}
		StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.ResultTreasureDisplay);
	}
	
	public void OnClickChangeButton(int index) {
		// ユーザー入力待機状態でなければ、処理しない
		var stm = StateMachineManager.Instance;
		if (stm.GetNextState(StateMachineName.Map) != (int)MapState.ResultChangeUserWait) {
			return;
		}
		MapDataCarrier.Instance.SelectChangeIndex = index;
		StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.ResultChangeResult);
	}
}
