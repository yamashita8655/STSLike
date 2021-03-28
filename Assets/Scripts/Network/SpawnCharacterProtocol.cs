using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCharacterProtocol : SpawnCharacterProtocolInterface
{
	override public void Initialize(System.Object target, Action<NotifyParameterBase> notifyCallback) {
		IsShowLoading = false;
		base.Initialize(target, notifyCallback);
	}

	override public void Notify(BaseSerializeData notifyData) {
		LogManager.Instance.Log("SpawnCharacterProtocol");
		
		SerializeSpawnCharacterData data = notifyData as SerializeSpawnCharacterData;
		// ここで、jsonParamをクラスに変える
		NotifyParameter param = new NotifyParameter(
				data.UniqueId,
				data.Name,
				data.ModelId
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
