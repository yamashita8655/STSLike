using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchUserProtocol : SearchUserProtocolInterface
{
	override public void Initialize(System.Object target, SendParameterBase param, Action<RecieveParameterBase> recieveCallback) {
		IsShowLoading = true;
		base.Initialize(target, param, recieveCallback);
	}

	override public void Recieve(BaseSerializeData recvData) {
		LogManager.Instance.Log("SearchUserProtocol");
		SerializeSearchUserData data = recvData as SerializeSearchUserData;

		// ここで、jsonParamをクラスに変える
		RecieveParameter param = new RecieveParameter(
				(ResultCode_)(int.Parse(data.ResultCode)),
				data.UniqueIds,
				data.Names,
				data.Images
			);
		if (RecieveCallback != null) {
			RecieveCallback(param);
		}
	}
}
