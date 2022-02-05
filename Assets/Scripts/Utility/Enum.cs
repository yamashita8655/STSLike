using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class EnumSelf : MonoBehaviour {

	public enum MapType {
		Enemy = 0,
		Elite,
		Boss,
		Heal,
		Treasure,
		Max
	};
	
	public enum ActionType {
        None,
        AddDamage,
        ContinuousDamage,
        Heal,
        AddShield,
		Max
	};
	
	public enum HealType {
        None,
        Heal,
		Max
	};
	
	public enum EffectType {
        None,
        Damage,
        Shield,
        ShieldDamage,
        Heal,
		// バフ、デバフ
        Strength,
        DiceMinusOne,
        Regenerate,
		Max
	};
	
	public enum TargetType {
        None,
        Self,
		Opponent,
		Max
	};
	
	public enum TimingType {
        None,
		BattleStart,
		TurnStart,
		BeforeAdd,
		Add,
		AfterAdd,
		TurnEnd,
		SelfDeath,
		OpponentDeath,
		BattleEnd,
		Max
	};

	public enum EnemyActionType {
		None,
		Random,
		Rotation
	}
	
	// TODO これ、マスターデータで設定しないから、どこかにどっちのタイプに属するのか
	// 書いておかないとダメだな…。
	public enum PowerKind {
		Power = 0,
		TurnPower,
	}

	public enum PowerType {
		None = -1,
		Strength = 0,
		Regenerate,
		Max
	}
	
	public enum TurnPowerType {
		None = -1,
		DiceMinusOne = 0,
		Max
	}
}
