using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryProtocol : PlayerInventoryProtocolInterface
{
	override public void Initialize(System.Object target, Action<NotifyParameterBase> notifyCallback) {
		IsShowLoading = true;
		base.Initialize(target, notifyCallback);
	}

	override public void Notify(BaseSerializeData notifyData) {
		LogManager.Instance.Log("PlayerInventoryProtocol");
		
		SerializePlayerInventoryData data = notifyData as SerializePlayerInventoryData;
		// ここで、jsonParamをクラスに変える
		NotifyParameter param = new NotifyParameter(
				data.UniqueIds,
				data.ItemIds
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
