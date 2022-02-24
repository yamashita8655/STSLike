using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuRegularCardSettingInitializeState : StateBase {

    /// <summary>
    /// メイン前処理.
    /// 戻り値は、同一フレーム内で次の処理に移行してよければfalse、1フレーム飛ばして欲しい場合はfalse.
    /// </summary>
    override public bool OnBeforeMain()
    {
		var scene = MenuDataCarrier.Instance.Scene as MenuScene;
		scene.RegularCardSettingRoot.SetActive(true);
		scene.CardDetailRoot.SetActive(false);

		// 装備カードの反映
		for (int i = 0; i < 6; i++) {
			scene.UpdateEquipCardDisplay(i);
		}

		// 装備カードコスト反映
		scene.UpdateEquipCardCostText();
		
		if (MenuDataCarrier.Instance.RegularSettingCardObjects.Count == 0) {
			CasheObject();
		} else {
			for (int i = 0; i < MenuDataCarrier.Instance.RegularSettingCardObjects.Count; i++) {
				MenuDataCarrier.Instance.RegularSettingCardObjects[i].GetComponent<RegularSettingCardContentItem>().UpdateDisplay();
			}
			EndState();
		}
		
		scene.UpdateMaxCostUpDisplay();

		return false;
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

		Debug.Log(Time.realtimeSinceStartup);

		// 初めて開くので、オブジェクト生成する
		for (int i = 1; i < 6; i++) {
			var list = MasterAction2Table.Instance.GetRarityCardCloneList(i);
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
		
		Debug.Log(Time.realtimeSinceStartup);

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
