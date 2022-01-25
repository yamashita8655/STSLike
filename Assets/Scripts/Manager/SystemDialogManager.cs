using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SystemDialogManager : BestPracticeSingleton<SystemDialogManager> {
    [SerializeField]
    private GameObject LoadingObject = null;

    public void Initialize() {
        // ここのチェックは、開発中だけでいい
#if UNITY_EDITOR
        if (LoadingObject == null) {
            Debug.Log("SerializeFieldResourceManager:LoadingObject error");
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
}
