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
	private Text CuTreasureDetailCardDetail = null;
	public Text TreasureDetailCardDetail => CuTreasureDetailCardDetail;
	
	[SerializeField]
	private Image CuTreasureDetailCardImage = null;
	public Image TreasureDetailCardImage => CuTreasureDetailCardImage;
	
	[SerializeField]
	private Image CuTreasureRarityFrameImage = null;
	public Image TreasureRarityFrameImage => CuTreasureRarityFrameImage;
	
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
	
	public void OnClickCarryCardDetailButton(int index) {
		// ユーザー入力待機状態でなければ、処理しない
		//var stm = StateMachineManager.Instance;
		//if (stm.GetState(StateMachineName.Map) != (int)MapState.DungeonResultDisplay) {
		//	return;
		//}

		MasterAction2Table.Data data = MapDataCarrier.Instance.CuPlayerStatus.GetActionData(index);
		CarryCardDetailController.Open(data);
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
	
	public void UpdatePlayerValueObject(int diceIndex) {
		PlayerStatus player = MapDataCarrier.Instance.CuPlayerStatus;
		EnemyStatus enemy = MapDataCarrier.Instance.CuEnemyStatus;

		MasterAction2Table.Data pdata = player.GetActionData(diceIndex);
		CuPlayerActionNameStrings[diceIndex].text = pdata.Name;

		// TODO これ、MapInitializeStateで先読みしといた方がいいかも
		int attackIndex = diceIndex;
		ResourceManager.Instance.RequestExecuteOrder(
			string.Format(Const.AttackButtonImagePath, pdata.Rarity),
			ExecuteOrder.Type.Sprite,
			gameObject,
			(rawSprite) => {
				CuAttackButtonImages[attackIndex].sprite = rawSprite as Sprite;
			}
		);

		var list = pdata.ActionPackList;
		int index = 0;
		for (index = 0; index < list.Count; index++) {
			if (index >= 10) {
				LogManager.Instance.LogError("MapScene:UpdatePlayerValueObject:添え字10以上になってる");
			}
			int val = list[index].Value;
			if (
					(list[index].Effect == EnumSelf.EffectType.Damage) ||
					(list[index].Effect == EnumSelf.EffectType.DamageSuction)
			) {
				int strength = player.GetPower().GetValue(EnumSelf.PowerType.Strength);
				val += strength;
				if (player.GetTurnPowerValue(EnumSelf.TurnPowerType.Weakness) > 0) {
					val = val - (val * 25 / 100);
				}
				
				if (enemy.GetTurnPowerValue(EnumSelf.TurnPowerType.Vulnerable) > 0) {
					val = val + (val * 50 / 100);
				}
			} else if (list[index].Effect == EnumSelf.EffectType.Shield) {
				if (player.GetTurnPowerValue(EnumSelf.TurnPowerType.ShieldWeakness) > 0) {
					val = val - (val * 25 / 100);
				}
			}
			MapDataCarrier.Instance.ValueObjects[diceIndex][index].GetComponent<ValueController>().UpdateDisplay(
				list[index].Effect,
				list[index].Value,
				val
			);
		}

		for (; index < 10; index++) {
			MapDataCarrier.Instance.ValueObjects[diceIndex][index].GetComponent<ValueController>().Hide();
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
				(list[index].Effect == EnumSelf.EffectType.DamageSuction)
			) {
				int strength = enemy.GetPower().GetValue(EnumSelf.PowerType.Strength);
				val += strength;
				if (enemy.GetTurnPowerValue(EnumSelf.TurnPowerType.Weakness) > 0) {
					val = val - (val * 25 / 100);
				}
				
				if (player.GetTurnPowerValue(EnumSelf.TurnPowerType.Vulnerable) > 0) {
					val = val + (val * 50 / 100);
				}
			} else if (list[index].Effect == EnumSelf.EffectType.Shield) {
				if (enemy.GetTurnPowerValue(EnumSelf.TurnPowerType.ShieldWeakness) > 0) {
					val = val - (val * 25 / 100);
				}
			}

			GameObject obj = MapDataCarrier.Instance.EnemyValueObjects[index];
			obj.GetComponent<ValueController>().UpdateDisplay(list[index].Effect, list[index].Value, val);
		}

		for (; index < 10; index++) {
			GameObject obj = MapDataCarrier.Instance.EnemyValueObjects[index];
			obj.GetComponent<ValueController>().Hide();
		}
	}
}
