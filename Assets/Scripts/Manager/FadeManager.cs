using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeManager : BestPracticeSingleton<FadeManager> {
	public enum Type {
		Simple = 0,
		Mask,
		None,
	};

	[SerializeField]
	private FadeControllerScript CuMaskFadeController = null;
	
	[SerializeField]
	private FadeControllerScript CuSimpleFadeController = null;

	private Type CurrentFadeType = Type.None;
	
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

	public void FadeIn(float time, Action callback)
	{
		if (CurrentFadeType == Type.None) {
			LogManager.Instance.LogError("FadeManager Call Error : Fade Statue in None");
			return;
		}

		if (CurrentFadeType == Type.Simple) {
			CuSimpleFadeController.FadeIn(time, callback);
		} else if (CurrentFadeType == Type.Mask) {
			CuMaskFadeController.FadeIn(time, callback);
		}
	}

	public void FadeOut(Type type, float time, Action callback)
	{
		CurrentFadeType = type;
		if (CurrentFadeType == Type.Simple) {
			CuSimpleFadeController.FadeOut(time, callback);
		} else if (CurrentFadeType == Type.Mask) {
			CuMaskFadeController.FadeOut(time, callback);
		}
	}
}
