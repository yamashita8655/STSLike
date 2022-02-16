using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandSkillContentController : MonoBehaviour
{
	[SerializeField]
	private Text NameText = null;
	
	[SerializeField]
	private Image ActionImage = null;
	
	[SerializeField]
	private Text UseCountText = null;
	
	[SerializeField]
	private Image[] NeedDiceImages = null;
	
	public void Initialize(GameObject attachRoot) {
		transform.SetParent(attachRoot.transform);
		transform.localPosition = Vector3.zero;
		transform.localScale = Vector3.one;
	}
	
	public void OnClick() {
	}
	
	public void UpdateDisplay() {
	}
}
