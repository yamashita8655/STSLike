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
		Max,
		None
	};
	
	// 定義用。これプログラム中で編集しちゃダメ。Readonlyにしたいけど、リストの初期化が多分無理
	private List<string> SaveKeyList = new List<string>(){
		"BgmVolume",
		"BgmMute",
		"SeVolume",
		"SeMute",
	};
	
	public void Initialize() {
		CreateFirstData();
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
}

