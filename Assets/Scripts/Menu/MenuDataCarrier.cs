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
	
	
	// regularカード
	public List<GameObject> RegularSettingCardObjects { get; set; }
	public RegularSettingCardContentItem SelectCardContentItem { get; set; }
	
	public int EquipSelectIndex { get; set; }
	public List<RegularCardButtonController> RegularCardButtonControllers { get; set; }
	public MasterAction2Table.Data EquipCardSelectData { get; set; }
	
	// regularアーティファクト
	public List<GameObject> RegularSettingArtifactObjects { get; set; }
	public RegularSettingArtifactContentItem SelectArtifactContentItem { get; set; }
	
	public int EquipArtifactSelectIndex { get; set; }
	public List<RegularArtifactButtonController> RegularArtifactButtonControllers { get; set; }
	public MasterArtifactTable.Data EquipArtifactSelectData { get; set; }
	
	public void Initialize() {
		NextSceneName = LocalSceneManager.SceneName.None;
		DungeonData = null;
		
		RegularSettingCardObjects = new List<GameObject>();
		SelectCardContentItem = null;
		RegularCardButtonControllers = new List<RegularCardButtonController>();
		EquipCardSelectData = null;
		
		RegularSettingArtifactObjects = new List<GameObject>();
		SelectArtifactContentItem = null;
		RegularArtifactButtonControllers = new List<RegularArtifactButtonController>();
		EquipArtifactSelectData = null;
	}

	public void Release() {
		Scene = null;
	}
}
