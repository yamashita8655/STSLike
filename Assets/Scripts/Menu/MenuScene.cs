using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class MenuScene : SceneBase
{
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
	// ↑↑レギュラーカードメニュー↑↑

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

	public void UpdateEquipCardDisplay(int index) {
		int id = PlayerPrefsManager.Instance.GetRegularSettingCardId(index);
		MasterAction2Table.Data data = MasterAction2Table.Instance.GetData(id);
		SFEquipCardTexts[index].text = data.Name;
	}
	
	public void UpdateEquipCardCostText() {
		NowRegularCostText.text = GetNowEquipCost().ToString();
	}
	
	public int GetNowEquipCost() {
		int cost = 0;
		for (int i = 0; i < 6; i++) {
			int id = PlayerPrefsManager.Instance.GetRegularSettingCardId(i);
			MasterAction2Table.Data data = MasterAction2Table.Instance.GetData(id);
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
		MenuData data = new MenuData();
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
			PlayerPrefsManager.Instance.SaveParameter(PlayerPrefsManager.SaveType.BgmVolume, saveString);
		}
		
		// SEのボリュームに変更があれば、保存
		saveString = "";
		int prevSeVolume = PlayerPrefsManager.Instance.GetSeVolume();
		sliderVolume = (int)(SFSeSlider.value);

		if (prevBgmVolume != sliderVolume) {
			saveString = sliderVolume.ToString();
			PlayerPrefsManager.Instance.SaveParameter(PlayerPrefsManager.SaveType.SeVolume, saveString);
		}

		// BGMのミュートに変更があれば、保存
		bool isOn = PlayerPrefsManager.Instance.GetBgmIsMute();
		bool toggleIsOn = SFBgmToggle.isOn;

		if (isOn != toggleIsOn) {
			saveString = toggleIsOn.ToString();
    		PlayerPrefsManager.Instance.SaveParameter(PlayerPrefsManager.SaveType.BgmMute, saveString);
    	}

		// SEのミュートに変更があれば、保存
		isOn = PlayerPrefsManager.Instance.GetSeIsMute();
		toggleIsOn = SFSeToggle.isOn;

		if (isOn != toggleIsOn) {
			saveString = toggleIsOn.ToString();
    		PlayerPrefsManager.Instance.SaveParameter(PlayerPrefsManager.SaveType.SeMute, saveString);
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
		
		int maxCost = Const.BaseRegularCardMaxCost + level;

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
        var stm = StateMachineManager.Instance;
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
		StateMachineManager.Instance.ChangeState(StateMachineName.Menu, (int)MenuState.RegularCardSettingUserWait);
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
	// ↑↑レギュラーカード↑↑
}
