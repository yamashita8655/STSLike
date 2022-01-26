using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class MapScene : SceneBase
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
	
	[SerializeField]
	private Text CuNowFloorText = null;
	public Text NowFloorText => CuNowFloorText;
	
	[SerializeField]
	private Text CuMaxFloorText = null;
	public Text MaxFloorText => CuMaxFloorText;
	
	[SerializeField]
	private GameObject CuHealRoot = null;
	public GameObject HealRoot => CuHealRoot;
	
	[SerializeField]
	private Text[] CuHealTexts = null;
	public Text[] HealTexts => CuHealTexts;
	
	[SerializeField]
	private Button CuHealDecideButton = null;
	public Button HealDecideButton => CuHealDecideButton;
	
	[SerializeField]
	private Text CuPlayerShieldText = null;
	public Text PlayerShieldText => CuPlayerShieldText;
	
	[SerializeField]
	private Text CuEnemyShieldText = null;
	public Text EnemyShieldText => CuEnemyShieldText;
	
	[SerializeField]
	private Image CuEnemyImage = null;
	public Image EnemyImage => CuEnemyImage;

	// Start is called before the first frame update
	IEnumerator Start() {
		while (EntryPoint.IsInitialized == false) {
			yield return null;
		}

		// データキャリア
		MapDataCarrier.Instance.Initialize();
		MapDataCarrier.Instance.Scene = this;

		MenuData data = (MenuData)LocalSceneManager.Instance.SceneData;
		MapDataCarrier.Instance.DungeonData = data.Data;
		
		// ステートマシン
		InitializeStateMachine();
		
		StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.Initialize);
			
		FadeManager.Instance.FadeIn(0.5f, null);
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
	
	public void OnClickDifficultButton(int index) {
		int mapIndex = MapDataCarrier.Instance.CurrentMapNumber;
		Enum.MapType type = MapDataCarrier.Instance.MapTypeList[mapIndex];
		MapDataCarrier.Instance.CurrentMapNumber++;
		MapDataCarrier.Instance.HandDifficultList[index] = -1;
		//StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.UpdateDifficult);

		if (type == Enum.MapType.Heal) {
			StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.HealInitialize);
		} else {
			StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleInitialize);
		}
	}

	// -----以下、バトルシーン用
	public void OnClickDiceRollButton()
	{
		// ユーザー入力待機状態でなければ、処理しない
		var stm = StateMachineManager.Instance;
		if (stm.GetState(StateMachineName.Map) != (int)MapState.BattleDiceRollUserWait) {
			return;
		}
		StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleDiceRoll);
	}
	
	public void OnClickAttackButton(int index)
	{
		// ユーザー入力待機状態でなければ、処理しない
		var stm = StateMachineManager.Instance;
		if (stm.GetState(StateMachineName.Map) != (int)MapState.BattleAttackSelectUserWait) {
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
		if (stm.GetState(StateMachineName.Map) != (int)MapState.ResultTreasureUserWait) {
			return;
		}
		TreasureDecideButton.interactable = true;
		MapDataCarrier.Instance.SelectTreasureIndex = index;
		StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.ResultDetailUpdate);
	}
	
	public void OnClickSkipButton() {
		// ユーザー入力待機状態でなければ、処理しない
		var stm = StateMachineManager.Instance;
		if (stm.GetState(StateMachineName.Map) != (int)MapState.ResultTreasureUserWait) {
			return;
		}
		StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.ResultEnd);
	}
	
	public void OnClickDecideButton() {
		// ユーザー入力待機状態でなければ、処理しない
		var stm = StateMachineManager.Instance;
		if (stm.GetState(StateMachineName.Map) != (int)MapState.ResultTreasureUserWait) {
			return;
		}
		StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.ResultChangeDisplay);
	}
	
	public void OnClickBackButton() {
		// ユーザー入力待機状態でなければ、処理しない
		var stm = StateMachineManager.Instance;
		if (stm.GetState(StateMachineName.Map) != (int)MapState.ResultChangeUserWait) {
			return;
		}
		StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.ResultTreasureDisplay);
	}
	
	public void OnClickChangeButton(int index) {
		// ユーザー入力待機状態でなければ、処理しない
		var stm = StateMachineManager.Instance;
		if (stm.GetState(StateMachineName.Map) != (int)MapState.ResultChangeUserWait) {
			return;
		}
		MapDataCarrier.Instance.SelectChangeIndex = index;
		StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.ResultChangeResult);
	}

	// 以下ヒール系
	public void OnClickHealSkipButton() {
		// ユーザー入力待機状態でなければ、処理しない
		var stm = StateMachineManager.Instance;
		if (stm.GetState(StateMachineName.Map) != (int)MapState.HealUserWait) {
			return;
		}
		StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.HealEnd);
	}
	
	public void OnClickHealButton(int index) {
		// ユーザー入力待機状態でなければ、処理しない
		var stm = StateMachineManager.Instance;
		if (stm.GetState(StateMachineName.Map) != (int)MapState.HealUserWait) {
			return;
		}
		HealDecideButton.interactable = true;
		MapDataCarrier.Instance.SelectHealIndex = index;
		StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.HealDetailUpdate);
	}
	
	public void OnClickHealDecideButton() {
		// ユーザー入力待機状態でなければ、処理しない
		var stm = StateMachineManager.Instance;
		if (stm.GetState(StateMachineName.Map) != (int)MapState.HealUserWait) {
			return;
		}
		StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.HealResult);
	}

	public void UpdateParameterText() {
		PlayerNowHpText.text = MapDataCarrier.Instance.CuPlayerStatus.GetNowHp().ToString();
		EnemyNowHpText.text = MapDataCarrier.Instance.CuEnemyStatus.GetNowHp().ToString();

		int playerShield = MapDataCarrier.Instance.CuPlayerStatus.GetNowShield();
		if (playerShield > 0) {
			PlayerShieldText.text = string.Format("[{0}]", playerShield.ToString());
		} else {
			PlayerShieldText.text = "";
		}
		
		int enemyShield = MapDataCarrier.Instance.CuEnemyStatus.GetNowShield();
		if (enemyShield > 0) {
			EnemyShieldText.text = string.Format("[{0}]", enemyShield.ToString());
		} else {
			EnemyShieldText.text = "";
		}
	}
}
