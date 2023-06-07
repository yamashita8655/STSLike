using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleEffectManager : SimpleMonoBehaviourSingleton<ScaleEffectManager> {
	
	[SerializeField]
	private GameObject RawScaleEffectObject = null;

	private List<ScaleEffectController> SpawnEffectList = new List<ScaleEffectController>();

	public void Initialize() {
	}
	
	public void SpawnEffect(
		string imagePath,
		GameObject parent,
		float startScale,
		float endScale,
		float time
	) {
		var obj = GameObject.Instantiate(RawScaleEffectObject) as GameObject;
		obj.transform.SetParent(parent.transform);
		obj.transform.localPosition = Vector3.zero;
		obj.transform.localScale = Vector3.one;
		var ctrl = obj.GetComponent<ScaleEffectController>();
		ctrl.Initialize(
			imagePath,
			parent,
			startScale,
			endScale,
			time
		);

		SpawnEffectList.Add(ctrl);
	}

	public void UpdateSelf(float deltaTime) {
		for (int i = 0; i < SpawnEffectList.Count; i++) {
			SpawnEffectList[i].UpdateSelf(deltaTime);
			if (SpawnEffectList[i].CanDestroy() == true)
			{
				GameObject.Destroy(SpawnEffectList[i].gameObject);
				SpawnEffectList[i] = null;
			}
		}

		var list = new List<ScaleEffectController>();
		for (int i = 0; i < SpawnEffectList.Count; i++) {
			if (SpawnEffectList[i] != null)
			{
				list.Add(SpawnEffectList[i]);
			}
		}

		SpawnEffectList = list;
	}
}
