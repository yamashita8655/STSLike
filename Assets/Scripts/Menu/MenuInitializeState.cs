using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInitializeState : StateBase {

	private MenuScene Scene = null;

	private readonly string DungeonButtonPath = "Prefab/UI/DungeonButton";
//	private readonly string CardContentItemPath = "Prefab/UI/CardContentItem";
//	private readonly string ArtifactContentItemPath = "Prefab/UI/ArtifactContentItem";

    /// <summary>
    /// メイン前処理.
    /// 戻り値は、同一フレーム内で次の処理に移行してよければfalse、1フレーム飛ばして欲しい場合はfalse.
    /// </summary>
    override public bool OnBeforeMain()
    {
		Scene = MenuDataCarrier.Instance.Scene as MenuScene;

		Scene.OptionRoot.SetActive(false);
		Scene.DungeonRoot.SetActive(false);
		Scene.CardUnlockRoot.SetActive(false);
		Scene.ArtifactUnlockRoot.SetActive(false);
		Scene.RegularCardSettingRoot.SetActive(false);
		Scene.CardEquipSelectRoot.SetActive(false);
		Scene.RegularArtifactSettingRoot.SetActive(false);
		Scene.ArtifactEquipSelectRoot.SetActive(false);
		Scene.TrophyRoot.SetActive(false);

		// 固定テキストの設定
		Scene.MenuDungeonText.text = MasterStringTable.Instance.GetString("Menu_MenuDungeon");
		Scene.MenuRegularCardText.text = MasterStringTable.Instance.GetString("Menu_MenuRegularCard");
		Scene.MenuRegularArtifactText.text = MasterStringTable.Instance.GetString("Menu_MenuRegularArtifact");
		Scene.MenuOptionText.text = MasterStringTable.Instance.GetString("Menu_MenuOption");
		Scene.MenuOptionCloseButtonText.text = MasterStringTable.Instance.GetString("Common_Close");
		Scene.MenuRegularCardReturnButtonText.text = MasterStringTable.Instance.GetString("Common_Return");
		Scene.MenuRegularArtifactReturnButtonText.text = MasterStringTable.Instance.GetString("Common_Return");

		// BGMに関する設定
		Scene.MBgmSlider.value = PlayerPrefsManager.Instance.GetBgmVolume();
		Scene.MBgmToggle.isOn = PlayerPrefsManager.Instance.GetBgmIsMute();
		Scene.UpdateBgmVolumeText();

		// SEに関する設定
		Scene.MSeSlider.value = PlayerPrefsManager.Instance.GetSeVolume();
		Scene.MSeToggle.isOn = PlayerPrefsManager.Instance.GetSeIsMute();
		Scene.UpdateSeVolumeText();

		Scene.DungeonName.text = "";
		Scene.DungeonDetail.text = "";
		Scene.DungeonFloorCount.text = "";
		Scene.DungeonStartButton.SetActive(false);

		Scene.StartCoroutine(CoObjectLoad());

		return false;
    }
	
	private IEnumerator CoObjectLoad() {
		yield return LoadDungeon();
		yield return LoadPopupObjects();

		FadeManager.Instance.FadeIn(0.5f, null);
		StateMachineManager.Instance.ChangeState(StateMachineName.Menu, (int)MenuState.UserWait);
	}
	
	private IEnumerator LoadDungeon() {
		bool isLoadEnd = false;
		ResourceManager.Instance.RequestExecuteOrder(
			DungeonButtonPath,
			ExecuteOrder.Type.GameObject,
			Scene.gameObject,
			(rawobj) => {
				var dict = MasterDungeonTable.Instance.GetCloneDict();
				foreach (var data in dict) {
					GameObject obj = GameObject.Instantiate(rawobj) as GameObject;
					obj.transform.SetParent(Scene.DungeonContent.transform);
					obj.transform.localPosition = Vector3.zero;
					obj.transform.localScale = Vector3.one;

					obj.GetComponent<DungeonButtonController>().Initialize(data.Value, Scene.OnClickDungeonListButtonCallback);
				}
				isLoadEnd = true;
			}
		);

		while (isLoadEnd == false) {
			yield return null;
		}
	}
	
	private IEnumerator LoadPopupObjects() {
		int objectCount = 10;
		
		int loadCount = objectCount;
		int loadedCount = 0;

		for (int i = 0; i < objectCount; i++) {
			int index = i;
			ResourceManager.Instance.RequestExecuteOrder(
				Const.PopupObjectPath,
				ExecuteOrder.Type.GameObject,
				Scene.gameObject,
				(rawObject) => {
					GameObject obj = GameObject.Instantiate(rawObject) as GameObject;
					obj.transform.SetParent(Scene.PopupObjectRoot.transform);
					obj.transform.localPosition = Vector3.zero;
					obj.transform.localScale = Vector3.one;
					var ctrl = obj.GetComponent<PopupAnimationController>();
					ctrl.Initialize();
					MenuDataCarrier.Instance.PopupAnimationControllers.Add(ctrl);
					loadedCount++;
				}
			);
		}
		
		while (loadCount < loadedCount) {
			yield return null;
		}
	}

    /// <summary>
    /// メイン更新処理.
    /// </summary>
    /// <param name="delta">経過時間</param>
    override public void OnUpdateMain(float delta)
    {
    }

    /// <summary>
    /// ステート解放時処理.
    /// </summary>
    override public void OnRelease()
    {
    }
}
