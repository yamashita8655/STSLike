using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendChatMessageProtocol : SendChatMessageProtocolInterface
{
	override public void Initialize(System.Object target, SendParameterBase param, Action<RecieveParameterBase> recieveCallback) {
		IsShowLoading = false;
		base.Initialize(target, param, recieveCallback);
	}

	override public void Send() {
	}
	
	override public void Recieve(BaseSerializeData recvData) {
		LogManager.Instance.Log("SendChatMessageProtocol");
		SerializeSendChatMessageData data = recvData as SerializeSendChatMessageData;

		// ここで、jsonParamをクラスに変える
		RecieveParameter param = new RecieveParameter(
				(ResultCode_)(int.Parse(data.ResultCode))
			);

		Debug.Log("TargetObject");
		Debug.Log(TargetObject);
		Debug.Log(TargetObject.ToString());

		if (TargetObject != null) {
			if (TargetObject.ToString() != "null") {
				if ((RecieveCallback != null)) {
					RecieveCallback(param);
				}
			}
		}
	}
}
