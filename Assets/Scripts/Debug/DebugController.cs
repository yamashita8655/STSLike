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
        Debug.Log("Next:" + StateMachineManager.Instance.GetNextState(StateMachineName.Map));
        Debug.Log("Current:" + StateMachineManager.Instance.GetState(StateMachineName.Map));
        Debug.Log("Prev:" + StateMachineManager.Instance.GetPrevState(StateMachineName.Map));

        string output = "";
        var history = StateMachineManager.Instance.GetHistory(StateMachineName.Map);
        for (int i = 0; i < history.Count; i++) {
            output += history[i];
        }
        Debug.Log("History:" + output);
    }
}
