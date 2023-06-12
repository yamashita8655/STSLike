using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectCardControllerManager : SimpleMonoBehaviourSingleton<SelectCardControllerManager> {
	
	[SerializeField]
	private GameObject RawSelectCardControllerObject = null;
	
	[SerializeField]
	private GameObject SelectCardRoot = null;
	
	private List<SelectCardController> SelectCardControllerList = new List<SelectCardController>();

	private SelectCardController CurrentObject = null;

	public void Initialize() {
	}
	
	public void AddCardController(
		MasterAction2Table.Data data
	) {
		var obj = GameObject.Instantiate(RawSelectCardControllerObject) as GameObject;
		obj.transform.SetParent(SelectCardRoot.transform);
		obj.transform.localPosition = Vector3.zero;
		obj.transform.localScale = Vector3.one;
		var ctrl = obj.GetComponent<SelectCardController>();

		StartCoroutine(ctrl.Initialize(data));
		SelectCardControllerList.Add(ctrl);
	}

	public void UpdateSelf(float deltaTime)
	{
		if (CurrentObject == null)
		{
			if (SelectCardControllerList.Count > 0)
			{
				CurrentObject = SelectCardControllerList[0];
				SelectCardControllerList.RemoveAt(0);
				CurrentObject.PlayAnimation();
			}
		}
		else
		{
			if (CurrentObject.CanDestroy() == true)
			{
				GameObject.Destroy(CurrentObject.gameObject);
				CurrentObject = null;
			}
		}
	}
}
