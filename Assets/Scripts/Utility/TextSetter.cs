using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextSetter : MonoBehaviour
{
	[SerializeField]
	private Text CuText = null;

	public void SetText(string text) {
		CuText.text = text;
	}
}
