using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterEquipItemDataTable : SimpleSingleton<MasterEquipItemDataTable>
{
	public class Data {
		public int Id { get; private set; }
		public string Name { get; private set; }
		public string ImagePath { get; private set; }
//        public Enum.EquipPart Part { get; private set; }

//        public Data(int id, string name, string imagePath, Enum.EquipPart part)
        public Data(int id, string name, string imagePath)
		{
			Id = id;
			Name = name;
			ImagePath = imagePath;
//            Part = part;
		}
	};

	private readonly string FilePath = "csv/equipitemdata";

	private Dictionary<int, Data> DataDict = new Dictionary<int, Data>();

	public void Initialize()
	{
		if (DataDict.Count > 0) {
			LogManager.Instance.Log("MasterItemTable:Initialize return.");
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

//			Data data = new Data(int.Parse(paramList[0]), paramList[1], paramList[2], (Enum.EquipPart)(int.Parse(paramList[3])));

//			DataDict.Add(int.Parse(paramList[0]), data);
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
