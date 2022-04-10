using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPrefsManager : SimpleMonoBehaviourSingleton<PlayerPrefsManager> {
	//public enum "{
	//	"BgmVolume,
	//	BgmMute,
	//	SeVolume,
	//	SeMute,
	//	Point,
	//	FindCardIds,
	//	UnlockCardIds,
	//	UsedRegularCostPoint,
	//	RegularSettingCardIds,
	//	FindArtifactIds,
	//	UnlockArtifactIds,
	//	UsedArtifactRegularCostPoint,
	//	RegularSettingArtifactIds,

	//	// トロフィー保存用
	//	EnemyKillCountStart,
	//	EnemyKillCount101 = EnemyKillCountStart,
	//	EnemyKillCount198,
	//	EnemyKillCount199,
	//	EnemyKillCount201,
	//	EnemyKillCount202,
	//	EnemyKillCount203,
	//	EnemyKillCount204,
	//	EnemyKillCount205,
	//	EnemyKillCount206,
	//	EnemyKillCount298,
	//	EnemyKillCount299,
	//	EnemyKillCount301,
	//	EnemyKillCount302,
	//	EnemyKillCount303,
	//	EnemyKillCount304,
	//	EnemyKillCount305,
	//	EnemyKillCount306,
	//	EnemyKillCount307,
	//	EnemyKillCount308,
	//	EnemyKillCount309,
	//	EnemyKillCount310,
	//	EnemyKillCount311,
	//	EnemyKillCount312,
	//	EnemyKillCount313,
	//	EnemyKillCount314,
	//	EnemyKillCount398,
	//	EnemyKillCount399,
	//	EnemyKillCount9999,
	//	EnemyKillCount9998,
	//	EnemyKillCount9997,
	//	EnemyKillCount9996,
	//	EnemyKillCount9995,
	//	EnemyKillCount90000,
	//	EnemyKillCount99999,
	//	EnemyKillCountEnd = EnemyKillCount99999,// 処理では使わないけど、Max的な意味合いで使う

	//	Max,
	//	None
	//};
	
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
		
		// トロフィーアンロック
		"TrophyUnlock1",
		"TrophyUnlock2",
		"TrophyUnlock3",
		"TrophyUnlock4",
		"TrophyUnlock5",
		"TrophyUnlock6",
		"TrophyUnlock7",
		"TrophyUnlock8",
		"TrophyUnlock9",
		"TrophyUnlock10",
		"TrophyUnlock11",
		"TrophyUnlock12",
		"TrophyUnlock13",
		"TrophyUnlock14",
		"TrophyUnlock15",
		"TrophyUnlock16",
		"TrophyUnlock17",
		"TrophyUnlock18",
		"TrophyUnlock19",
		"TrophyUnlock20",
		"TrophyUnlock21",
		"TrophyUnlock22",
		"TrophyUnlock23",
		"TrophyUnlock24",
		"TrophyUnlock25",
		"TrophyUnlock26",
		"TrophyUnlock27",
		"TrophyUnlock28",
		"TrophyUnlock29",
		"TrophyUnlock30",
		"TrophyUnlock31",
		"TrophyUnlock32",
		"TrophyUnlock33",
		"TrophyUnlock34",
		"TrophyUnlock35",
		"TrophyUnlock36",
		"TrophyUnlock37",
		"TrophyUnlock38",
		"TrophyUnlock39",
		"TrophyUnlock40",
		"TrophyUnlock41",
		"TrophyUnlock42",
		"TrophyUnlock43",
		"TrophyUnlock44",
		"TrophyUnlock45",
		"TrophyUnlock46",
		"TrophyUnlock47",
		"TrophyUnlock48",
		"TrophyUnlock49",
		"TrophyUnlock50",
		"TrophyUnlock51",
		"TrophyUnlock52",
		"TrophyUnlock53",
		"TrophyUnlock54",
		"TrophyUnlock55",
		"TrophyUnlock56",
		"TrophyUnlock57",
		"TrophyUnlock58",
		"TrophyUnlock59",
		"TrophyUnlock60",
		"TrophyUnlock61",
		"TrophyUnlock62",
		"TrophyUnlock63",
		"TrophyUnlock64",
		"TrophyUnlock65",
		"TrophyUnlock66",
		"TrophyUnlock67",
		"TrophyUnlock68",
		"TrophyUnlock69",
		"TrophyUnlock70",
		"TrophyUnlock71",
		"TrophyUnlock72",
		"TrophyUnlock73",
		"TrophyUnlock74",
		"TrophyUnlock75",
		"TrophyUnlock76",
		"TrophyUnlock77",
		"TrophyUnlock78",
		"TrophyUnlock79",
		"TrophyUnlock80",
		"TrophyUnlock81",
		"TrophyUnlock82",
		"TrophyUnlock83",
		"TrophyUnlock84",
		"TrophyUnlock85",
		"TrophyUnlock86",
		"TrophyUnlock87",
		"TrophyUnlock88",
		"TrophyUnlock89",
		"TrophyUnlock90",
		"TrophyUnlock91",
		"TrophyUnlock92",
		"TrophyUnlock93",
		"TrophyUnlock94",
		"TrophyUnlock95",
		"TrophyUnlock96",
		"TrophyUnlock97",
		"TrophyUnlock98",
		"TrophyUnlock99",
		"TrophyUnlock100",
		"TrophyUnlock101",
		"TrophyUnlock102",
		"TrophyUnlock103",
		"TrophyUnlock104",
		"TrophyUnlock105",
		"TrophyUnlock106",
		"TrophyUnlock107",
		"TrophyUnlock108",
		"TrophyUnlock109",
		"TrophyUnlock110",
		"TrophyUnlock111",
		"TrophyUnlock112",
		"TrophyUnlock113",
		"TrophyUnlock114",
		"TrophyUnlock115",
		"TrophyUnlock116",
		"TrophyUnlock117",
		"TrophyUnlock118",
		"TrophyUnlock119",
		"TrophyUnlock120",
		"TrophyUnlock121",
		"TrophyUnlock122",
		"TrophyUnlock123",
		"TrophyUnlock124",
		"TrophyUnlock125",
		"TrophyUnlock126",
		"TrophyUnlock127",
	};

	private List<int> FindCardIds = new List<int>();
	private List<int> UnlockCardIds = new List<int>();
	private List<int> RegularSettingCardIds = new List<int>();
	
	private List<int> FindArtifactIds = new List<int>();
	private List<int> UnlockArtifactIds = new List<int>();
	private List<int> RegularSettingArtifactIds = new List<int>();
	
	private Dictionary<string, int> EnemyKillCountDict = new Dictionary<string, int>();
	private Dictionary<string, int> TrophyUnlockedDict = new Dictionary<string, int>();
	
	public void Initialize() {
		CreateFirstData();
		InitializeCardStatusList();
		InitializeArtifactStatusList();
		InitializeEnemyKillCountDict();
		InitializeTrophyUnlockedDict();
	}

	// 初回のセーブデータ作成
	private void CreateFirstData() {
		for (int i = 0; i < SaveKeyList.Count; i++) {
			string key = SaveKeyList[i];
			bool res = PlayerPrefs.HasKey(key);
			if (res == false) {
				string saveString = "";
				if (key == "BgmVolume") {
					saveString = "50";
				} else if (key == "BgmMute") {
					saveString = "False";
				} else if (key == "SeVolume") {
					saveString = "50";
				} else if (key == "SeMute") {
					saveString = "False";
				} else if (key == "Point") {
					saveString = "0";
				} else if (key == "FindCardIds") {
					// 初期装備カードは、見つけた状態にしておく
					saveString = "1-2-101";
				} else if (key == "UnlockCardIds") {
					// 初期装備カードは、アンロック状態にしておく
					saveString = "1-2-101";
				} else if (key == "UsedRegularCostPoint") {
					saveString = "0";
				} else if (key == "RegularSettingCardIds") {
					//
					saveString = "2";
				} else if (key == "FindArtifactIds") {
					saveString = "";
				} else if (key == "UnlockArtifactIds") {
					saveString = "";
				} else if (key == "UsedArtifactRegularCostPoint") {
					saveString = "0";
				} else if (key == "RegularSettingArtifactIds") {
					saveString = "";
				} else if (
					(key == "EnemyKillCount101") ||
					(key == "EnemyKillCount198") ||
					(key == "EnemyKillCount199") ||
					(key == "EnemyKillCount201") ||
					(key == "EnemyKillCount202") ||
					(key == "EnemyKillCount203") ||
					(key == "EnemyKillCount204") ||
					(key == "EnemyKillCount205") ||
					(key == "EnemyKillCount206") ||
					(key == "EnemyKillCount298") ||
					(key == "EnemyKillCount299") ||
					(key == "EnemyKillCount301") ||
					(key == "EnemyKillCount302") ||
					(key == "EnemyKillCount303") ||
					(key == "EnemyKillCount304") ||
					(key == "EnemyKillCount305") ||
					(key == "EnemyKillCount306") ||
					(key == "EnemyKillCount307") ||
					(key == "EnemyKillCount308") ||
					(key == "EnemyKillCount309") ||
					(key == "EnemyKillCount310") ||
					(key == "EnemyKillCount311") ||
					(key == "EnemyKillCount312") ||
					(key == "EnemyKillCount313") ||
					(key == "EnemyKillCount314") ||
					(key == "EnemyKillCount398") ||
					(key == "EnemyKillCount399") ||
					(key == "EnemyKillCount9999") ||
					(key == "EnemyKillCount9998") ||
					(key == "EnemyKillCount9997") ||
					(key == "EnemyKillCount9996") ||
					(key == "EnemyKillCount9995") ||
					(key == "EnemyKillCount90000") ||
					(key == "EnemyKillCount99999")
				) {
					saveString = "0";
				} else if (
					(key == "TrophyUnlock1") ||
					(key == "TrophyUnlock2") ||
					(key == "TrophyUnlock3") ||
					(key == "TrophyUnlock4") ||
					(key == "TrophyUnlock5") ||
					(key == "TrophyUnlock6") ||
					(key == "TrophyUnlock7") ||
					(key == "TrophyUnlock8") ||
					(key == "TrophyUnlock9") ||
					(key == "TrophyUnlock10") ||
					(key == "TrophyUnlock11") ||
					(key == "TrophyUnlock12") ||
					(key == "TrophyUnlock13") ||
					(key == "TrophyUnlock14") ||
					(key == "TrophyUnlock15") ||
					(key == "TrophyUnlock16") ||
					(key == "TrophyUnlock17") ||
					(key == "TrophyUnlock18") ||
					(key == "TrophyUnlock19") ||
					(key == "TrophyUnlock20") ||
					(key == "TrophyUnlock21") ||
					(key == "TrophyUnlock22") ||
					(key == "TrophyUnlock23") ||
					(key == "TrophyUnlock24") ||
					(key == "TrophyUnlock25") ||
					(key == "TrophyUnlock26") ||
					(key == "TrophyUnlock27") ||
					(key == "TrophyUnlock28") ||
					(key == "TrophyUnlock29") ||
					(key == "TrophyUnlock30") ||
					(key == "TrophyUnlock31") ||
					(key == "TrophyUnlock32") ||
					(key == "TrophyUnlock33") ||
					(key == "TrophyUnlock34") ||
					(key == "TrophyUnlock35") ||
					(key == "TrophyUnlock36") ||
					(key == "TrophyUnlock37") ||
					(key == "TrophyUnlock38") ||
					(key == "TrophyUnlock39") ||
					(key == "TrophyUnlock40") ||
					(key == "TrophyUnlock41") ||
					(key == "TrophyUnlock42") ||
					(key == "TrophyUnlock43") ||
					(key == "TrophyUnlock44") ||
					(key == "TrophyUnlock45") ||
					(key == "TrophyUnlock46") ||
					(key == "TrophyUnlock47") ||
					(key == "TrophyUnlock48") ||
					(key == "TrophyUnlock49") ||
					(key == "TrophyUnlock50") ||
					(key == "TrophyUnlock51") ||
					(key == "TrophyUnlock52") ||
					(key == "TrophyUnlock53") ||
					(key == "TrophyUnlock54") ||
					(key == "TrophyUnlock55") ||
					(key == "TrophyUnlock56") ||
					(key == "TrophyUnlock57") ||
					(key == "TrophyUnlock58") ||
					(key == "TrophyUnlock59") ||
					(key == "TrophyUnlock60") ||
					(key == "TrophyUnlock61") ||
					(key == "TrophyUnlock62") ||
					(key == "TrophyUnlock63") ||
					(key == "TrophyUnlock64") ||
					(key == "TrophyUnlock65") ||
					(key == "TrophyUnlock66") ||
					(key == "TrophyUnlock67") ||
					(key == "TrophyUnlock68") ||
					(key == "TrophyUnlock69") ||
					(key == "TrophyUnlock70") ||
					(key == "TrophyUnlock71") ||
					(key == "TrophyUnlock72") ||
					(key == "TrophyUnlock73") ||
					(key == "TrophyUnlock74") ||
					(key == "TrophyUnlock75") ||
					(key == "TrophyUnlock76") ||
					(key == "TrophyUnlock77") ||
					(key == "TrophyUnlock78") ||
					(key == "TrophyUnlock79") ||
					(key == "TrophyUnlock80") ||
					(key == "TrophyUnlock81") ||
					(key == "TrophyUnlock82") ||
					(key == "TrophyUnlock83") ||
					(key == "TrophyUnlock84") ||
					(key == "TrophyUnlock85") ||
					(key == "TrophyUnlock86") ||
					(key == "TrophyUnlock87") ||
					(key == "TrophyUnlock88") ||
					(key == "TrophyUnlock89") ||
					(key == "TrophyUnlock90") ||
					(key == "TrophyUnlock91") ||
					(key == "TrophyUnlock92") ||
					(key == "TrophyUnlock93") ||
					(key == "TrophyUnlock94") ||
					(key == "TrophyUnlock95") ||
					(key == "TrophyUnlock96") ||
					(key == "TrophyUnlock97") ||
					(key == "TrophyUnlock98") ||
					(key == "TrophyUnlock99") ||
					(key == "TrophyUnlock100") ||
					(key == "TrophyUnlock101") ||
					(key == "TrophyUnlock102") ||
					(key == "TrophyUnlock103") ||
					(key == "TrophyUnlock104") ||
					(key == "TrophyUnlock105") ||
					(key == "TrophyUnlock106") ||
					(key == "TrophyUnlock107") ||
					(key == "TrophyUnlock108") ||
					(key == "TrophyUnlock109") ||
					(key == "TrophyUnlock110") ||
					(key == "TrophyUnlock111") ||
					(key == "TrophyUnlock112") ||
					(key == "TrophyUnlock113") ||
					(key == "TrophyUnlock114") ||
					(key == "TrophyUnlock115") ||
					(key == "TrophyUnlock116") ||
					(key == "TrophyUnlock117") ||
					(key == "TrophyUnlock118") ||
					(key == "TrophyUnlock119") ||
					(key == "TrophyUnlock120") ||
					(key == "TrophyUnlock121") ||
					(key == "TrophyUnlock122") ||
					(key == "TrophyUnlock123") ||
					(key == "TrophyUnlock124") ||
					(key == "TrophyUnlock125") ||
					(key == "TrophyUnlock126") ||
					(key == "TrophyUnlock127")
				) {
					saveString = "0";
				}
				PlayerPrefs.SetString(key, saveString);
			}
		}
	}
	
	//public void SaveParameter("type", string parameter) {
	//	string key = SaveKeyList[(int)type];
	//	PlayerPrefs.SetString(key, parameter);
	//	PlayerPrefs.Save();
	//}
	
	public void SaveParameter(string key, string parameter) {
		PlayerPrefs.SetString(key, parameter);
		PlayerPrefs.Save();
	}
	
	public string GetParameter(string key) {
		string flag = PlayerPrefs.GetString(key);
		return flag;
	}

	public int GetBgmVolume()
	{
		string saveString = GetParameter("BgmVolume");
		int volume = int.Parse(saveString);
		return volume;
	}

	public bool GetBgmIsMute()
	{
		string saveString = GetParameter("BgmMute");
		bool isMute = bool.Parse(saveString);
		return isMute;
	}
	
	public int GetSeVolume()
	{
		string saveString = GetParameter("SeVolume");
		int volume = int.Parse(saveString);
		return volume;
	}

	public bool GetSeIsMute()
	{
		string saveString = GetParameter("SeMute");
		bool isMute = bool.Parse(saveString);
		return isMute;
	}
	
	public int GetPoint()
	{
		string saveString = GetParameter("Point");
		int point = int.Parse(saveString);
		return point;
	}
	
	public void AddPoint(int addValue)
	{
		string saveString = GetParameter("Point");
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
		SaveParameter("Point", saveString);
	}
	
	public void InitializeCardStatusList() {
		string saveString = GetParameter("FindCardIds");
		if (string.IsNullOrEmpty(saveString) == false) {
			char[] split = {'-'};
			List<string> lineList = Functions.SplitString(saveString, split);

			for (int i = 0; i < lineList.Count; i++) {
				Debug.Log(lineList[i]);
				FindCardIds.Add(int.Parse(lineList[i]));
			}
		}
		
		saveString = GetParameter("UnlockCardIds");
		if (string.IsNullOrEmpty(saveString) == false) {
			char[] split = {'-'};
			List<string> lineList = Functions.SplitString(saveString, split);

			for (int i = 0; i < lineList.Count; i++) {
				UnlockCardIds.Add(int.Parse(lineList[i]));
			}
		}
		
		saveString = GetParameter("RegularSettingCardIds");
		if (string.IsNullOrEmpty(saveString) == false) {
			char[] split = {'-'};
			List<string> lineList = Functions.SplitString(saveString, split);

			for (int i = 0; i < lineList.Count; i++) {
				RegularSettingCardIds.Add(int.Parse(lineList[i]));
			}
		}
	}
	
	public void SaveFindCardId(int id) {
		string saveString = GetParameter("FindCardIds");

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
		
		SaveParameter("FindCardIds", saveString);
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
		string saveString = GetParameter("UnlockCardIds");

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
		
		SaveParameter("UnlockCardIds", saveString);
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
		string saveString = GetParameter("UsedRegularCostPoint");
		int point = int.Parse(saveString);
		return point;
	}
	
	public void AddUsedRegularCostPoint(int addValue)
	{
		string saveString = GetParameter("UsedRegularCostPoint");
		int val = int.Parse(saveString) + addValue;
		SaveUsedRegularCostPoint(val);
	}
	
	public void SaveUsedRegularCostPoint(int val)
	{
		string saveString = val.ToString();
		SaveParameter("UsedRegularCostPoint", saveString);
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
		SaveParameter("RegularSettingCardIds", saveString);
	}
	
	public void InitializeArtifactStatusList() {
		string saveString = GetParameter("FindArtifactIds");
		if (string.IsNullOrEmpty(saveString) == false) {
			char[] split = {'-'};
			List<string> lineList = Functions.SplitString(saveString, split);

			for (int i = 0; i < lineList.Count; i++) {
				Debug.Log(lineList[i]);
				FindArtifactIds.Add(int.Parse(lineList[i]));
			}
		}
		
		saveString = GetParameter("UnlockArtifactIds");
		if (string.IsNullOrEmpty(saveString) == false) {
			char[] split = {'-'};
			List<string> lineList = Functions.SplitString(saveString, split);

			for (int i = 0; i < lineList.Count; i++) {
				UnlockArtifactIds.Add(int.Parse(lineList[i]));
			}
		}
		
		saveString = GetParameter("RegularSettingArtifactIds");
		if (string.IsNullOrEmpty(saveString) == false) {
			char[] split = {'-'};
			List<string> lineList = Functions.SplitString(saveString, split);

			for (int i = 0; i < lineList.Count; i++) {
				RegularSettingArtifactIds.Add(int.Parse(lineList[i]));
			}
		}
	}
	
	public void InitializeEnemyKillCountDict() {
		string saveString = "";
		saveString = GetParameter("EnemyKillCount101");
		EnemyKillCountDict.Add("EnemyKillCount101", int.Parse(saveString));
		saveString = GetParameter("EnemyKillCount198");
		EnemyKillCountDict.Add("EnemyKillCount198", int.Parse(saveString));
		saveString = GetParameter("EnemyKillCount199");
		EnemyKillCountDict.Add("EnemyKillCount199", int.Parse(saveString));
		saveString = GetParameter("EnemyKillCount201");
		EnemyKillCountDict.Add("EnemyKillCount201", int.Parse(saveString));
		saveString = GetParameter("EnemyKillCount202");
		EnemyKillCountDict.Add("EnemyKillCount202", int.Parse(saveString));
		saveString = GetParameter("EnemyKillCount203");
		EnemyKillCountDict.Add("EnemyKillCount203", int.Parse(saveString));
		saveString = GetParameter("EnemyKillCount204");
		EnemyKillCountDict.Add("EnemyKillCount204", int.Parse(saveString));
		saveString = GetParameter("EnemyKillCount205");
		EnemyKillCountDict.Add("EnemyKillCount205", int.Parse(saveString));
		saveString = GetParameter("EnemyKillCount206");
		EnemyKillCountDict.Add("EnemyKillCount206", int.Parse(saveString));
		saveString = GetParameter("EnemyKillCount298");
		EnemyKillCountDict.Add("EnemyKillCount298", int.Parse(saveString));
		saveString = GetParameter("EnemyKillCount299");
		EnemyKillCountDict.Add("EnemyKillCount299", int.Parse(saveString));
		saveString = GetParameter("EnemyKillCount301");
		EnemyKillCountDict.Add("EnemyKillCount301", int.Parse(saveString));
		saveString = GetParameter("EnemyKillCount302");
		EnemyKillCountDict.Add("EnemyKillCount302", int.Parse(saveString));
		saveString = GetParameter("EnemyKillCount303");
		EnemyKillCountDict.Add("EnemyKillCount303", int.Parse(saveString));
		saveString = GetParameter("EnemyKillCount304");
		EnemyKillCountDict.Add("EnemyKillCount304", int.Parse(saveString));
		saveString = GetParameter("EnemyKillCount305");
		EnemyKillCountDict.Add("EnemyKillCount305", int.Parse(saveString));
		saveString = GetParameter("EnemyKillCount306");
		EnemyKillCountDict.Add("EnemyKillCount306", int.Parse(saveString));
		saveString = GetParameter("EnemyKillCount307");
		EnemyKillCountDict.Add("EnemyKillCount307", int.Parse(saveString));
		saveString = GetParameter("EnemyKillCount308");
		EnemyKillCountDict.Add("EnemyKillCount308", int.Parse(saveString));
		saveString = GetParameter("EnemyKillCount309");
		EnemyKillCountDict.Add("EnemyKillCount309", int.Parse(saveString));
		saveString = GetParameter("EnemyKillCount310");
		EnemyKillCountDict.Add("EnemyKillCount310", int.Parse(saveString));
		saveString = GetParameter("EnemyKillCount311");
		EnemyKillCountDict.Add("EnemyKillCount311", int.Parse(saveString));
		saveString = GetParameter("EnemyKillCount312");
		EnemyKillCountDict.Add("EnemyKillCount312", int.Parse(saveString));
		saveString = GetParameter("EnemyKillCount313");
		EnemyKillCountDict.Add("EnemyKillCount313", int.Parse(saveString));
		saveString = GetParameter("EnemyKillCount314");
		EnemyKillCountDict.Add("EnemyKillCount314", int.Parse(saveString));
		saveString = GetParameter("EnemyKillCount398");
		EnemyKillCountDict.Add("EnemyKillCount398", int.Parse(saveString));
		saveString = GetParameter("EnemyKillCount399");
		EnemyKillCountDict.Add("EnemyKillCount399", int.Parse(saveString));
		saveString = GetParameter("EnemyKillCount9999");
		EnemyKillCountDict.Add("EnemyKillCount9999", int.Parse(saveString));
		saveString = GetParameter("EnemyKillCount9998");
		EnemyKillCountDict.Add("EnemyKillCount9998", int.Parse(saveString));
		saveString = GetParameter("EnemyKillCount9997");
		EnemyKillCountDict.Add("EnemyKillCount9997", int.Parse(saveString));
		saveString = GetParameter("EnemyKillCount9996");
		EnemyKillCountDict.Add("EnemyKillCount9996", int.Parse(saveString));
		saveString = GetParameter("EnemyKillCount9995");
		EnemyKillCountDict.Add("EnemyKillCount9995" ,int.Parse(saveString));
		saveString = GetParameter("EnemyKillCount90000");
		EnemyKillCountDict.Add("EnemyKillCount90000", int.Parse(saveString));
		saveString = GetParameter("EnemyKillCount99999");
		EnemyKillCountDict.Add("EnemyKillCount99999", int.Parse(saveString));
	}
	
	public void InitializeTrophyUnlockedDict() {
		string saveString = "";
		saveString = GetParameter("TrophyUnlock1");
		TrophyUnlockedDict.Add("TrophyUnlock1", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock2");
		TrophyUnlockedDict.Add("TrophyUnlock2", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock3");
		TrophyUnlockedDict.Add("TrophyUnlock3", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock4");
		TrophyUnlockedDict.Add("TrophyUnlock4", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock5");
		TrophyUnlockedDict.Add("TrophyUnlock5", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock6");
		TrophyUnlockedDict.Add("TrophyUnlock6", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock7");
		TrophyUnlockedDict.Add("TrophyUnlock7", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock8");
		TrophyUnlockedDict.Add("TrophyUnlock8", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock9");
		TrophyUnlockedDict.Add("TrophyUnlock9", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlockedDict0");
		TrophyUnlockedDict.Add("TrophyUnlockedDict0", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlockedDict1");
		TrophyUnlockedDict.Add("TrophyUnlockedDict1", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlockedDict2");
		TrophyUnlockedDict.Add("TrophyUnlockedDict2", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlockedDict3");
		TrophyUnlockedDict.Add("TrophyUnlockedDict3", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlockedDict4");
		TrophyUnlockedDict.Add("TrophyUnlockedDict4", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlockedDict5");
		TrophyUnlockedDict.Add("TrophyUnlockedDict5", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlockedDict6");
		TrophyUnlockedDict.Add("TrophyUnlockedDict6", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlockedDict7");
		TrophyUnlockedDict.Add("TrophyUnlockedDict7", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlockedDict8");
		TrophyUnlockedDict.Add("TrophyUnlockedDict8", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlockedDict9");
		TrophyUnlockedDict.Add("TrophyUnlockedDict9", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock20");
		TrophyUnlockedDict.Add("TrophyUnlock20", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock21");
		TrophyUnlockedDict.Add("TrophyUnlock21", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock22");
		TrophyUnlockedDict.Add("TrophyUnlock22", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock23");
		TrophyUnlockedDict.Add("TrophyUnlock23", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock24");
		TrophyUnlockedDict.Add("TrophyUnlock24", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock25");
		TrophyUnlockedDict.Add("TrophyUnlock25", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock26");
		TrophyUnlockedDict.Add("TrophyUnlock26", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock27");
		TrophyUnlockedDict.Add("TrophyUnlock27", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock28");
		TrophyUnlockedDict.Add("TrophyUnlock28", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock29");
		TrophyUnlockedDict.Add("TrophyUnlock29", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock30");
		TrophyUnlockedDict.Add("TrophyUnlock30", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock31");
		TrophyUnlockedDict.Add("TrophyUnlock31", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock32");
		TrophyUnlockedDict.Add("TrophyUnlock32", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock33");
		TrophyUnlockedDict.Add("TrophyUnlock33", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock34");
		TrophyUnlockedDict.Add("TrophyUnlock34", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock35");
		TrophyUnlockedDict.Add("TrophyUnlock35", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock36");
		TrophyUnlockedDict.Add("TrophyUnlock36", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock37");
		TrophyUnlockedDict.Add("TrophyUnlock37", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock38");
		TrophyUnlockedDict.Add("TrophyUnlock38", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock39");
		TrophyUnlockedDict.Add("TrophyUnlock39", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock40");
		TrophyUnlockedDict.Add("TrophyUnlock40", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock41");
		TrophyUnlockedDict.Add("TrophyUnlock41", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock42");
		TrophyUnlockedDict.Add("TrophyUnlock42", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock43");
		TrophyUnlockedDict.Add("TrophyUnlock43", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock44");
		TrophyUnlockedDict.Add("TrophyUnlock44", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock45");
		TrophyUnlockedDict.Add("TrophyUnlock45", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock46");
		TrophyUnlockedDict.Add("TrophyUnlock46", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock47");
		TrophyUnlockedDict.Add("TrophyUnlock47", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock48");
		TrophyUnlockedDict.Add("TrophyUnlock48", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock49");
		TrophyUnlockedDict.Add("TrophyUnlock49", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock50");
		TrophyUnlockedDict.Add("TrophyUnlock50", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock51");
		TrophyUnlockedDict.Add("TrophyUnlock51", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock52");
		TrophyUnlockedDict.Add("TrophyUnlock52", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock53");
		TrophyUnlockedDict.Add("TrophyUnlock53", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock54");
		TrophyUnlockedDict.Add("TrophyUnlock54", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock55");
		TrophyUnlockedDict.Add("TrophyUnlock55", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock56");
		TrophyUnlockedDict.Add("TrophyUnlock56", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock57");
		TrophyUnlockedDict.Add("TrophyUnlock57", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock58");
		TrophyUnlockedDict.Add("TrophyUnlock58", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock59");
		TrophyUnlockedDict.Add("TrophyUnlock59", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock60");
		TrophyUnlockedDict.Add("TrophyUnlock60", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock61");
		TrophyUnlockedDict.Add("TrophyUnlock61", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock62");
		TrophyUnlockedDict.Add("TrophyUnlock62", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock63");
		TrophyUnlockedDict.Add("TrophyUnlock63", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock64");
		TrophyUnlockedDict.Add("TrophyUnlock64", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock65");
		TrophyUnlockedDict.Add("TrophyUnlock65", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock66");
		TrophyUnlockedDict.Add("TrophyUnlock66", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock67");
		TrophyUnlockedDict.Add("TrophyUnlock67", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock68");
		TrophyUnlockedDict.Add("TrophyUnlock68", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock69");
		TrophyUnlockedDict.Add("TrophyUnlock69", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock70");
		TrophyUnlockedDict.Add("TrophyUnlock70", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock71");
		TrophyUnlockedDict.Add("TrophyUnlock71", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock72");
		TrophyUnlockedDict.Add("TrophyUnlock72", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock73");
		TrophyUnlockedDict.Add("TrophyUnlock73", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock74");
		TrophyUnlockedDict.Add("TrophyUnlock74", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock75");
		TrophyUnlockedDict.Add("TrophyUnlock75", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock76");
		TrophyUnlockedDict.Add("TrophyUnlock76", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock77");
		TrophyUnlockedDict.Add("TrophyUnlock77", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock78");
		TrophyUnlockedDict.Add("TrophyUnlock78", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock79");
		TrophyUnlockedDict.Add("TrophyUnlock79", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock80");
		TrophyUnlockedDict.Add("TrophyUnlock80", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock81");
		TrophyUnlockedDict.Add("TrophyUnlock81", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock82");
		TrophyUnlockedDict.Add("TrophyUnlock82", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock83");
		TrophyUnlockedDict.Add("TrophyUnlock83", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock84");
		TrophyUnlockedDict.Add("TrophyUnlock84", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock85");
		TrophyUnlockedDict.Add("TrophyUnlock85", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock86");
		TrophyUnlockedDict.Add("TrophyUnlock86", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock87");
		TrophyUnlockedDict.Add("TrophyUnlock87", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock88");
		TrophyUnlockedDict.Add("TrophyUnlock88", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock89");
		TrophyUnlockedDict.Add("TrophyUnlock89", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock90");
		TrophyUnlockedDict.Add("TrophyUnlock90", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock91");
		TrophyUnlockedDict.Add("TrophyUnlock91", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock92");
		TrophyUnlockedDict.Add("TrophyUnlock92", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock93");
		TrophyUnlockedDict.Add("TrophyUnlock93", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock94");
		TrophyUnlockedDict.Add("TrophyUnlock94", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock95");
		TrophyUnlockedDict.Add("TrophyUnlock95", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock96");
		TrophyUnlockedDict.Add("TrophyUnlock96", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock97");
		TrophyUnlockedDict.Add("TrophyUnlock97", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock98");
		TrophyUnlockedDict.Add("TrophyUnlock98", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlock99");
		TrophyUnlockedDict.Add("TrophyUnlock99", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlockedDict00");
		TrophyUnlockedDict.Add("TrophyUnlockedDict00", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlockedDict01");
		TrophyUnlockedDict.Add("TrophyUnlockedDict01", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlockedDict02");
		TrophyUnlockedDict.Add("TrophyUnlockedDict02", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlockedDict03");
		TrophyUnlockedDict.Add("TrophyUnlockedDict03", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlockedDict04");
		TrophyUnlockedDict.Add("TrophyUnlockedDict04", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlockedDict05");
		TrophyUnlockedDict.Add("TrophyUnlockedDict05", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlockedDict06");
		TrophyUnlockedDict.Add("TrophyUnlockedDict06", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlockedDict07");
		TrophyUnlockedDict.Add("TrophyUnlockedDict07", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlockedDict08");
		TrophyUnlockedDict.Add("TrophyUnlockedDict08", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlockedDict09");
		TrophyUnlockedDict.Add("TrophyUnlockedDict09", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlockedDict10");
		TrophyUnlockedDict.Add("TrophyUnlockedDict10", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlockedDict11");
		TrophyUnlockedDict.Add("TrophyUnlockedDict11", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlockedDict12");
		TrophyUnlockedDict.Add("TrophyUnlockedDict12", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlockedDict13");
		TrophyUnlockedDict.Add("TrophyUnlockedDict13", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlockedDict14");
		TrophyUnlockedDict.Add("TrophyUnlockedDict14", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlockedDict15");
		TrophyUnlockedDict.Add("TrophyUnlockedDict15", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlockedDict16");
		TrophyUnlockedDict.Add("TrophyUnlockedDict16", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlockedDict17");
		TrophyUnlockedDict.Add("TrophyUnlockedDict17", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlockedDict18");
		TrophyUnlockedDict.Add("TrophyUnlockedDict18", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlockedDict19");
		TrophyUnlockedDict.Add("TrophyUnlockedDict19", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlockedDict20");
		TrophyUnlockedDict.Add("TrophyUnlockedDict20", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlockedDict21");
		TrophyUnlockedDict.Add("TrophyUnlockedDict21", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlockedDict22");
		TrophyUnlockedDict.Add("TrophyUnlockedDict22", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlockedDict23");
		TrophyUnlockedDict.Add("TrophyUnlockedDict23", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlockedDict24");
		TrophyUnlockedDict.Add("TrophyUnlockedDict24", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlockedDict25");
		TrophyUnlockedDict.Add("TrophyUnlockedDict25", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlockedDict26");
		TrophyUnlockedDict.Add("TrophyUnlockedDict26", int.Parse(saveString));
		saveString = GetParameter("TrophyUnlockedDict27");
		TrophyUnlockedDict.Add("TrophyUnlockedDict27", int.Parse(saveString));
	}
	
	public void SaveFindArtifactId(int id) {
		string saveString = GetParameter("FindArtifactIds");

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
		
		SaveParameter("FindArtifactIds", saveString);
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
		string saveString = GetParameter("UnlockArtifactIds");

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
		
		SaveParameter("UnlockArtifactIds", saveString);
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
		string saveString = GetParameter("UsedArtifactRegularCostPoint");
		int point = int.Parse(saveString);
		return point;
	}
	
	public void AddUsedArtifactRegularCostPoint(int addValue)
	{
		string saveString = GetParameter("UsedArtifactRegularCostPoint");
		int val = int.Parse(saveString) + addValue;
		SaveUsedArtifactRegularCostPoint(val);
	}
	
	public void SaveUsedArtifactRegularCostPoint(int val)
	{
		string saveString = val.ToString();
		SaveParameter("UsedArtifactRegularCostPoint", saveString);
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
		SaveParameter("RegularSettingArtifactIds", saveString);
	}
	
	public void AddEnemyKillCount(int id, int addValue) {
		string key = $"EnemyKillCount{id}";
		EnemyKillCountDict[key] += addValue;
		SaveEnemyKillCount(key, EnemyKillCountDict[key]);
	}

	public void SaveEnemyKillCount(string key, int count) {
		EnemyKillCountDict[key] = count;
		SaveParameter(key, count.ToString());
	}
	
	public int GetEnemyKillCount(int id) {
		string key = $"EnemyKillCount{id}";
		return EnemyKillCountDict[key];
	}
	
	public void SaveTrophyUnlock(int id, int unlocked) {
		string key = $"TrophyUnlock{id}";
		TrophyUnlockedDict[key] =unlocked;
		SaveParameter(key, unlocked.ToString());
	}
	
	public int GetTrophyUnlock(int id) {
		string key = $"TrophyUnlock{id}";
		return TrophyUnlockedDict[key];
	}
}
