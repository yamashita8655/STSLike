using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMailProtocolInterface : SendRecvProtocol {
	public class SendParameter : SendParameterBase {
	};

	public class RecieveParameter : RecieveParameterBase {
		public string[] MailUniqueIds { get; private set; }
		public string[] Froms { get; private set; }
		public string[] Subjects { get; private set; }
		public string[] Bodys { get; private set; }
		public string[] Dates { get; private set; }
		public int[] MailTypes { get; private set; }
		public int[] Unreads { get; private set; }
		public RecieveParameter(
			ResultCode_ resultCode,
			string[] mailUniqueIds,
			string[] froms,
			string[] subjects,
			string[] bodys,
			string[] dates,
			int[] mailTypes,
			int[] unreads
		) {
			ResultCode = resultCode;

			MailUniqueIds = mailUniqueIds;
			Froms = froms;
			Subjects = subjects;
			Bodys = bodys;
			Dates = dates;
			MailTypes = mailTypes;
			Unreads = unreads;
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