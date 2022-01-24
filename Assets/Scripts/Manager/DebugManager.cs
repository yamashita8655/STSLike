using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugManager : BestPracticeSingleton<DebugManager> {
    [SerializeField]
    private GameObject DebugObject = null;

    [SerializeField]
    private DebugController CuDebugController = null;

    public void Initialize() {
        // ここのチェックは、開発中だけでいい
#if UNITY_EDITOR
        if (DebugObject == null) {
            Debug.Log("DebugManager:DebugObject error");
        }
        
        if (CuDebugController == null) {
            Debug.Log("DebugManager:CuDebugController error");
        }
#endif

        CuDebugController.Initialize();
    }
    
	public void OpenDebug()
    {
        DebugObject.SetActive(true);
    }

    public void CloseDebug()
    {
        DebugObject.SetActive(false);
    }

    public void UpdateDebugLog(string addText)
    {
        CuDebugController.UpdateDebugLog(addText);
    }
}
