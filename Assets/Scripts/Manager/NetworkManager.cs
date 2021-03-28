using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : SimpleMonoBehaviourSingleton<NetworkManager>
{
	private NetworkSystem CuNetworkSystem = null;

	public void Initialize()
	{
		CuNetworkSystem = new NetworkSystem();
		CuNetworkSystem.Initialize();
	}
	
	public void Request(SendRecvProtocolType type, SendParameterBase param, Action<RecieveParameterBase> recieveCallback)
    {
        CuNetworkSystem.Request(null, type, param, recieveCallback);
    }

    public void Request(System.Object target, SendRecvProtocolType type, SendParameterBase param, Action<RecieveParameterBase> recieveCallback) {
        CuNetworkSystem.Request(target, type, param, recieveCallback);
	}

	public void Send(ProtocolBase protocol) {
		CuNetworkSystem.Send(protocol);
	}

	public void Notify(NotifyProtocolType type, string jsonParam) {
		CuNetworkSystem.Notify(type, jsonParam);
	}
	
	public void ActivateNotify(System.Object target, NotifyProtocolType type, Action<NotifyParameterBase> notifyCallback) {
        CuNetworkSystem.ActivateNotify(target, type, notifyCallback);
	}
	
	void Update() {
		if (CuNetworkSystem != null) {
			CuNetworkSystem.SelfUpdate();
		}
	}
}
