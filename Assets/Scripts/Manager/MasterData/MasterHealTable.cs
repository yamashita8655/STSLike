using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterHealTable : SimpleSingleton<MasterHealTable>
{
	public class Data {
		public int Id { get; private set; }
		public string Name { get; private set; }
		public List<int> Values { get; private set; }
		public string Detail { get; private set; }

        public Data(
			int id,
			string name,
			List<int> values,
			string detail
		)
		{
			Id			= id;
			Name		= name;
			Values		= values;
			Detail		= detail;
		}
	};

	private readonly string FilePath = "csv/healtable";

	private Dictionary<int, Data> DataDict = new Dictionary<int, Data>();

	public void Initialize()
	{
		if (DataDict.Count > 0) {
			LogManager.Instance.Log("MasterHealTable:Initialize return.");
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
			List<string> paramList = Functions.SplitString(lineList[i], split2);

			List<string> valueString = Functions.SplitString(paramList[2], split3);
			List<int> values = new List<int>();
			for (int i2 = 0; i2 < valueString.Count; i2++) {
				values.Add(int.Parse(valueString[i2]));
			}

			Data data = new Data(
				int.Parse(paramList[0]),
				paramList[1],
				values,
				paramList[3]
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
