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
	private bool IsPowerUpdateFlag = false;

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
			DebuffPower.AddParameter(type, val);
		} else {
			BuffPower.AddParameter(type, val);
		}
		IsPowerUpdateFlag = true;
	}
	
	public Power GetPower() {
		Power retPower = new Power();
		for (int i = 0; i < (int)EnumSelf.PowerType.Max; i++) {
			int val = BuffPower.GetParameter((EnumSelf.PowerType)i) + DebuffPower.GetParameter((EnumSelf.PowerType)i);
			retPower.SetParameter((EnumSelf.PowerType)i, val);
		}

		return retPower;
	}
	
	public void AddTurnPower(EnumSelf.TurnPowerType type, int val) {
		CuTurnPower.AddTurnPowerCount(type, val);
		IsPowerUpdateFlag = true;
	}
	
	public TurnPower GetTurnPower() {
		return CuTurnPower;
	}
	
	public int GetTurnPowerCount(EnumSelf.TurnPowerType type) {
		return CuTurnPower.GetTurnPowerCount(type);
	}

	public bool GetAndResetUpdatePowerFlag() {
		bool isUpdate = IsPowerUpdateFlag;
		if (IsPowerUpdateFlag == true) {
			IsPowerUpdateFlag = false;
		}
		return isUpdate;
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
	private bool IsPowerUpdateFlag = false;

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
			DebuffPower.AddParameter(type, val);
		} else {
			BuffPower.AddParameter(type, val);
		}

		IsPowerUpdateFlag = true;
	}

	public Power GetPower() {
		Power retPower = new Power();
		for (int i = 0; i < (int)EnumSelf.PowerType.Max; i++) {
			int val = BuffPower.GetParameter((EnumSelf.PowerType)i) + DebuffPower.GetParameter((EnumSelf.PowerType)i);
			retPower.SetParameter((EnumSelf.PowerType)i, val);
		}

		return retPower;
	}
	
	public void AddTurnPower(EnumSelf.TurnPowerType type, int val) {
		CuTurnPower.AddTurnPowerCount(type, val);
		IsPowerUpdateFlag = true;
	}
	
	public TurnPower GetTurnPower() {
		return CuTurnPower;
	}
	
	public int GetTurnPowerCount(EnumSelf.TurnPowerType type) {
		return CuTurnPower.GetTurnPowerCount(type);
	}
	
	public bool GetAndResetUpdatePowerFlag() {
		bool isUpdate = IsPowerUpdateFlag;
		if (IsPowerUpdateFlag == true) {
			IsPowerUpdateFlag = false;
		}
		return isUpdate;
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

	public int GetParameter(EnumSelf.PowerType type) {
		return Parameter[(int)type];
	}
	
	public void SetParameter(EnumSelf.PowerType type, int val) {
		Parameter[(int)type] = val;
	}
	
	public void AddParameter(EnumSelf.PowerType type, int val) {
		Parameter[(int)type] += val;
	}
}

// こっちは、効果量は固定で、効果ターンが可変する物。
public class TurnPower {
	private List<int> TurnPowerCount = new List<int>();
	public TurnPower() {
		for (int i = 0; i < (int)EnumSelf.TurnPowerType.Max; i++) {
			TurnPowerCount.Add(0);
		}
	}

	public int GetTurnPowerCount(EnumSelf.TurnPowerType type) {
		return TurnPowerCount[(int)type];
	}
	
	public void SetTurnPowerCount(EnumSelf.TurnPowerType type, int val) {
		TurnPowerCount[(int)type] = val;
	}
	
	public void AddTurnPowerCount(EnumSelf.TurnPowerType type, int val) {
		TurnPowerCount[(int)type] += val;
		if (TurnPowerCount[(int)type] < 0) {
			TurnPowerCount[(int)type] = 0;
		}
	}
}
