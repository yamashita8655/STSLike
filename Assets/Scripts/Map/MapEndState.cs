﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEndState : StateBase {

    /// <summary>
    /// メイン前処理.
    /// </summary>
    override public bool OnBeforeMain()
    {
		Debug.Log("MapEndState");
		return false;
    }

    /// <summary>
    /// メイン更新処理.
    /// </summary>
    /// <param name="delta">経過時間</param>
    override public void OnUpdateMain(float delta)
    {
		FadeManager.Instance.FadeOut(FadeManager.Type.Mask, 0.5f, () => {
			LocalSceneManager.Instance.LoadScene(MapDataCarrier.Instance.NextSceneName, null);
		});
    }

    /// <summary>
    /// ステート解放時処理.
    /// </summary>
    override public void OnRelease()
    {
    }
}
