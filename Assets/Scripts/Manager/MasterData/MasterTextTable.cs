using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterTextTable : SimpleSingleton<MasterTextTable>
{
	public class Data {
		public string Key { get; private set; }
		public string Text { get; private set; }

		public Data(string key, string text)
		{
			Key = key;
			Text = text;
		}
	};

	private readonly string FilePath = "csv/text";

	private Dictionary<string, Data> DataDict = new Dictionary<string, Data>();

	public void Initialize()
	{
		if (DataDict.Count > 0) {
			LogManager.Instance.Log("MasterTextTable:Initialize return.");
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
			Data data = new Data(paramList[0], paramList[1]);

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
}
