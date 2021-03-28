using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotifyMailProtocol : NotifyMailProtocolInterface
{
	override public void Initialize(System.Object target, Action<NotifyParameterBase> notifyCallback) {
		IsShowLoading = false;
		base.Initialize(target, notifyCallback);
	}

	override public void Notify(BaseSerializeData notifyData) {
		LogManager.Instance.Log("NotifyMailProtocol");
		
		SerializeNotifyMailData data = notifyData as SerializeNotifyMailData;
		// ここで、jsonParamをクラスに変える
		NotifyParameter param = new NotifyParameter(
				data.UnreadCount
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
