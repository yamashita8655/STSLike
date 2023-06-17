using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;

	/// <summary>
	/// シーンの管理を行う
	/// </summary>
public class SelectCardAnimationController : AnimationController
{
	private Action HitCallback = null;
	
	public void Play(string stateName, Action hitCallback, Action callback)
	{
		EndCallback = callback;
		HitCallback = hitCallback;
		BaseAnimation.Play(stateName, -1, 0f);
	}

	public void SpawnHitCallback()
	{
		if (HitCallback != null)
		{
			HitCallback();
		}
	}
}
