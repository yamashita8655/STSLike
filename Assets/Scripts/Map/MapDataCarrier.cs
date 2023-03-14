using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDataCarrier : SimpleMonoBehaviourSingleton<MapDataCarrier> {
	// シーン制御用
	public SceneBase Scene { get; set; }
	
	public LocalSceneManager.SceneName NextSceneName { get; set; }

	public List<EnumSelf.MapType> MapTypeList = null;
	
	public List<int> HandDifficultList = null;
	public int SelectDifficultNumber { get; set; }
	public int SelectDifficultIndex { get; set; }
	
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
	public MasterAction2Table.Data SelectEraseData = null;
	
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

	// レアリティ別の、トレジャーから手に入る、まだ未獲得のアーティファクトリスト
	public List<List<int>> RarityNoAcquiredArtifactList { get; set; }

	public int BattleTurnCount { get; set; }

	public int CurrentTotalDiceCost { get; set; }
	
	public List<BattleCardButtonController> BattleCardButtonControllers { get; set; }
	public MasterAction2Table.Data SelectBattleCardData { get; set; }
	public MasterAction2Table.Data DoubleAttackBattleCardData { get; set; }
	public MasterAction2Table.Data Cost6DoubleAttackBattleCardData { get; set; }
	public bool IsDoubleAttackCard { get; set; }
	public bool IsCost6DoubleAttackCard { get; set; }
	
	public List<MasterAction2Table.Data> OriginalDeckList { get; set; }
	public List<MasterAction2Table.Data> BattleDeckList { get; set; }
	public List<MasterAction2Table.Data> TrashList { get; set; }
	public List<MasterAction2Table.Data> DiscardList { get; set; }
	
	public List<CardContentItem> CardContentItemList { get; set; }

	public int AddDiceCost { get; set; }
	
	public List<PopupAnimationController> PopupAnimationControllers { get; set; }
	
	public int EventBattleFloorAdd { get; set; }

	public List<int> ChestList { get; set; }
	
	public List<ChestObjectController> ResultChestCtrls { get; set; }
	
	public void Initialize() {
		NextSceneName = LocalSceneManager.SceneName.None;
		MapTypeList = new List<EnumSelf.MapType>();
		HandDifficultList = new List<int>();
		DiceValueList = new List<int>();
		TreasureList = new List<MasterAction2Table.Data>();
		HealList = new List<MasterHealTable.Data>();
		ArtifactList = new List<MasterArtifactTable.Data>();
		CarryArtifactList = new List<ArtifactButtonContentItem>();
		ResultChestCtrls = new List<ChestObjectController>();

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
	
		RarityNoAcquiredArtifactList = new List<List<int>>();
		RarityNoAcquiredArtifactList.Add(new List<int>());
		RarityNoAcquiredArtifactList.Add(new List<int>());
		RarityNoAcquiredArtifactList.Add(new List<int>());
		RarityNoAcquiredArtifactList.Add(new List<int>());
		RarityNoAcquiredArtifactList.Add(new List<int>());

		DungeonData = null;
	
		BattleCardButtonControllers = new List<BattleCardButtonController>();
		SelectBattleCardData = null;
		DoubleAttackBattleCardData = null;
		Cost6DoubleAttackBattleCardData = null;
	
		OriginalDeckList = new List<MasterAction2Table.Data>();
		BattleDeckList = new List<MasterAction2Table.Data>();
		TrashList = new List<MasterAction2Table.Data>();
		DiscardList = new List<MasterAction2Table.Data>();

		CardContentItemList = new List<CardContentItem>();

		PopupAnimationControllers = new List<PopupAnimationController>();

		ChestList = new List<int>(){0,0,0};
	}

	public void RemoveRarityNoAcquiredArtifactList(int id) {
		MasterArtifactTable.Data data = MasterArtifactTable.Instance.GetData(id);
		int rarityIndex = data.Rarity-1;
		var list = RarityNoAcquiredArtifactList[rarityIndex];
		for (int i = 0; i < list.Count; i++) {
			if (id == list[i]) {
				list.RemoveAt(i);
				break;
			}
		}
	}
	
	public void CopyDeck() {
		BattleDeckList = new List<MasterAction2Table.Data>(OriginalDeckList);
	}
	
	public void DeckShuffle() {
		// 直接BattleDeckListにシャッフルをかけると、新しい参照先に変更されてしまうので、それは問題の為。
		var workList = new List<MasterAction2Table.Data>(BattleDeckList);
		workList.AddRange(TrashList);
		TrashList.Clear();
		workList = workList.OrderBy(a => Guid.NewGuid()).ToList();
		BattleDeckList.Clear();
		BattleDeckList.AddRange(workList);
	}
	
	public BattleCardButtonController GetNonActiveBattleCardController() {
		BattleCardButtonController ctrl = null;
		for (int i = 0; i < BattleCardButtonControllers.Count; i++) {
			if (BattleCardButtonControllers[i].gameObject.activeSelf == false) {
				ctrl = BattleCardButtonControllers[i];
				break;
			}
		}

		return ctrl;
	}
	
	public int GetHandCount() {
		int count = 0;
		for (int i = 0; i < BattleCardButtonControllers.Count; i++) {
			if (BattleCardButtonControllers[i].gameObject.activeSelf == true) {
				count++;
			}
		}

		return count;
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

