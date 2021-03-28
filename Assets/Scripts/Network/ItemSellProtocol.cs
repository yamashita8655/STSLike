using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSellProtocol : ItemSellProtocolInterface
{
	override public void Initialize(System.Object target, SendParameterBase param, Action<RecieveParameterBase> recieveCallback) {
		IsShowLoading = true;
		base.Initialize(target, param, recieveCallback);
	}

	override public void Recieve(BaseSerializeData recvData) {
		LogManager.Instance.Log("ItemSellProtocol");
		SerializeItemSellData data = recvData as SerializeItemSellData;

		// ここで、jsonParamをクラスに変える
		RecieveParameter param = new RecieveParameter(
				(ResultCode_)(int.Parse(data.ResultCode))
			);
		if (RecieveCallback != null) {
			RecieveCallback(param);
		}
	}
}
