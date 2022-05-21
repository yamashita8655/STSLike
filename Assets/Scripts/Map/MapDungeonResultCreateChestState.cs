using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDungeonResultCreateChestState : StateBase {
    /// <summary>
    /// メイン前処理.
    /// 戻り値は、同一フレーム内で次の処理に移行してよければfalse、1フレーム飛ばして欲しい場合はfalse.
    /// </summary>
    override public bool OnBeforeMain()
    {
		var scene = MapDataCarrier.Instance.Scene as MapScene;
		
		CreateChest();

		var list = MapDataCarrier.Instance.ChestList;
		int loadCount = list[0] + list[1] + list[2];
		if (MapDataCarrier.Instance.IsClear == true) {
			if (loadCount > 0) {
				scene.AdmobChestButton.gameObject.SetActive(true);
			} else {
				scene.AdmobChestButton.gameObject.SetActive(false);
			}
		} else {
			scene.AdmobChestButton.interactable = true;
			if (loadCount > 0) {
				scene.AdmobChestButton.gameObject.SetActive(true);
			} else {
				scene.AdmobChestButton.gameObject.SetActive(false);
			}
		}

		return false;
    }
	
	private void CreateChest() {
		var scene = MapDataCarrier.Instance.Scene as MapScene;

		// チェスト表示適用
		var list = MapDataCarrier.Instance.ChestList;
		int loadCount = list[0] + list[1] + list[2];
		int loadedCount = 0;
		for (int i = 0; i < list.Count; i++) {
			int count = list[i];
			for (int i2 = 0; i2 < count; i2++) {
				int rarity = i;
				ResourceManager.Instance.RequestExecuteOrder(
					Const.ChestObjectPath,
					ExecuteOrder.Type.GameObject,
					scene.gameObject,
					(rawObject) => {
						GameObject obj = GameObject.Instantiate(rawObject) as GameObject;
						var ctrl = obj.GetComponent<ChestObjectController>();
						ctrl.Initialize(rarity);
						obj.transform.SetParent(scene.ChestRoot.transform);
						obj.transform.localPosition = Vector3.zero;
						obj.transform.localScale = Vector3.one;
						MapDataCarrier.Instance.ResultChestCtrls.Add(ctrl);
						loadedCount++;
						if (loadedCount == loadCount) {
							if (MapDataCarrier.Instance.IsClear == true) {
								StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.DungeonResultOpenChest);
							} else {
								StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.DungeonResultUserWait);
								
							}
						}
					}
				);
			}
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
