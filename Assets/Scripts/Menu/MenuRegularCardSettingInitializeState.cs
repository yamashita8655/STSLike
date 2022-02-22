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

		scene.CarryPointText.text = PlayerPrefsManager.Instance.GetPoint().ToString();

		// TODO ここは、セットしているカードから計算する
		scene.NowRegularCostText.text = "0";
		
		scene.MaxRegularCostText.text = PlayerPrefsManager.Instance.GetMaxRegularCost().ToString();
		return false;
    }

    /// <summary>
    /// メイン更新処理.
    /// </summary>
    /// <param name="delta">経過時間</param>
    override public void OnUpdateMain(float delta)
    {
		StateMachineManager.Instance.ChangeState(StateMachineName.Menu, (int)MenuState.RegularCardSettingUserWait);
    }

    /// <summary>
    /// ステート解放時処理.
    /// </summary>
    override public void OnRelease()
    {
    }
}
