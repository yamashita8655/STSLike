using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkSystemInterface {

    protected List<ProtocolBase> RequestQueue = new List<ProtocolBase>();

    protected List<ProtocolBase> SendRecvProtocolList = new List<ProtocolBase>();

    protected List<ProtocolBase> NotifyProtocolList = new List<ProtocolBase>();

    protected int RecieveCount = 0;

	public NetworkSystemInterface() {
		
	}

	virtual public void Initialize()
	{
		// ProtocolTypeと同じ並びにする必要がある
		SendRecvProtocolList.Add(new LoginProtocol());
		SendRecvProtocolList.Add(new SignUpProtocol());
		SendRecvProtocolList.Add(new SendChatMessageProtocol());
		SendRecvProtocolList.Add(new GetFishProtocol());
		SendRecvProtocolList.Add(new ItemSellProtocol());
		SendRecvProtocolList.Add(new ChangeMapProtocol());
		SendRecvProtocolList.Add(new HomeUpgradeProtocol());
		SendRecvProtocolList.Add(new SendMailProtocol());
		SendRecvProtocolList.Add(new GetMailProtocol());
		SendRecvProtocolList.Add(new SendMailReadProtocol());
		SendRecvProtocolList.Add(new GetPurchaseListProtocol());
		SendRecvProtocolList.Add(new SearchUserProtocol());
		
		// NotifyProtocolTypeと同じ並びにする必要がある
		NotifyProtocolList.Add(new SpawnCharacterProtocol());
		NotifyProtocolList.Add(new NotifyChatMessageProtocol());
		NotifyProtocolList.Add(new PlayerInventoryProtocol());
		NotifyProtocolList.Add(new PlayerMoneyProtocol());
		NotifyProtocolList.Add(new NotifyMailProtocol());
	}

	public void SelfUpdate() {
		while (RequestQueue.Count > 0) {
			ProtocolBase protocol = RequestQueue[0];
			RequestQueue.RemoveAt(0);
			Send(protocol);
		}
	}
    
	public void Request(SendRecvProtocolType type, SendParameterBase param, Action<RecieveParameterBase> recieveCallback)
    {
        Request(null, type, param, recieveCallback);
    }

    public void Request(System.Object target, SendRecvProtocolType type, SendParameterBase param, Action<RecieveParameterBase> recieveCallback) {
		SendRecvProtocol protocol = SendRecvProtocolList[(int)type] as SendRecvProtocol;
        protocol.Initialize(target, param, recieveCallback);
		RequestQueue.Add(protocol);
	}
	
	public void ActivateNotify(System.Object target, NotifyProtocolType type, Action<NotifyParameterBase> notifyCallback) {
		NotifyProtocol protocol = NotifyProtocolList[(int)type] as NotifyProtocol;
        protocol.Initialize(target, notifyCallback);
	}
	
	virtual public void Send(ProtocolBase protocol) {
		//protocol.Send();
	}
	
	virtual public void Recieve(SendRecvProtocolType type, string jsonParam) {
		ProtocolBase protocol = SendRecvProtocolList[(int)type];
		if (type == SendRecvProtocolType.Login) {
			SerializeLoginData data = JsonUtility.FromJson<SerializeLoginData>(jsonParam);
			(protocol as LoginProtocol).Recieve(data);
		} else if (type == SendRecvProtocolType.SignUp) {
			SerializeSignUpData data = JsonUtility.FromJson<SerializeSignUpData>(jsonParam);
			(protocol as SignUpProtocol).Recieve(data);
		} else if (type == SendRecvProtocolType.SendChatMessage) {
			SerializeSendChatMessageData data = JsonUtility.FromJson<SerializeSendChatMessageData>(jsonParam);
			(protocol as SendChatMessageProtocol).Recieve(data);
		} else if (type == SendRecvProtocolType.GetFish) {
			SerializeGetFishData data = JsonUtility.FromJson<SerializeGetFishData>(jsonParam);
			(protocol as GetFishProtocol).Recieve(data);
		} else if (type == SendRecvProtocolType.ItemSell) {
			SerializeItemSellData data = JsonUtility.FromJson<SerializeItemSellData>(jsonParam);
			(protocol as ItemSellProtocol).Recieve(data);
		} else if (type == SendRecvProtocolType.ChangeMap) {
			SerializeChangeMapData data = JsonUtility.FromJson<SerializeChangeMapData>(jsonParam);
			(protocol as ChangeMapProtocol).Recieve(data);
		} else if (type == SendRecvProtocolType.HomeUpgrade) {
			SerializeHomeUpgradeData data = JsonUtility.FromJson<SerializeHomeUpgradeData>(jsonParam);
			(protocol as HomeUpgradeProtocol).Recieve(data);
		} else if (type == SendRecvProtocolType.SendMail) {
			SerializeSendMailData data = JsonUtility.FromJson<SerializeSendMailData>(jsonParam);
			(protocol as SendMailProtocol).Recieve(data);
		} else if (type == SendRecvProtocolType.GetMail) {
			SerializeGetMailData data = JsonUtility.FromJson<SerializeGetMailData>(jsonParam);
			(protocol as GetMailProtocol).Recieve(data);
		} else if (type == SendRecvProtocolType.SendMailRead) {
			SerializeSendMailReadData data = JsonUtility.FromJson<SerializeSendMailReadData>(jsonParam);
			(protocol as SendMailReadProtocol).Recieve(data);
		} else if (type == SendRecvProtocolType.GetPurchaseList) {
			SerializeGetPurchaseListData data = JsonUtility.FromJson<SerializeGetPurchaseListData>(jsonParam);
			(protocol as GetPurchaseListProtocol).Recieve(data);
		} else if (type == SendRecvProtocolType.SearchUser) {
			SerializeSearchUserData data = JsonUtility.FromJson<SerializeSearchUserData>(jsonParam);
			(protocol as SearchUserProtocol).Recieve(data);
		}
	}

	virtual public void Notify(NotifyProtocolType type, string jsonParam) {
		ProtocolBase protocol = NotifyProtocolList[(int)type];
		if (type == NotifyProtocolType.SpawnCharacter) {
			SerializeSpawnCharacterData data = JsonUtility.FromJson<SerializeSpawnCharacterData>(jsonParam);
			(protocol as SpawnCharacterProtocol).Notify(data);
		} else if (type == NotifyProtocolType.NotifyChatMessage) {
			SerializeNotifyChatMessageData data = JsonUtility.FromJson<SerializeNotifyChatMessageData>(jsonParam);
			(protocol as NotifyChatMessageProtocol).Notify(data);
		} else if (type == NotifyProtocolType.PlayerInventory) {
			SerializePlayerInventoryData data = JsonUtility.FromJson<SerializePlayerInventoryData>(jsonParam);
			(protocol as PlayerInventoryProtocol).Notify(data);
		} else if (type == NotifyProtocolType.PlayerMoney) {
			SerializePlayerMoneyData data = JsonUtility.FromJson<SerializePlayerMoneyData>(jsonParam);
			(protocol as PlayerMoneyProtocol).Notify(data);
		} else if (type == NotifyProtocolType.NotifyMail) {
			SerializeNotifyMailData data = JsonUtility.FromJson<SerializeNotifyMailData>(jsonParam);
			(protocol as NotifyMailProtocol).Notify(data);
		}
	}
}
