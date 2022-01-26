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
	
	public List<int> DiceValueList = null;
	public int SelectAttackIndex = 0;
	
	public int SelectTreasureIndex = 0;

	public List<MasterActionTable.Data> TreasureList = null;
	
	public int SelectChangeIndex = 0;
	
	public int MaxFloor = 0;
	public int NowFloor = 0;
	
	public int SelectHealIndex = 0;
	
	public List<MasterHealTable.Data> HealList = null;
	
	public int ContinuousCount = 0;
	public int MaxContinuousCount = 0;
	
	public int EnemyContinuousCount = 0;
	public int EnemyMaxContinuousCount = 0;

	public MasterDungeonTable.Data DungeonData { get; set; }

	public void Initialize() {
		NextSceneName = LocalSceneManager.SceneName.None;
		MapTypeList = new List<Enum.MapType>();
		HandDifficultList = new List<int>();
		DiceValueList = new List<int>();
		TreasureList = new List<MasterActionTable.Data>();
		HealList = new List<MasterHealTable.Data>();
		DungeonData = null;
	}

	public void Release() {
		Scene = null;
	}
}
