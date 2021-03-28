using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillScene : MonoBehaviour
{
	[SerializeField]
	private GameObject Root1 = null;
	
	[SerializeField]
	private GameObject Root2 = null;
	
	[SerializeField]
	private GameObject MoveObject = null;

    [SerializeField]
    private FlickController CuFlickController = null;

    private LinearCulc XCulc = null;
	private LinearCulc YCulc = null;

	private float Counter = 0f;
	private bool MoveNow = false;

    // Start is called before the first frame update
    void Start()
    {
        //Culc = new LinearCulc();
        CuFlickController.Initialize((res, x, y) => {
            Debug.Log(res);
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (MoveNow == true) {
			Counter += Time.deltaTime;
			if (Counter >= 3f) {
				Counter = 3f;
				MoveNow = false;
			}
			float x = XCulc.GetValue(Counter);
			float y = YCulc.GetValue(Counter);
			MoveObject.transform.localPosition = new Vector3(x, y, 0f);
		}
    }

	public void OnClick1() {
		// まずは、初期位置移動
		MoveObject.transform.SetParent(Root1.transform);
		MoveObject.transform.localPosition = Vector3.zero;
		MoveObject.transform.localScale = Vector3.one;
	}
	
	public void OnClick2() {
		// 親だけ変える
		MoveObject.transform.SetParent(Root2.transform);
		MoveObject.transform.localScale = Vector3.one;

		float x = MoveObject.transform.localPosition.x;
		float y = MoveObject.transform.localPosition.y;

		XCulc = new LinearCulc(3f, x, 0f);
		YCulc = new LinearCulc(3f, y, 0f);

		MoveNow = true;
	}
}
