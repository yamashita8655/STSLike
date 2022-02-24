using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuDataCarrier : SimpleMonoBehaviourSingleton<MenuDataCarrier> {
	// シーン制御用
	public SceneBase Scene { get; set; }
	
	public LocalSceneManager.SceneName NextSceneName { get; set; }

	public SceneDataBase Data { get; set; }

	public MasterDungeonTable.Data DungeonData { get; set; }
	
	public List<GameObject> RegularSettingCardObjects { get; set; }
	
	public RegularSettingCardContentItem SelectCardContentItem { get; set; }
	
	public int EquipSelectIndex { get; set; }
	
	public void Initialize() {
		NextSceneName = LocalSceneManager.SceneName.None;
		DungeonData = null;
		RegularSettingCardObjects = new List<GameObject>();
		SelectCardContentItem = null;
	}

	public void Release() {
		Scene = null;
	}
}
