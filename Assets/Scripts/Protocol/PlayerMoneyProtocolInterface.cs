using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoneyProtocolInterface : NotifyProtocol {
	public class NotifyParameter : NotifyParameterBase {
		public int Money { get; private set; }
		public NotifyParameter(int money) {
			Money = money;
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