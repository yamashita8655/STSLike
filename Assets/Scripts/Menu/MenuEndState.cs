using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuEndState : StateBase {

    /// <summary>
    /// メイン前処理.
    /// 戻り値は、同一フレーム内で次の処理に移行してよければfalse、1フレーム飛ばして欲しい場合はfalse.
    /// </summary>
    override public bool OnBeforeMain()
    {
		return false;
    }

    /// <summary>
    /// メイン更新処理.
    /// </summary>
    /// <param name="delta">経過時間</param>
    override public void OnUpdateMain(float delta)
    {
        FadeManager.Instance.FadeOut(FadeManager.Type.Mask, 0.5f, () => {
			LocalSceneManager.Instance.LoadScene(MenuDataCarrier.Instance.NextSceneName, MenuDataCarrier.Instance.Data);
        });
    }

    /// <summary>
    /// ステート解放時処理.
    /// </summary>
    override public void OnRelease()
    {
    }
}
