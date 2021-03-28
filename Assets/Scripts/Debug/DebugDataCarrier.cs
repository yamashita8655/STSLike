using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugDataCarrier : SimpleMonoBehaviourSingleton<DebugDataCarrier> {
	// シーン制御用
	public SceneBase Scene { get; set; }
	
	public void Initialize() {
	}

	public void Release() {
		Scene = null;
	}
}
