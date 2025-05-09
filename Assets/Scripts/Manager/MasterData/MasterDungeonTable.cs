﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterDungeonTable : SimpleSingleton<MasterDungeonTable>
{
	public class Data {
		public string Id { get; private set; }
		public string Name { get; private set; }
		public string Detail { get; private set; }
		public string ImagePath { get; private set; }
		public int FloorCount { get; private set; }
		public List<int> EnemyLotIds { get; private set; }
		public List<int> LotFloors { get; private set; }
		public int EliteLotId { get; private set; }
		public int BossLotId { get; private set; }
		public int RewardPoint { get; private set; }

		public Data(
			string id,
			string name,
			string detail,
			string imagePath,
			int floorCount,
			List<int> enemyLotIds,
			List<int> lotFloors,
			int eliteLotId,
			int bossLotId,
			int rewardPoint 
		)
		{
			Id = id;
			Name = name;
			Detail = detail;
			ImagePath = imagePath;
			FloorCount = floorCount;
			EnemyLotIds = enemyLotIds;
			LotFloors = lotFloors;
			BossLotId = bossLotId;
			EliteLotId = eliteLotId;
			RewardPoint = rewardPoint;
		}
	};

	private readonly string FilePath = "csv/dungeontable";

	private Dictionary<string, Data> DataDict = new Dictionary<string, Data>();

	public void Initialize()
	{
		if (DataDict.Count > 0) {
			LogManager.Instance.Log("MasterDungeonTable:Initialize return.");
			return;
		}
		TextAsset asset = Resources.Load<TextAsset>(FilePath);

		string text = asset.text;
		char[] split = {'\n'};
		List<string> lineList = Functions.SplitString(text, split);

		char[] split2 = { ',' };
		char[] split3 = { '-' };
		// 1行目はメタデータなので、読み飛ばす
		for (int i = 1; i < lineList.Count; i++) {
			if (string.IsNullOrEmpty(lineList[i])) {
				continue;
			}
			List<string> paramList = Functions.SplitString(lineList[i], split2);

			List<string> enemyLotIdString = Functions.SplitString(paramList[5], split3);
			List<string> lotFloorString = Functions.SplitString(paramList[6], split3);

			List<int> enemyLotIds = new List<int>();
			for (int i2 = 0; i2 < enemyLotIdString.Count; i2++) {
				enemyLotIds.Add(int.Parse(enemyLotIdString[i2]));
			}
			
			List<int> lotFloors = new List<int>();
			for (int i2 = 0; i2 < lotFloorString.Count; i2++) {
				lotFloors.Add(int.Parse(lotFloorString[i2]));
			}
			
			Data data = new Data(
				paramList[0],
				paramList[1],
				paramList[2],
				paramList[3],
				int.Parse(paramList[4]),
				enemyLotIds,
				lotFloors,
				int.Parse(paramList[7]),
				int.Parse(paramList[8]),
				int.Parse(paramList[9])
			);

			DataDict.Add(paramList[0], data);
		}
	}

	// DataはSet関数をpublicに用意していないので、クローンにしなくて良い
	public Data GetData(string id)
	{
		Data data = null;
		DataDict.TryGetValue(id, out data);

		return data;
	}
    
	// ディクショナリは外で操作されると困るので、クローンを返す
	public Dictionary<string, Data> GetCloneDict()
    {
		Dictionary<string, Data> dict = new Dictionary<string, Data>(DataDict);
        return dict;
    }
}
