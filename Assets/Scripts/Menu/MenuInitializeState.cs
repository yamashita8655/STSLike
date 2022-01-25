using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInitializeState : StateBase {

    /// <summary>
    /// メイン前処理.
    /// 戻り値は、同一フレーム内で次の処理に移行してよければfalse、1フレーム飛ばして欲しい場合はfalse.
    /// </summary>
    override public bool OnBeforeMain()
    {
		var scene = MenuDataCarrier.Instance.Scene as MenuScene;

		scene.OptionRoot.SetActive(false);
		// BGMに関する設定
		scene.MBgmSlider.value = PlayerPrefsManager.Instance.GetBgmVolume();
		scene.MBgmToggle.isOn = PlayerPrefsManager.Instance.GetBgmIsMute();
		scene.UpdateBgmVolumeText();

		// SEに関する設定
		scene.MSeSlider.value = PlayerPrefsManager.Instance.GetSeVolume();
		scene.MSeToggle.isOn = PlayerPrefsManager.Instance.GetSeIsMute();
		scene.UpdateSeVolumeText();

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
