using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeManager : BestPracticeSingleton<FadeManager> {
	public enum Type {
		Simple = 0,
		Mask,
	};

	[SerializeField]
	private FadeControllerScript CuMaskFadeController = null;
	
	[SerializeField]
	private FadeControllerScript CuSimpleFadeController = null;
	
	public void Initialize() {
		// ここのチェックは、開発中だけでいい
#if UNITY_EDITOR
		if (CuMaskFadeController == null) {
			Debug.Log("SerializeFieldResourceManager:CuMaskFadeController error");
		}
		if (CuSimpleFadeController == null) {
			Debug.Log("SerializeFieldResourceManager:CuSimpleFadeController error");
		}
#endif
		CuMaskFadeController.Initialize();
		CuSimpleFadeController.Initialize();
	}

	public void FadeIn(Type type, float time, Action callback)
	{
		if (type == Type.Simple) {
			CuSimpleFadeController.FadeIn(time, callback);
		} else if (type == Type.Mask) {
			CuMaskFadeController.FadeIn(time, callback);
		}
	}

	public void FadeOut(Type type, float time, Action callback)
	{
		if (type == Type.Simple) {
			CuSimpleFadeController.FadeOut(time, callback);
		} else if (type == Type.Mask) {
			CuMaskFadeController.FadeOut(time, callback);
		}
	}
}
