using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeControllerScript : MonoBehaviour
{
	public enum State {
		None = 0,
		FadeIn,
		FadeOut,
		FadeWait
	};

	[SerializeField]
	private FadeObjectBase CuFadeObjectBase = null;

	private LinearCulc FadeInCulc = new LinearCulc();
	private LinearCulc FadeOutCulc = new LinearCulc();
	private float PassTime = 0f;
	private float AnimationTime = 0f;
	private float WaitTime = 0f;
	private State NowState = State.None;

	private Action EndCallback = null;

	public void Initialize()
	{
		NowState = State.None;
		CuFadeObjectBase.Initialize();
		CuFadeObjectBase.UpdateFade(0f);
		gameObject.SetActive(false);
	}
	public void FadeOut(float time, Action callback, float waitTime = 0.5f) {
		if (NowState != State.None) {
			return;
		}

		FadeInCulc.SetEndCount(time);
		FadeInCulc.SetStartValue(0f);
		FadeInCulc.SetEndValue(1f);
		AnimationTime = time;
		PassTime = 0f;
		WaitTime = waitTime;

		NowState = State.FadeIn;
		EndCallback = callback;
		gameObject.SetActive(true);
	}

	public void FadeIn(float time, Action callback) {
		if (NowState != State.None) {
			return;
		}

		FadeOutCulc.SetEndCount(time);
		FadeOutCulc.SetStartValue(1f);
		FadeOutCulc.SetEndValue(0f);
		AnimationTime = time;
		PassTime = 0f;

		NowState = State.FadeOut;
		EndCallback = callback;
		gameObject.SetActive(true);
	}

	void Update()
	{
		if (NowState == State.FadeIn) {
			PassTime += Time.deltaTime;
			float alpha = FadeInCulc.GetValue(PassTime);
			CuFadeObjectBase.UpdateFade(alpha);
			if (PassTime >= AnimationTime) {
				NowState = State.FadeWait;
				PassTime = 0f;
			}
		} else if (NowState == State.FadeOut) {
			PassTime += Time.deltaTime;
			float alpha = FadeOutCulc.GetValue(PassTime);
			CuFadeObjectBase.UpdateFade(alpha);
			if (PassTime >= AnimationTime) {
				NowState = State.None;
				gameObject.SetActive(false);
				EndCallback?.Invoke();
			}
		} else if (NowState == State.FadeWait) {
			PassTime += Time.deltaTime;
			if (PassTime >= WaitTime) {
				NowState = State.None;
				EndCallback?.Invoke();
			}
		}
	}
}
