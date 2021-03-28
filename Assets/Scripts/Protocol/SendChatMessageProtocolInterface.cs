using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendChatMessageProtocolInterface : SendRecvProtocol {
	public class SendParameter : SendParameterBase {
		public string UniqueId { get; private set; }
		public string Body { get; private set; }
		public SendParameter(string uniqueId, string body) {
			UniqueId = uniqueId;
			Body = body;
		}
	};

	public class RecieveParameter : RecieveParameterBase {
		public RecieveParameter(
			ResultCode_ resultCode
		) {
			ResultCode = resultCode;

		}
	};

	protected Action<RecieveParameterBase> RecieveCallback = null;

	protected SendParameterBase _SendParameter = null;

	override public void Initialize(System.Object target, SendParameterBase param, Action<RecieveParameterBase> recieveCallback) {
		_SendParameter = param;
		RecieveCallback = recieveCallback;
		TargetObject = target;
	}

	override public void Send() {
	}

	override public void Recieve(BaseSerializeData recvData) {
	}
}