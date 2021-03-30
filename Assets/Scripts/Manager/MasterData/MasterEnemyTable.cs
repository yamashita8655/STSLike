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
		public int ActionType { get; private set; }
		public int ActionId1 { get; private set; }
		public int ActionId2 { get; private set; }
		public int ActionId3 { get; private set; }

        public Data(
			int id,
			string name,
			int hp,
			int mHp,
			int actionType,
			int actionId1,
			int actionId2,
			int actionId3
		)
		{
			Id			= id;
			Name		= name;
			Hp			= hp;
			MHp			= mHp;
			ActionType	= actionType;
			ActionId1	= actionId1;
			ActionId2 	= actionId2;
            ActionId3   = actionId3;
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

			Data data = new Data(
				int.Parse(paramList[0]),
				paramList[1],
				int.Parse(paramList[2]),
				int.Parse(paramList[3]),
				int.Parse(paramList[4]),
				int.Parse(paramList[5]),
				int.Parse(paramList[6]),
				int.Parse(paramList[7])
			);

			DataDict.Add(int.Parse(paramList[0]), data);
		}
	}

	// DataはSet関数をpublicに用意していないので、クローンにしなくて良い
	public Data GetData(int id)
	{
		Data data = null;
		DataDict.TryGetValue(id, out data);

		return data;
	}
}


