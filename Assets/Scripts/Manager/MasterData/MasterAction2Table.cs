using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterAction2Table : SimpleSingleton<MasterAction2Table>
{
	public class Data {
		public int Id { get; private set; }
		public int Rarity { get; private set; }
		public string Name { get; private set; }
		public int EquipCost { get; private set; }
		public int UnlockCost { get; private set; }
		public int DiceCost { get; private set; }
		public EnumSelf.UseType UseType { get; private set; }
		public string Detail { get; private set; }
		public string ImagePath { get; private set; }
		public List<ActionPack> ActionPackList { get; private set; }
        public Data(
			int id,
			int rarity,
			string name,
			int equipCost,
			int unlockCost,
			int diceCost,
			EnumSelf.UseType useType,
			string detail,
			string imagePath,
			List<ActionPack> actionPackList
		)
		{
			Id				= id;
			Rarity			= rarity;
			Name			= name;
			EquipCost		= equipCost;
			UnlockCost		= unlockCost;
			DiceCost		= diceCost;
			UseType			= useType;
			Detail			= detail;
			ImagePath		= imagePath;
			ActionPackList	= actionPackList;
		}
	};

	private readonly string FilePath = "csv/actiontable2";

	private Dictionary<int, Data> DataDict = new Dictionary<int, Data>();
	
	private List<List<int>> BattleRarityCardList = new List<List<int>>();
	private List<List<int>> AllRarityCardList = new List<List<int>>();

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

		BattleRarityCardList.Add(new List<int>());
		BattleRarityCardList.Add(new List<int>());
		BattleRarityCardList.Add(new List<int>());
		BattleRarityCardList.Add(new List<int>());
		BattleRarityCardList.Add(new List<int>());
		
		AllRarityCardList.Add(new List<int>());
		AllRarityCardList.Add(new List<int>());
		AllRarityCardList.Add(new List<int>());
		AllRarityCardList.Add(new List<int>());
		AllRarityCardList.Add(new List<int>());

		char[] split2 = { ',' };
		// 1行目はメタデータなので、読み飛ばす
		for (int i = 1; i < lineList.Count; i++) {
			List<string> paramList = Functions.SplitString(lineList[i], split2);

			// 最初に#が付いてる物は、コメント行なので飛ばす
			if (paramList[0] == "#") {
				continue;
			}

			int id = int.Parse(paramList[0]);

			// アクションパックを先に作っておく
			List<ActionPack> list = new List<ActionPack>();
			int index = 9;
			int damageNumberCount = 0;
			while (true) {
				if (paramList[index] == "NONE") {
					break;
				}
				EnumSelf.EffectType effect = GetEffectType(paramList[index]);
				EnumSelf.TargetType target = GetTargetType(paramList[index+1]);
				int value = int.Parse(paramList[index+2]);
				EnumSelf.TimingType timing = GetTimingType(paramList[index+3]);

				ActionPack pack = new ActionPack(int.Parse(paramList[0]), effect, target, value, timing, damageNumberCount);
				
				if (
					(effect == EnumSelf.EffectType.Damage) ||
					(effect == EnumSelf.EffectType.DamageSuction) ||
					(effect == EnumSelf.EffectType.DamageShieldSuction) ||
					(effect == EnumSelf.EffectType.DamageGainMaxHp) ||
					(effect == EnumSelf.EffectType.DamageMultiStrength) ||
					(effect == EnumSelf.EffectType.ShieldBash)
				) {
					damageNumberCount++;
				}

				list.Add(pack);

				index += 4;
			}

			Data data = new Data(
				id,
				int.Parse(paramList[1]),
				paramList[2],
				int.Parse(paramList[3]),
				int.Parse(paramList[4]),
				int.Parse(paramList[5]),
				GetUseType(paramList[6]),
				paramList[7],
				paramList[8],
				list
			);

			DataDict.Add(id, data);

			// TODO とりあえず、1000以下がプレイヤーのカードという扱い
			if (id < 1000) {
				AllRarityCardList[int.Parse(paramList[1])-1].Add(id);
				
				// ボスカードは、通常ロットテーブルには加えない
				if (id < 900) {
					BattleRarityCardList[int.Parse(paramList[1])-1].Add(id);
				}
			}
		}
	}

	private EnumSelf.EffectType GetEffectType(string typeString) {
		EnumSelf.EffectType type = EnumSelf.EffectType.None;

		if (typeString == "Damage") {
			type = EnumSelf.EffectType.Damage;
		} else if (typeString == "DamageSuction") {
			type = EnumSelf.EffectType.DamageSuction;
		} else if (typeString == "DamageShieldSuction") {
			type = EnumSelf.EffectType.DamageShieldSuction;
		} else if (typeString == "ShieldBash") {
			type = EnumSelf.EffectType.ShieldBash;
		} else if (typeString == "DamageGainMaxHp") {
			type = EnumSelf.EffectType.DamageGainMaxHp;
		} else if (typeString == "DamageMultiStrength") {
			type = EnumSelf.EffectType.DamageMultiStrength;
		} else if (typeString == "TrueDamage") {
			type = EnumSelf.EffectType.TrueDamage;
		} else if (typeString == "RemovePower") {
			type = EnumSelf.EffectType.RemovePower;
		} else if (typeString == "Shield") {
			type = EnumSelf.EffectType.Shield;
		} else if (typeString == "ShieldDouble") {
			type = EnumSelf.EffectType.ShieldDouble;
		} else if (typeString == "ShieldDamage") {
			type = EnumSelf.EffectType.ShieldDamage;
		} else if (typeString == "Heal") {
			type = EnumSelf.EffectType.Heal;
		} else if (typeString == "Warning") {
			type = EnumSelf.EffectType.Warning;
		} else if (typeString == "Stun") {
			type = EnumSelf.EffectType.Stun;
		} else if (typeString == "Death") {
			type = EnumSelf.EffectType.Death;
		} else if (typeString == "Curse") {
			type = EnumSelf.EffectType.Curse;
		} else if (typeString == "Draw") {
			type = EnumSelf.EffectType.Draw;
		} else if (typeString == "GainDiceCost") {
			type = EnumSelf.EffectType.GainDiceCost;
		} else if (typeString == "Hand2DeckTop") {
			type = EnumSelf.EffectType.Hand2DeckTop;
		} else if (typeString == "Hand2Erase") {
			type = EnumSelf.EffectType.Hand2Erase;
		} else if (typeString == "Hand2Discard") {
			type = EnumSelf.EffectType.Hand2Discard;
		} else if (typeString == "Hand2Trash") {
			type = EnumSelf.EffectType.Hand2Trash;
		} else if (typeString == "Hand2Erase") {
			type = EnumSelf.EffectType.Hand2Erase;
		} else if (typeString == "Strength") {
			type = EnumSelf.EffectType.Strength;
		} else if (typeString == "FastStrength") {
			type = EnumSelf.EffectType.FastStrength;
		} else if (typeString == "Toughness") {
			type = EnumSelf.EffectType.Toughness;
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
		} else if (typeString == "Vulnerable") {
			type = EnumSelf.EffectType.Vulnerable;
		} else if (typeString == "ShieldWeakness") {
			type = EnumSelf.EffectType.ShieldWeakness;
		} else if (typeString == "TurnRegenerate") {
			type = EnumSelf.EffectType.TurnRegenerate;
		} else if (typeString == "Patient") {
			type = EnumSelf.EffectType.Patient;
		} else if (typeString == "AutoShield") {
			type = EnumSelf.EffectType.AutoShield;
		} else if (typeString == "Thorn") {
			type = EnumSelf.EffectType.Thorn;
		} else if (typeString == "RotBody") {
			type = EnumSelf.EffectType.RotBody;
		} else if (typeString == "Versak") {
			type = EnumSelf.EffectType.Versak;
		} else if (typeString == "ReactiveShield") {
			type = EnumSelf.EffectType.ReactiveShield;
		} else if (typeString == "SubStrength") {
			type = EnumSelf.EffectType.SubStrength;
		} else if (typeString == "ShieldPreserve") {
			type = EnumSelf.EffectType.ShieldPreserve;
		} else if (typeString == "Invincible") {
			type = EnumSelf.EffectType.Invincible;
		} else if (typeString == "DoubleAttack") {
			type = EnumSelf.EffectType.DoubleAttack;
		} else if (typeString == "Cost6DoubleAttack") {
			type = EnumSelf.EffectType.Cost6DoubleAttack;
		} else if (typeString == "Critical") {
			type = EnumSelf.EffectType.Critical;
		} else if (typeString == "AddMaxDiceCost") {
			type = EnumSelf.EffectType.AddMaxDiceCost;
		} else if (typeString == "NonDraw") {
			type = EnumSelf.EffectType.NonDraw;
		} else if (typeString == "HealCharge") {
			type = EnumSelf.EffectType.HealCharge;
		} else if (typeString == "DoubleStrength") {
			type = EnumSelf.EffectType.DoubleStrength;
		} else if (typeString == "DebugDisaster") {
			type = EnumSelf.EffectType.DebugDisaster;
		}

		if (type == EnumSelf.EffectType.None) {
			LogManager.Instance.LogError("MasterAction2Table:GetEffectType:" + typeString + " is None");
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
	
	private EnumSelf.UseType GetUseType(string typeString) {
		EnumSelf.UseType type = EnumSelf.UseType.Repeat;
		
		if (typeString == "Repeat") {
			type = EnumSelf.UseType.Repeat;
		} else if (typeString == "Discard") {
			type = EnumSelf.UseType.Discard;
		} else if (typeString == "Erase") {
			type = EnumSelf.UseType.Erase;
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
	
	// リストは外で操作されると困るので、クローンを返す
	public List<int> GetBattleRarityCardCloneList(int rarity)
	{
		List<int> list = new List<int>(BattleRarityCardList[rarity-1]);
		return list;
	}
	
	// リストは外で操作されると困るので、クローンを返す
	public List<int> GetAllRarityCardCloneList(int rarity)
	{
		List<int> list = new List<int>(AllRarityCardList[rarity-1]);
		return list;
	}
}

public class ActionPack {
	public int ExecuteActionId { get; private set; }
	public EnumSelf.EffectType Effect { get; private set; }
	public EnumSelf.TargetType Target { get; private set; }
	public int Value { get; private set; }
	public EnumSelf.TimingType Timing { get; private set; }
	public int DamageNumberCount { get; private set; }// そのアクションのDamage、DamageSuction、DamageShieldSuction、ShieldBashが何個目か

	public ActionPack(
		int executeActionId,
		EnumSelf.EffectType effect,
		EnumSelf.TargetType target,
		int value,
		EnumSelf.TimingType timing,
		int damageNumberCount
	) {
		ExecuteActionId = executeActionId;
		Effect = effect;
		Target = target;
		Value = value;
		Timing = timing;
		DamageNumberCount = damageNumberCount;
	}
}
