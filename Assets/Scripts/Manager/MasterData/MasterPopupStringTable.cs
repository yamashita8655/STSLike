using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterPopupStringTable : SimpleSingleton<MasterPopupStringTable>
{
	public class Data {
		public string Id { get; private set; }
		public string StringTableKey { get; private set; }
		public List<string> OtherIds { get; private set; }

        public Data(
			string id,
			string stringTableKey,
			List<string> otherIds
		)
		{
			Id = id;
			StringTableKey = stringTableKey;
			OtherIds = otherIds;
		}
	};

	private readonly string FilePath = "csv/popupstringtable";

	private Dictionary<string, Data> DataDict = new Dictionary<string, Data>();

	public void Initialize()
	{
		if (DataDict.Count > 0) {
			LogManager.Instance.Log("MasterPopupStringTable:Initialize return.");
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
			
			if (paramList[0] == "#") {
				continue;
			}

			List<string> otherIds = new List<string>();
			if (paramList[2] != "NONE") {
				List<string> otherIdsString = Functions.SplitString(paramList[2], split3);
				for (int i2 = 0; i2 < otherIdsString.Count; i2++) {
					otherIds.Add(otherIdsString[i2]);
				}
			}

			Data data = new Data(
				paramList[0],
				paramList[1],
				otherIds
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
}
