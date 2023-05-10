using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class MenuScene : SceneBase
{
	// ↓↓固定テキスト↓↓
	[SerializeField]
	private Text SFMenuDungeonText = null;
	public Text MenuDungeonText => SFMenuDungeonText;
	
	[SerializeField]
	private Text SFMenuRegularCardText = null;
	public Text MenuRegularCardText => SFMenuRegularCardText;
	
	[SerializeField]
	private Text SFMenuRegularArtifactText = null;
	public Text MenuRegularArtifactText => SFMenuRegularArtifactText;
	
	[SerializeField]
	private Text SFMenuOptionText = null;
	public Text MenuOptionText => SFMenuOptionText;
	
	[SerializeField]
	private Text SFMenuOptionCloseButtonText = null;
	public Text MenuOptionCloseButtonText => SFMenuOptionCloseButtonText;
	
	[SerializeField]
	private Text SFMenuRegularCardReturnButtonText = null;
	public Text MenuRegularCardReturnButtonText => SFMenuRegularCardReturnButtonText;
	
	[SerializeField]
	private Text SFMenuRegularArtifactReturnButtonText = null;
	public Text MenuRegularArtifactReturnButtonText => SFMenuRegularArtifactReturnButtonText;
	
	// ↓↓ダンジョンメニュー↓↓
	[SerializeField]
	private GameObject SFDungeonRoot = null;
	public GameObject DungeonRoot => SFDungeonRoot;

	[SerializeField]
	private GameObject SFDungeonContent = null;
	public GameObject DungeonContent => SFDungeonContent;
	
	[SerializeField]
	private Text SFDungeonName = null;
	public Text DungeonName => SFDungeonName;
	
	[SerializeField]
	private Text SFDungeonDetail = null;
	public Text DungeonDetail => SFDungeonDetail;
	
	[SerializeField]
	private GameObject SFDungeonStartButton = null;
	public GameObject DungeonStartButton => SFDungeonStartButton;
	
	[SerializeField]
	private Text SFDungeonFloorCount = null;
	public Text DungeonFloorCount => SFDungeonFloorCount;
	// ↑↑ダンジョンメニュー↑↑

	// ↓↓オプションメニュー↓↓
	[SerializeField]
	private GameObject SFOptionRoot = null;
	public GameObject OptionRoot => SFOptionRoot;
	// トグル
	[SerializeField]
	private Toggle SFBgmToggle = null;
	public Toggle MBgmToggle => SFBgmToggle;
	[SerializeField]
	private Toggle SFSeToggle = null;
	public Toggle MSeToggle => SFSeToggle;

	// スライダー
	[SerializeField]
	private Slider SFBgmSlider = null;
	public Slider MBgmSlider => SFBgmSlider;
	[SerializeField]
	private Slider SFSeSlider = null;
	public Slider MSeSlider => SFSeSlider;

	// テキスト
	[SerializeField]
	private Text SFBgmVolumeText = null;
	[SerializeField]
	private Text SFSeVolumeText = null;
	[SerializeField]
	private Text SFBgmText = null;
	[SerializeField]
	private Text SFSeText = null;
	[SerializeField]
	private Text SFMuteText = null;
	// ↑↑オプションメニュー↑↑
	
	// ↓↓カードアンロック↓↓
	[SerializeField]
	private GameObject SFCardUnlockRoot = null;
	public GameObject CardUnlockRoot => SFCardUnlockRoot;

	[SerializeField]
	private GameObject SFCardContent = null;
	public GameObject CardContent => SFCardContent;
	// ↑↑カードアンロック↑↑
	
	// ↓↓アーティファクトアンロック↓↓
	[SerializeField]
	private GameObject SFArtifactUnlockRoot = null;
	public GameObject ArtifactUnlockRoot => SFArtifactUnlockRoot;

	[SerializeField]
	private GameObject SFArtifactContent = null;
	public GameObject ArtifactContent => SFArtifactContent;
	// ↑↑アーティファクトアンロック↑↑
	
	// ↓↓レギュラーカードメニュー↓↓
	[SerializeField]
	private GameObject SFRegularCardSettingRoot = null;
	public GameObject RegularCardSettingRoot => SFRegularCardSettingRoot;
	
	[SerializeField]
	private Text SFCarryPointText = null;
	public Text CarryPointText => SFCarryPointText;
	
	[SerializeField]
	private Text SFNowRegularCostText = null;
	public Text NowRegularCostText => SFNowRegularCostText;
	
	[SerializeField]
	private Text SFMaxRegularCostText = null;
	public Text MaxRegularCostText => SFMaxRegularCostText;
	
	[SerializeField]
	private GameObject SFCardContentRoot = null;
	public GameObject CardContentRoot => SFCardContentRoot;
	
	[SerializeField]
	private Text SFMaxCostUpNeedPointText = null;
	public Text MaxCostUpNeedPointText => SFMaxCostUpNeedPointText;
	
	[SerializeField]
	private Button SFMaxCostUpButton = null;
	public Button MaxCostUpButton => SFMaxCostUpButton;
	
	// TODO ボタンをコンポーネント化は、とりあえずあと
	// ひとまず、何が装備されているかだけ見れるようにしておく
	[SerializeField]
	private Text[] SFEquipCardTexts = null;
	public Text[] EquipCardTexts => SFEquipCardTexts;
	
	// カード詳細系
	[SerializeField]
	private GameObject SFCardDetailRoot = null;
	public GameObject CardDetailRoot => SFCardDetailRoot;
	
	[SerializeField]
	private Text SFCardDetailNameText = null;
	public Text CardDetailNameText => SFCardDetailNameText;
	
	[SerializeField]
	private Text SFCardDetailDetailText = null;
	public Text CardDetailDetailText => SFCardDetailDetailText;
	
	[SerializeField]
	private Text SFCardDetailCostText = null;
	public Text CardDetailCostText => SFCardDetailCostText;
	
	[SerializeField]
	private Image SFCardDetailImage = null;
	public Image CardDetailImage => SFCardDetailImage;
	
	[SerializeField]
	private Image SFCardDetailRarityFrameImage = null;
	public Image CardDetailRarityFrameImage => SFCardDetailRarityFrameImage;
	
	[SerializeField]
	private Button SFCardDetailUnlockButton = null;
	public Button CardDetailUnlockButton => SFCardDetailUnlockButton;
	
	[SerializeField]
	private Button SFCardDetailEquipButton = null;
	public Button CardDetailEquipButton => SFCardDetailEquipButton;
	
	[SerializeField]
	private GameObject SFCardEquipSelectRoot = null;
	public GameObject CardEquipSelectRoot => SFCardEquipSelectRoot;
	
	[SerializeField]
	private Button[] SFCardEquipSelectButtons = null;
	public Button[] CardEquipSelectButtons => SFCardEquipSelectButtons;
	
	[SerializeField]
	private GameObject SFCarryEquipCardContentRoot = null;
	public GameObject CarryEquipCardContentRoot => SFCarryEquipCardContentRoot;
	
	// ↑↑レギュラーカードメニュー↑↑
	
	// ↓↓レギュラーアーティファクトメニュー↓↓
	[SerializeField]
	private GameObject SFRegularArtifactSettingRoot = null;
	public GameObject RegularArtifactSettingRoot => SFRegularArtifactSettingRoot;
	
	[SerializeField]
	private Text SFArtifactCarryPointText = null;
	public Text ArtifactCarryPointText => SFArtifactCarryPointText;
	
	[SerializeField]
	private Text SFArtifactNowRegularCostText = null;
	public Text ArtifactNowRegularCostText => SFArtifactNowRegularCostText;
	
	[SerializeField]
	private Text SFArtifactMaxRegularCostText = null;
	public Text ArtifactMaxRegularCostText => SFArtifactMaxRegularCostText;
	
	[SerializeField]
	private GameObject SFArtifactContentRoot = null;
	public GameObject ArtifactContentRoot => SFArtifactContentRoot;
	
	[SerializeField]
	private Text SFArtifactMaxCostUpNeedPointText = null;
	public Text ArtifactMaxCostUpNeedPointText => SFArtifactMaxCostUpNeedPointText;
	
	[SerializeField]
	private Button SFArtifactMaxCostUpButton = null;
	public Button ArtifactMaxCostUpButton => SFArtifactMaxCostUpButton;
	
	// ↓↓↓トロフィー↓↓↓
	[SerializeField]
	private GameObject SFTrophyRoot = null;
	public GameObject TrophyRoot => SFTrophyRoot;
	
	[SerializeField]
	private GameObject SFTrophyNonAchieveListCellRoot = null;
	public GameObject TrophyNonAchieveListCellRoot => SFTrophyNonAchieveListCellRoot;
	
	[SerializeField]
	private GameObject SFTrophyAchieveListCellRoot = null;
	public GameObject TrophyAchieveListCellRoot => SFTrophyAchieveListCellRoot;
	
	[SerializeField]
	private GameObject SFNonAchieveListRoot = null;
	public GameObject NonAchieveListRoot => SFNonAchieveListRoot;
	
	[SerializeField]
	private GameObject SFAchieveListRoot = null;
	public GameObject AchieveListRoot => SFAchieveListRoot;
	
	[SerializeField]
	private Toggle SFNonAchieveToggle = null;
	public Toggle NonAchieveToggle => SFNonAchieveToggle;

	private Dictionary<int, TrophyCellItemController> TrophyLockCellDict = new Dictionary<int, TrophyCellItemController>();

	// ↑↑↑トロフィー↑↑↑

	
	// TODO ボタンをコンポーネント化は、とりあえずあと
	// ひとまず、何が装備されているかだけ見れるようにしておく
	[SerializeField]
	private Text[] SFEquipArtifactTexts = null;
	public Text[] EquipArtifactTexts => SFEquipArtifactTexts;
	
	// カード詳細系
	[SerializeField]
	private GameObject SFArtifactDetailRoot = null;
	public GameObject ArtifactDetailRoot => SFArtifactDetailRoot;
	
	[SerializeField]
	private Text SFArtifactDetailNameText = null;
	public Text ArtifactDetailNameText => SFArtifactDetailNameText;
	
	[SerializeField]
	private Text SFArtifactDetailDetailText = null;
	public Text ArtifactDetailDetailText => SFArtifactDetailDetailText;
	
	[SerializeField]
	private Text SFArtifactDetailCostText = null;
	public Text ArtifactDetailCostText => SFArtifactDetailCostText;
	
	[SerializeField]
	private Image SFArtifactDetailImage = null;
	public Image ArtifactDetailImage => SFArtifactDetailImage;
	
	[SerializeField]
	private Image SFArtifactDetailRarityFrameImage = null;
	public Image ArtifactDetailRarityFrameImage => SFArtifactDetailRarityFrameImage;
	
	[SerializeField]
	private Button SFArtifactDetailUnlockButton = null;
	public Button ArtifactDetailUnlockButton => SFArtifactDetailUnlockButton;
	
	[SerializeField]
	private Button SFArtifactDetailEquipButton = null;
	public Button ArtifactDetailEquipButton => SFArtifactDetailEquipButton;
	
	[SerializeField]
	private GameObject SFArtifactEquipSelectRoot = null;
	public GameObject ArtifactEquipSelectRoot => SFArtifactEquipSelectRoot;
	
	[SerializeField]
	private Button[] SFArtifactEquipSelectButtons = null;
	public Button[] ArtifactEquipSelectButtons => SFArtifactEquipSelectButtons;
	
	[SerializeField]
	private GameObject SFCarryEquipArtifactContentRoot = null;
	public GameObject CarryEquipArtifactContentRoot => SFCarryEquipArtifactContentRoot;
	
	// ↑↑レギュラーアーティファクトメニュー↑↑
	
	[SerializeField]
	private GameObject CuPopupObjectRoot = null;
	public GameObject PopupObjectRoot => CuPopupObjectRoot;

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

		// テキスト設定
		SFBgmText.text = MasterTextTable.Instance.GetData("OptionBgmText").Text;
		SFSeText.text = MasterTextTable.Instance.GetData("OptionSeText").Text;
		SFMuteText.text = MasterTextTable.Instance.GetData("OptionMuteText").Text;

		StateMachineManager.Instance.ChangeState(StateMachineName.Menu, (int)MenuState.Initialize);
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
	// ↓↓メニュー機能↓↓
	public void OnClickOptionButton() {
        var stm = StateMachineManager.Instance;
		// ユーザー入力待機状態でなければ、処理しない
		if (stm.GetNextState(StateMachineName.Menu) != (int)MenuState.UserWait) {
			return;
		}
		OptionRoot.SetActive(true);
	}
	
	public void OnClickDungeonButton() {
        var stm = StateMachineManager.Instance;
		// ユーザー入力待機状態でなければ、処理しない
		if (stm.GetNextState(StateMachineName.Menu) != (int)MenuState.UserWait) {
			return;
		}
		DungeonRoot.SetActive(true);
	}
	
	public void OnClickDungeonCloseButton() {
        var stm = StateMachineManager.Instance;
		// ユーザー入力待機状態でなければ、処理しない
		if (stm.GetNextState(StateMachineName.Menu) != (int)MenuState.UserWait) {
			return;
		}
		DungeonRoot.SetActive(false);
	}
	
	public void OnClickCardUnlockButton() {
        var stm = StateMachineManager.Instance;
		// ユーザー入力待機状態でなければ、処理しない
		if (stm.GetNextState(StateMachineName.Menu) != (int)MenuState.UserWait) {
			return;
		}
		CardUnlockRoot.SetActive(true);
	}
	
	public void OnClickArtifactUnlockButton() {
        var stm = StateMachineManager.Instance;
		// ユーザー入力待機状態でなければ、処理しない
		if (stm.GetNextState(StateMachineName.Menu) != (int)MenuState.UserWait) {
			return;
		}
		ArtifactUnlockRoot.SetActive(true);
	}
	
	public void OnClickRegularCardSettingButton() {
        var stm = StateMachineManager.Instance;
		// ユーザー入力待機状態でなければ、処理しない
		if (stm.GetNextState(StateMachineName.Menu) != (int)MenuState.UserWait) {
			return;
		}
		StateMachineManager.Instance.ChangeState(StateMachineName.Menu, (int)MenuState.RegularCardSettingInitialize);
	}
	
	public void OnClickRegularArtifactSettingButton() {
        var stm = StateMachineManager.Instance;
		// ユーザー入力待機状態でなければ、処理しない
		if (stm.GetNextState(StateMachineName.Menu) != (int)MenuState.UserWait) {
			return;
		}
		StateMachineManager.Instance.ChangeState(StateMachineName.Menu, (int)MenuState.RegularArtifactSettingInitialize);
	}
	
	public void OnClickTrophyButton() {
        var stm = StateMachineManager.Instance;
		// ユーザー入力待機状態でなければ、処理しない
		if (stm.GetNextState(StateMachineName.Menu) != (int)MenuState.UserWait) {
			return;
		}
		TrophyRoot.SetActive(true);
		NonAchieveToggle.isOn = true;
		OnNonAchieveToggleValueChange(true);

		var dict = MasterTrophyTable.Instance.GetCloneDict();

		if (TrophyLockCellDict.Count == 0) {
			foreach (var data in dict) {
				int isReceipt = PlayerPrefsManager.Instance.GetTrophyUnlock(data.Value.Id);
				if (isReceipt == 1) {
					var data2 = data.Value;
					ResourceManager.Instance.RequestExecuteOrder(
						Const.TrophyCellItemPath,
						ExecuteOrder.Type.GameObject,
						gameObject,
						(rawobj) => {
							GameObject obj = GameObject.Instantiate(rawobj) as GameObject;
							obj.transform.SetParent(SFTrophyAchieveListCellRoot.transform);
							obj.transform.localPosition = Vector3.zero;
							obj.transform.localScale = Vector3.one;
							var ctrl = obj.GetComponent<TrophyCellItemController>();
							ctrl.Initialize(data2, true, (ctrl) => {});
							TrophyLockCellDict.Add(data2.Id, ctrl);
						}
					);
				} else {
					MasterTrophyTable.Data tData = data.Value;
					ResourceManager.Instance.RequestExecuteOrder(
						Const.TrophyCellItemPath,
						ExecuteOrder.Type.GameObject,
						gameObject,
						(rawobj) => {
							GameObject obj = GameObject.Instantiate(rawobj) as GameObject;
							obj.transform.SetParent(SFTrophyNonAchieveListCellRoot.transform);
							obj.transform.localPosition = Vector3.zero;
							obj.transform.localScale = Vector3.one;
							var ctrl = obj.GetComponent<TrophyCellItemController>();
							ctrl.Initialize(
								tData,
								false,
								(ctrl) => {
									ctrl.UpdateCellButtonInteractable(false);
									var data2 = ctrl.GetData();
									if (data2.RewardType == EnumSelf.TrophyRewardType.CardCostUp) {
										
									}
									ctrl.transform.SetParent(SFTrophyAchieveListCellRoot.transform);
									PlayerPrefsManager.Instance.SaveTrophyUnlock(data2.Id, 1);
								}
							);
							TrophyLockCellDict.Add(tData.Id, ctrl);
						}
					);
				}
			}
		} else {
			foreach (var data in dict) {
				int isReceipt = PlayerPrefsManager.Instance.GetTrophyUnlock(data.Value.Id);
				var ctrl = TrophyLockCellDict[data.Value.Id];
				bool isAchieved = isReceipt == 1 ? true : false;
				ctrl.UpdateDisplay(isAchieved);
			}
		}
	}

	public void UpdateEquipCardCostText() {
		NowRegularCostText.text = GetNowEquipCost().ToString();
	}
	
	public int GetNowEquipCost() {
		int cost = 0;
        List<int> ids = PlayerPrefsManager.Instance.GetRegularSettingCardIds();

        for (int i = 0; i < ids.Count; i++) {
			MasterAction2Table.Data data = MasterAction2Table.Instance.GetData(ids[i]);
			cost += data.EquipCost;
		}

		return cost;
	}
	
	public void UpdateEquipArtifactCostText() {
		ArtifactNowRegularCostText.text = GetNowArtifactEquipCost().ToString();
	}
	
	public int GetNowArtifactEquipCost() {
		int cost = 0;
        List<int> ids = PlayerPrefsManager.Instance.GetRegularSettingArtifactIds();

        for (int i = 0; i < ids.Count; i++) {
			MasterArtifactTable.Data data = MasterArtifactTable.Instance.GetData(ids[i]);
			cost += data.EquipCost;
		}

		return cost;
	}
	// ↑↑メニュー機能↑↑

	// ↓↓ダンジョン機能↓↓
	public void OnClickDungeonListButtonCallback(MasterDungeonTable.Data data) {
        var stm = StateMachineManager.Instance;
		// ユーザー入力待機状態でなければ、処理しない
		if (stm.GetNextState(StateMachineName.Menu) != (int)MenuState.UserWait) {
			return;
		}

		MenuDataCarrier.Instance.DungeonData = data;

		// ここで、表示内容更新
		SFDungeonName.text = data.Name;
		SFDungeonDetail.text = data.Detail;
		SFDungeonFloorCount.text = string.Format("{0}階層", data.FloorCount.ToString());
		SFDungeonStartButton.SetActive(true);
	}
	
	public void OnClickDungeonStartButton() {
        var stm = StateMachineManager.Instance;
		// ユーザー入力待機状態でなければ、処理しない
		if (stm.GetNextState(StateMachineName.Menu) != (int)MenuState.UserWait) {
			return;
		}

		MenuDataCarrier.Instance.NextSceneName = LocalSceneManager.SceneName.Map;
		MapData data = new MapData();
		data.Data = MenuDataCarrier.Instance.DungeonData;
		MenuDataCarrier.Instance.Data = (SceneDataBase)data;

		StateMachineManager.Instance.ChangeState(StateMachineName.Menu, (int)MenuState.End);
	}
	// ↑↑ダンジョン機能↑↑

	// ↓↓オプション機能↓↓
	public void OnClickOptionCloseButton() {
        var stm = StateMachineManager.Instance;
		// ユーザー入力待機状態でなければ、処理しない
		if (stm.GetNextState(StateMachineName.Menu) != (int)MenuState.UserWait) {
			return;
		}
		OptionRoot.SetActive(false);
		CheckVolumeState();
	}
	
	public void OnClickSeMuteToggle(bool isOn) {
		SoundManager.Instance.SetSeMuteFlag(isOn);
	}
	
	public void OnClickBgmMuteToggle(bool isOn) {
		SoundManager.Instance.SetBgmMuteFlag(isOn);
	}
	
	public void OnValueChangeSeVolumeSlider(float val) {
		SoundManager.Instance.SetSeVolume(val);
		UpdateSeVolumeText();
	}
	
	public void OnValueChangeBgmVolumeSlider(float val) {
		SoundManager.Instance.SetBgmVolume(val);
		UpdateBgmVolumeText();
	}

	public void UpdateBgmVolumeText()
	{
		SFBgmVolumeText.text = ((int)SFBgmSlider.value).ToString();
	}

	public void UpdateSeVolumeText()
	{
		SFSeVolumeText.text = ((int)SFSeSlider.value).ToString();
	}

	private void CheckVolumeState() {
		// BGMのボリュームに変更があれば、保存
		string saveString = "";
		int prevBgmVolume = PlayerPrefsManager.Instance.GetBgmVolume();
		int sliderVolume = (int)(SFBgmSlider.value);

		if (prevBgmVolume != sliderVolume) {
			saveString = sliderVolume.ToString();
			PlayerPrefsManager.Instance.SaveParameter("BgmVolume", saveString);
		}
		
		// SEのボリュームに変更があれば、保存
		saveString = "";
		int prevSeVolume = PlayerPrefsManager.Instance.GetSeVolume();
		sliderVolume = (int)(SFSeSlider.value);

		if (prevBgmVolume != sliderVolume) {
			saveString = sliderVolume.ToString();
			PlayerPrefsManager.Instance.SaveParameter("SeVolume", saveString);
		}

		// BGMのミュートに変更があれば、保存
		bool isOn = PlayerPrefsManager.Instance.GetBgmIsMute();
		bool toggleIsOn = SFBgmToggle.isOn;

		if (isOn != toggleIsOn) {
			saveString = toggleIsOn.ToString();
    		PlayerPrefsManager.Instance.SaveParameter("BgmMute", saveString);
    	}

		// SEのミュートに変更があれば、保存
		isOn = PlayerPrefsManager.Instance.GetSeIsMute();
		toggleIsOn = SFSeToggle.isOn;

		if (isOn != toggleIsOn) {
			saveString = toggleIsOn.ToString();
    		PlayerPrefsManager.Instance.SaveParameter("SeMute", saveString);
    	}
	}
	// ↑↑オプション機能↑↑
	
	// ↓↓カードアンロック↓↓
	public void OnClickCardUnlockCloseButton() {
        var stm = StateMachineManager.Instance;
		// ユーザー入力待機状態でなければ、処理しない
		if (stm.GetNextState(StateMachineName.Menu) != (int)MenuState.UserWait) {
			return;
		}
		CardUnlockRoot.SetActive(false);
	}
	// ↑↑カードアンロック↑↑
	
	// ↓↓レギュラーカード↓↓
	public void OnClickRegularCardSettingCloseButton() {
        var stm = StateMachineManager.Instance;
		// ユーザー入力待機状態でなければ、処理しない
		if (stm.GetNextState(StateMachineName.Menu) != (int)MenuState.RegularCardSettingUserWait) {
			return;
		}
		StateMachineManager.Instance.ChangeState(StateMachineName.Menu, (int)MenuState.RegularCardSettingEnd);
	}
	
	public void OnClickMaxCostUpButton() {
        var stm = StateMachineManager.Instance;
		// ユーザー入力待機状態でなければ、処理しない
		if (stm.GetNextState(StateMachineName.Menu) != (int)MenuState.RegularCardSettingUserWait) {
			return;
		}
		StateMachineManager.Instance.ChangeState(StateMachineName.Menu, (int)MenuState.RegularCardSettingMaxCostUp);
	}

	public void UpdateMaxCostUpDisplay() {
		int carryPoint = PlayerPrefsManager.Instance.GetPoint();
		CarryPointText.text = carryPoint.ToString();

		int usedPoint = PlayerPrefsManager.Instance.GetUsedRegularCostPoint();
		int level = MasterRegularCardMaxCostTable.Instance.GetNowLevel(usedPoint);
		
		int maxCost = GetMaxCost();
		MaxRegularCostText.text = maxCost.ToString();

		if (MasterRegularCardMaxCostTable.Instance.IsMaxLevel(level) == true) {
			MaxCostUpButton.interactable = false;
			MaxCostUpNeedPointText.text = "-";
		} else {
			int nextNeedPoint = MasterRegularCardMaxCostTable.Instance.GetNextLevelNeedPoint(usedPoint);
			MaxCostUpButton.interactable = true;
			MaxCostUpNeedPointText.text = nextNeedPoint.ToString();

			if (carryPoint >= nextNeedPoint) {
				MaxCostUpButton.interactable = true;
			} else {
				MaxCostUpButton.interactable = false;
			}
		}
	}

	public int GetMaxCost() {
		int usedPoint = PlayerPrefsManager.Instance.GetUsedRegularCostPoint();
		int level = MasterRegularCardMaxCostTable.Instance.GetNowLevel(usedPoint);
		
		int maxCost = Const.BaseRegularCardMaxCost + level + PlayerPrefsManager.Instance.GetUnlockCardCostUp();

		return maxCost;
	}
	
	public void OnClickEquipCardDetailButton(int index) {
        var stm = StateMachineManager.Instance;
		// ユーザー入力待機状態でなければ、処理しない
		if (stm.GetNextState(StateMachineName.Menu) != (int)MenuState.RegularCardSettingUserWait) {
			return;
		}

		MenuDataCarrier.Instance.EquipSelectIndex = index;
		StateMachineManager.Instance.ChangeState(StateMachineName.Menu, (int)MenuState.RegularCardSettingEquipCardDetailOpen);
	}
	
	public void OnClickCardDetailButton(RegularSettingCardContentItem item) {
        var stm = StateMachineManager.Instance;
		// ユーザー入力待機状態でなければ、処理しない
		if (stm.GetNextState(StateMachineName.Menu) != (int)MenuState.RegularCardSettingUserWait) {
			return;
		}
		MenuDataCarrier.Instance.SelectCardContentItem = item;
		StateMachineManager.Instance.ChangeState(StateMachineName.Menu, (int)MenuState.RegularCardSettingCardDetailOpen);
	}
	
	public void OnClickCardDetailCloseButton() {
        var stm = StateMachineManager.Instance;
		// ユーザー入力待機状態でなければ、処理しない
		if (stm.GetNextState(StateMachineName.Menu) != (int)MenuState.RegularCardSettingUserWait) {
			return;
		}
		
		MenuDataCarrier.Instance.SelectCardContentItem = null;
		MenuDataCarrier.Instance.EquipCardSelectData = null;

		StateMachineManager.Instance.ChangeState(StateMachineName.Menu, (int)MenuState.RegularCardSettingCardDetailClose);
	}
	
	public void OnClickCardDetailUnlockButton() {
        var stm = StateMachineManager.Instance;
		// ユーザー入力待機状態でなければ、処理しない
		if (stm.GetNextState(StateMachineName.Menu) != (int)MenuState.RegularCardSettingUserWait) {
			return;
		}
		StateMachineManager.Instance.ChangeState(StateMachineName.Menu, (int)MenuState.RegularCardSettingCardDetailUnlock);
	}
	
	public void OnClickCardDetailEquipButton() {
        var stm = StateMachineManager.Instance;
		// ユーザー入力待機状態でなければ、処理しない
		if (stm.GetNextState(StateMachineName.Menu) != (int)MenuState.RegularCardSettingUserWait) {
			return;
		}
		StateMachineManager.Instance.ChangeState(StateMachineName.Menu, (int)MenuState.RegularCardSettingCardDetailEquip);
	}
	
	public void OnClickCardDetailEquipSelectButton(int index) {
/*        var stm = StateMachineManager.Instance;
		// ユーザー入力待機状態でなければ、処理しない
		if (stm.GetNextState(StateMachineName.Menu) != (int)MenuState.RegularCardSettingCardDetailEquipUserWait) {
			return;
		}

		var item = MenuDataCarrier.Instance.SelectCardContentItem;
		var data = item.GetData();

		// 装備している情報保存
		PlayerPrefsManager.Instance.SaveRegularSettingCardId(data.Id, index);
		
		// 現在コスト更新
		UpdateEquipCardCostText();

		// ボタンの表示修正
		for (int i = 0; i < 6; i++) {
			UpdateEquipCardDisplay(i);
		}
		
		SFCardEquipSelectRoot.SetActive(false);
		StateMachineManager.Instance.ChangeState(StateMachineName.Menu, (int)MenuState.RegularCardSettingUserWait);*/
	}
	
	public void OnClickCardDetailEquipSelectBackButton() {
        var stm = StateMachineManager.Instance;
		// ユーザー入力待機状態でなければ、処理しない
		if (stm.GetNextState(StateMachineName.Menu) != (int)MenuState.RegularCardSettingCardDetailEquipUserWait) {
			return;
		}
		SFCardEquipSelectRoot.SetActive(false);
		StateMachineManager.Instance.ChangeState(StateMachineName.Menu, (int)MenuState.RegularCardSettingUserWait);
	}
	
	public void OnClickCarryCardButton(MasterAction2Table.Data data) {
        var stm = StateMachineManager.Instance;
		// ユーザー入力待機状態でなければ、処理しない
		if (stm.GetNextState(StateMachineName.Menu) != (int)MenuState.RegularCardSettingUserWait) {
			return;
		}

		MenuDataCarrier.Instance.EquipCardSelectData = data;
		StateMachineManager.Instance.ChangeState(StateMachineName.Menu, (int)MenuState.RegularCardSettingEquipCardDetailOpen);
	}
	
	public void OnClickCarryCardDetailButton(RegularCardButtonController ctrl) {
		// TODO 選択された装備カードを取り外す処理
        var stm = StateMachineManager.Instance;
		// ユーザー入力待機状態でなければ、処理しない
		if (stm.GetNextState(StateMachineName.Menu) != (int)MenuState.RegularCardSettingUserWait) {
			return;
		}

		var data = ctrl.GetData();
		MenuDataCarrier.Instance.RegularCardButtonControllers.Remove(ctrl);
		//ctrl.gameObject.SetActive(false);
		GameObject.Destroy(ctrl.gameObject);
		PlayerPrefsManager.Instance.RemoveRegularSettingCardId(data.Id);

		UpdateEquipCardCostText();
	}
	// ↑↑レギュラーカード↑↑
	
	// ↓↓レギュラーアーティファクト↓↓
	public void OnClickRegularArtifactSettingCloseButton() {
        var stm = StateMachineManager.Instance;
		// ユーザー入力待機状態でなければ、処理しない
		if (stm.GetNextState(StateMachineName.Menu) != (int)MenuState.RegularArtifactSettingUserWait) {
			return;
		}
		StateMachineManager.Instance.ChangeState(StateMachineName.Menu, (int)MenuState.RegularArtifactSettingEnd);
	}
	
	public void OnClickArtifactMaxCostUpButton() {
		Debug.Log("Click");
        var stm = StateMachineManager.Instance;
		// ユーザー入力待機状態でなければ、処理しない
		if (stm.GetNextState(StateMachineName.Menu) != (int)MenuState.RegularArtifactSettingUserWait) {
			return;
		}
		StateMachineManager.Instance.ChangeState(StateMachineName.Menu, (int)MenuState.RegularArtifactSettingMaxCostUp);
	}

	public void UpdateArtifactMaxCostUpDisplay() {
		int carryPoint = PlayerPrefsManager.Instance.GetPoint();
		ArtifactCarryPointText.text = carryPoint.ToString();

		int usedPoint = PlayerPrefsManager.Instance.GetUsedArtifactRegularCostPoint();
		int level = MasterRegularArtifactMaxCostTable.Instance.GetNowLevel(usedPoint);
		
		int maxCost = GetArtifactMaxCost();
		ArtifactMaxRegularCostText.text = maxCost.ToString();

		if (MasterRegularArtifactMaxCostTable.Instance.IsMaxLevel(level) == true) {
			ArtifactMaxCostUpButton.interactable = false;
			ArtifactMaxCostUpNeedPointText.text = "-";
		} else {
			int nextNeedPoint = MasterRegularArtifactMaxCostTable.Instance.GetNextLevelNeedPoint(usedPoint);
			ArtifactMaxCostUpButton.interactable = true;
			ArtifactMaxCostUpNeedPointText.text = nextNeedPoint.ToString();

			if (carryPoint >= nextNeedPoint) {
				ArtifactMaxCostUpButton.interactable = true;
			} else {
				ArtifactMaxCostUpButton.interactable = false;
			}
		}
	}

	public int GetArtifactMaxCost() {
		int usedPoint = PlayerPrefsManager.Instance.GetUsedArtifactRegularCostPoint();
		int level = MasterRegularArtifactMaxCostTable.Instance.GetNowLevel(usedPoint);
		
		int maxCost = Const.BaseRegularArtifactMaxCost + level + PlayerPrefsManager.Instance.GetUnlockArtifactCostUp();

		return maxCost;
	}
	
	public void OnClickEquipArtifactDetailButton(int index) {
        var stm = StateMachineManager.Instance;
		// ユーザー入力待機状態でなければ、処理しない
		if (stm.GetNextState(StateMachineName.Menu) != (int)MenuState.RegularArtifactSettingUserWait) {
			return;
		}

		MenuDataCarrier.Instance.EquipArtifactSelectIndex = index;
		StateMachineManager.Instance.ChangeState(StateMachineName.Menu, (int)MenuState.RegularArtifactSettingEquipArtifactDetailOpen);
	}
	
	public void OnClickArtifactDetailButton(RegularSettingArtifactContentItem item) {
        var stm = StateMachineManager.Instance;
		// ユーザー入力待機状態でなければ、処理しない
		if (stm.GetNextState(StateMachineName.Menu) != (int)MenuState.RegularArtifactSettingUserWait) {
			return;
		}
		MenuDataCarrier.Instance.SelectArtifactContentItem = item;
		StateMachineManager.Instance.ChangeState(StateMachineName.Menu, (int)MenuState.RegularArtifactSettingArtifactDetailOpen);
	}
	
	public void OnClickArtifactDetailCloseButton() {
        var stm = StateMachineManager.Instance;
		// ユーザー入力待機状態でなければ、処理しない
		if (stm.GetNextState(StateMachineName.Menu) != (int)MenuState.RegularArtifactSettingUserWait) {
			return;
		}
		StateMachineManager.Instance.ChangeState(StateMachineName.Menu, (int)MenuState.RegularArtifactSettingArtifactDetailClose);
	}
	
	public void OnClickArtifactDetailUnlockButton() {
        var stm = StateMachineManager.Instance;
		// ユーザー入力待機状態でなければ、処理しない
		if (stm.GetNextState(StateMachineName.Menu) != (int)MenuState.RegularArtifactSettingUserWait) {
			return;
		}
		StateMachineManager.Instance.ChangeState(StateMachineName.Menu, (int)MenuState.RegularArtifactSettingArtifactDetailUnlock);
	}
	
	public void OnClickArtifactDetailEquipButton() {
        var stm = StateMachineManager.Instance;
		// ユーザー入力待機状態でなければ、処理しない
		if (stm.GetNextState(StateMachineName.Menu) != (int)MenuState.RegularArtifactSettingUserWait) {
			return;
		}
		StateMachineManager.Instance.ChangeState(StateMachineName.Menu, (int)MenuState.RegularArtifactSettingArtifactDetailEquip);
	}
	
	public void OnClickArtifactDetailEquipSelectBackButton() {
        var stm = StateMachineManager.Instance;
		// ユーザー入力待機状態でなければ、処理しない
		if (stm.GetNextState(StateMachineName.Menu) != (int)MenuState.RegularArtifactSettingArtifactDetailEquipUserWait) {
			return;
		}
		SFArtifactEquipSelectRoot.SetActive(false);
		StateMachineManager.Instance.ChangeState(StateMachineName.Menu, (int)MenuState.RegularArtifactSettingUserWait);
	}
	
	public void OnClickCarryArtifactButton(MasterArtifactTable.Data data) {
        var stm = StateMachineManager.Instance;
		// ユーザー入力待機状態でなければ、処理しない
		if (stm.GetNextState(StateMachineName.Menu) != (int)MenuState.RegularArtifactSettingUserWait) {
			return;
		}

		MenuDataCarrier.Instance.EquipArtifactSelectData = data;
		StateMachineManager.Instance.ChangeState(StateMachineName.Menu, (int)MenuState.RegularArtifactSettingEquipArtifactDetailOpen);
	}
	
	public void OnClickCarryArtifactDetailButton(RegularArtifactButtonController ctrl) {
		// TODO 選択された装備カードを取り外す処理
        var stm = StateMachineManager.Instance;
		// ユーザー入力待機状態でなければ、処理しない
		if (stm.GetNextState(StateMachineName.Menu) != (int)MenuState.RegularArtifactSettingUserWait) {
			return;
		}

		var data = ctrl.GetData();
		MenuDataCarrier.Instance.RegularArtifactButtonControllers.Remove(ctrl);
		//ctrl.gameObject.SetActive(false);
		GameObject.Destroy(ctrl.gameObject);
		PlayerPrefsManager.Instance.RemoveRegularSettingArtifactId(data.Id);

		UpdateEquipArtifactCostText();
	}
	// ↑↑レギュラーアーティファクト↑↑
	
	public void OnClickCardDetailBgButton() {
		MasterAction2Table.Data data = null;

		if (MenuDataCarrier.Instance.SelectCardContentItem != null) {
			data = MenuDataCarrier.Instance.SelectCardContentItem.GetData();
		}
		
		if (MenuDataCarrier.Instance.EquipCardSelectData != null) {
			data = MenuDataCarrier.Instance.EquipCardSelectData;
		}

		if (PlayerPrefsManager.Instance.IsFindCard(data.Id) == false) {
			return;
		}

		MenuDataCarrier.Instance.SpawnPopup(data);
	}
	
	public void OnClickArtifactDetailBgButton() {
		MasterArtifactTable.Data data = null;

		if (MenuDataCarrier.Instance.SelectArtifactContentItem != null) {
			data = MenuDataCarrier.Instance.SelectArtifactContentItem.GetData();
		}
		
		if (MenuDataCarrier.Instance.EquipArtifactSelectData != null) {
			data = MenuDataCarrier.Instance.EquipArtifactSelectData;
		}

		if (PlayerPrefsManager.Instance.IsFindArtifact(data.Id) == false) {
			return;
		}

		MenuDataCarrier.Instance.SpawnPopup(data);
	}

	// ↓トロフィー
	public void OnClickTrophyCloseButton() {
        var stm = StateMachineManager.Instance;
		// ユーザー入力待機状態でなければ、処理しない
		if (stm.GetNextState(StateMachineName.Menu) != (int)MenuState.UserWait) {
			return;
		}
		TrophyRoot.SetActive(false);
	}
	
	public void OnNonAchieveToggleValueChange(bool isOn) {
		SFNonAchieveListRoot.SetActive(isOn);
		SFAchieveListRoot.SetActive(!isOn);
	}
	// ↑トロフィー
}
