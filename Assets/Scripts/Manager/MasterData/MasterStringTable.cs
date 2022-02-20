using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterStringTable : SimpleSingleton<MasterStringTable>
{
	public class Data {
		public string Key { get; private set; }
		public List<string> Value { get; private set; }

        public Data(
			string key,
			List<string> value
		)
		{
			Key			= key;
			Value		= value;
		}
	};

	private readonly string FilePath = "csv/stringtable";

	private Dictionary<string, Data> DataDict = new Dictionary<string, Data>();

	public void Initialize()
	{
		if (DataDict.Count > 0) {
			LogManager.Instance.Log("MasterStringTable:Initialize return.");
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

			// マルチランゲージ対応する時に、ここに国別の物追加する
			List<string> values = new List<string>();

			values.Add(paramList[1]);

			Data data = new Data(
				paramList[0],
				values
			);

			DataDict.Add(paramList[0], data);
		}
	}

	//// DataはSet関数をpublicに用意していないので、クローンにしなくて良い
	//public Data GetData(int id)
	//{
	//	Data data = null;
	//	DataDict.TryGetValue(id, out data);

	//	return data;
	//}
	
	public string GetString(string key)
	{
		Data data = null;
		DataDict.TryGetValue(key, out data);

		// TODO とりあえず、日本語固定
		int contryIndex = 0;
		string str = data.Value[contryIndex];
		str = str.Replace("<br>", "\n");
		str = str.Replace("<comma>", ",");

		return str;
	}
}
