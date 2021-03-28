using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalServerManager : SimpleSingleton<LocalServerManager>
{
	public void Initialize()
	{
	}

	public void CallSpawnCharacterNotify(SerializeSpawnCharacterData data) {
		string json = JsonUtility.ToJson(data);
		NetworkManager.Instance.Notify(NotifyProtocolType.SpawnCharacter, json);
	}
	
	public void CallNotifyChatMessageNotify(SerializeNotifyChatMessageData data) {
		string json = JsonUtility.ToJson(data);
		NetworkManager.Instance.Notify(NotifyProtocolType.NotifyChatMessage, json);
	}
	
	public void CallPlayerInventoryNotify(SerializePlayerInventoryData data) {
		string json = JsonUtility.ToJson(data);
		Debug.Log(json);
		NetworkManager.Instance.Notify(NotifyProtocolType.PlayerInventory, json);
	}
	
	public void CallPlayerMoneyNotify(SerializePlayerMoneyData data) {
		string json = JsonUtility.ToJson(data);
		NetworkManager.Instance.Notify(NotifyProtocolType.PlayerMoney, json);
	}
	
	public void CallNotifyMailNotify(SerializeNotifyMailData data) {
		string json = JsonUtility.ToJson(data);
		NetworkManager.Instance.Notify(NotifyProtocolType.NotifyMail, json);
	}
}
