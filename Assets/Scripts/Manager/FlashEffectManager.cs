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
	private bool SpawnNow = false;

	public void Initialize() {
		for (int i = 0; i < FlashEffectObjects.Length; i++) {
			FlashEffectObjects[i].SetActive(false);
		}
		SpawnNow = false;
	}
	
	public void SpawnEffect(int type) {
		if (SpawnNow == true)
		{
			Debug.LogError("=====SpawnEffect:前のエフェクトが終わっていない");
			return;
		}

		CurrentEffectType = type;
		FlashEffectObjects[CurrentEffectType].SetActive(true);
		PassTime = EffectTime;
		SpawnNow = true;
	}

	void Update() {
		if (SpawnNow == true)
		{
			if (PassTime < 0f)
			{
				FlashEffectObjects[CurrentEffectType].SetActive(false);
				SpawnNow = false;
			}
			else
			{
				PassTime -= Time.deltaTime;
			}
		}
	}
}
