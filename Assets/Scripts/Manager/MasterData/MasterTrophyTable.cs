using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterTrophyTable : SimpleSingleton<MasterTrophyTable>
{
	public class Data {
		public int Id { get; private set; }
		public string Detail { get; private set; }
		public EnumSelf.TrophyCountType Type { get; private set; }
		public int CompleteCount { get; private set; }
		public int Parameter { get; private set; }
		public EnumSelf.TrophyRewardType RewardType { get; private set; }
		public int RewardValue { get; private set; }

		public Data(
			int id,
			string detail,
			EnumSelf.TrophyCountType type,
			int completeCount,
			int parameter,
			EnumSelf.TrophyRewardType rewardType,
			int rewardValue
		)
		{
			Id				=	id;
			Detail			=	detail;
			Type			=   type;
			CompleteCount  	=   completeCount;
			Parameter      	=   parameter;
			RewardType     	=   rewardType;
			RewardValue    	=   rewardValue;
		}
	};

	private readonly string FilePath = "csv/trophytable";

	private Dictionary<string, Data> DataDict = new Dictionary<string, Data>();

	public void Initialize()
	{
		if (DataDict.Count > 0) {
			LogManager.Instance.Log("MasterTrophyTable:Initialize return.");
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
			Data data = new Data(
					int.Parse(paramList[0]),
					paramList[1],
					ConvertTrophyCountType(paramList[2]),
					int.Parse(paramList[3]),
					int.Parse(paramList[4]),
					ConvertTrophyRewardType(paramList[5]),
					int.Parse(paramList[6])
				);

			DataDict.Add(paramList[0], data);
		}
	}
	
	private EnumSelf.TrophyCountType ConvertTrophyCountType(string typeString) {
		EnumSelf.TrophyCountType type = EnumSelf.TrophyCountType.None;

		if (typeString == "DeleteEnemy") {
			type = EnumSelf.TrophyCountType.DeleteEnemy;
		} else if (typeString == "HealCount") {
			type = EnumSelf.TrophyCountType.HealCount;
		} else if (typeString == "DiceCostUpCount") {
			type = EnumSelf.TrophyCountType.DiceCostUpCount;
		} else if (typeString == "EraseCount") {
			type = EnumSelf.TrophyCountType.EraseCount;
		} else if (typeString == "FindRarityCard") {
			type = EnumSelf.TrophyCountType.FindRarityCard;
		} else if (typeString == "FindRarityArtifact") {
			type = EnumSelf.TrophyCountType.FindRarityArtifact;
		} else if (typeString == "GetStrength") {
			type = EnumSelf.TrophyCountType.GetStrength;
		} else if (typeString == "GetShield") {
			type = EnumSelf.TrophyCountType.GetShield;
		} else if (typeString == "DeckCount") {
			type = EnumSelf.TrophyCountType.DeckCount;
		} else if (typeString == "GiveDamage") {
			type = EnumSelf.TrophyCountType.GiveDamage;
		} else if (typeString == "MaxHp") {
			type = EnumSelf.TrophyCountType.MaxHp;
		} else if (typeString == "HP1Win") {
			type = EnumSelf.TrophyCountType.HP1Win;
		} else if (typeString == "NoDamageBoss") {
			type = EnumSelf.TrophyCountType.NoDamageBoss;
		} else if (typeString == "NoDamageElite") {
			type = EnumSelf.TrophyCountType.NoDamageElite;
		} else if (typeString == "DoubleKO") {
			type = EnumSelf.TrophyCountType.DoubleKO;
		}

		return type;
	}
	
	private EnumSelf.TrophyRewardType ConvertTrophyRewardType(string typeString) {
		EnumSelf.TrophyRewardType type = EnumSelf.TrophyRewardType.None;

		if (typeString == "CardCostUp") {
			type = EnumSelf.TrophyRewardType.CardCostUp;
		} else if (typeString == "ArtifactCostUp") {
			type = EnumSelf.TrophyRewardType.ArtifactCostUp;
		}

		return type;
	}

	// DataはSet関数をpublicに用意していないので、クローンにしなくて良い
	public Data GetData(string id)
	{
		Data data = null;
		DataDict.TryGetValue(id, out data);

		return data;
	}
}
