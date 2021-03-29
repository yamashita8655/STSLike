using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDataCarrier : SimpleMonoBehaviourSingleton<MapDataCarrier> {
	// シーン制御用
	public SceneBase Scene { get; set; }
	
	public LocalSceneManager.SceneName NextSceneName { get; set; }

	public int CurrentMapNumber = 0;
	public List<Enum.MapType> MapTypeList = null;
	
	public List<int> HandDifficultList = null;
	
	public PlayerStatus CuPlayerStatus = null;
	public EnemyStatus CuEnemyStatus = null;

	public void Initialize() {
		NextSceneName = LocalSceneManager.SceneName.None;
		MapTypeList = new List<Enum.MapType>();
		HandDifficultList = new List<int>();
	}

	public void Release() {
		Scene = null;
	}
}
