using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;

	/// <summary>
	/// シーンの管理を行う
	/// </summary>
public class PopupAnimationController : AnimationController
{
	[SerializeField]
	private Text PopupText = null;

	// ＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝

	/// <summary>
	/// データ読み込む
	/// </summary>
	public void Initialize(string text)
	{
		PopupText.text = text;
	}
	
	public void Play(string stateName, Action callback)
	{
		EndCallback = callback;
		BaseAnimation.Play(stateName, -1, 0f);
	}
}
