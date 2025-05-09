﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeDataCarrier : SimpleMonoBehaviourSingleton<HomeDataCarrier> {
	// シーン制御用
	public SceneBase Scene { get; set; }
	
	public LocalSceneManager.SceneName NextSceneName { get; set; }
	
	public void Initialize() {
		NextSceneName = LocalSceneManager.SceneName.None;
	}

	public void Release() {
		Scene = null;
	}
}
