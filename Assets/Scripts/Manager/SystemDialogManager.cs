using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SystemDialogManager : BestPracticeSingleton<SystemDialogManager> {
    [SerializeField]
    private GameObject LoadingObject = null;

    [SerializeField]
    private GameObject DebugObject = null;

    public void Initialize() {
        // ここのチェックは、開発中だけでいい
#if UNITY_EDITOR
        if (LoadingObject == null) {
            Debug.Log("SerializeFieldResourceManager:LoadingObject error");
        }
        
		if (DebugObject == null) {
            Debug.Log("SerializeFieldResourceManager:DebugObject error");
        }
#endif
    }

    public void OpenLoading()
    {
        LoadingObject.SetActive(true);
    }

    public void CloseLoading()
    {
        LoadingObject.SetActive(false);
    }

    public void OpenDebug()
    {
        DebugObject.SetActive(true);
    }

    public void CloseDebug()
    {
        DebugObject.SetActive(false);
    }
}
