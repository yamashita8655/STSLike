using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;

	/// <summary>
	/// シーンの管理を行う
	/// </summary>
public class CountdownAnimationController : AnimationController
{
	public void CallSoundCallback()
	{
		SoundManager.Instance.PlaySe(Enum.Se.Effect_CountDownEffect);		
	}
}
