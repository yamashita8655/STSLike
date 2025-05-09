﻿using System;
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
	};

	private List<int> FindCardIds = new List<int>();
	private List<int> UnlockCardIds = new List<int>();
	private List<int> RegularSettingCardIds = new List<int>();
	
	public void Initialize() {
		CreateFirstData();
		InitializeCardStatusList();
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
					saveString = "1-2-20";
				} else if (i == (int)SaveType.UnlockCardIds) {
					// 初期装備カードは、アンロック状態にしておく
					saveString = "1-2-20";
				} else if (i == (int)SaveType.UsedRegularCostPoint) {
					saveString = "0";
				} else if (i == (int)SaveType.RegularSettingCardIds) {
					//
					saveString = "1-1-1-2-20-20";
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

			// 必ず6個設定されている状態にするようにする
			// 現状は、RegularSettingCardIdsの初期値で必ず6個設定されている
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
	
	public void SaveUnclookCardId(int id) {
		string saveString = GetParameter(SaveType.UnlockCardIds);

		if (UnlockCardIds.Count == 0) {
			saveString = id.ToString();
		} else {
			if (UnlockCardIds.Contains(id) == true) {
				LogManager.Instance.LogWarning("PlayerPrefsManager:SaveUnclookCardId already unlock:" + id);
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
	
	public int GetRegularSettingCardId(int index)
	{
		return RegularSettingCardIds[index];
	}
	
	public void SaveRegularSettingCardId(int val, int index)
	{
		RegularSettingCardIds[index] = val;
		string saveString = string.Format(
				"{0}-{1}-{2}-{3}-{4}-{5}",
				RegularSettingCardIds[0].ToString(),
				RegularSettingCardIds[1].ToString(),
				RegularSettingCardIds[2].ToString(),
				RegularSettingCardIds[3].ToString(),
				RegularSettingCardIds[4].ToString(),
				RegularSettingCardIds[5].ToString()
			);
		SaveParameter(SaveType.RegularSettingCardIds, saveString);
	}
}
