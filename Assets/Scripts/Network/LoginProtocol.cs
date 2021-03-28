using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginProtocol : LoginProtocolInterface
{
	override public void Initialize(System.Object target, SendParameterBase param, Action<RecieveParameterBase> recieveCallback) {
		IsShowLoading = true;
		base.Initialize(target, param, recieveCallback);
	}

	override public void Recieve(BaseSerializeData recvData) {
		LogManager.Instance.Log("LoginProtocol");
		SerializeLoginData data = recvData as SerializeLoginData;

		// ここで、jsonParamをクラスに変える
		RecieveParameter param = new RecieveParameter(
				(ResultCode_)(int.Parse(data.ResultCode)),
				data.UniqueId,
				data.Name,
				data.ModelId
			);
		if (RecieveCallback != null) {
			RecieveCallback(param);
		}
	}
}
