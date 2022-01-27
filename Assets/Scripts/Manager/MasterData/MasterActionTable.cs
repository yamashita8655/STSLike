using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterActionTable : SimpleSingleton<MasterActionTable>
{
	public class Data {
		public int Id { get; private set; }
		public int Rarity { get; private set; }
		public string Name { get; private set; }
		public EnumSelf.ActionType Type1 { get; private set; }
		public int Value1 { get; private set; }
		public int Value2 { get; private set; }

        public Data(
			int id,
			int rarity,
			string name,
			EnumSelf.ActionType type1,
			int value1,
			int value2
		)
		{
			Id			= id;
			Rarity		= rarity;
			Name		= name;
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

			EnumSelf.ActionType type1 = GetActionType(paramList[4]);

			Data data = new Data(
				int.Parse(paramList[0]),
				int.Parse(paramList[1]),
				paramList[2],
				type1,
				int.Parse(paramList[5]),
				int.Parse(paramList[6])
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
}
