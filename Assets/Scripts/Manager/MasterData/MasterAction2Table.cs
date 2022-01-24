using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterAction2Table : SimpleSingleton<MasterAction2Table>
{
	public class Data {
		public int Id { get; private set; }
		public int Rarity { get; private set; }
		public string Name { get; private set; }
		public List<ActionPack> ActionPackList { get; private set; }

        public Data(
			int id,
			int rarity,
			string name,
			List<ActionPack> actionPackList
		)
		{
			Id				= id;
			Rarity			= rarity;
			Name			= name;
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
			int index = 4;
			while (true) {
				if (paramList[index] == "NONE") {
					break;
				}
				Enum.EffectType effect = GetEffectType(paramList[index]);
				Enum.TargetType target = GetTargetType(paramList[index+1]);
				int value = int.Parse(paramList[index+2]);
				Enum.TimingType timing = GetTimingType(paramList[index+3]);

				ActionPack pack = new ActionPack(effect, target, value, timing);

				list.Add(pack);

				index += 4;
			}

			Data data = new Data(
				int.Parse(paramList[0]),
				int.Parse(paramList[1]),
				paramList[2],
				list
			);

			DataDict.Add(int.Parse(paramList[0]), data);
		}
	}

	private Enum.EffectType GetEffectType(string typeString) {
		Enum.EffectType type = Enum.EffectType.None;

		if (typeString == "Damage") {
			type = Enum.EffectType.Damage;
		} else if (typeString == "Shield") {
			type = Enum.EffectType.Shield;
		} else if (typeString == "Heal") {
			type = Enum.EffectType.Heal;
		}

		return type;
	}
	
	private Enum.TargetType GetTargetType(string typeString) {
		Enum.TargetType type = Enum.TargetType.None;

		if (typeString == "Self") {
			type = Enum.TargetType.Self;
		} else if (typeString == "Opponent") {
			type = Enum.TargetType.Opponent;
		}

		return type;
	}
	
	private Enum.TimingType GetTimingType(string typeString) {
		Enum.TimingType type = Enum.TimingType.None;
		
		if (typeString == "BattleStart") {
			type = Enum.TimingType.BattleStart;
		} else if (typeString == "TurnStart") {
			type = Enum.TimingType.TurnStart;
		} else if (typeString == "BeforeAdd") {
			type = Enum.TimingType.BeforeAdd;
		} else if (typeString == "Add") {
			type = Enum.TimingType.Add;
		} else if (typeString == "AfterAdd") {
			type = Enum.TimingType.AfterAdd;
		} else if (typeString == "TurnEnd") {
			type = Enum.TimingType.TurnEnd;
		} else if (typeString == "SelfDeath") {
			type = Enum.TimingType.SelfDeath;
		} else if (typeString == "OpponentDeath") {
			type = Enum.TimingType.OpponentDeath;
		} else if (typeString == "BattleEnd") {
			type = Enum.TimingType.BattleEnd;
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

public class ActionPack {
	public Enum.EffectType Effect { get; private set; }
	public Enum.TargetType Target { get; private set; }
	public int Value { get; private set; }
	public Enum.TimingType Timing { get; private set; }

	public ActionPack(
		Enum.EffectType effect,
		Enum.TargetType target,
		int value,
		Enum.TimingType timing
	) {
		Effect = effect;
		Target = target;
		Value = value;
		Timing = timing;
	}
}
