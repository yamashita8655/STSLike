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
		public string InitiativeActionId { get; private set; }
		public int AIID { get; private set; }
		public List<int> AddDropActionIds { get; private set; }

        public Data(
			int id,
			string name,
			int hp,
			int mHp,
			string imagePath,
			string initiativeActionId,
			int aiId,
			List<int> addDropActionIds
		)
		{
			Id			= id;
			Name		= name;
			Hp			= hp;
			MHp			= mHp;
			ImagePath	= imagePath;
			InitiativeActionId = initiativeActionId;
			AIID	= aiId;
			AddDropActionIds = addDropActionIds;
		}
	};

	private readonly string FilePath = "csv/enemytable";

	private Dictionary<int, Data> DataDict = new Dictionary<int, Data>();
	private Dictionary<int, int> DataIndexDict = new Dictionary<int, int>();

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
		char[] split3 = { '-' };
		// 1行目はメタデータなので、読み飛ばす
		int index = 0;
		for (int i = 1; i < lineList.Count; i++) {
			List<string> paramList = Functions.SplitString(lineList[i], split2);
			
			List<int> addDropActionIds = new List<int>();

			if (paramList[7] != "NONE") {
				List<string> addDropActionIdsString = Functions.SplitString(paramList[7], split3);
				for (int i2 = 0; i2 < addDropActionIdsString.Count; i2++) {
					addDropActionIds.Add(int.Parse(addDropActionIdsString[i2]));
				}
			}

			Data data = new Data(
				int.Parse(paramList[0]),
				paramList[1],
				int.Parse(paramList[2]),
				int.Parse(paramList[3]),
				paramList[4],
				paramList[5],
				int.Parse(paramList[6]),
				addDropActionIds
			);

			DataDict.Add(int.Parse(paramList[0]), data);
			DataIndexDict.Add(int.Parse(paramList[0]), index);
			index++;
		}
	}
	
	// DataはSet関数をpublicに用意していないので、クローンにしなくて良い
	public Data GetData(int id)
	{
		Data data = null;
		DataDict.TryGetValue(id, out data);

		return data;
	}
	
	// ディクショナリは外で操作されると困るので、クローンを返す
	public Dictionary<int, Data> GetCloneDict()
    {
		Dictionary<int, Data> dict = new Dictionary<int, Data>(DataDict);
        return dict;
    }
	
	public int GetIndexFromId(int id)
	{
		int index = 0;
		DataIndexDict.TryGetValue(id, out index);

		return index;
	}
}


