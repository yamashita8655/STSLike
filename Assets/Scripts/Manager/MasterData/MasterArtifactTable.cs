using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterArtifactTable : SimpleSingleton<MasterArtifactTable>
{
	public class Data {
		public int Id { get; private set; }
		public int Rarity { get; private set; }
		public string Name { get; private set; }
		public string Detail { get; private set; }
		public string ImagePath { get; private set; }
		public EnumSelf.ArtifactEffectType Type { get; private set; }
		public int ActionId { get; private set; }
		public EnumSelf.ParameterType ParameterType { get; private set; }


        public Data(
			int id,
			int rarity,
			string name,
			string detail,
			string imagePath,
			EnumSelf.ArtifactEffectType type,
			int actionId,
			EnumSelf.ParameterType parameterType
		)
		{
			Id			= id;
			Rarity		= rarity;
			Name		= name;
			Detail		= detail;
			ImagePath	= imagePath;
			Type		= type;
			ActionId	= actionId;
			ParameterType = parameterType;
		}
	};

	private readonly string FilePath = "csv/artifacttable";

	private Dictionary<int, Data> DataDict = new Dictionary<int, Data>();

	public void Initialize()
	{
		if (DataDict.Count > 0) {
			LogManager.Instance.Log("MasterArtifactTable:Initialize return.");
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

			if (paramList[0] == "#") {
				continue;
			}

			Data data = new Data(
				int.Parse(paramList[0]),
				int.Parse(paramList[1]),
				paramList[2],
				paramList[3],
				paramList[4],
				ConvertArtifactEffectType(paramList[5]),
				int.Parse(paramList[6]),
				ConvertParameterType(paramList[7])
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
	
	// ディクショナリは外で操作されると困るので、クローンを返す
	public Dictionary<int, Data> GetCloneDict()
    {
		Dictionary<int, Data> dict = new Dictionary<int, Data>(DataDict);
        return dict;
    }
	
	private EnumSelf.ArtifactEffectType ConvertArtifactEffectType(string typeString) {
		EnumSelf.ArtifactEffectType type = EnumSelf.ArtifactEffectType.None;

		if (typeString == "ExecuteAction") {
			type = EnumSelf.ArtifactEffectType.ExecuteAction;
		} else if (typeString == "AddParameter") {
			type = EnumSelf.ArtifactEffectType.AddParameter;
		}

		return type;
	}
	
	private EnumSelf.ParameterType ConvertParameterType(string typeString) {
		EnumSelf.ParameterType type = EnumSelf.ParameterType.None;

		if (typeString == "Revive") {
			type = EnumSelf.ParameterType.Revive;
		} else if (typeString == "UsedRevive") {
			type = EnumSelf.ParameterType.UsedRevive;
		} else if (typeString == "UpgradeReward") {
			type = EnumSelf.ParameterType.UpgradeReward;
		} else if (typeString == "DiceShield") {
			type = EnumSelf.ParameterType.DiceShield;
		} else if (typeString == "RestUp8") {
			type = EnumSelf.ParameterType.RestUp8;
		} else if (typeString == "RestUp15") {
			type = EnumSelf.ParameterType.RestUp15;
		} else if (typeString == "RestUp21") {
			type = EnumSelf.ParameterType.RestUp21;
		} else if (typeString == "AntiCurse") {
			type = EnumSelf.ParameterType.AntiCurse;
		} else if (typeString == "ApprenticeKnight") {
			type = EnumSelf.ParameterType.ApprenticeKnight;
		}

		return type;
	}
}
