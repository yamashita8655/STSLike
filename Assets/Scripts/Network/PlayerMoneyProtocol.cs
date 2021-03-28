using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoneyProtocol : PlayerMoneyProtocolInterface
{
	override public void Initialize(System.Object target, Action<NotifyParameterBase> notifyCallback) {
		IsShowLoading = false;
		base.Initialize(target, notifyCallback);
	}

	override public void Notify(BaseSerializeData notifyData) {
		LogManager.Instance.Log("PlayerMoneyProtocol");
		
		SerializePlayerMoneyData data = notifyData as SerializePlayerMoneyData;
		// ここで、jsonParamをクラスに変える
		NotifyParameter param = new NotifyParameter(
				data.Money
			);

		if (TargetObject != null) {
			if (TargetObject.ToString() != "null") {
				if (NotifyCallback != null) {
					NotifyCallback(param);
				}
			}
		}
	}
}
