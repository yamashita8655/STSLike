using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHuntDataCarrier : SimpleMonoBehaviourSingleton<ItemHuntDataCarrier> {
	// シーン制御用
	public SceneBase Scene { get; set; }
	
	public LocalSceneManager.SceneName NextSceneName { get; set; }

	public float HuntTimerPassTime { get; set; }

    public List<EquipItemBase> HuntedEquipItemList { get; set; }

    public void Initialize() {
		NextSceneName = LocalSceneManager.SceneName.None;
        HuntedEquipItemList = new List<EquipItemBase>();
    }

	public void Release() {
		Scene = null;
	}
}
