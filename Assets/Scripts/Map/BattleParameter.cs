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
	private List<MasterAction2Table.Data> ActionDataList;
	private List<MasterAction2Table.Data> InitiativeActionDataList;
	private int MaxDiceCount;
	
	private Power BuffPower = null;
	private Power DebuffPower = null;

	private TurnPower CuTurnPower = null;

	public PlayerStatus() {
		ActionDataList = new List<MasterAction2Table.Data>(){
			null,null,null,null,null,null
		};
		InitiativeActionDataList = new List<MasterAction2Table.Data>();
		
		BuffPower = new Power();
		DebuffPower = new Power();

		CuTurnPower = new TurnPower();
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
	
	public void SetActionData(int index, MasterAction2Table.Data data) {
		ActionDataList[index] = data;
	}
	public MasterAction2Table.Data GetActionData(int index) {
		return ActionDataList[index];
	}
	public void AddActionData(int index, MasterAction2Table.Data data) {
		ActionDataList[index] = data;
	}
	
	public MasterAction2Table.Data GetInitiativeFirstActionData() {
		MasterAction2Table.Data data = null;
		if (InitiativeActionDataList.Count != 0) {
			data = InitiativeActionDataList[0];
		}
		return data;
	}
	public void RemoveInitiativeFirstActionData() {
		if (InitiativeActionDataList.Count != 0) {
			InitiativeActionDataList.RemoveAt(0);
		}
	}
	public int GetInitiativeActionDataCount() {
		return InitiativeActionDataList.Count;
	}
	public void AddInitiativeActionData(MasterAction2Table.Data data) {
		InitiativeActionDataList.Add(data);
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
	
	public void AddPower(EnumSelf.PowerType type, int val) {
		if (val < 0) {
			DebuffPower.AddValue(type, val);
		} else {
			BuffPower.AddValue(type, val);
		}
	}
	
	public Power GetPower() {
		Power retPower = new Power();
		for (int i = 0; i < (int)EnumSelf.PowerType.Max; i++) {
			int val = BuffPower.GetValue((EnumSelf.PowerType)i) + DebuffPower.GetValue((EnumSelf.PowerType)i);
			retPower.SetValue((EnumSelf.PowerType)i, val);
		}

		return retPower;
	}
	
	public void AddTurnPower(EnumSelf.TurnPowerType type, int val) {
		CuTurnPower.AddTurnPowerValue(type, val);
	}
	
	public TurnPower GetTurnPower() {
		return CuTurnPower;
	}
	
	public int GetTurnPowerValue(EnumSelf.TurnPowerType type) {
		return CuTurnPower.GetTurnPowerValue(type);
	}
	
	public void ResetPower() {
		for (int i = 0; i < (int)EnumSelf.PowerType.Max; i++) {
			BuffPower.SetValue((EnumSelf.PowerType)i, 0);
			DebuffPower.SetValue((EnumSelf.PowerType)i, 0);
		}
	}
	
	public void ResetTurnPower() {
		for (int i = 0; i < (int)EnumSelf.TurnPowerType.Max; i++) {
			CuTurnPower.SetTurnPowerValue((EnumSelf.TurnPowerType)i, 0);
		}
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
	private List<MasterAction2Table.Data> ActionDataList;
	private List<MasterAction2Table.Data> InitiativeActionDataList;

	private MasterEnemyTable.Data Data = null;
	private int CurrentActionIndex = 0;

	private MasterAction2Table.Data DecideActionData = null;

	private Power BuffPower = null;
	private Power DebuffPower = null;

	private TurnPower CuTurnPower = null;

	public EnemyStatus(MasterEnemyTable.Data data) {
		ActionDataList = new List<MasterAction2Table.Data>();
		InitiativeActionDataList = new List<MasterAction2Table.Data>();
		BuffPower = new Power();
		DebuffPower = new Power();
		Data = data;
		CuTurnPower = new TurnPower();
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
	
	public void AddActionData(MasterAction2Table.Data data) {
		ActionDataList.Add(data);
	}
	public MasterAction2Table.Data GetActionData() {
		return DecideActionData;
	}
	
	public MasterAction2Table.Data GetInitiativeFirstActionData() {
		MasterAction2Table.Data data = null;
		if (InitiativeActionDataList.Count != 0) {
			data = InitiativeActionDataList[0];
		}
		return data;
	}
	public int GetInitiativeActionDataCount() {
		return InitiativeActionDataList.Count;
	}
	public void RemoveInitiativeFirstActionData() {
		if (InitiativeActionDataList.Count != 0) {
			InitiativeActionDataList.RemoveAt(0);
		}
	}
	public void AddInitiativeActionData(MasterAction2Table.Data data) {
		InitiativeActionDataList.Add(data);
	}

	public void LotActionData() {
	
		MasterAction2Table.Data data = null;
		if (Data.ActionType == EnumSelf.EnemyActionType.Random) {
			int all = ActionDataList.Count;
			int index = UnityEngine.Random.Range(0, all);
			data = ActionDataList[index];
		} else if (Data.ActionType == EnumSelf.EnemyActionType.Rotation) {
			data = ActionDataList[CurrentActionIndex];
			CurrentActionIndex++;
			if (CurrentActionIndex >= ActionDataList.Count) {
				CurrentActionIndex = 0;
			} 
		}
		DecideActionData = data;
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

	public void AddPower(EnumSelf.PowerType type, int val) {
		if (val < 0) {
			DebuffPower.AddValue(type, val);
		} else {
			BuffPower.AddValue(type, val);
		}
	}

	public Power GetPower() {
		Power retPower = new Power();
		for (int i = 0; i < (int)EnumSelf.PowerType.Max; i++) {
			int val = BuffPower.GetValue((EnumSelf.PowerType)i) + DebuffPower.GetValue((EnumSelf.PowerType)i);
			retPower.SetValue((EnumSelf.PowerType)i, val);
		}

		return retPower;
	}
	
	public void AddTurnPower(EnumSelf.TurnPowerType type, int val) {
		CuTurnPower.AddTurnPowerValue(type, val);
	}
	
	public TurnPower GetTurnPower() {
		return CuTurnPower;
	}
	
	public int GetTurnPowerValue(EnumSelf.TurnPowerType type) {
		return CuTurnPower.GetTurnPowerValue(type);
	}

	public void ResetPower() {
		for (int i = 0; i < (int)EnumSelf.PowerType.Max; i++) {
			BuffPower.SetValue((EnumSelf.PowerType)i, 0);
			DebuffPower.SetValue((EnumSelf.PowerType)i, 0);
		}
	}
	
	public void ResetTurnPower() {
		for (int i = 0; i < (int)EnumSelf.TurnPowerType.Max; i++) {
			CuTurnPower.SetTurnPowerValue((EnumSelf.TurnPowerType)i, 0);
		}
	}
}

// こっちは、効果が永続で、効果量が可変する物。
public class Power {
	private List<int> Parameter = new List<int>();
	public Power() {
		for (int i = 0; i < (int)EnumSelf.PowerType.Max; i++) {
			Parameter.Add(0);
		}
	}

	public int GetValue(EnumSelf.PowerType type) {
		return Parameter[(int)type];
	}
	
	public void SetValue(EnumSelf.PowerType type, int val) {
		Parameter[(int)type] = val;
	}
	
	public void AddValue(EnumSelf.PowerType type, int val) {
		Parameter[(int)type] += val;
	}
}

// こっちは、効果量は固定で、効果ターンが可変する物。
public class TurnPower {
	private List<int> TurnPowerParameter = new List<int>();
	public TurnPower() {
		for (int i = 0; i < (int)EnumSelf.TurnPowerType.Max; i++) {
			TurnPowerParameter.Add(0);
		}
	}

	public int GetTurnPowerValue(EnumSelf.TurnPowerType type) {
		return TurnPowerParameter[(int)type];
	}
	
	public void SetTurnPowerValue(EnumSelf.TurnPowerType type, int val) {
		TurnPowerParameter[(int)type] = val;
	}
	
	public void AddTurnPowerValue(EnumSelf.TurnPowerType type, int val) {
		TurnPowerParameter[(int)type] += val;
		if (TurnPowerParameter[(int)type] < 0) {
			TurnPowerParameter[(int)type] = 0;
		}
	}
}

