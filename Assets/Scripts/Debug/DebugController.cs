using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugController : MonoBehaviour
{
    [SerializeField]
    private Text DebugText = null;

    [SerializeField]
    private GameObject LogObject = null;

    private int LogIndex = 1;

    public void Initialize()
    {
        DebugManager.Instance.CloseDebug();
        LogObject.SetActive(false);
    }

	public void OnClickOpenButton()
	{
		DebugManager.Instance.OpenDebug();
	}

	public void OnClickCloseButton()
	{
        DebugManager.Instance.CloseDebug();
	}

    public void OnClickOpenLogButton()
    {
        LogObject.SetActive(true);
    }

    public void OnClickCloseLogButton()
    {
        LogObject.SetActive(false);
    }
    
	public void OnClickClearLogButton()
    {
        DebugText.text = "";
    }

    public void UpdateDebugLog(string addText)
    {
        // TODO 文字数が15000文字だかを超えると、エラーが出るので、念頭に入れておくこと
        DebugText.text += LogIndex + ":" + addText + "\n";
        LogIndex++;
    }
}
