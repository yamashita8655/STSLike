using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignUpProtocol : SignUpProtocolInterface
{
	override public void Initialize(System.Object target, SendParameterBase param, Action<RecieveParameterBase> recieveCallback) {
		IsShowLoading = true;
        base.Initialize(target, param, recieveCallback);
    }

	override public void Recieve(BaseSerializeData recvData) {
		LogManager.Instance.Log("SignUpProtocol");
		
		SerializeSignUpData data = recvData as SerializeSignUpData;
		// ここで、jsonParamをクラスに変える
		RecieveParameter param = new RecieveParameter(
				(ResultCode_)(int.Parse(data.ResultCode)),
                data.UserId,
                data.UniqueId,
				data.Name,
				data.ModelId
			);

		if (RecieveCallback != null) {
			RecieveCallback(param);
		}
	}
}
