using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterRegularCardMaxCostTable : SimpleSingleton<MasterRegularCardMaxCostTable>
{
	public class Data {
		public int Id { get; private set; }
		public int Border { get; private set; }
        public Data(
			int id,
			int border
		)
		{
			Id		= id;
			Border	= border;
		}
	};

	private readonly string FilePath = "csv/regularcardmaxcosttable";

	private Dictionary<int, Data> DataDict = new Dictionary<int, Data>();

	public void Initialize()
	{
		if (DataDict.Count > 0) {
			LogManager.Instance.Log("MasterRegularCardMaxCostTable:Initialize return.");
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

			// 最初に#が付いてる物は、コメント行なので飛ばす
			if (paramList[0] == "#") {
				continue;
			}

			Data data = new Data(
				int.Parse(paramList[0]),
				int.Parse(paramList[1])
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
	
	// ディクショナリは外で操作されると困るので、クローンを返す
	public Dictionary<int, Data> GetCloneDict()
    {
		Dictionary<int, Data> dict = new Dictionary<int, Data>(DataDict);
        return dict;
    }

	public int GetNowLevel(int totalPoint) {
		int level = 0;

		for (int i = 1; i < DataDict.Count; i++) {
			int lowBorder = DataDict[i-1].Border;
			int highBorder = DataDict[i].Border;
			if ((lowBorder < totalPoint) && (totalPoint <= highBorder)) {
				level = i;
				break;
			}
		}

		return level;
	}
	
	public bool IsMaxLevel(int nowLevel) {
		bool res = false;
		int maxLevel = DataDict.Count-1;// 0レベルが存在するので、個数-1
		if (nowLevel == maxLevel) {
			res = true;
		}
		return res;
	}
	
	public int GetNextLevelNeedPoint(int totalPoint) {
		int point = 0;
		int level = 0;

		for (int i = 1; i < DataDict.Count; i++) {
			int lowBorder = DataDict[i-1].Border;
			int highBorder = DataDict[i].Border;
			if ((lowBorder < totalPoint) && (totalPoint <= highBorder)) {
				level = i;
				break;
			}
		}
		
		if (IsMaxLevel(level) == false) {
			point = DataDict[level+1].Border - totalPoint;
		}

		return point;
	}
}

