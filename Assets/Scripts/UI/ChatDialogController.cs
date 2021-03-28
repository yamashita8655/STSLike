using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatDialogController : MonoBehaviour
{
	[SerializeField]
	private GameObject DialogRootObject = null;
	
	[SerializeField]
	private Text HeaderText = null;

	public void Open() {
		DialogRootObject.SetActive(true);

		HeaderText.text = "チャット";
	}

	public void Close() {
		DialogRootObject.SetActive(false);
	}
}
