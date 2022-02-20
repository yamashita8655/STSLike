using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterArtifactLotTable : SimpleSingleton<MasterArtifactLotTable>
{
	public class Data {
		public List<List<int>> LotList { get; private set; }

		public Data(
			List<List<int>> lotList
		)
		{
			LotList = lotList;
		}
	};

	private readonly string FilePath = "csv/artifactlottable";

	private Dictionary<string, Data> DataDict = new Dictionary<string, Data>();

	public void Initialize()
	{
		if (DataDict.Count > 0) {
			LogManager.Instance.Log("MasterArtifactLotTable:Initialize return.");
			return;
		}
		TextAsset asset = Resources.Load<TextAsset>(FilePath);

		string text = asset.text;
		char[] split = {'\n'};
		List<string> lineList = Functions.SplitString(text, split);

		char[] split2 = { ',' };
		// 1行目はメタデータなので、読み飛ばす
		int count = 0;
		List<List<int>> dataList = null;
		List<int> ratioList = null;
		for (int i = 1; i < lineList.Count; i++) {
			if (string.IsNullOrEmpty(lineList[i])) {
				continue;
			}

			List<string> paramList = Functions.SplitString(lineList[i], split2);

			if (count == 0) {
				dataList = new List<List<int>>();
				ratioList = new List<int>();
				ratioList.Add(int.Parse(paramList[1]));
				ratioList.Add(int.Parse(paramList[2]));
				ratioList.Add(int.Parse(paramList[3]));
				ratioList.Add(int.Parse(paramList[4]));
				ratioList.Add(int.Parse(paramList[5]));
				
				dataList.Add(ratioList);
				count++;
			} else {
				ratioList = new List<int>();
				ratioList.Add(int.Parse(paramList[1]));
				ratioList.Add(int.Parse(paramList[2]));
				ratioList.Add(int.Parse(paramList[3]));
				ratioList.Add(int.Parse(paramList[4]));
				ratioList.Add(int.Parse(paramList[5]));
				dataList.Add(ratioList);
				count++;
				if (count == 5) {
					count = 0;
					Data data = new Data(
						dataList
					);
					DataDict.Add(paramList[0], data);
				}
			}
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
