using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SerializeFieldResourceManager : BestPracticeSingleton<SerializeFieldResourceManager> {
	//[SerializeField]
	//private GameObject[] CuRawFishObjects = null;
	//public GameObject[] RawFishObjects => CuRawFishObjects;
	
	public void Initialize() {
		// ここのチェックは、開発中だけでいい
#if UNITY_EDITOR
		//if ((CuRawFishObjects == null) || (CuRawFishObjects.Length == 0)) {
		//	LogManager.Instance.Log("SerializeFieldResourceManager:CuRawFishObjects error");
		//}
#endif
	}
}
