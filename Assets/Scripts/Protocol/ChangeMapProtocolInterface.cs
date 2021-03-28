using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMapProtocolInterface : SendRecvProtocol {
	public class SendParameter : SendParameterBase {
		public string UniqueId { get; private set; }
		public SendParameter(string uniqueId) {
			UniqueId = uniqueId;
		}
	};

	public class RecieveParameter : RecieveParameterBase {
		public string MapId { get; private set; }
		public int HomeLevel { get; private set; }
		public string HomeModelId { get; private set; }
		public string HomeMapId { get; private set; }
		public string MapEventPlaceJson { get; private set; }
		public string HomeEventPlaceJson { get; private set; }
		public RecieveParameter(
			ResultCode_ resultCode,
			string mapId,
			int homeLevel,
			string homeModelId,
			string homeMapId,
			string mapEventPlaceJson,
			string homeEventPlaceJson
		) {
			ResultCode = resultCode;

			MapId = mapId;
			HomeLevel = homeLevel;
			HomeModelId = homeModelId;
			HomeMapId = homeMapId;
			MapEventPlaceJson = mapEventPlaceJson;
			HomeEventPlaceJson = homeEventPlaceJson;
		}
	};

	protected Action<RecieveParameterBase> RecieveCallback = null;

	protected SendParameterBase _SendParameter = null;

	override public void Initialize(System.Object target, SendParameterBase param, Action<RecieveParameterBase> recieveCallback) {
		_SendParameter = param;
		RecieveCallback = recieveCallback;
		TargetObject = target;
	}

	override public void Send() {
	}

	override public void Recieve(BaseSerializeData recvData) {
	}
}