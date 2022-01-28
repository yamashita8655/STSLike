using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterActionTable : SimpleSingleton<MasterActionTable>
{
	public class Data {
		public int Id { get; private set; }
		public int Rarity { get; private set; }
		public string Name { get; private set; }
		public string Detail { get; private set; }
		public string ImagePath { get; private set; }
		public EnumSelf.ActionType Type1 { get; private set; }
		public int Value1 { get; private set; }
		public int Value2 { get; private set; }

        public Data(
			int id,
			int rarity,
			string name,
			string detail,
			string imagePath,
			EnumSelf.ActionType type1,
			int value1,
			int value2
		)
		{
			Id			= id;
			Rarity		= rarity;
			Name		= name;
			Detail		= detail;
			ImagePath	= imagePath;
			Type1		= type1;
			Value1		= value1;
			Value2		= value2;
		}
	};

	private readonly string FilePath = "csv/actiontable";

	private Dictionary<int, Data> DataDict = new Dictionary<int, Data>();

	public void Initialize()
	{
		if (DataDict.Count > 0) {
			LogManager.Instance.Log("MasterActionTable:Initialize return.");
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

			EnumSelf.ActionType type1 = GetActionType(paramList[6]);

			Data data = new Data(
				int.Parse(paramList[0]),
				int.Parse(paramList[1]),
				paramList[2],
				//paramList[3], // cospはデータとして不要
				paramList[4],
				paramList[5],
				type1,
				int.Parse(paramList[7]),
				int.Parse(paramList[8])
			);

			DataDict.Add(int.Parse(paramList[0]), data);
		}
	}

	private EnumSelf.ActionType GetActionType(string typeString) {
		EnumSelf.ActionType type = EnumSelf.ActionType.None;

		if (typeString == "AddDamage") {
			type = EnumSelf.ActionType.AddDamage;
		} else if (typeString == "ContinuousDamage") {
			type = EnumSelf.ActionType.ContinuousDamage;
		} else if (typeString == "Heal") {
			type = EnumSelf.ActionType.Heal;
		} else if (typeString == "AddShield") {
			type = EnumSelf.ActionType.AddShield;
		}

		return type;
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
}
