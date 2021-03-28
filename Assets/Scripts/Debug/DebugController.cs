using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugController : MonoBehaviour
{
	public void OnClickOpenButton()
	{
		SystemDialogManager.Instance.OpenDebug();
	}

	public void OnClickCloseButton()
	{
		SystemDialogManager.Instance.CloseDebug();
	}

	private int SpawnCharacterIndex = 10000;
	public void OnClickHomeSpawnOtherCharacterButton()
	{
		SerializeSpawnCharacterData data = new SerializeSpawnCharacterData();
		data.ResultCode = "0";
		data.UniqueId = SpawnCharacterIndex.ToString();
		data.Name = "Name" + SpawnCharacterIndex.ToString();
		data.ModelId = "Prefab/Model/Avater/Doctor";
		SpawnCharacterIndex++;
		LocalServerManager.Instance.CallSpawnCharacterNotify(data);
	}
	
	public void OnClickPlayerChatMessageButton()
	{
		SerializeNotifyChatMessageData data = new SerializeNotifyChatMessageData();
		data.ResultCode = "0";
		data.Type = "0";
		data.UniqueId = "10000";
		data.Name = "Name10000";
		data.Body = "これは、メッセージ通知テスト発言です。";
		LocalServerManager.Instance.CallNotifyChatMessageNotify(data);
	}
	
	public void OnClickSystemChatMessageButton()
	{
		SerializeNotifyChatMessageData data = new SerializeNotifyChatMessageData();
		data.ResultCode = "0";
		data.Type = "1";
		data.UniqueId = "10000";
		data.Name = "システムメッセージ";
		data.Body = "これは、システムメッセージ通知発言です。";
		LocalServerManager.Instance.CallNotifyChatMessageNotify(data);
	}
	
	public void OnClickNotifyMailButton()
	{
		SerializeNotifyMailData data = new SerializeNotifyMailData();
		data.ResultCode = "0";
		data.UnreadCount = 1;
		LocalServerManager.Instance.CallNotifyMailNotify(data);
	}
}
