using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugController : MonoBehaviour
{
	public void OnClickOpenButton()
	{
		SystemDialogManager.Instance.OpenDebug();
	}

	public void OnClickCloseButton()
	{
		SystemDialogManager.Instance.CloseDebug();
	}

    public void OnClickCurrectStateDisplay()
    {
        Debug.Log(StateMachineManager.Instance.GetNextState(StateMachineName.Map));
        Debug.Log(StateMachineManager.Instance.GetState(StateMachineName.Map));
        Debug.Log(StateMachineManager.Instance.GetPrevState(StateMachineName.Map));
    }
}
