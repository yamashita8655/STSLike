using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotifyMailProtocolInterface : NotifyProtocol {
	public class NotifyParameter : NotifyParameterBase {
		public int UnreadCount { get; private set; }
		public NotifyParameter(int unreadCount) {
			UnreadCount = unreadCount;
		}
	};

	protected Action<NotifyParameterBase> NotifyCallback = null;

	override public void Initialize(System.Object target, Action<NotifyParameterBase> notifyCallback) {
		NotifyCallback = notifyCallback;
		TargetObject = target;
	}

	override public void Notify(BaseSerializeData notifyData) {
	}

}