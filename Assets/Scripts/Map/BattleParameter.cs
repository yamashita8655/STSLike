using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus
{
	private int NowHp;
	private int MaxHp;

	public PlayerStatus() {
		
	}

	public void SetNowHp(int val) {
		NowHp = val;
	}
	public int GetNowHp() {
		return NowHp;
	}
	public void AddNowHp(int val) {
		NowHp += val;
		if (NowHp <= 0) {
			NowHp = 0;
		}

		if (NowHp >= MaxHp) {
			NowHp = MaxHp;
		}
	}
	
	public void SetMaxHp(int val) {
		MaxHp = val;
	}
	public int GetMaxHp() {
		return MaxHp;
	}
	public void AddMaxHp(int val) {
		MaxHp += val;
		if (MaxHp <= 1) {
			MaxHp = 1;
		}
	}

	public bool IsDead() {
		return (NowHp <= MaxHp);
	}
}

public class EnemyStatus
{
	private int NowHp;
	private int MaxHp;

	public EnemyStatus() {
		
	}
}
