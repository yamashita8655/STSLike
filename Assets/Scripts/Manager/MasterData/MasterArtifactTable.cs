using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterArtifactTable : SimpleSingleton<MasterArtifactTable>
{
	public class Data {
		public int Id { get; private set; }
		public int UId { get; private set; }
		public int EquipCost { get; private set; }
		public int UnlockCost { get; private set; }
		public string LotType { get; private set; }
		public int Rarity { get; private set; }
		public string Name { get; private set; }
		public string Detail { get; private set; }
		public string ImagePath { get; private set; }
		public EnumSelf.ArtifactEffectType Type { get; private set; }
		public int ActionId { get; private set; }
		public EnumSelf.ParameterType ParameterType { get; private set; }
		public List<string> PopupStringIds { get; private set; }


        public Data(
			int id,
			int uid,
			int equipCost,
			int unlockCost,
			string lotType,
			int rarity,
			string name,
			string detail,
			string imagePath,
			EnumSelf.ArtifactEffectType type,
			int actionId,
			EnumSelf.ParameterType parameterType,
			List<string> popupStringIds
		)
		{
			Id			= id;
			UId			= uid;
			EquipCost	= equipCost;
			UnlockCost	= unlockCost;
			LotType		= lotType;
			Rarity		= rarity;
			Name		= name;
			Detail		= detail;
			ImagePath	= imagePath;
			Type		= type;
			ActionId	= actionId;
			ParameterType = parameterType;
			PopupStringIds = popupStringIds;
		}
	};

	private readonly string FilePath = "csv/artifacttable";

	private Dictionary<int, Data> DataDict = new Dictionary<int, Data>();

	private List<List<int>> RarityArtifactList = new List<List<int>>();

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
		
		RarityArtifactList.Add(new List<int>());
		RarityArtifactList.Add(new List<int>());
		RarityArtifactList.Add(new List<int>());
		RarityArtifactList.Add(new List<int>());
		RarityArtifactList.Add(new List<int>());

		char[] split2 = { ',' };
		char[] split3 = { '-' };
		// 1行目はメタデータなので、読み飛ばす
		for (int i = 1; i < lineList.Count; i++) {
			List<string> paramList = Functions.SplitString(lineList[i], split2);

			if (paramList[0] == "#") {
				continue;
			}
			
			List<string> popupStringIds = new List<string>();
			if (paramList[12] != "NONE") {
				List<string> popupStringId = Functions.SplitString(paramList[12], split3);
				for (int i2 = 0; i2 < popupStringId.Count; i2++) {
					popupStringIds.Add(popupStringId[i2]);
				}
			}


			Data data = new Data(
				int.Parse(paramList[0]),
				int.Parse(paramList[1]),
				int.Parse(paramList[2]),
				int.Parse(paramList[3]),
				paramList[4],
				int.Parse(paramList[5]),
				paramList[6],
				paramList[7],
				paramList[8],
				ConvertArtifactEffectType(paramList[9]),
				int.Parse(paramList[10]),
				ConvertParameterType(paramList[11]),
				popupStringIds
			);

			DataDict.Add(int.Parse(paramList[0]), data);

			// TODO NORMALが、トレジャーから手に入るリスト
			if (paramList[4] == "NORMAL") {
				RarityArtifactList[int.Parse(paramList[5])-1].Add(int.Parse(paramList[0]));
			}
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
	
	// リストは外で操作されると困るので、クローンを返す
	public List<int> GetRarityArtifactCloneList(int rarity)
	{
		List<int> list = new List<int>(RarityArtifactList[rarity-1]);
		return list;
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
		} else if (typeString == "RestUp1") {
			type = EnumSelf.ParameterType.RestUp1;
		} else if (typeString == "RestUp2") {
			type = EnumSelf.ParameterType.RestUp2;
		} else if (typeString == "RestUp3") {
			type = EnumSelf.ParameterType.RestUp3;
		} else if (typeString == "AntiCurse") {
			type = EnumSelf.ParameterType.AntiCurse;
		} else if (typeString == "ApprenticeKnight") {
			type = EnumSelf.ParameterType.ApprenticeKnight;
		} else if (typeString == "KnightMaster") {
			type = EnumSelf.ParameterType.KnightMaster;
		} else if (typeString == "AddVersak") {
			type = EnumSelf.ParameterType.AddVersak;
		} else if (typeString == "AddMaxHp1") {
			type = EnumSelf.ParameterType.AddMaxHp1;
		} else if (typeString == "AddMaxHp2") {
			type = EnumSelf.ParameterType.AddMaxHp2;
		} else if (typeString == "AddMaxHp3") {
			type = EnumSelf.ParameterType.AddMaxHp3;
		} else if (typeString == "AddMaxHp4") {
			type = EnumSelf.ParameterType.AddMaxHp4;
		} else if (typeString == "WeaknessUp") {
			type = EnumSelf.ParameterType.WeaknessUp;
		} else if (typeString == "VulnerableUp") {
			type = EnumSelf.ParameterType.VulnerableUp;
		} else if (typeString == "Minimalist") {
			type = EnumSelf.ParameterType.Minimalist;
		} else if (typeString == "SupportFire") {
			type = EnumSelf.ParameterType.SupportFire;
		} else if (typeString == "AntiWeakness") {
			type = EnumSelf.ParameterType.AntiWeakness;
		} else if (typeString == "AntiShieldWeakness") {
			type = EnumSelf.ParameterType.AntiShieldWeakness;
		} else if (typeString == "ShieldOne") {
			type = EnumSelf.ParameterType.ShieldOne;
		} else if (typeString == "ShieldTwo") {
			type = EnumSelf.ParameterType.ShieldTwo;
		} else if (typeString == "ShieldThree") {
			type = EnumSelf.ParameterType.ShieldThree;
		} else if (typeString == "UseCurseShield") {
			type = EnumSelf.ParameterType.UseCurseShield;
		} else if (typeString == "FirstAidKit") {
			type = EnumSelf.ParameterType.FirstAidKit;
		} else if (typeString == "GodBless") {
			type = EnumSelf.ParameterType.GodBless;
		} else if (typeString == "SeekersAmulet") {
			type = EnumSelf.ParameterType.SeekersAmulet;
		} else if (typeString == "Momonga") {
			type = EnumSelf.ParameterType.Momonga;
		} else if (typeString == "UsedMomonga") {
			type = EnumSelf.ParameterType.UsedMomonga;
		} else if (typeString == "AssassinRod") {
			type = EnumSelf.ParameterType.AssassinRod;
		} else if (typeString == "HeroSword") {
			type = EnumSelf.ParameterType.HeroSword;
		} else if (typeString == "HeroShield") {
			type = EnumSelf.ParameterType.HeroShield;
		} else if (typeString == "AddDoubleAttack6") {
			type = EnumSelf.ParameterType.AddDoubleAttack6;
		} else if (typeString == "ZeroTurnEndShield") {
			type = EnumSelf.ParameterType.ZeroTurnEndShield;
		} else if (typeString == "DyingAddVersak") {
			type = EnumSelf.ParameterType.DyingAddVersak;
		} else if (typeString == "GameStart3Draw") {
			type = EnumSelf.ParameterType.GameStart3Draw;
		} else if (typeString == "AddAnotherStrength1") {
			type = EnumSelf.ParameterType.AddAnotherStrength1;
		} else if (typeString == "DamageAddStrength1") {
			type = EnumSelf.ParameterType.DamageAddStrength1;
		} else if (typeString == "NoAttackGetTurnShieldPreserve") {
			type = EnumSelf.ParameterType.NoAttackGetTurnShieldPreserve;
		} else if (typeString == "Use3ShieldAddToughness") {
			type = EnumSelf.ParameterType.Use3ShieldAddToughness;
		} else if (typeString == "ReduseShieldLimit15") {
			type = EnumSelf.ParameterType.ReduseShieldLimit15;
		} else if (typeString == "ZeroHand1Draw") {
			type = EnumSelf.ParameterType.ZeroHand1Draw;
		} else if (typeString == "Under1CostGainDamage4") {
			type = EnumSelf.ParameterType.Under1CostGainDamage4;
		} else if (typeString == "TurnStartDrawGain1") {
			type = EnumSelf.ParameterType.TurnStartDrawGain1;
		} else if (typeString == "Use10Card1Draw") {
			type = EnumSelf.ParameterType.Use10Card1Draw;
		} else if (typeString == "Use10CardGain3Activity") {
			type = EnumSelf.ParameterType.Use10CardGain3Activity;
		} else if (typeString == "TurnStart1SelfTrueDamage2Shield") {
			type = EnumSelf.ParameterType.TurnStart1SelfTrueDamage2Shield;
		} else if (typeString == "ExtraAddHealCharge1") {
			type = EnumSelf.ParameterType.ExtraAddHealCharge1;
		} else if (typeString == "Award") {
			type = EnumSelf.ParameterType.Award;
		}

		return type;
	}
}
