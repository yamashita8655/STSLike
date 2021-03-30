using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus
{
	private int NowHp;
	private int MaxHp;
	private List<MasterActionTable.Data> ActionDataList;
	private int MaxDiceCount;

	public PlayerStatus() {
		ActionDataList = new List<MasterActionTable.Data>(){
			null,null,null,null,null,null
		};
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

	public void SetActionData(int index, MasterActionTable.Data data) {
		ActionDataList[index] = data;
	}
	public MasterActionTable.Data GetActionData(int index) {
		return ActionDataList[index];
	}
	
	public void SetMaxDiceCount(int val) {
		MaxDiceCount = val;
	}
	public int GetMaxDiceCount() {
		return MaxDiceCount;
	}

	public bool IsDead() {
		return (NowHp <= 0);
	}
}

public class EnemyStatus
{
	private int NowHp;
	private int MaxHp;

	public EnemyStatus() {
		
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
		return (NowHp <= 0);
	}
}
