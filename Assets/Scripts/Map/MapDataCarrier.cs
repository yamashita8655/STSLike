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
	public int SelectDifficultNumber { get; set; }
	
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
	
	public int InitiativeActionPackCount = 0;
	public int MaxInitiativeActionPackCount = 0;
	
	public int EnemyActionPackCount = 0;
	public int EnemyMaxActionPackCount = 0;
	
	public int EnemyInitiativeActionPackCount = 0;
	public int EnemyMaxInitiativeActionPackCount = 0;

	public MasterDungeonTable.Data DungeonData { get; set; }
	
	public bool IsClear { get; set; }
	
	public List<ArtifactButtonContentItem> CarryArtifactList = null;
	
	public List<List<GameObject>> ValueObjects { get; set; }
	public List<GameObject> EnemyValueObjects { get; set; }
	
	public List<GameObject> PowerObjects { get; set; }
	public List<GameObject> TurnPowerObjects { get; set; }
	public List<GameObject> EnemyPowerObjects { get; set; }
	public List<GameObject> EnemyTurnPowerObjects { get; set; }
	
	public List<int> PowerValues { get; set; }
	public List<int> TurnPowerValues { get; set; }

	public void Initialize() {
		NextSceneName = LocalSceneManager.SceneName.None;
		MapTypeList = new List<EnumSelf.MapType>();
		HandDifficultList = new List<int>();
		DiceValueList = new List<int>();
		TreasureList = new List<MasterAction2Table.Data>();
		HealList = new List<MasterHealTable.Data>();
		ArtifactList = new List<MasterArtifactTable.Data>();
		CarryArtifactList = new List<ArtifactButtonContentItem>();

		ValueObjects = new List<List<GameObject>>();
		// サイコロの数は6個なので、6個分リスト作る
		for (int i = 0; i < 6; i++) {
			ValueObjects.Add(new List<GameObject>());
		}

		EnemyValueObjects = new List<GameObject>();
		PowerObjects = new List<GameObject>();
		TurnPowerObjects = new List<GameObject>();
		EnemyPowerObjects = new List<GameObject>();
		EnemyTurnPowerObjects = new List<GameObject>();

		PowerValues = new List<int>();
		for (int i = 0; i < (int)EnumSelf.PowerType.Max; i++) {
			PowerValues.Add(0);
		}

		TurnPowerValues = new List<int>();
		for (int i = 0; i < (int)EnumSelf.TurnPowerType.Max; i++) {
			TurnPowerValues.Add(0);
		}

		DungeonData = null;
	}

	public void Release() {
		Scene = null;
	}
}
