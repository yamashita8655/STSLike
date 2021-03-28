using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtocolBase
{
	protected bool IsShowLoading;
	protected System.Object TargetObject;
	
	public bool GetShowLoading() {
		return IsShowLoading;
	}
}

public class SendRecvProtocol : ProtocolBase
{
	virtual public void Initialize(System.Object target, SendParameterBase param, Action<RecieveParameterBase> recieveCallback) {
	}

	virtual public void Send() {
	}
	
	virtual public void Recieve(BaseSerializeData recvData) {
	}
}

public class NotifyProtocol : ProtocolBase
{
	virtual public void Initialize(System.Object target, Action<NotifyParameterBase> notifyCallback) {
	}

	virtual public void Notify(BaseSerializeData notifyData) {
	}
}

public class SendParameterBase {
	public SendParameterBase() {
		
	}
}

public class RecieveParameterBase {
	protected ResultCode_ ResultCode;
	public RecieveParameterBase() {
		
	}

	public ResultCode_ GetResultCode()
	{
		return ResultCode;
	}
}

public class NotifyParameterBase {
	public NotifyParameterBase() {
		
	}
}
