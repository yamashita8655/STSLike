using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMapProtocol : ChangeMapProtocolInterface
{
	[Serializable]
	public class RecvData { 
		public string[] Path;
		public int[] X;
		public int[] Z;
		public string[] Event;
		public int[] ScaleX;
		public int[] ScaleY;
		public int[] ScaleZ;
	}

	override public void Initialize(System.Object target, SendParameterBase param, Action<RecieveParameterBase> recieveCallback) {
		IsShowLoading = true;
		base.Initialize(target, param, recieveCallback);
	}

	override public void Recieve(BaseSerializeData recvData) {
		LogManager.Instance.Log("ChangeMapProtocol");
		SerializeChangeMapData data = recvData as SerializeChangeMapData;
			
		RecvData mapData = JsonUtility.FromJson<RecvData>(data.MapEventPlaceJson);
		
		RecvData homeData = JsonUtility.FromJson<RecvData>(data.HomeEventPlaceJson);

		// ここで、jsonParamをクラスに変える
		RecieveParameter param = new RecieveParameter(
				(ResultCode_)(int.Parse(data.ResultCode)),
                data.MapId,
                data.HomeLevel,
                data.HomeModelId,
                data.HomeMapId,
                data.MapEventPlaceJson,
				data.HomeEventPlaceJson
			);
		if (RecieveCallback != null) {
			RecieveCallback(param);
		}
	}

    // デバッグサーバー用に、アクセス可能にする
    public string DebugGetSendUniqueId()
    {
        return (_SendParameter as SendParameter).UniqueId;
    }
}
