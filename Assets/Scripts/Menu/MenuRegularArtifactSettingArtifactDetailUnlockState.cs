using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuRegularArtifactSettingArtifactDetailUnlockState : StateBase {

    /// <summary>
    /// メイン前処理.
    /// 戻り値は、同一フレーム内で次の処理に移行してよければfalse、1フレーム飛ばして欲しい場合はfalse.
    /// </summary>
    override public bool OnBeforeMain()
    {
		var scene = MenuDataCarrier.Instance.Scene as MenuScene;
		var item = MenuDataCarrier.Instance.SelectArtifactContentItem;
		var data = item.GetData();

		int usedPoint = data.UnlockCost;
		int carryPoint = PlayerPrefsManager.Instance.GetPoint();

		PlayerPrefsManager.Instance.AddPoint(-usedPoint);
		PlayerPrefsManager.Instance.SaveUnlookArtifactId(data.Id);

		item.UpdateDisplay();
		
		scene.UpdateMaxCostUpDisplay();

		return false;
    }

    /// <summary>
    /// メイン更新処理.
    /// </summary>
    /// <param name="delta">経過時間</param>
    override public void OnUpdateMain(float delta)
    {
		StateMachineManager.Instance.ChangeState(StateMachineName.Menu, (int)MenuState.RegularArtifactSettingArtifactDetailOpen);
    }

    /// <summary>
    /// ステート解放時処理.
    /// </summary>
    override public void OnRelease()
    {
    }
}
