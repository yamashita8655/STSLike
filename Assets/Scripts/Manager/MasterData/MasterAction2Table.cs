using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterAction2Table : SimpleSingleton<MasterAction2Table>
{
	public class Data {
		public int Id { get; private set; }
		public int Rarity { get; private set; }
		public string Name { get; private set; }
		public string Detail { get; private set; }
		public string ImagePath { get; private set; }
		public List<ActionPack> ActionPackList { get; private set; }

        public Data(
			int id,
			int rarity,
			string name,
			string detail,
			string imagePath,
			List<ActionPack> actionPackList
		)
		{
			Id				= id;
			Rarity			= rarity;
			Name			= name;
			Detail			= detail;
			ImagePath		= imagePath;
			ActionPackList	= actionPackList;
		}
	};

	private readonly string FilePath = "csv/actiontable2";

	private Dictionary<int, Data> DataDict = new Dictionary<int, Data>();

	public void Initialize()
	{
		if (DataDict.Count > 0) {
			LogManager.Instance.Log("MasterActionTable2:Initialize return.");
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

			// アクションパックを先に作っておく
			List<ActionPack> list = new List<ActionPack>();
			int index = 6;
			while (true) {
				if (paramList[index] == "NONE") {
					break;
				}
				EnumSelf.EffectType effect = GetEffectType(paramList[index]);
				EnumSelf.TargetType target = GetTargetType(paramList[index+1]);
				int value = int.Parse(paramList[index+2]);
				EnumSelf.TimingType timing = GetTimingType(paramList[index+3]);

				ActionPack pack = new ActionPack(effect, target, value, timing);

				list.Add(pack);

				index += 4;
			}

			Data data = new Data(
				int.Parse(paramList[0]),
				int.Parse(paramList[1]),
				paramList[2],
				paramList[4],
				paramList[5],
				list
			);

			DataDict.Add(int.Parse(paramList[0]), data);
		}
	}

	private EnumSelf.EffectType GetEffectType(string typeString) {
		EnumSelf.EffectType type = EnumSelf.EffectType.None;

		if (typeString == "Damage") {
			type = EnumSelf.EffectType.Damage;
		} else if (typeString == "Shield") {
			type = EnumSelf.EffectType.Shield;
		} else if (typeString == "ShieldDamage") {
			type = EnumSelf.EffectType.ShieldDamage;
		} else if (typeString == "Heal") {
			type = EnumSelf.EffectType.Heal;
		} else if (typeString == "Strength") {
			type = EnumSelf.EffectType.Strength;
		} else if (typeString == "DiceMinusOne") {
			type = EnumSelf.EffectType.DiceMinusOne;
		} else if (typeString == "Regenerate") {
			type = EnumSelf.EffectType.Regenerate;
		} else if (typeString == "ReverseHeal") {
			type = EnumSelf.EffectType.ReverseHeal;
		} else if (typeString == "Poison") {
			type = EnumSelf.EffectType.Poison;
		} else if (typeString == "Weakness") {
			type = EnumSelf.EffectType.Weakness;
		}

		return type;
	}
	
	private EnumSelf.TargetType GetTargetType(string typeString) {
		EnumSelf.TargetType type = EnumSelf.TargetType.None;

		if (typeString == "Self") {
			type = EnumSelf.TargetType.Self;
		} else if (typeString == "Opponent") {
			type = EnumSelf.TargetType.Opponent;
		}

		return type;
	}
	
	private EnumSelf.TimingType GetTimingType(string typeString) {
		EnumSelf.TimingType type = EnumSelf.TimingType.None;
		
		if (typeString == "BattleStart") {
			type = EnumSelf.TimingType.BattleStart;
		} else if (typeString == "TurnStart") {
			type = EnumSelf.TimingType.TurnStart;
		} else if (typeString == "BeforeAdd") {
			type = EnumSelf.TimingType.BeforeAdd;
		} else if (typeString == "Add") {
			type = EnumSelf.TimingType.Add;
		} else if (typeString == "AfterAdd") {
			type = EnumSelf.TimingType.AfterAdd;
		} else if (typeString == "TurnEnd") {
			type = EnumSelf.TimingType.TurnEnd;
		} else if (typeString == "SelfDeath") {
			type = EnumSelf.TimingType.SelfDeath;
		} else if (typeString == "OpponentDeath") {
			type = EnumSelf.TimingType.OpponentDeath;
		} else if (typeString == "BattleEnd") {
			type = EnumSelf.TimingType.BattleEnd;
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

public class ActionPack {
	public EnumSelf.EffectType Effect { get; private set; }
	public EnumSelf.TargetType Target { get; private set; }
	public int Value { get; private set; }
	public EnumSelf.TimingType Timing { get; private set; }

	public ActionPack(
		EnumSelf.EffectType effect,
		EnumSelf.TargetType target,
		int value,
		EnumSelf.TimingType timing
	) {
		Effect = effect;
		Target = target;
		Value = value;
		Timing = timing;
	}
}
