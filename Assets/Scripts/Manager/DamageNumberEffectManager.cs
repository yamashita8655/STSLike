using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageNumberEffectManager : SimpleMonoBehaviourSingleton<DamageNumberEffectManager> {
	
	[SerializeField]
	private GameObject RawDamageNumberEffectObject = null;

	public void Initialize() {
	}
	
	public void SpawnEffect(
		string stateName,
		int damage,
		GameObject parent
	) {
		var obj = GameObject.Instantiate(RawDamageNumberEffectObject) as GameObject;
		var ctrl = obj.GetComponent<DamageNumberEffectController>();
		ctrl.Play(
			stateName,
			damage,
			parent
		);
	}
}
