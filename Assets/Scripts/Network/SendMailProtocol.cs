using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendMailProtocol : SendMailProtocolInterface
{
	override public void Initialize(System.Object target, SendParameterBase param, Action<RecieveParameterBase> recieveCallback) {
		IsShowLoading = true;
		base.Initialize(target, param, recieveCallback);
	}

	override public void Recieve(BaseSerializeData recvData) {
		LogManager.Instance.Log("SendMailProtocol");
		SerializeSendMailData data = recvData as SerializeSendMailData;

		// ここで、jsonParamをクラスに変える
		RecieveParameter param = new RecieveParameter(
				(ResultCode_)(int.Parse(data.ResultCode))
			);
		if (RecieveCallback != null) {
			RecieveCallback(param);
		}
	}
}
