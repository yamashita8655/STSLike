using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus
{
	private int NowHp;
	private int MaxHp;
	private int NowShield;
	private int MaxShield;
	private List<MasterActionTable.Data> ActionDataList;
	private List<MasterAction2Table.Data> ActionDataList2;
	private int MaxDiceCount;

	public PlayerStatus() {
		ActionDataList = new List<MasterActionTable.Data>(){
			null,null,null,null,null,null
		};
		
		ActionDataList2 = new List<MasterAction2Table.Data>(){
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
	
	public void SetActionData2(int index, MasterAction2Table.Data data) {
		ActionDataList2[index] = data;
	}
	public MasterAction2Table.Data GetActionData2(int index) {
		return ActionDataList2[index];
	}
	
	public void SetMaxDiceCount(int val) {
		MaxDiceCount = val;
	}
	public int GetMaxDiceCount() {
		return MaxDiceCount;
	}
	
	public void SetNowShield(int val) {
		NowShield = val;
	}
	public int GetNowShield() {
		return NowShield;
	}
	public void AddNowShield(int val) {
		NowShield += val;
		if (NowShield <= 0) {
			NowShield = 0;
		}

		if (NowShield >= MaxShield) {
			NowShield = MaxShield;
		}
	}
	
	public void SetMaxShield(int val) {
		MaxShield = val;
	}
	public int GetMaxShield() {
		return MaxShield;
	}
	public void AddMaxShield(int val) {
		MaxShield += val;
		if (MaxShield <= 0) {
			MaxShield = 0;
		}
	}

	public bool IsDead() {
		return (NowHp <= 0);
	}
}

public class EnemyStatus
{
	private int NowHp;
	private int MaxHp;
	
	private int NowShield;
	private int MaxShield;
	
	// TODO
	// とりあえず、一個だけ登録
	// 複数持たせて、ローテかランダムかの対応は、後でやる
	private List<MasterActionTable.Data> ActionDataList;
	private List<MasterAction2Table.Data> ActionDataList2;

	public EnemyStatus() {
		ActionDataList = new List<MasterActionTable.Data>();
		ActionDataList2 = new List<MasterAction2Table.Data>();
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
	
	public void AddActionData(MasterActionTable.Data data) {
		ActionDataList.Add(data);
	}
	public MasterActionTable.Data GetActionData() {
		// TODO とりあえず0番目決め打ち
		return ActionDataList[0];
	}
	
	public void AddActionData2(MasterAction2Table.Data data) {
		ActionDataList2.Add(data);
	}
	public MasterAction2Table.Data GetActionData2() {
		return ActionDataList2[0];
	}
	
	public void SetNowShield(int val) {
		NowShield = val;
	}
	public int GetNowShield() {
		return NowShield;
	}
	public void AddNowShield(int val) {
		NowShield += val;
		if (NowShield <= 0) {
			NowShield = 0;
		}

		if (NowShield >= MaxShield) {
			NowShield = MaxShield;
		}
	}
	
	public void SetMaxShield(int val) {
		MaxShield = val;
	}
	public int GetMaxShield() {
		return MaxShield;
	}
	public void AddMaxShield(int val) {
		MaxShield += val;
		if (MaxShield <= 0) {
			MaxShield = 0;
		}
	}

	public bool IsDead() {
		return (NowHp <= 0);
	}
}
