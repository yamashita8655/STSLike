using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuRegularArtifactSettingInitializeState : StateBase {

	private MenuScene Scene = null;

    /// <summary>
    /// メイン前処理.
    /// 戻り値は、同一フレーム内で次の処理に移行してよければfalse、1フレーム飛ばして欲しい場合はfalse.
    /// </summary>
    override public bool OnBeforeMain()
    {
		Scene = MenuDataCarrier.Instance.Scene as MenuScene;
		Scene.RegularArtifactSettingRoot.SetActive(true);
		Scene.ArtifactDetailRoot.SetActive(false);

		// アーティファクトコスト反映
		Scene.UpdateEquipArtifactCostText();
		Scene.UpdateArtifactMaxCostUpDisplay();
		
		// カードリスト反映
		if (MenuDataCarrier.Instance.RegularSettingArtifactObjects.Count == 0) {
			CasheObject();
		} else {
			for (int i = 0; i < MenuDataCarrier.Instance.RegularSettingArtifactObjects.Count; i++) {
				MenuDataCarrier.Instance.RegularSettingArtifactObjects[i].GetComponent<RegularSettingArtifactContentItem>().UpdateDisplay();
			}
			EndState();
		}
		
		// 既に設定されているアーティファクトリスト設定
		if (MenuDataCarrier.Instance.RegularArtifactButtonControllers.Count == 0) {
			LoadRegularArtifactObjects();
		}

		return false;
    }
	
	private void LoadRegularArtifactObjects() {
		List<int> regularIds = PlayerPrefsManager.Instance.GetRegularSettingArtifactIds();

		for (int i = 0; i < regularIds.Count; i++) {
			MasterArtifactTable.Data data = MasterArtifactTable.Instance.GetData(regularIds[i]);
			ResourceManager.Instance.RequestExecuteOrder(
				Const.RegularArtifactButtonItemPath,
				ExecuteOrder.Type.GameObject,
				Scene.gameObject,
				(rawObject) => {
					GameObject obj = GameObject.Instantiate(rawObject) as GameObject;
					obj.transform.SetParent(Scene.CarryEquipArtifactContentRoot.transform);
					obj.transform.localPosition = Vector3.zero;
					obj.transform.localScale = Vector3.one;
					RegularArtifactButtonController ctrl = obj.GetComponent<RegularArtifactButtonController>();
					ctrl.Initialize(
						data,
						Scene.OnClickCarryArtifactButton,
						Scene.OnClickCarryArtifactDetailButton
					);
					MenuDataCarrier.Instance.RegularArtifactButtonControllers.Add(ctrl);
				}
			);
		}
	}
	
	private IEnumerator CoLoadObject() {
		yield return null;
		
		var scene = MenuDataCarrier.Instance.Scene as MenuScene;

		// 初めて開くので、オブジェクト生成する
		for (int i = 1; i < 6; i++) {
			var list = MasterArtifactTable.Instance.GetRarityArtifactCloneList(i);
			for (int i2 = 0; i2 < list.Count; i2++) {
				int id = list[i2];
				ResourceManager.Instance.RequestExecuteOrder(
					Const.RegularSettingArtifactContentItemPath,
					ExecuteOrder.Type.GameObject,
					scene.gameObject,
					(rawObject) => {
						var obj = GameObject.Instantiate(rawObject) as GameObject;
						obj.transform.SetParent(scene.ArtifactContentRoot.transform);

						obj.transform.localPosition = Vector3.zero;
						obj.transform.localScale = Vector3.one;

						var data = MasterArtifactTable.Instance.GetData(id);
						obj.GetComponent<RegularSettingArtifactContentItem>().Initialize(
							data,
							(d) => {
								scene.OnClickArtifactDetailButton(d);
							}
						);

						MenuDataCarrier.Instance.RegularSettingArtifactObjects.Add(obj);
					}
				);
				yield return null;
			}
		}
		
		// TODO 待つ必要があったら、対応する事
		EndState();
	}
	
	private void CasheObject() {
		var scene = MenuDataCarrier.Instance.Scene as MenuScene;
		// とりあえず、先に読んでキャッシュ
		ResourceManager.Instance.RequestExecuteOrder(
			Const.RegularSettingArtifactContentItemPath,
			ExecuteOrder.Type.GameObject,
			scene.gameObject,
			(rawObject) => {
				scene.StartCoroutine(CoLoadObject());
			}
		);
	}
	
	private void EndState() {
		StateMachineManager.Instance.ChangeState(StateMachineName.Menu, (int)MenuState.RegularArtifactSettingUserWait);
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
