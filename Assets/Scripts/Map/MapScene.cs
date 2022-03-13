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
	private Image[] CuAttackButtonImages = null;
	public Image[] AttackButtonImages => CuAttackButtonImages;
	
	[SerializeField]
	private GameObject CuTurnEndButtonObject = null;
	public GameObject TurnEndButtonObject => CuTurnEndButtonObject;
	
	[SerializeField]
	private Text CuCurrentTotalDiceCostText = null;
	public Text CurrentTotalDiceCostText => CuCurrentTotalDiceCostText;
	
	[SerializeField]
	private Text CuDeckCountText = null;
	public Text DeckCountText => CuDeckCountText;
	
	[SerializeField]
	private Text CuTrashCountText = null;
	public Text TrashCountText => CuTrashCountText;
	
	[SerializeField]
	private Text CuDiscardCountText = null;
	public Text DiscardCountText => CuDiscardCountText;
	
	[SerializeField]
	private GameObject CuHandRoot = null;
	public GameObject HandRoot => CuHandRoot;
	
	[SerializeField]
	private Text CuOriginalDeckCountText = null;
	public Text OriginalDeckCountText => CuOriginalDeckCountText;
	
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
	private GameObject[] CuPlayerActionValueRoots = null;
	public GameObject[] PlayerActionValueRoots => CuPlayerActionValueRoots;
	
	[SerializeField]
	private Button[] CuPlayerActionButtons = null;
	public Button[] PlayerActionButtons => CuPlayerActionButtons;
	
	[SerializeField]
	private GameObject CuPowerRoot = null;
	public GameObject PowerRoot => CuPowerRoot;
	
	[SerializeField]
	private GameObject CuTurnPowerRoot = null;
	public GameObject TurnPowerRoot => CuTurnPowerRoot;
	
	[SerializeField]
	private Text CuEnemyNameText = null;
	public Text EnemyNameText => CuEnemyNameText;
	
	[SerializeField]
	private Text CuEnemyActionText = null;
	public Text EnemyActionText => CuEnemyActionText;
	
	[SerializeField]
	private GameObject CuEnemyActionValueRoot = null;
	public GameObject EnemyActionValueRoot => CuEnemyActionValueRoot;
	
	[SerializeField]
	private GameObject CuEnemyPowerRoot = null;
	public GameObject EnemyPowerRoot => CuEnemyPowerRoot;
	
	[SerializeField]
	private GameObject CuEnemyTurnPowerRoot = null;
	public GameObject EnemyTurnPowerRoot => CuEnemyTurnPowerRoot;
	
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
	private Text CuTreasureDetailCardName = null;
	public Text TreasureDetailCardName => CuTreasureDetailCardName;
	
	[SerializeField]
	private Image[] CuTreasureButtonRarityFrameImages = null;
	public Image[] TreasureButtonRarityFrameImages => CuTreasureButtonRarityFrameImages;
	
	[SerializeField]
	private Text CuTreasureDetailCardDetail = null;
	public Text TreasureDetailCardDetail => CuTreasureDetailCardDetail;
	
	[SerializeField]
	private Image CuTreasureDetailCardImage = null;
	public Image TreasureDetailCardImage => CuTreasureDetailCardImage;
	
	[SerializeField]
	private Image CuTreasureRarityFrameImage = null;
	public Image TreasureRarityFrameImage => CuTreasureRarityFrameImage;
	
	[SerializeField]
	private Text CuTreasureDiceCostText = null;
	public Text TreasureDiceCostText => CuTreasureDiceCostText;
	
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
	private Button[] CuHealButtons = null;
	public Button[] HealButtons => CuHealButtons;
	
	[SerializeField]
	private Text CuHealDetailText = null;
	public Text HealDetailText => CuHealDetailText;
	
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
	
	[SerializeField]
	private Image CuBgImage = null;
	public Image BgImage => CuBgImage;
	
	// ダンジョンリザルト関係
	[SerializeField]
	private GameObject CuDungeonResultRoot = null;
	public GameObject DungeonResultRoot => CuDungeonResultRoot;
	
	[SerializeField]
	private Text CuResultText = null;
	public Text ResultText => CuResultText;
	
	[SerializeField]
	private Text CuCurrentPointLabel = null;
	public Text CurrentPointLabel => CuCurrentPointLabel;
	
	[SerializeField]
	private Text CuGetPointLabel = null;
	public Text GetPointLabel => CuGetPointLabel;
	
	[SerializeField]
	private Text CuCurrentPointText = null;
	public Text CurrentPointText => CuCurrentPointText;
	
	[SerializeField]
	private Text CuGetPointText = null;
	public Text GetPointText => CuGetPointText;
	
	[SerializeField]
	private GameObject CuAdmobButton = null;
	public GameObject AdmobButton => CuAdmobButton;

	// アーティファクト関係
	[SerializeField]
	private GameObject CuArtifactRoot = null;
	public GameObject ArtifactRoot => CuArtifactRoot;
	
	[SerializeField]
	private Text[] CuArtifactTexts = null;
	public Text[] ArtifactTexts => CuArtifactTexts;
	
	[SerializeField]
	private Image[] CuArtifactRarityImages = null;
	public Image[] ArtifactRarityImages => CuArtifactRarityImages;
	
	[SerializeField]
	private Image CuArtifactDetailRarityImage = null;
	public Image ArtifactDetailRarityImage => CuArtifactDetailRarityImage;
	
	[SerializeField]
	private Button CuArtifactDecideButton = null;
	public Button ArtifactDecideButton => CuArtifactDecideButton;
	
	[SerializeField]
	private GameObject CuArtifactContentRoot = null;
	public GameObject ArtifactContentRoot => CuArtifactContentRoot;
	
	[SerializeField]
	private Text CuArtifactNameText = null;
	public Text ArtifactNameText => CuArtifactNameText;
	
	[SerializeField]
	private Image CuArtifactImage = null;
	public Image ArtifactImage => CuArtifactImage;
	
	[SerializeField]
	private Text CuArtifactDetailText = null;
	public Text ArtifactDetailText => CuArtifactDetailText;

	[SerializeField]
	private ArtifactDetailController CuCarryArtifactDetailController = null;
	public ArtifactDetailController CarryArtifactDetailController => CuCarryArtifactDetailController;
	
	[SerializeField]
	private CardDetailController CuCarryCardDetailController = null;
	public CardDetailController CarryCardDetailController => CuCarryCardDetailController;
	
	[SerializeField]
	private GameObject CuCardListRoot = null;
	public GameObject CardListRoot => CuCardListRoot;
	
	[SerializeField]
	private GameObject CuCardListContentRoot = null;
	public GameObject CardListContentRoot => CuCardListContentRoot;
	
	[SerializeField]
	private EraseCardDetailController CuEraseCardDetailController = null;
	public EraseCardDetailController EraseCardDetailController => CuEraseCardDetailController;
	
	[SerializeField]
	private GameObject CuHandCardRoot = null;
	public GameObject HandCardRoot => CuHandCardRoot;
	
	[SerializeField]
	private GameObject CuHandCardSelectRoot = null;
	public GameObject HandCardSelectRoot => CuHandCardSelectRoot;
	
	[SerializeField]
	private Button CuHandCardSelectDecideButton = null;
	public Button HandCardSelectDecideButton => CuHandCardSelectDecideButton;
	
	[SerializeField]
	private Text CuHandCardSelectText = null;
	public Text HandCardSelectText => CuHandCardSelectText;
	
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
		EnumSelf.MapType type = MapDataCarrier.Instance.MapTypeList[mapIndex];
		MapDataCarrier.Instance.CurrentMapNumber++;
		MapDataCarrier.Instance.SelectDifficultNumber = MapDataCarrier.Instance.HandDifficultList[index];
		MapDataCarrier.Instance.HandDifficultList[index] = -1;
		//StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.UpdateDifficult);

		if (type == EnumSelf.MapType.Heal) {
			StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.HealInitialize);
		} else if (type == EnumSelf.MapType.Treasure) {
			StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.ArtifactInitialize);
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
	
	public void OnClickAttackButton(BattleCardButtonController ctrl)
	{
		// ユーザー入力待機状態でなければ、処理しない
		var stm = StateMachineManager.Instance;
		if (stm.GetState(StateMachineName.Map) != (int)MapState.BattleAttackSelectUserWait) {
			return;
		}

		// 押されたボタンは、非表示にして、コストを減らす
		ctrl.gameObject.SetActive(false);

		var data = ctrl.GetData();
		MapDataCarrier.Instance.SelectBattleCardData = data;
		int cost = data.DiceCost;

		if (BattleCalculationFunction.IsCurse(data.Id) == true) {
			if (MapDataCarrier.Instance.CuPlayerStatus.GetParameterListFlag(EnumSelf.ParameterType.AntiCurse) == true) {
				cost = 0;
			}
		}

		MapDataCarrier.Instance.CurrentTotalDiceCost -= cost;
		if (MapDataCarrier.Instance.CurrentTotalDiceCost < 0) {
			MapDataCarrier.Instance.CurrentTotalDiceCost = 0;
		}
		UpdateCurrentTotalDiceCostText();

		StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleUpdateAttackButtonDisplay);
	}
	
	public void OnClickTurnEndButton()
	{
		// ユーザー入力待機状態でなければ、処理しない
		var stm = StateMachineManager.Instance;
		if (stm.GetState(StateMachineName.Map) != (int)MapState.BattleAttackSelectUserWait) {
			return;
		}

		StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattlePlayerTurnSkip);
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

		//StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.ResultEnd);
		MapDataCarrier.Instance.SelectTreasureIndex = 3;
		StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.ResultChangeResult);
	}
	
	public void OnClickDecideButton() {
		// ユーザー入力待機状態でなければ、処理しない
		var stm = StateMachineManager.Instance;
		if (stm.GetState(StateMachineName.Map) != (int)MapState.ResultTreasureUserWait) {
			return;
		}
		//StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.ResultChangeDisplay);
		StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.ResultChangeResult);
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

		int index = MapDataCarrier.Instance.SelectHealIndex;
		if ((index == 0) || (index == 1)) {
			StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.HealResult);
		} else {
			OnClickEraseCardButton();
		}
	}

	// 以下アーティファクト系
	public void OnClickArtifactSkipButton() {
		// ユーザー入力待機状態でなければ、処理しない
		var stm = StateMachineManager.Instance;
		if (stm.GetState(StateMachineName.Map) != (int)MapState.ArtifactUserWait) {
			return;
		}
		StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.ArtifactEnd);
	}
	
	public void OnClickArtifactButton(int index) {
		// ユーザー入力待機状態でなければ、処理しない
		var stm = StateMachineManager.Instance;
		if (stm.GetState(StateMachineName.Map) != (int)MapState.ArtifactUserWait) {
			return;
		}
		ArtifactDecideButton.interactable = true;
		MapDataCarrier.Instance.SelectArtifactIndex = index;
		StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.ArtifactDetailUpdate);
	}
	
	public void OnClickArtifactDecideButton() {
		// ユーザー入力待機状態でなければ、処理しない
		var stm = StateMachineManager.Instance;
		if (stm.GetState(StateMachineName.Map) != (int)MapState.ArtifactUserWait) {
			return;
		}
		StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.ArtifactResult);
	}
	
	// リザルト処理
	public void OnClickDungeonResultCloseButton() {
		// ユーザー入力待機状態でなければ、処理しない
		var stm = StateMachineManager.Instance;
		if (stm.GetState(StateMachineName.Map) != (int)MapState.DungeonResultDisplay) {
			return;
		}

		MapDataCarrier.Instance.NextSceneName = LocalSceneManager.SceneName.Menu;
		StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.End);
	}
	
	public void OnClickDungeonResultAdmobButton() {
		// ユーザー入力待機状態でなければ、処理しない
		var stm = StateMachineManager.Instance;
		if (stm.GetState(StateMachineName.Map) != (int)MapState.DungeonResultDisplay) {
			return;
		}

		LogManager.Instance.Log("Admob");
	}
	
	public void OnClickCarryArtifactButton(MasterArtifactTable.Data data) {
		// ユーザー入力待機状態でなければ、処理しない
		//var stm = StateMachineManager.Instance;
		//if (stm.GetState(StateMachineName.Map) != (int)MapState.DungeonResultDisplay) {
		//	return;
		//}

		CarryArtifactDetailController.Open(data);
	}
	
	//public void OnClickCarryCardDetailButton(int index) {
	//	// ユーザー入力待機状態でなければ、処理しない
	//	//var stm = StateMachineManager.Instance;
	//	//if (stm.GetState(StateMachineName.Map) != (int)MapState.DungeonResultDisplay) {
	//	//	return;
	//	//}

	//	MasterAction2Table.Data data = MapDataCarrier.Instance.CuPlayerStatus.GetActionData(index);
	//	CarryCardDetailController.Open(data);
	//}
	
	//public void OnClickCarryCardDetailButton(BattleCardButtonController ctrl) {
	//	// ユーザー入力待機状態でなければ、処理しない
	//	//var stm = StateMachineManager.Instance;
	//	//if (stm.GetState(StateMachineName.Map) != (int)MapState.DungeonResultDisplay) {
	//	//	return;
	//	//}

	//	MasterAction2Table.Data data = ctrl.GetData();
	//	CarryCardDetailController.Open(data);
	//}
	
	public void OnClickCarryCardDetailButton(MasterAction2Table.Data data) {
		// ユーザー入力待機状態でなければ、処理しない
		//var stm = StateMachineManager.Instance;
		//if (stm.GetState(StateMachineName.Map) != (int)MapState.DungeonResultDisplay) {
		//	return;
		//}

		CarryCardDetailController.Open(data);
	}
	
	public void OnClickEraseCardDetailButton(MasterAction2Table.Data data) {
		// ユーザー入力待機状態でなければ、処理しない
		//var stm = StateMachineManager.Instance;
		//if (stm.GetState(StateMachineName.Map) != (int)MapState.DungeonResultDisplay) {
		//	return;
		//}

		EraseCardDetailController.Open(
			data,
			(d) => {
				MapDataCarrier.Instance.SelectEraseData = d;
				StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.HealResult);
			}
		);
	}
	
	public void UpdateParameterText() {
		var player = MapDataCarrier.Instance.CuPlayerStatus;

		PlayerNowHpText.text = player.GetNowHp().ToString();
		PlayerMaxHpText.text = player.GetMaxHp().ToString();
		
		int playerShield = MapDataCarrier.Instance.CuPlayerStatus.GetNowShield();
		if (playerShield > 0) {
			PlayerShieldText.text = string.Format("[{0}]", playerShield.ToString());
		} else {
			PlayerShieldText.text = "";
		}

		var enemy = MapDataCarrier.Instance.CuEnemyStatus;

		if (enemy != null) {
			EnemyNowHpText.text = enemy.GetNowHp().ToString();
			EnemyMaxHpText.text = enemy.GetMaxHp().ToString();
			int enemyShield = enemy.GetNowShield();
			if (enemyShield > 0) {
				EnemyShieldText.text = string.Format("[{0}]", enemyShield.ToString());
			} else {
				EnemyShieldText.text = "";
			}
		}
	}
	
	public void UpdateEnemyValueObject() {
		EnemyStatus enemy = MapDataCarrier.Instance.CuEnemyStatus;
		PlayerStatus player = MapDataCarrier.Instance.CuPlayerStatus;

		MasterAction2Table.Data data = MapDataCarrier.Instance.CuEnemyStatus.GetActionData();
		// テキスト表示
		CuEnemyActionText.text = data.Name;

		// 効果量表示
		var list = data.ActionPackList;
		int index = 0;
		for (index = 0; index < list.Count; index++) {
			if (index >= 10) {
				LogManager.Instance.LogError("MapScene:UpdateEnemyValueObject:ValueObject10個超えてるよ");
			}
			int val = list[index].Value;
			if (
				(list[index].Effect == EnumSelf.EffectType.Damage) ||
				(list[index].Effect == EnumSelf.EffectType.DamageSuction) ||
				(list[index].Effect == EnumSelf.EffectType.ShieldBash)
			) {
				val = BattleCalculationFunction.CalcEnemyDamageValue(list[index]);
			} else if (
				(list[index].Effect == EnumSelf.EffectType.Shield) ||
				(list[index].Effect == EnumSelf.EffectType.StrengthShield)
			) {
				val = BattleCalculationFunction.CalcEnemyShieldValue(list[index]);
			}

			GameObject obj = MapDataCarrier.Instance.EnemyValueObjects[index];
			obj.GetComponent<ValueController>().UpdateDisplay(list[index].Effect, list[index].Value, val);
		}

		for (; index < 10; index++) {
			GameObject obj = MapDataCarrier.Instance.EnemyValueObjects[index];
			obj.GetComponent<ValueController>().Hide();
		}
	}
	
	public void AddArtifactObject(MasterArtifactTable.Data data) {
		var player = MapDataCarrier.Instance.CuPlayerStatus;
		if (data.ParameterType != EnumSelf.ParameterType.None) {
			player.SetParameterListFlag(data.ParameterType, true);

			// 最大HP増やす系は、ここで処理する
			if (data.ParameterType == EnumSelf.ParameterType.AddMaxHp1) {
				player.AddMaxHp(6);
				player.AddNowHp(6);
				UpdateParameterText();
			} else if (data.ParameterType == EnumSelf.ParameterType.AddMaxHp2) {
				player.AddMaxHp(10);
				player.AddNowHp(10);
				UpdateParameterText();
			} else if (data.ParameterType == EnumSelf.ParameterType.AddMaxHp3) {
				player.AddMaxHp(15);
				player.AddNowHp(15);
				UpdateParameterText();
			} else if (data.ParameterType == EnumSelf.ParameterType.AddMaxHp4) {
				player.AddMaxHp(21);
				player.AddNowHp(21);
				UpdateParameterText();
			}
		}

		MasterArtifactTable.Data localData = data;
		ResourceManager.Instance.RequestExecuteOrder(
			Const.ArtifactButtonPath,
			ExecuteOrder.Type.GameObject,
			gameObject,
			(rawObj) => {
				GameObject obj = GameObject.Instantiate(rawObj) as GameObject;
				obj.transform.SetParent(CuArtifactContentRoot.transform);
				obj.transform.localPosition = Vector3.zero;
				obj.transform.localScale = Vector3.one;

				var component = obj.GetComponent<ArtifactButtonContentItem>();
				component.Initialize(
					localData,
					(d) => {
						OnClickCarryArtifactButton(d);
					}
				);
				MapDataCarrier.Instance.CarryArtifactList.Add(component);
			}
		);
	}
	
	public void RemoveArtifactObject(EnumSelf.ParameterType type) {
		var player = MapDataCarrier.Instance.CuPlayerStatus;
		for (int i = 0; i < MapDataCarrier.Instance.CarryArtifactList.Count; i++) {
			if (MapDataCarrier.Instance.CarryArtifactList[i].GetData().ParameterType == type) {
				GameObject.Destroy(MapDataCarrier.Instance.CarryArtifactList[i].gameObject);
				MapDataCarrier.Instance.CarryArtifactList.RemoveAt(i);
				break;
			}
		}
		player.SetParameterListFlag(type, false);
	}
	
	public void UpdateCurrentTotalDiceCostText() {
		CurrentTotalDiceCostText.text = MapDataCarrier.Instance.CurrentTotalDiceCost.ToString();
	}
	
	public void OnClickBattleDeckButton() {
		// TODO これは、正直いつ押されてもいいので、そう作っておく
		UpdateCardList(MapDataCarrier.Instance.BattleDeckList, OnClickCarryCardDetailButton);
		CuCardListRoot.SetActive(true);
	}

	public void OnClickTrashButton() {
		// TODO これは、正直いつ押されてもいいので、そう作っておく
		UpdateCardList(MapDataCarrier.Instance.TrashList, OnClickCarryCardDetailButton);
		CuCardListRoot.SetActive(true);
	}

	public void OnClickDiscardButton() {
		// TODO これは、正直いつ押されてもいいので、そう作っておく
		UpdateCardList(MapDataCarrier.Instance.DiscardList, OnClickCarryCardDetailButton);
		CuCardListRoot.SetActive(true);
	}
	
	public void OnClickOriginalDeckButton() {
		// TODO これは、正直いつ押されてもいいので、そう作っておく
		UpdateCardList(MapDataCarrier.Instance.OriginalDeckList, OnClickCarryCardDetailButton);
		CuCardListRoot.SetActive(true);
	}
	
	public void OnClickEraseCardButton() {
		UpdateCardList(MapDataCarrier.Instance.OriginalDeckList, OnClickEraseCardDetailButton);
		CuCardListRoot.SetActive(true);
	}
	
	public void OnClickDeckListCloseButton() {
		CuCardListRoot.SetActive(false);
	}

	public void UpdateCardList(
		List<MasterAction2Table.Data> dataList,
		Action<MasterAction2Table.Data> callback
	) {
		var contentItemList = MapDataCarrier.Instance.CardContentItemList;
		int i = 0;
		for (; i < dataList.Count; i++) {
			var data = dataList[i];

			if (i < contentItemList.Count) {
				contentItemList[i].gameObject.SetActive(true);
				contentItemList[i].Initialize(
					data,
					callback
				);
			} else {
				ResourceManager.Instance.RequestExecuteOrder(
					Const.CardContentItemPath,
					ExecuteOrder.Type.GameObject,
					gameObject,
					(rawObject) => {
						GameObject obj = GameObject.Instantiate(rawObject) as GameObject;
						obj.transform.SetParent(CardListContentRoot.transform);
						obj.transform.localPosition = Vector3.zero;
						obj.transform.localScale = Vector3.one;
						var ctrl = obj.GetComponent<CardContentItem>();
						ctrl.Initialize(
							data,
							callback
						);
						MapDataCarrier.Instance.CardContentItemList.Add(ctrl);
					}
				);
			}
		}
		
		for (; i < contentItemList.Count; i++) {
			contentItemList[i].gameObject.SetActive(false);
		}
	}

	public void UpdateOriginalDeckCountText() {
		OriginalDeckCountText.text = MapDataCarrier.Instance.OriginalDeckList.Count.ToString();
	}
	
	public void UpdateCardListCountText() {
		var discardList = MapDataCarrier.Instance.DiscardList;
		var trashList = MapDataCarrier.Instance.TrashList;
		var deckList = MapDataCarrier.Instance.BattleDeckList;
		DiscardCountText.text = discardList.Count.ToString();
		TrashCountText.text = trashList.Count.ToString();
		DeckCountText.text = deckList.Count.ToString();
	}

	public void DrawCard(int drawCount) {
		var player = MapDataCarrier.Instance.CuPlayerStatus;
		int resistCount = player.GetTurnPowerValue(EnumSelf.TurnPowerType.Resist);

		int drewCount = 0;

		var deckList = MapDataCarrier.Instance.BattleDeckList;
		var trashList = MapDataCarrier.Instance.TrashList;

		while (drewCount < drawCount) {
			MasterAction2Table.Data drawCard = null;
			if (deckList.Count > 0) {
				drawCard = deckList[0];
				if (BattleCalculationFunction.IsCurse(drawCard.Id) == true) {
					drawCount += resistCount;
				}
				deckList.RemoveAt(0);
				AddHand(drawCard);
                //var ctrl = MapDataCarrier.Instance.GetNonActiveBattleCardController();
                //if (ctrl != null) {
				//	ctrl.gameObject.SetActive(true);
				//	ctrl.SetData(drawCard);
				//	ctrl.UpdateDisplay();
				//	ctrl.UpdateInteractable(MapDataCarrier.Instance.CurrentTotalDiceCost);
				//} else {
				//	trashList.Add(drawCard);
				//}
				drewCount++;
			} else {
				if (trashList.Count > 0) {
					MapDataCarrier.Instance.DeckShuffle();
				} else {
					drewCount++;
				}
			}
		}

		TrashCountText.text = trashList.Count.ToString();
		DeckCountText.text = deckList.Count.ToString();
	}
	
	public void AddHand(MasterAction2Table.Data data) {
		var trashList = MapDataCarrier.Instance.TrashList;
		var ctrl = MapDataCarrier.Instance.GetNonActiveBattleCardController();
    	if (ctrl != null) {
			ctrl.gameObject.SetActive(true);
			ctrl.SetData(data);
			ctrl.UpdateDisplay();
			ctrl.UpdateInteractable(MapDataCarrier.Instance.CurrentTotalDiceCost);
		} else {
			trashList.Add(data);
		}
	}
	
	public void CheckAddCard(ActionPack pack) {
		// 動的にカード加える系（呪いとか、複製とか）
		if (
			(pack.Effect == EnumSelf.EffectType.AddCard2Deck) ||
			(pack.Effect == EnumSelf.EffectType.AddCard2Hand) ||
			(pack.Effect == EnumSelf.EffectType.AddCurse2Deck) ||
			(pack.Effect == EnumSelf.EffectType.AddCurse2Hand) ||
			(pack.Effect == EnumSelf.EffectType.AddCurse2DeckEternal)
		) {
			int actionId = pack.Value;
			MasterAction2Table.Data addData = MasterAction2Table.Instance.GetData(actionId);

			if (
				(pack.Effect == EnumSelf.EffectType.AddCard2Deck) ||
				(pack.Effect == EnumSelf.EffectType.AddCurse2Deck)
			) {
				// 山札に加える
				var deckList = MapDataCarrier.Instance.BattleDeckList;
				int random = UnityEngine.Random.Range(0, deckList.Count+1);
				deckList.Insert(random, addData);
			} else if (
				(pack.Effect == EnumSelf.EffectType.AddCard2Hand) ||
				(pack.Effect == EnumSelf.EffectType.AddCurse2Hand)
			) {
				// 手札に加える
				AddHand(addData);
			} else if (pack.Effect == EnumSelf.EffectType.AddCurse2DeckEternal) {
				// 戦闘中の山札に加えて、かつ、オリジナルにも加える
				var battleDeckList = MapDataCarrier.Instance.BattleDeckList;
				var originalDeckList = MapDataCarrier.Instance.OriginalDeckList;

				int random = UnityEngine.Random.Range(0, battleDeckList.Count+1);
				battleDeckList.Insert(random, addData);

				originalDeckList.Add(addData);
			}
		}
	}

	public void UpdateHandSelectToggle(bool isOn) {
		// ユーザー入力待機状態でなければ、処理しない
		var stm = StateMachineManager.Instance;
		if (stm.GetState(StateMachineName.Map) != (int)MapState.BattleHandSelectUserWait) {
			return;
		}

		MasterAction2Table.Data data = MapDataCarrier.Instance.SelectBattleCardData;
		int nowActionPackCount = MapDataCarrier.Instance.ActionPackCount;
		ActionPack pack = data.ActionPackList[nowActionPackCount]; 
		int maxValue = pack.Value;

		var ctrls = MapDataCarrier.Instance.BattleCardButtonControllers;
		int count = 0;
		for (int i = 0; i < ctrls.Count; i++) {
			if (ctrls[i].IsSelect() == true) {
				count++;
			}
		}

		int handCount = MapDataCarrier.Instance.GetHandCount();
		if (maxValue >= handCount) {
			maxValue = handCount;
		}
		
		HandCardSelectText.text = string.Format("説明文\n{0}/{1}", count.ToString(), maxValue.ToString());

		if (count >= maxValue) {
			HandCardSelectDecideButton.interactable = true;
			for (int i = 0; i < ctrls.Count; i++) {
				if (ctrls[i].IsSelect() == false) {
					ctrls[i].UpdateToggleActive(false);
				}
			}
		} else {
			HandCardSelectDecideButton.interactable = false;
			for (int i = 0; i < ctrls.Count; i++) {
				if (ctrls[i].ToggleActiveSelf() == false) {
					ctrls[i].UpdateToggleActive(true);
				}
			}
		}
	}
	
	public void OnClickHandCardSelectDecideButton() {
		// ユーザー入力待機状態でなければ、処理しない
		var stm = StateMachineManager.Instance;
		if (stm.GetState(StateMachineName.Map) != (int)MapState.BattleHandSelectUserWait) {
			return;
		}
		StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleHandSelectEnd);
	}
}
