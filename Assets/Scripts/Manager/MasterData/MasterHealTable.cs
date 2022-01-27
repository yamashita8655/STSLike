using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterHealTable : SimpleSingleton<MasterHealTable>
{
	public class Data {
		public int Id { get; private set; }
		public string Name { get; private set; }
		public EnumSelf.HealType Type1 { get; private set; }
		public int Value1 { get; private set; }

        public Data(
			int id,
			string name,
			EnumSelf.HealType type1,
			int value1
		)
		{
			Id			= id;
			Name		= name;
			Type1		= type1;
			Value1		= value1;
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
		// 1行目はメタデータなので、読み飛ばす
		for (int i = 1; i < lineList.Count; i++) {
			List<string> paramList = Functions.SplitString(lineList[i], split2);

			EnumSelf.HealType type1 = GetHealType(paramList[2]);

			Data data = new Data(
				int.Parse(paramList[0]),
				paramList[1],
				type1,
				int.Parse(paramList[3])
			);

			DataDict.Add(int.Parse(paramList[0]), data);
		}
	}

	private EnumSelf.HealType GetHealType(string typeString) {
		EnumSelf.HealType type = EnumSelf.HealType.None;

		if (typeString == "Heal") {
			type = EnumSelf.HealType.Heal;
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
