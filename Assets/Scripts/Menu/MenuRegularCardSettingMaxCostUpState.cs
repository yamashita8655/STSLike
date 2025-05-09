using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuRegularCardSettingMaxCostUpState : StateBase {

    /// <summary>
    /// メイン前処理.
    /// 戻り値は、同一フレーム内で次の処理に移行してよければfalse、1フレーム飛ばして欲しい場合はfalse.
    /// </summary>
    override public bool OnBeforeMain()
    {
		var scene = MenuDataCarrier.Instance.Scene as MenuScene;
		
		int usedPoint = PlayerPrefsManager.Instance.GetUsedRegularCostPoint();
		int nextNeedPoint = MasterRegularCardMaxCostTable.Instance.GetNextLevelNeedPoint(usedPoint);

		PlayerPrefsManager.Instance.AddPoint(-nextNeedPoint);
		PlayerPrefsManager.Instance.AddUsedRegularCostPoint(nextNeedPoint);
		
		scene.UpdateMaxCostUpDisplay();
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
