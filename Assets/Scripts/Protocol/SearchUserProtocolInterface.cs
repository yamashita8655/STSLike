using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchUserProtocolInterface : SendRecvProtocol {
	public class SendParameter : SendParameterBase {
		public string UserName { get; private set; }
		public SendParameter(string userName) {
			UserName = userName;
		}
	};

	public class RecieveParameter : RecieveParameterBase {
		public string[] UniqueIds { get; private set; }
		public string[] Names { get; private set; }
		public string[] Images { get; private set; }
		public RecieveParameter(
			ResultCode_ resultCode,
			string[] uniqueIds,
			string[] names,
			string[] images
		) {
			ResultCode = resultCode;

			UniqueIds = uniqueIds;
			Names = names;
			Images = images;
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