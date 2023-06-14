using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;

	/// <summary>
	/// シーンの管理を行う
	/// </summary>
public class EnemyAnimationController : AnimationController
{
	private Action CuSpawnEffectCallback = null;
	
	public void Play(string stateName, Action spawnEffectCallback, Action callback)
	{
		EndCallback = callback;
		CuSpawnEffectCallback = spawnEffectCallback;
		BaseAnimation.Play(stateName, -1, 0f);
	}

	public void SpawnEffectCallback()
	{
		if (CuSpawnEffectCallback != null)
		{
			CuSpawnEffectCallback();
		}
	}
}
