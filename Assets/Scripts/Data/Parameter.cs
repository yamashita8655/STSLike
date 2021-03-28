using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parameter
{
	public int Hp { get; private set; }
	public int PhysicalAttackPower { get; private set; }
	public int MagicAttackPower { get; private set; }
	public int AdditiveDamage { get; private set; }
	public int ConstantPhysicalDefense { get; private set; }
	public int ConstantMagicDefense { get; private set; }
	public int RatioPhysicalDefense { get; private set; }
	public int RatioMagicDefense { get; private set; }
	public int CriticalRatio { get; private set; }
	public int CriticalDamageRatio { get; private set; }
	public int HitRatio { get; private set; }
	public int AvoidRatio { get; private set; }
	public int DisablePower { get; private set; }
	public int BarrierUp { get; private set; }
	public int HealUp { get; private set; }
	public int CooldownReduction { get; private set; }
	public int ShieldBlockRatio { get; private set; }
	public int SkillSlot { get; private set; }

	public Parameter(
		int hp,
		int physicalAttackPower,
		int magicAttackPower,
		int additiveDamage,
		int constantPhysicalDefense,
		int constantMagicDefense,
		int ratioPhysicalDefense,
		int ratioMagicDefense,
		int criticalRatio,
		int criticalDamageRatio,
		int hitRatio,
		int avoidRatio,
		int disablePower,
		int barrierUp,
		int healUp,
		int cooldownReduction,
		int shieldBlockRatio,
		int skillSlot
	)
	{
		Hp							=	hp;
		PhysicalAttackPower			=	physicalAttackPower;
		MagicAttackPower			=	magicAttackPower;
		AdditiveDamage				=	additiveDamage;
		ConstantPhysicalDefense		=	constantPhysicalDefense;
		ConstantMagicDefense		=	constantMagicDefense;
		RatioPhysicalDefense		=	ratioPhysicalDefense;
		RatioMagicDefense			=	ratioMagicDefense;
		CriticalRatio				=	criticalRatio;
		CriticalDamageRatio			=	criticalDamageRatio;
		HitRatio					=	hitRatio;
		AvoidRatio					=	avoidRatio;
		DisablePower				=	disablePower;
		BarrierUp					=	barrierUp;
		HealUp						=	healUp;
		CooldownReduction			=	cooldownReduction;
		ShieldBlockRatio			=	shieldBlockRatio;
		SkillSlot					=	skillSlot;
	}
}

