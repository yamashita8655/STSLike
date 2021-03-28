using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotifyChatMessageProtocolInterface : NotifyProtocol {
	public class NotifyParameter : NotifyParameterBase {
		public string Type { get; private set; }
		public string UniqueId { get; private set; }
		public string Name { get; private set; }
		public string Body { get; private set; }
		public NotifyParameter(string type, string uniqueId, string name, string body) {
			Type = type;
			UniqueId = uniqueId;
			Name = name;
			Body = body;
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