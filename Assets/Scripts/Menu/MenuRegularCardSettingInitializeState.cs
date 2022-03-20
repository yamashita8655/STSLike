using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuRegularCardSettingInitializeState : StateBase {
		
	private MenuScene Scene = null;

    /// <summary>
    /// メイン前処理.
    /// 戻り値は、同一フレーム内で次の処理に移行してよければfalse、1フレーム飛ばして欲しい場合はfalse.
    /// </summary>
    override public bool OnBeforeMain()
    {
		Scene = MenuDataCarrier.Instance.Scene as MenuScene;
		Scene.RegularCardSettingRoot.SetActive(true);
		Scene.CardDetailRoot.SetActive(false);

		// 装備カードコスト反映
		Scene.UpdateEquipCardCostText();
		Scene.UpdateMaxCostUpDisplay();
		
		// カードリスト反映
		if (MenuDataCarrier.Instance.RegularSettingCardObjects.Count == 0) {
			CasheObject();
		} else {
			for (int i = 0; i < MenuDataCarrier.Instance.RegularSettingCardObjects.Count; i++) {
				MenuDataCarrier.Instance.RegularSettingCardObjects[i].GetComponent<RegularSettingCardContentItem>().UpdateDisplay();
			}
			EndState();
		}
		
		// 既に設定されているカードリスト設定
		if (MenuDataCarrier.Instance.RegularCardButtonControllers.Count == 0) {
			LoadRegularCardObjects();
		}

		return false;
    }
	
	private void LoadRegularCardObjects() {
		List<int> regularIds = PlayerPrefsManager.Instance.GetRegularSettingCardIds();

		for (int i = 0; i < regularIds.Count; i++) {
			MasterAction2Table.Data data = MasterAction2Table.Instance.GetData(regularIds[i]);
			ResourceManager.Instance.RequestExecuteOrder(
				Const.RegularCardButtonItemPath,
				ExecuteOrder.Type.GameObject,
				Scene.gameObject,
				(rawObject) => {
					GameObject obj = GameObject.Instantiate(rawObject) as GameObject;
					obj.transform.SetParent(Scene.CarryEquipCardContentRoot.transform);
					obj.transform.localPosition = Vector3.zero;
					obj.transform.localScale = Vector3.one;
					RegularCardButtonController ctrl = obj.GetComponent<RegularCardButtonController>();
					ctrl.Initialize(
						data,
						Scene.OnClickCarryCardButton,
						Scene.OnClickCarryCardDetailButton
					);
					MenuDataCarrier.Instance.RegularCardButtonControllers.Add(ctrl);
				}
			);
		}
	}
	
	private void CasheObject() {
		var scene = MenuDataCarrier.Instance.Scene as MenuScene;
		// とりあえず、先に読んでキャッシュ
		ResourceManager.Instance.RequestExecuteOrder(
			Const.RegularSettingCardContentItemPath,
			ExecuteOrder.Type.GameObject,
			scene.gameObject,
			(rawObject) => {
				scene.StartCoroutine(CoLoadObject());
			}
		);
	}
	
	private IEnumerator CoLoadObject() {
		yield return null;
		
		var scene = MenuDataCarrier.Instance.Scene as MenuScene;

		// 初めて開くので、オブジェクト生成する
		for (int i = 1; i < 6; i++) {
			var list = MasterAction2Table.Instance.GetAllRarityCardCloneList(i);
			for (int i2 = 0; i2 < list.Count; i2++) {
				int id = list[i2];
				ResourceManager.Instance.RequestExecuteOrder(
					Const.RegularSettingCardContentItemPath,
					ExecuteOrder.Type.GameObject,
					scene.gameObject,
					(rawObject) => {
						var obj = GameObject.Instantiate(rawObject) as GameObject;
						obj.transform.SetParent(scene.CardContentRoot.transform);

						obj.transform.localPosition = Vector3.zero;
						obj.transform.localScale = Vector3.one;

						var data = MasterAction2Table.Instance.GetData(id);
						obj.GetComponent<RegularSettingCardContentItem>().Initialize(
							data,
							(d) => {
								scene.OnClickCardDetailButton(d);
							}
						);

						MenuDataCarrier.Instance.RegularSettingCardObjects.Add(obj);
					}
				);
				yield return null;
			}
		}
		
		// TODO 待つ必要があったら、対応する事
		EndState();
	}

	private void EndState() {
		StateMachineManager.Instance.ChangeState(StateMachineName.Menu, (int)MenuState.RegularCardSettingUserWait);
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
