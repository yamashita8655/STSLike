using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPrefsManager : SimpleMonoBehaviourSingleton<PlayerPrefsManager> {
	public enum SaveType {
		UniqueIdIndex,
		HuntedItemList,
		Inventory,
		Money,
		Max,
		None
	};
	
	// 定義用。これプログラム中で編集しちゃダメ。Readonlyにしたいけど、リストの初期化が多分無理
	private List<string> SaveKeyList = new List<string>(){
		"UniqueIdIndex",
		"HuntedItemList",
		"Inventory",
		"Money",
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
				if (i == (int)SaveType.UniqueIdIndex) {
					saveString = "1";
				} else if (i == (int)SaveType.Money) {
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

	// ここから下は、プロジェクト依存処理
	// コピペする時は、消す事
	public void SaveMoney(int money) {
		PlayerPrefsManager.Instance.SaveParameter(SaveType.Money, money.ToString());
	}
	
	public string GetMoney() {
		return PlayerPrefsManager.Instance.GetParameter(SaveType.Money);
	}
}

