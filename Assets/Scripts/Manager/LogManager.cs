using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

public class LogManager : SimpleSingleton<LogManager> {
	[Conditional("UNITY_EDITOR")]
	public void Log(object message)
	{
		UnityEngine.Debug.Log(message);
	}

    [Conditional("UNITY_EDITOR")]
    public void LogError(object message)
    {
        UnityEngine.Debug.LogError(message);
    }
    
	[Conditional("UNITY_EDITOR")]
    public void LogWarning(object message)
    {
        UnityEngine.Debug.LogWarning(message);
    }
}

