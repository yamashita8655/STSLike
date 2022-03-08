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
	private List<MasterAction2Table.Data> BackupActionDataList;
	private List<MasterAction2Table.Data> InitiativeActionDataList;
	private List<bool> ParameterFlagList;
	private int MaxDiceCount;
	
	private Power BuffPower = null;
	private Power DebuffPower = null;

	private TurnPower CuTurnPower = null;
	
	private bool UseAttack = false;
	private int UseShieldCount = 0;

	public PlayerStatus() {
		ActionDataList = new List<MasterAction2Table.Data>(){
			null,null,null,null,null,null
		};
		BackupActionDataList = new List<MasterAction2Table.Data>(){
			null,null,null,null,null,null
		};
		InitiativeActionDataList = new List<MasterAction2Table.Data>();

		ParameterFlagList = new List<bool>();
		for (int i = 0; i < (int)EnumSelf.ParameterType.Max; i++) {
			ParameterFlagList.Add(false);
		}
		
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
		BackupActionDataList[index] = data;
	}
	public void SetCurseActionData(int index, MasterAction2Table.Data data) {
		ActionDataList[index] = data;
	}
	public void ResetActionData(int index) {
		ActionDataList[index] = BackupActionDataList[index];
	}
	public MasterAction2Table.Data GetActionData(int index) {
		return ActionDataList[index];
	}
	public List<MasterAction2Table.Data> GetActionDataCloseList() {
		List<MasterAction2Table.Data> list = new List<MasterAction2Table.Data>(ActionDataList);
		return list;
	}
	public List<MasterAction2Table.Data> GetBackUpActionDataCloseList() {
		List<MasterAction2Table.Data> list = new List<MasterAction2Table.Data>(BackupActionDataList);
		return list;
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
	
	public void ResetPower(EnumSelf.PowerType type) {
		BuffPower.SetValue(type, 0);
		DebuffPower.SetValue(type, 0);
	}
	
	public void ResetPowerAll() {
		for (int i = 0; i < (int)EnumSelf.PowerType.Max; i++) {
			BuffPower.SetValue((EnumSelf.PowerType)i, 0);
			DebuffPower.SetValue((EnumSelf.PowerType)i, 0);
		}
	}
	
	public void SetTurnPowerValue(EnumSelf.TurnPowerType type, int val) {
		CuTurnPower.SetTurnPowerValue((EnumSelf.TurnPowerType)type, val);
	}
	
	public void ResetTurnPower() {
		for (int i = 0; i < (int)EnumSelf.TurnPowerType.Max; i++) {
			CuTurnPower.SetTurnPowerValue((EnumSelf.TurnPowerType)i, 0);
		}
	}
	
	public void SetParameterListFlag(EnumSelf.ParameterType type, bool flag) {
		ParameterFlagList[(int)type] = flag;
	}
	
	public bool GetParameterListFlag(EnumSelf.ParameterType type) {
		return ParameterFlagList[(int)type];
	}

	public void SetUseAttack(bool isUse) => UseAttack = isUse;
	public bool GetUseAttack() => UseAttack;

	public void AddUseShieldCount(int addValue) => UseShieldCount += addValue;
	public void SetUseShieldCount(int val) => UseShieldCount = val;
	public int GetUseShieldCount() => UseShieldCount;
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
	
	private int CurrentAITurnCountIndex = 0;

	private MasterAction2Table.Data DecideActionData = null;

	private Power BuffPower = null;
	private Power DebuffPower = null;

	private TurnPower CuTurnPower = null;
	
	private MasterEnemyAITable.Data AIData = null;

	public EnemyStatus(MasterEnemyTable.Data data) {
		ActionDataList = new List<MasterAction2Table.Data>();
		InitiativeActionDataList = new List<MasterAction2Table.Data>();
		BuffPower = new Power();
		DebuffPower = new Power();
		Data = data;
		CuTurnPower = new TurnPower();
	}

	public MasterEnemyTable.Data GetEnemyData() {
		return Data;
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
	public void ClearActionData() {
		ActionDataList.Clear();
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
		if (AIData.EnemyActionType == EnumSelf.EnemyActionType.Random) {
			int all = ActionDataList.Count;
			int index = UnityEngine.Random.Range(0, all);
			data = ActionDataList[index];
		} else if (AIData.EnemyActionType == EnumSelf.EnemyActionType.Rotation) {
			data = ActionDataList[CurrentActionIndex];
			CurrentActionIndex++;
			if (CurrentActionIndex >= ActionDataList.Count) {
				CurrentActionIndex = 0;
			} 
		}

		CurrentAITurnCountIndex++;

		DecideActionData = data;
	}

	public void CheckAIForTurnProgress() {
		if (AIData.AIChangeType == EnumSelf.AIChangeType.TurnProgress) {
			if (CurrentAITurnCountIndex >= AIData.AIChangeValue) {
				AIData = MasterEnemyAITable.Instance.GetData(AIData.AfterAIId);
				UpdateAIData(AIData);
				// ターンプログレスは、敵のターン終了、つまり、行動再抽選前に行うので
				// ここでは再抽選しない
				//status.LotActionData();
			}
		}
	}
	
	public void CheckAIForHpBorder() {
		if (AIData.AIChangeType == EnumSelf.AIChangeType.HpBorder) {
			if (NowHp <= AIData.AIChangeValue) {
				AIData = MasterEnemyAITable.Instance.GetData(AIData.AfterAIId);
				UpdateAIData(AIData);
				LotActionData();
			}
		}
	}
	
	public void CheckAIForLotHpBorder() {
		if (AIData.AIChangeType == EnumSelf.AIChangeType.LotHpBorder) {
			if (NowHp <= AIData.AIChangeValue) {
				AIData = MasterEnemyAITable.Instance.GetData(AIData.AfterAIId);
				UpdateAIData(AIData);
				LotActionData();
			}
		}
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
	
	public void ResetPower(EnumSelf.PowerType type) {
		BuffPower.SetValue(type, 0);
		DebuffPower.SetValue(type, 0);
	}

	public void ResetPowerAll() {
		for (int i = 0; i < (int)EnumSelf.PowerType.Max; i++) {
			BuffPower.SetValue((EnumSelf.PowerType)i, 0);
			DebuffPower.SetValue((EnumSelf.PowerType)i, 0);
		}
	}
	
	public void SetTurnPowerValue(EnumSelf.TurnPowerType type, int val) {
		CuTurnPower.SetTurnPowerValue((EnumSelf.TurnPowerType)type, val);
	}
	
	public void ResetTurnPower() {
		for (int i = 0; i < (int)EnumSelf.TurnPowerType.Max; i++) {
			CuTurnPower.SetTurnPowerValue((EnumSelf.TurnPowerType)i, 0);
		}
	}

	//// こっちは、初期化時に呼び出す関数
	//public void InitializeAIData(MasterEnemyAITable.Data data) {
	//	AIData = data;
	//	for (int i = 0; i < AIData.ActionIds.Count; i++) {
	//		MasterAction2Table.Data actionData = MasterAction2Table.Instance.GetData(AIData.ActionIds[i]);
	//		AddActionData(actionData);
	//	}
	//}

	// こっちは、バトル中にAIが切り替わる時に呼び出す関数
	public void UpdateAIData(MasterEnemyAITable.Data data) {
		CurrentActionIndex = 0;
		CurrentAITurnCountIndex = 0;
		ClearActionData();
		AIData = data;
		for (int i = 0; i < AIData.ActionIds.Count; i++) {
			MasterAction2Table.Data actionData = MasterAction2Table.Instance.GetData(AIData.ActionIds[i]);
			AddActionData(actionData);
		}
	}
	
	public MasterEnemyAITable.Data GetAIData() {
		return AIData;
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
		Debug.Log(type);
		TurnPowerParameter[(int)type] += val;
		if (TurnPowerParameter[(int)type] < 0) {
			TurnPowerParameter[(int)type] = 0;
		}
	}
}

