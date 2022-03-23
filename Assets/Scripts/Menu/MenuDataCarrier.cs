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
	
	public List<PopupAnimationController> PopupAnimationControllers { get; set; }

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
		
		PopupAnimationControllers = new List<PopupAnimationController>();
	}
	
	public void SpawnPopup(List<string> ids) { 
		List<string> list = new List<string>();
		for (int i = 0; i < ids.Count; i++) {
			CreateSpawnStringList(list, ids[i]);
		}

		if (list.Count > PopupAnimationControllers.Count) {
			LogManager.Instance.LogError("SpawnPopup,10個以上指定されてNULLエラー");
		}

		int index = 0;
		for (; index < list.Count; index++) {
			PopupAnimationControllers[index].Initialize(list[index]);
			PopupAnimationControllers[index].Play("Play", () => {});
		}
		
		for (; index < PopupAnimationControllers.Count; index++) {
			PopupAnimationControllers[index].Play("Init",() => {});
		}
		
	}

	private void CreateSpawnStringList(List<string> list, string id) {
		var data = MasterPopupStringTable.Instance.GetData(id);
		list.Add(MasterStringTable.Instance.GetString(data.StringTableKey));
		var otherIds = data.OtherIds;
		for (int i = 0; i < otherIds.Count; i++) {
			CreateSpawnStringList(list, otherIds[i]);
		}
	}
	
	public void SpawnPopup(MasterAction2Table.Data data) {
		HashSet<string> list = new HashSet<string>();
		var packs = data.ActionPackList;
		for (int i = 0; i < packs.Count; i++) {
			list.Add($"{packs[i].Effect}");
		}
		SpawnPopup(new List<string>(list));
	}

	public void SpawnPopup(MasterArtifactTable.Data data) {
		SpawnPopup(data.PopupStringIds);
	}

	public void Release() {
		Scene = null;
	}
}
