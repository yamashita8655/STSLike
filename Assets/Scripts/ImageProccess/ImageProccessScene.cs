using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageProccessScene : MonoBehaviour
{
    [SerializeField]
    private DisplayTouchController Ctrl = null;
    
	[SerializeField]
    private GameObject NumberRoot = null;
	
	[SerializeField]
    private GameObject TextRaw = null;

    private List<GameObject> NumberList = new List<GameObject>();
    private List<string> NumberStringList = new List<string>() {
        "①",
        "②",
        "③",
        "④",
        "⑤",
        "⑥",
        "⑦",
        "⑧",
        "⑨"
    };

    // Start is called before the first frame update
    void Start()
    {
        Ctrl.Initialize(CallbackTouchDisplay);
    }

    private void CallbackTouchDisplay(Vector2 vec)
    {
		GameObject obj = GameObject.Instantiate(TextRaw) as GameObject;
        Text text = obj.GetComponent<Text>();
		int index = NumberList.Count;
        text.text = NumberStringList[index];

		obj.transform.SetParent(NumberRoot.transform);
		obj.transform.localPosition = new Vector3(vec.x, vec.y, 0f);
		obj.transform.localScale = Vector3.one;
		obj.SetActive(true);

        NumberList.Add(obj);
    }

	public void OnClickRemoveButton() {
		int index = NumberList.Count-1;
		GameObject obj = NumberList[index];
		NumberList.RemoveAt(index);

		GameObject.Destroy(obj);
	}
}
