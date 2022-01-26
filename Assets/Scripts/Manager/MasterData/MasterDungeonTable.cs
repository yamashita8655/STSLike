using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterDungeonTable : SimpleSingleton<MasterDungeonTable>
{
	public class Data {
		public string Id { get; private set; }
		public string Name { get; private set; }
		public string Detail { get; private set; }
		public int FloorCount { get; private set; }

		public Data(string id, string name, string detail, int floorCount)
		{
			Id = id;
			Name = name;
			Detail = detail;
			FloorCount = floorCount;
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
		// 1行目はメタデータなので、読み飛ばす
		for (int i = 1; i < lineList.Count; i++) {
			if (string.IsNullOrEmpty(lineList[i])) {
				continue;
			}
			List<string> paramList = Functions.SplitString(lineList[i], split2);
			Data data = new Data(paramList[0], paramList[1], paramList[2], int.Parse(paramList[3]));

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
