using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMailProtocol : GetMailProtocolInterface
{
	override public void Initialize(System.Object target, SendParameterBase param, Action<RecieveParameterBase> recieveCallback) {
		IsShowLoading = true;
		base.Initialize(target, param, recieveCallback);
	}

	override public void Recieve(BaseSerializeData recvData) {
		LogManager.Instance.Log("GetMailProtocol");
		SerializeGetMailData data = recvData as SerializeGetMailData;

		// ここで、jsonParamをクラスに変える
		RecieveParameter param = new RecieveParameter(
				(ResultCode_)(int.Parse(data.ResultCode)),
                data.MailUniqueIds,
                data.Froms,
				data.Subjects,
				data.Bodys,
				data.Dates,
				data.MailTypes,
				data.Unreads
			);
		if (RecieveCallback != null) {
			RecieveCallback(param);
		}
	}
}
