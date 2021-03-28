using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryProtocolInterface : NotifyProtocol {
	public class NotifyParameter : NotifyParameterBase {
		public string[] UniqueIds { get; private set; }
		public string[] ItemIds { get; private set; }
		public NotifyParameter(string[] uniqueIds, string[] itemIds) {
			UniqueIds = uniqueIds;
			ItemIds = itemIds;
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