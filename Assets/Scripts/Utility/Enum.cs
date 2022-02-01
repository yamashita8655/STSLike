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
        Heal,
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
}
