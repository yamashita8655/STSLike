using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBattleDiceRollUserWaitState : StateBase {

	/// <summary>
	/// 初期化前処理.
	/// </summary>
	override public bool OnBeforeInit()
	{
		var scene = MapDataCarrier.Instance.Scene as MapScene;
		scene.DiceRollButton.SetActive(true);

		for (int i = 0; i < scene.DiceImages.Length; i++) {
			scene.DiceImages[i].gameObject.SetActive(false);
		}

		return true;
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
