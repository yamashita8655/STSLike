using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotifyChatMessageProtocol : NotifyChatMessageProtocolInterface
{
	override public void Initialize(System.Object target, Action<NotifyParameterBase> notifyCallback) {
		IsShowLoading = false;
		base.Initialize(target, notifyCallback);
	}

	override public void Notify(BaseSerializeData notifyData) {
		LogManager.Instance.Log("NotifyChatMessageProtocol");
		
		SerializeNotifyChatMessageData data = notifyData as SerializeNotifyChatMessageData;
		// ここで、jsonParamをクラスに変える
		NotifyParameter param = new NotifyParameter(
				data.Type,
				data.UniqueId,
				data.Name,
				data.Body
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
