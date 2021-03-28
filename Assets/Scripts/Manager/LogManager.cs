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
}

