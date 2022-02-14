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
	
	//public enum ActionType {
    //    None,
    //    AddDamage,
    //    ContinuousDamage,
    //    Heal,
    //    AddShield,
	//	Max
	//};
	
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
        Warning,
        Stun,
		// バフ、デバフ
        Strength,
        DiceMinusOne,
        Regenerate,
        ReverseHeal,
        Poison,
        Weakness,
        Vulnerable,
        Patient,
        AutoShield,
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
		Poison,
		Max
	}
	
	public enum TurnPowerType {
		None = -1,
		DiceMinusOne = 0,
		ReverseHeal,
		Weakness,
        Vulnerable,

		// ターン経過時に1ずつ減らさない物
		Patient,
		AutoShield,
		Max
	}
	
	public enum AIChangeType {
		None = -1,
		TurnProgress = 0,// 指定されたターンが経過したら、指定のAIIDに遷移する。基のIDに戻る際も、Rotationで2個セットして、2ターンと決め打ちすれば、これを使って実現可能
		HpBorder,// 指定されたHp以下になったら、指定のAIIDに遷移する。基のIDに戻ると、またHp減った時にHpボーダーを参照するので、同じ行動をするHpボーダーを条件としないAIを作って遷移した方が良い。ちなみに、殴った瞬間AI変わるのは、相手がスタンするならまだしも、理不尽過ぎるので、通常は下記の行動抽選時に行う方が、猶予を与えられて良き
		LotHpBorder,// 敵抽選開始時に指定されたHp以下なら、指定のAIIDに遷移する。基のIDに戻ると、またHp減った時にHpボーダーを参照するので、同じ行動をするHpボーダーを条件としないAIを作って遷移した方が良い。
		Max
	}
}
