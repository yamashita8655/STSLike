using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPrefsManager : SimpleMonoBehaviourSingleton<PlayerPrefsManager> {
	public enum SaveType {
		BgmVolume,
		BgmMute,
		SeVolume,
		SeMute,
		Point,
		FindCardIds,
		UnlockCardIds,
		UsedRegularCostPoint,
		RegularSettingCardIds,
		FindArtifactIds,
		UnlockArtifactIds,
		UsedArtifactRegularCostPoint,
		RegularSettingArtifactIds,

		// トロフィー保存用
		EnemyKillCountStart,
		EnemyKillCount101 = EnemyKillCountStart,
		EnemyKillCount198,
		EnemyKillCount199,
		EnemyKillCount201,
		EnemyKillCount202,
		EnemyKillCount203,
		EnemyKillCount204,
		EnemyKillCount205,
		EnemyKillCount206,
		EnemyKillCount298,
		EnemyKillCount299,
		EnemyKillCount301,
		EnemyKillCount302,
		EnemyKillCount303,
		EnemyKillCount304,
		EnemyKillCount305,
		EnemyKillCount306,
		EnemyKillCount307,
		EnemyKillCount308,
		EnemyKillCount309,
		EnemyKillCount310,
		EnemyKillCount311,
		EnemyKillCount312,
		EnemyKillCount313,
		EnemyKillCount314,
		EnemyKillCount398,
		EnemyKillCount399,
		EnemyKillCount9999,
		EnemyKillCount9998,
		EnemyKillCount9997,
		EnemyKillCount9996,
		EnemyKillCount9995,
		EnemyKillCount90000,
		EnemyKillCount99999,
		EnemyKillCountEnd = EnemyKillCount99999,// 処理では使わないけど、Max的な意味合いで使う

		Max,
		None
	};
	
	// 定義用。これプログラム中で編集しちゃダメ。Readonlyにしたいけど、リストの初期化が多分無理
	private List<string> SaveKeyList = new List<string>(){
		"BgmVolume",
		"BgmMute",
		"SeVolume",
		"SeMute",
		"Point",
		"FindCardIds",
		"UnlockCardIds",
		"UsedRegularCostPoint",
		"RegularSettingCardIds",
		"FindArtifactIds",
		"UnlockArtifactIds",
		"UsedArtifactRegularCostPoint",
		"RegularSettingArtifactIds",
		// 敵キルカウント
		"EnemyKillCount101",
		"EnemyKillCount198",
		"EnemyKillCount199",
		"EnemyKillCount201",
		"EnemyKillCount202",
		"EnemyKillCount203",
		"EnemyKillCount204",
		"EnemyKillCount205",
		"EnemyKillCount206",
		"EnemyKillCount298",
		"EnemyKillCount299",
		"EnemyKillCount301",
		"EnemyKillCount302",
		"EnemyKillCount303",
		"EnemyKillCount304",
		"EnemyKillCount305",
		"EnemyKillCount306",
		"EnemyKillCount307",
		"EnemyKillCount308",
		"EnemyKillCount309",
		"EnemyKillCount310",
		"EnemyKillCount311",
		"EnemyKillCount312",
		"EnemyKillCount313",
		"EnemyKillCount314",
		"EnemyKillCount398",
		"EnemyKillCount399",
		"EnemyKillCount9999",
		"EnemyKillCount9998",
		"EnemyKillCount9997",
		"EnemyKillCount9996",
		"EnemyKillCount9995",
		"EnemyKillCount90000",
		"EnemyKillCount99999",
	};

	private List<int> FindCardIds = new List<int>();
	private List<int> UnlockCardIds = new List<int>();
	private List<int> RegularSettingCardIds = new List<int>();
	
	private List<int> FindArtifactIds = new List<int>();
	private List<int> UnlockArtifactIds = new List<int>();
	private List<int> RegularSettingArtifactIds = new List<int>();
	
	private List<int> EnemyKillCountList = new List<int>();
	
	public void Initialize() {
		CreateFirstData();
		InitializeCardStatusList();
		InitializeArtifactStatusList();
		InitializeEnemyKillCountList();
	}

	// 初回のセーブデータ作成
	private void CreateFirstData() {
		for (int i = 0; i < SaveKeyList.Count; i++) {
			string key = SaveKeyList[i];
			bool res = PlayerPrefs.HasKey(key);
			if (res == false) {
				string saveString = "";
				if (i == (int)SaveType.BgmVolume) {
					saveString = "50";
				} else if (i == (int)SaveType.BgmMute) {
					saveString = "False";
				} else if (i == (int)SaveType.SeVolume) {
					saveString = "50";
				} else if (i == (int)SaveType.SeMute) {
					saveString = "False";
				} else if (i == (int)SaveType.Point) {
					saveString = "0";
				} else if (i == (int)SaveType.FindCardIds) {
					// 初期装備カードは、見つけた状態にしておく
					saveString = "1-2-101";
				} else if (i == (int)SaveType.UnlockCardIds) {
					// 初期装備カードは、アンロック状態にしておく
					saveString = "1-2-101";
				} else if (i == (int)SaveType.UsedRegularCostPoint) {
					saveString = "0";
				} else if (i == (int)SaveType.RegularSettingCardIds) {
					//
					saveString = "2";
				} else if (i == (int)SaveType.FindArtifactIds) {
					saveString = "";
				} else if (i == (int)SaveType.UnlockArtifactIds) {
					saveString = "";
				} else if (i == (int)SaveType.UsedArtifactRegularCostPoint) {
					saveString = "0";
				} else if (i == (int)SaveType.RegularSettingArtifactIds) {
					saveString = "";
				} else if (
					((int)SaveType.EnemyKillCountStart <= i) && (i <= (int)SaveType.EnemyKillCountEnd)
				) {
					saveString = "0";
				}
				PlayerPrefs.SetString(key, saveString);
			}
		}
	}
	
	public void SaveParameter(SaveType type, string parameter) {
		string key = SaveKeyList[(int)type];
		PlayerPrefs.SetString(key, parameter);
		PlayerPrefs.Save();
	}
	
	public string GetParameter(SaveType type) {
		string key = SaveKeyList[(int)type];
		string flag = PlayerPrefs.GetString(key);

		return flag;
	}

	public int GetBgmVolume()
	{
		string saveString = GetParameter(SaveType.BgmVolume);
		int volume = int.Parse(saveString);
		return volume;
	}

	public bool GetBgmIsMute()
	{
		string saveString = GetParameter(SaveType.BgmMute);
		bool isMute = bool.Parse(saveString);
		return isMute;
	}
	
	public int GetSeVolume()
	{
		string saveString = GetParameter(SaveType.SeVolume);
		int volume = int.Parse(saveString);
		return volume;
	}

	public bool GetSeIsMute()
	{
		string saveString = GetParameter(SaveType.SeMute);
		bool isMute = bool.Parse(saveString);
		return isMute;
	}
	
	public int GetPoint()
	{
		string saveString = GetParameter(SaveType.Point);
		int point = int.Parse(saveString);
		return point;
	}
	
	public void AddPoint(int addValue)
	{
		string saveString = GetParameter(SaveType.Point);
		int val = int.Parse(saveString) + addValue;
		if (val >= Const.MaxPoint) {
			val = Const.MaxPoint;
		} else if (val <= 0) {
			val = 0;
		}
		SavePoint(val);
	}
	
	public void SavePoint(int val)
	{
		string saveString = val.ToString();
		SaveParameter(SaveType.Point, saveString);
	}
	
	public void InitializeCardStatusList() {
		string saveString = GetParameter(SaveType.FindCardIds);
		if (string.IsNullOrEmpty(saveString) == false) {
			char[] split = {'-'};
			List<string> lineList = Functions.SplitString(saveString, split);

			for (int i = 0; i < lineList.Count; i++) {
				Debug.Log(lineList[i]);
				FindCardIds.Add(int.Parse(lineList[i]));
			}
		}
		
		saveString = GetParameter(SaveType.UnlockCardIds);
		if (string.IsNullOrEmpty(saveString) == false) {
			char[] split = {'-'};
			List<string> lineList = Functions.SplitString(saveString, split);

			for (int i = 0; i < lineList.Count; i++) {
				UnlockCardIds.Add(int.Parse(lineList[i]));
			}
		}
		
		saveString = GetParameter(SaveType.RegularSettingCardIds);
		if (string.IsNullOrEmpty(saveString) == false) {
			char[] split = {'-'};
			List<string> lineList = Functions.SplitString(saveString, split);

			for (int i = 0; i < lineList.Count; i++) {
				RegularSettingCardIds.Add(int.Parse(lineList[i]));
			}
		}
	}
	
	public void SaveFindCardId(int id) {
		string saveString = GetParameter(SaveType.FindCardIds);

		if (FindCardIds.Count == 0) {
			saveString = id.ToString();
		} else {
			if (FindCardIds.Contains(id) == true) {
				LogManager.Instance.LogWarning("PlayerPrefsManager:SaveFindCardId already find:" + id);
				return;
			} else {
				saveString += "-" + id.ToString();
			}
		}

		FindCardIds.Add(id);
		
		SaveParameter(SaveType.FindCardIds, saveString);
	}
	
	public bool IsFindCard(int id) {
		bool res = false;
		for (int i = 0; i < FindCardIds.Count; i++) {
			if (FindCardIds[i] == id) {
				res = true;
				break;
			}
		}

		return res;
	}
	
	public void SaveUnlookCardId(int id) {
		string saveString = GetParameter(SaveType.UnlockCardIds);

		if (UnlockCardIds.Count == 0) {
			saveString = id.ToString();
		} else {
			if (UnlockCardIds.Contains(id) == true) {
				LogManager.Instance.LogWarning("PlayerPrefsManager:SaveUnlookCardId already unlock:" + id);
				return;
			} else {
				saveString += "-" + id.ToString();
			}
		}
		
		UnlockCardIds.Add(id);
		
		SaveParameter(SaveType.UnlockCardIds, saveString);
	}
	
	public bool IsUnlockCard(int id) {
		bool res = false;
		for (int i = 0; i < UnlockCardIds.Count; i++) {
			if (UnlockCardIds[i] == id) {
				res = true;
				break;
			}
		}

		return res;
	}
	
	public int GetUsedRegularCostPoint()
	{
		string saveString = GetParameter(SaveType.UsedRegularCostPoint);
		int point = int.Parse(saveString);
		return point;
	}
	
	public void AddUsedRegularCostPoint(int addValue)
	{
		string saveString = GetParameter(SaveType.UsedRegularCostPoint);
		int val = int.Parse(saveString) + addValue;
		SaveUsedRegularCostPoint(val);
	}
	
	public void SaveUsedRegularCostPoint(int val)
	{
		string saveString = val.ToString();
		SaveParameter(SaveType.UsedRegularCostPoint, saveString);
	}
	
	public List<int> GetRegularSettingCardIds()
	{
		return RegularSettingCardIds;
	}

	public void AddRegularSettingCardId(int id) {
		RegularSettingCardIds.Add(id);
		SaveRegularSettingCardId();
	}
	
	public void RemoveRegularSettingCardId(int id) {
		RegularSettingCardIds.Remove(id);
		SaveRegularSettingCardId();
	}
	
	public void SaveRegularSettingCardId()
	{
		string saveString = "";
		for (int i = 0; i < RegularSettingCardIds.Count; i++) {
			if (i == 0) {
				saveString += RegularSettingCardIds[i].ToString();
			} else {
				saveString += "-" + RegularSettingCardIds[i].ToString();
			}
		}
		SaveParameter(SaveType.RegularSettingCardIds, saveString);
	}
	
	public void InitializeArtifactStatusList() {
		string saveString = GetParameter(SaveType.FindArtifactIds);
		if (string.IsNullOrEmpty(saveString) == false) {
			char[] split = {'-'};
			List<string> lineList = Functions.SplitString(saveString, split);

			for (int i = 0; i < lineList.Count; i++) {
				Debug.Log(lineList[i]);
				FindArtifactIds.Add(int.Parse(lineList[i]));
			}
		}
		
		saveString = GetParameter(SaveType.UnlockArtifactIds);
		if (string.IsNullOrEmpty(saveString) == false) {
			char[] split = {'-'};
			List<string> lineList = Functions.SplitString(saveString, split);

			for (int i = 0; i < lineList.Count; i++) {
				UnlockArtifactIds.Add(int.Parse(lineList[i]));
			}
		}
		
		saveString = GetParameter(SaveType.RegularSettingArtifactIds);
		if (string.IsNullOrEmpty(saveString) == false) {
			char[] split = {'-'};
			List<string> lineList = Functions.SplitString(saveString, split);

			for (int i = 0; i < lineList.Count; i++) {
				RegularSettingArtifactIds.Add(int.Parse(lineList[i]));
			}
		}
	}
	
	public void InitializeEnemyKillCountList() {
		int startIndex = (int)SaveType.EnemyKillCountStart;
		int endIndex = (int)SaveType.EnemyKillCountEnd+1;
		for (int i = startIndex; i < endIndex; i++) {
			string saveString = GetParameter((SaveType)i);
			EnemyKillCountList.Add(int.Parse(saveString));
		}
	}
	
	public void SaveFindArtifactId(int id) {
		string saveString = GetParameter(SaveType.FindArtifactIds);

		if (FindArtifactIds.Count == 0) {
			saveString = id.ToString();
		} else {
			if (FindArtifactIds.Contains(id) == true) {
				LogManager.Instance.LogWarning("PlayerPrefsManager:SaveFindArtifactId already find:" + id);
				return;
			} else {
				saveString += "-" + id.ToString();
			}
		}

		FindArtifactIds.Add(id);
		
		SaveParameter(SaveType.FindArtifactIds, saveString);
	}
	
	public bool IsFindArtifact(int id) {
		bool res = false;
		for (int i = 0; i < FindArtifactIds.Count; i++) {
			if (FindArtifactIds[i] == id) {
				res = true;
				break;
			}
		}

		return res;
	}
	
	public void SaveUnlookArtifactId(int id) {
		string saveString = GetParameter(SaveType.UnlockArtifactIds);

		if (UnlockArtifactIds.Count == 0) {
			saveString = id.ToString();
		} else {
			if (UnlockArtifactIds.Contains(id) == true) {
				LogManager.Instance.LogWarning("PlayerPrefsManager:SaveUnlookArtifactId already unlock:" + id);
				return;
			} else {
				saveString += "-" + id.ToString();
			}
		}
		
		UnlockArtifactIds.Add(id);
		
		SaveParameter(SaveType.UnlockArtifactIds, saveString);
	}
	
	public bool IsUnlockArtifact(int id) {
		bool res = false;
		for (int i = 0; i < UnlockArtifactIds.Count; i++) {
			if (UnlockArtifactIds[i] == id) {
				res = true;
				break;
			}
		}

		return res;
	}
	
	public int GetUsedArtifactRegularCostPoint()
	{
		string saveString = GetParameter(SaveType.UsedArtifactRegularCostPoint);
		int point = int.Parse(saveString);
		return point;
	}
	
	public void AddUsedArtifactRegularCostPoint(int addValue)
	{
		string saveString = GetParameter(SaveType.UsedArtifactRegularCostPoint);
		int val = int.Parse(saveString) + addValue;
		SaveUsedArtifactRegularCostPoint(val);
	}
	
	public void SaveUsedArtifactRegularCostPoint(int val)
	{
		string saveString = val.ToString();
		SaveParameter(SaveType.UsedArtifactRegularCostPoint, saveString);
	}
	
	public List<int> GetRegularSettingArtifactIds()
	{
		return RegularSettingArtifactIds;
	}

	public void AddRegularSettingArtifactId(int id) {
		RegularSettingArtifactIds.Add(id);
		SaveRegularSettingArtifactId();
	}
	
	public void RemoveRegularSettingArtifactId(int id) {
		RegularSettingArtifactIds.Remove(id);
		SaveRegularSettingArtifactId();
	}
	
	public void SaveRegularSettingArtifactId()
	{
		string saveString = "";
		for (int i = 0; i < RegularSettingArtifactIds.Count; i++) {
			if (i == 0) {
				saveString += RegularSettingArtifactIds[i].ToString();
			} else {
				saveString += "-" + RegularSettingArtifactIds[i].ToString();
			}
		}
		SaveParameter(SaveType.RegularSettingArtifactIds, saveString);
	}
	
	public void AddEnemyKillCount(int id, int addValue) {
		int index = MasterEnemyTable.Instance.GetIndexFromId(id);
		EnemyKillCountList[index] += addValue;
		SaveEnemyKillCount(index, EnemyKillCountList[index]);
	}

	public void SaveEnemyKillCount(int typeIndex, int count) {
		EnemyKillCountList[typeIndex] = count;
		SaveParameter((SaveType)(typeIndex + (int)SaveType.EnemyKillCountStart), count.ToString());
	}
	
	public int GetEnemyKillCount(int id) {
		int index = MasterEnemyTable.Instance.GetIndexFromId(id);
		return EnemyKillCountList[index];
	}
}
