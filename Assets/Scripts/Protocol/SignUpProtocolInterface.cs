using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignUpProtocolInterface : SendRecvProtocol {
	public class SendParameter : SendParameterBase {
		public string LoginId { get; private set; }
		public string Password { get; private set; }
		public SendParameter(string loginId, string password) {
			LoginId = loginId;
			Password = password;
		}
	};

	public class RecieveParameter : RecieveParameterBase {
		public string UserId { get; private set; }
		public string UniqueId { get; private set; }
		public string Name { get; private set; }
		public string ModelId { get; private set; }
		public RecieveParameter(
			ResultCode_ resultCode,
			string userId,
			string uniqueId,
			string name,
			string modelId
		) {
			ResultCode = resultCode;

			UserId = userId;
			UniqueId = uniqueId;
			Name = name;
			ModelId = modelId;
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