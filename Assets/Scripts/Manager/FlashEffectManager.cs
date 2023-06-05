using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashEffectManager : SimpleMonoBehaviourSingleton<FlashEffectManager> {
	
	[SerializeField]
	private GameObject[] FlashEffectObjects;

	private float PassTime = 0f;
	private float EffectTime = 0.01f;
	private int CurrentEffectType = 0;

	public void Initialize() {
		for (int i = 0; i < FlashEffectObjects.Length; i++) {
			FlashEffectObjects[i].SetActive(false);
		}
	}
	
	public void SpawnEffect(int type) {
		if (PassTime > 0f)
		{
			Debug.LogError("=====SpawnEffect:前のエフェクトが終わっていない");
			return;
		}

		CurrentEffectType = type;
		FlashEffectObjects[CurrentEffectType].SetActive(true);
		PassTime = EffectTime;
	}

	void Update() {
		if (PassTime < 0f)
		{
			FlashEffectObjects[CurrentEffectType].SetActive(false);
		}
		else
		{
			PassTime -= Time.deltaTime;
		}
	}
}
