using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuRegularCardSettingCardDetailEquipState : StateBase {

    /// <summary>
    /// メイン前処理.
    /// 戻り値は、同一フレーム内で次の処理に移行してよければfalse、1フレーム飛ばして欲しい場合はfalse.
    /// </summary>
    override public bool OnBeforeMain()
    {
		var scene = MenuDataCarrier.Instance.Scene as MenuScene;
		//scene.CardEquipSelectRoot.SetActive(true);

		var item = MenuDataCarrier.Instance.SelectCardContentItem;
		var data = item.GetData();

		// TODO この辺、処理負荷高かったら、都度作り直しじゃなくて、オブジェクト再利用ロジックに変えてもいいかもね。
		ResourceManager.Instance.RequestExecuteOrder(
			Const.RegularCardButtonItemPath,
			ExecuteOrder.Type.GameObject,
			scene.gameObject,
			(rawObject) => {
				GameObject obj = GameObject.Instantiate(rawObject) as GameObject;
				obj.transform.SetParent(scene.CarryEquipCardContentRoot.transform);
				obj.transform.localPosition = Vector3.zero;
				obj.transform.localScale = Vector3.one;
				RegularCardButtonController ctrl = obj.GetComponent<RegularCardButtonController>();
				ctrl.Initialize(
					data,
					scene.OnClickCarryCardButton,
					scene.OnClickCarryCardDetailButton
				);
				MenuDataCarrier.Instance.RegularCardButtonControllers.Add(ctrl);
			}
		);

		PlayerPrefsManager.Instance.AddRegularSettingCardId(data.Id);

		scene.UpdateEquipCardCostText();

		return false;
    }

    /// <summary>
    /// メイン更新処理.
    /// </summary>
    /// <param name="delta">経過時間</param>
    override public void OnUpdateMain(float delta)
    {
		StateMachineManager.Instance.ChangeState(StateMachineName.Menu, (int)MenuState.RegularCardSettingCardDetailClose);
    }

    /// <summary>
    /// ステート解放時処理.
    /// </summary>
    override public void OnRelease()
    {
    }
}
