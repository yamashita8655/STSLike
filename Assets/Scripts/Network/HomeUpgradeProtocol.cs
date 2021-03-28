using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeUpgradeProtocol : HomeUpgradeProtocolInterface
{
	override public void Initialize(System.Object target, SendParameterBase param, Action<RecieveParameterBase> recieveCallback) {
		IsShowLoading = true;
		base.Initialize(target, param, recieveCallback);
	}

	override public void Recieve(BaseSerializeData recvData) {
		LogManager.Instance.Log("HomeUpgradeProtocol");
		SerializeHomeUpgradeData data = recvData as SerializeHomeUpgradeData;

		// ここで、jsonParamをクラスに変える
		RecieveParameter param = new RecieveParameter(
				(ResultCode_)(int.Parse(data.ResultCode))
			);
		if (RecieveCallback != null) {
			RecieveCallback(param);
		}
	}
}
