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
		scene.CardEquipSelectRoot.SetActive(true);

		var item = MenuDataCarrier.Instance.SelectCardContentItem;
		var data = item.GetData();

		int nowCost = scene.GetNowEquipCost();
		int maxCost = scene.GetMaxCost();
		int selectCardCost = data.EquipCost;

		for (int i = 0; i < 6; i++) {
			var equipCardId = PlayerPrefsManager.Instance.GetRegularSettingCardId(i);
			var equipData = MasterAction2Table.Instance.GetData(equipCardId);
			if ((nowCost-equipData.EquipCost+selectCardCost) <= maxCost) {
				scene.CardEquipSelectButtons[i].interactable = true;
			} else {
				scene.CardEquipSelectButtons[i].interactable = false;
			}
		}
		return false;
    }

    /// <summary>
    /// メイン更新処理.
    /// </summary>
    /// <param name="delta">経過時間</param>
    override public void OnUpdateMain(float delta)
    {
		StateMachineManager.Instance.ChangeState(StateMachineName.Menu, (int)MenuState.RegularCardSettingCardDetailEquipUserWait);
    }

    /// <summary>
    /// ステート解放時処理.
    /// </summary>
    override public void OnRelease()
    {
    }
}
