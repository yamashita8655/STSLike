using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageNumberEffectController : MonoBehaviour
{
	[SerializeField]
	private AnimationController CuAnimationController = null;
	
	[SerializeField]
	private Text DamageText = null;

	public void Play(
		string stateName,
		int damage,
		GameObject parent
	) {
		gameObject.transform.SetParent(parent.transform);
		gameObject.transform.localPosition = Vector3.zero;
		gameObject.transform.localScale = Vector3.one;
		DamageText.text = damage.ToString();

		CuAnimationController.Play(stateName, EndCallback);
	}

	private void EndCallback() {
		GameObject.Destroy(this.gameObject);
	}
}
