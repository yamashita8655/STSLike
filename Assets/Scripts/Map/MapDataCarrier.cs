using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDataCarrier : SimpleMonoBehaviourSingleton<MapDataCarrier> {
	// シーン制御用
	public SceneBase Scene { get; set; }
	
	public LocalSceneManager.SceneName NextSceneName { get; set; }

	public int CurrentMapNumber = 0;
	public List<EnumSelf.MapType> MapTypeList = null;
	
	public List<int> HandDifficultList = null;
	
	public PlayerStatus CuPlayerStatus = null;
	public EnemyStatus CuEnemyStatus = null;
	
	public List<int> DiceValueList = null;
	public int SelectAttackIndex = 0;
	
	public int SelectTreasureIndex = 0;

	public List<MasterAction2Table.Data> TreasureList = null;
	
	public int SelectChangeIndex = 0;
	
	public int MaxFloor = 0;
	public int NowFloor = 0;
	
	public int SelectHealIndex = 0;
	public List<MasterHealTable.Data> HealList = null;
	
	public int SelectArtifactIndex = 0;
	public List<MasterArtifactTable.Data> ArtifactList = null;
	
	public int ActionPackCount = 0;
	public int MaxActionPackCount = 0;
	
	public int EnemyActionPackCount = 0;
	public int EnemyMaxActionPackCount = 0;

	public MasterDungeonTable.Data DungeonData { get; set; }
	
	public bool IsClear { get; set; }
	
	public List<MasterArtifactTable.Data> GetArtifactList = null;
	
	public List<GameObject> ValueObjects { get; set; }
	public List<GameObject> EnemyValueObjects { get; set; }

	public void Initialize() {
		NextSceneName = LocalSceneManager.SceneName.None;
		MapTypeList = new List<EnumSelf.MapType>();
		HandDifficultList = new List<int>();
		DiceValueList = new List<int>();
		TreasureList = new List<MasterAction2Table.Data>();
		HealList = new List<MasterHealTable.Data>();
		ArtifactList = new List<MasterArtifactTable.Data>();
		GetArtifactList = new List<MasterArtifactTable.Data>();
		ValueObjects = new List<GameObject>();
		EnemyValueObjects = new List<GameObject>();
		DungeonData = null;
	}

	public void Release() {
		Scene = null;
	}
}
