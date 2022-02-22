using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInitializeState : StateBase {

	private readonly string DungeonButtonPath = "Prefab/UI/DungeonButton";
	private readonly string CardContentItemPath = "Prefab/UI/CardContentItem";
	private readonly string ArtifactContentItemPath = "Prefab/UI/ArtifactContentItem";

    /// <summary>
    /// メイン前処理.
    /// 戻り値は、同一フレーム内で次の処理に移行してよければfalse、1フレーム飛ばして欲しい場合はfalse.
    /// </summary>
    override public bool OnBeforeMain()
    {
		var scene = MenuDataCarrier.Instance.Scene as MenuScene;

		scene.OptionRoot.SetActive(false);
		scene.DungeonRoot.SetActive(false);
		scene.CardUnlockRoot.SetActive(false);
		scene.ArtifactUnlockRoot.SetActive(false);
		scene.RegularCardSettingRoot.SetActive(false);

		// BGMに関する設定
		scene.MBgmSlider.value = PlayerPrefsManager.Instance.GetBgmVolume();
		scene.MBgmToggle.isOn = PlayerPrefsManager.Instance.GetBgmIsMute();
		scene.UpdateBgmVolumeText();

		// SEに関する設定
		scene.MSeSlider.value = PlayerPrefsManager.Instance.GetSeVolume();
		scene.MSeToggle.isOn = PlayerPrefsManager.Instance.GetSeIsMute();
		scene.UpdateSeVolumeText();

		// TODO この辺、ちゃんとコルーチンで非同期読み込み待ちする事
		// Dungeonの初期化
		ResourceManager.Instance.RequestExecuteOrder(
			DungeonButtonPath,
			ExecuteOrder.Type.GameObject,
			scene.gameObject,
			(rawobj) => {
				var dict = MasterDungeonTable.Instance.GetCloneDict();
				foreach (var data in dict) {
					GameObject obj = GameObject.Instantiate(rawobj) as GameObject;
					obj.transform.SetParent(scene.DungeonContent.transform);
					obj.transform.localPosition = Vector3.zero;
					obj.transform.localScale = Vector3.one;

					obj.GetComponent<DungeonButtonController>().Initialize(data.Value, scene.OnClickDungeonListButtonCallback);

				}
			}
		);

		// TODO この辺、ちゃんとコルーチンで非同期読み込み待ちする事
		// カードアンロックの初期化
		ResourceManager.Instance.RequestExecuteOrder(
			CardContentItemPath,
			ExecuteOrder.Type.GameObject,
			scene.gameObject,
			(rawobj) => {
				var cardDict = MasterAction2Table.Instance.GetCloneDict();
				foreach (var data in cardDict) {
					GameObject obj = GameObject.Instantiate(rawobj) as GameObject;
					obj.transform.SetParent(scene.CardContent.transform);
					obj.transform.localPosition = Vector3.zero;
					obj.transform.localScale = Vector3.one;

					obj.GetComponent<CardContentItem>().Initialize(
						data.Value,
						(data) => {
							
						}
					);
				}
			}
		);
		
		// TODO この辺、ちゃんとコルーチンで非同期読み込み待ちする事
		// アーティファクトアンロックの初期化
		ResourceManager.Instance.RequestExecuteOrder(
			ArtifactContentItemPath,
			ExecuteOrder.Type.GameObject,
			scene.gameObject,
			(rawobj) => {
				var artifactDict = MasterArtifactTable.Instance.GetCloneDict();
				foreach (var data in artifactDict) {
					GameObject obj = GameObject.Instantiate(rawobj) as GameObject;
					obj.transform.SetParent(scene.ArtifactContent.transform);
					obj.transform.localPosition = Vector3.zero;
					obj.transform.localScale = Vector3.one;

					obj.GetComponent<ArtifactContentItem>().Initialize(
						data.Value,
						(data) => {
							
						}
					);
				}
			}
		);

		scene.DungeonName.text = "";
		scene.DungeonDetail.text = "";
		scene.DungeonFloorCount.text = "";
		scene.DungeonStartButton.SetActive(false);

		return false;
    }

    /// <summary>
    /// メイン更新処理.
    /// </summary>
    /// <param name="delta">経過時間</param>
    override public void OnUpdateMain(float delta)
    {
		StateMachineManager.Instance.ChangeState(StateMachineName.Menu, (int)MenuState.UserWait);
    }

    /// <summary>
    /// ステート解放時処理.
    /// </summary>
    override public void OnRelease()
    {
    }
}
