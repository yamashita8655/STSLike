using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetFishProtocolInterface : SendRecvProtocol {
	public class SendParameter : SendParameterBase {
	};

	public class RecieveParameter : RecieveParameterBase {
		public string UniqueId { get; private set; }
		public string ItemId { get; private set; }
		public RecieveParameter(
			ResultCode_ resultCode,
			string uniqueId,
			string itemId
		) {
			ResultCode = resultCode;

			UniqueId = uniqueId;
			ItemId = itemId;
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