using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterEnemyTable : SimpleSingleton<MasterEnemyTable>
{
	public class Data {
		public int Id { get; private set; }
		public string Name { get; private set; }
		public int Hp { get; private set; }
		public int MHp { get; private set; }
		public string ImagePath { get; private set; }
		public EnumSelf.EnemyActionType ActionType { get; private set; }
		public List<int> ActionIds { get; private set; }

        public Data(
			int id,
			string name,
			int hp,
			int mHp,
			string imagePath,
			EnumSelf.EnemyActionType actionType,
			List<int> actionIds
		)
		{
			Id			= id;
			Name		= name;
			Hp			= hp;
			MHp			= mHp;
			ImagePath	= imagePath;
			ActionType	= actionType;
			ActionIds	= actionIds;
		}
	};

	private readonly string FilePath = "csv/enemytable";

	private Dictionary<int, Data> DataDict = new Dictionary<int, Data>();

	public void Initialize()
	{
		if (DataDict.Count > 0) {
			LogManager.Instance.Log("MasterEnemyTable:Initialize return.");
			return;
		}
		TextAsset asset = Resources.Load<TextAsset>(FilePath);

		string text = asset.text;
		char[] split = {'\n'};
		List<string> lineList = Functions.SplitString(text, split);

		char[] split2 = { ',' };
		// 1行目はメタデータなので、読み飛ばす
		for (int i = 1; i < lineList.Count; i++) {
			List<string> paramList = Functions.SplitString(lineList[i], split2);

			// アクションIDリストを先に作っておく
			List<int> list = new List<int>();
			int index = 6;
			while (true) {
				if (paramList[index] == "NONE") {
					break;
				}
				list.Add(int.Parse(paramList[index]));

				index++;
			}

			Data data = new Data(
				int.Parse(paramList[0]),
				paramList[1],
				int.Parse(paramList[2]),
				int.Parse(paramList[3]),
				paramList[4],
				GetEnemyActionType(paramList[5]),
				list
			);

			DataDict.Add(int.Parse(paramList[0]), data);
		}
	}
	
	private EnumSelf.EnemyActionType GetEnemyActionType(string typeString) {
		EnumSelf.EnemyActionType type = EnumSelf.EnemyActionType.None;

		if (typeString == "Random") {
			type = EnumSelf.EnemyActionType.Random;
		} else if (typeString == "Rotation") {
			type = EnumSelf.EnemyActionType.Rotation;
		}

		return type;
	}

	// DataはSet関数をpublicに用意していないので、クローンにしなくて良い
	public Data GetData(int id)
	{
		Data data = null;
		DataDict.TryGetValue(id, out data);

		return data;
	}
}


