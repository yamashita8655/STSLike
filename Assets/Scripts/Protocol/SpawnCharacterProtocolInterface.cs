using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCharacterProtocolInterface : NotifyProtocol {
	public class NotifyParameter : NotifyParameterBase {
		public string UniqueId { get; private set; }
		public string Name { get; private set; }
		public string ModelId { get; private set; }
		public NotifyParameter(string uniqueId, string name, string modelId) {
			UniqueId = uniqueId;
			Name = name;
			ModelId = modelId;
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