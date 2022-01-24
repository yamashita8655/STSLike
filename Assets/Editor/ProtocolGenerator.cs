using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
//using System.Drawing.Image;
//using System.Drawing.Imaging;

/// <summary>
/// </summary>
public class ProtocolGenerator : Editor
{
	[MenuItem("ShortCutCommand/ProtocolGenerator")]
	private static void CreateProtocol()
	{
		// 定数扱いの変数たち
		string metaFilePath = "Meta/ProtocolMetaFile.csv";
		
		StreamReader descSr = new StreamReader(metaFilePath, Encoding.GetEncoding("UTF-8"));
		string descStr = descSr.ReadToEnd();
		descSr.Close();
		Debug.Log(descStr);

		descStr = descStr.Replace("\r\n", "\n");
		descStr = descStr.Replace("\r", "\n");

		List<string> stringList = new List<string>();
		stringList.AddRange(descStr.Split('\n'));
		
		List<List<string>> sendrecvList = new List<List<string>>();
		List<List<string>> notifyList = new List<List<string>>();

		List<List<string>> paramList = new List<List<string>>();
		for (int i = 0; i < stringList.Count; i++) {
			List<string> addList = new List<string>();
			addList.AddRange(stringList[i].Split(','));
			addList.RemoveAll(s => string.IsNullOrEmpty(s));
			if ((addList.Count <= 1) || (addList[1] == "send") || addList[1] == "recv") {
				sendrecvList.Add(addList);
			} else if (addList[1] == "notify") {
				notifyList.Add(addList);
			}
		}

		// プロトコルタイプEnum作成
		CreateProtocolTypeCs(sendrecvList, notifyList);
		
		// シリアライズデータ作成
		CreateSerializeCs(sendrecvList, notifyList);

		// プロトコルベース作成
		CreateProtocolBaseCs();

		// SendRecvプロトコルインターフェイス作成
		CreateSendRecvProtocolInterfaceCs(sendrecvList);

		// Notifyプロトコルインターフェイス作成
		CreateNotifyProtocolInterfaceCs(notifyList);

		// NetworkSystemInterface作成
		CreateNetworkSystemInterfaceCs(sendrecvList, notifyList);

		// アセット再更新
		AssetDatabase.Refresh();
	}
	
	public static void CreateProtocolTypeCs(List<List<string>> sendrecvList, List<List<string>> notifyList)
	{
		string outputFileName = "Assets/Scripts/Protocol/ProtocolType.cs";
		string outputString = "";

		outputString += "public enum SendRecvProtocolType : int {\n";

		for (int i = 0; i < sendrecvList.Count; i++) {
			List<string> paramList = sendrecvList[i];
			if (i == 0) {
				outputString += "	" + paramList[0] + " = 0,\n";
			} else {
				outputString += "	" + paramList[0] + ",\n";
			}
		}
		
		outputString += "}\n";
		outputString += "\n";
		
		outputString += "public enum NotifyProtocolType : int {\n";

		for (int i = 0; i < notifyList.Count; i++) {
			List<string> paramList = notifyList[i];
			if (i == 0) {
				outputString += "	" + paramList[0] + " = 0,\n";
			} else {
				outputString += "	" + paramList[0] + ",\n";
			}
		}
		
		outputString += "}\n";
		
		File.WriteAllText(outputFileName, outputString);
	}
	
	public static void CreateSerializeCs(List<List<string>> sendrecvList, List<List<string>> notifyList)
	{
		string outputFileName = "Assets/Scripts/Protocol/Serialize.cs";
		string outputString = "";

		outputString += "using System;\n";
		outputString += "using System.Collections;\n";
		outputString += "using System.Collections.Generic;\n";
		outputString += "using UnityEngine;\n";
		outputString += "\n";
		outputString += "public class BaseSerializeData {\n";
		outputString += "	public string ResultCode;\n";
		outputString += "}\n";
		outputString += "\n";

		for (int i = 0; i < sendrecvList.Count; i++) {
			outputString += "[Serializable]\n";
			
			List<string> paramList = sendrecvList[i];

			outputString += "public class Serialize" + paramList[0] + "Data : BaseSerializeData { \n";

			int index = 0;
			for (int j = 0; j < paramList.Count; j++) {
				if (paramList[j] == "recv") {
					break;
				}
				index++;
			}

			for (int j = index+1; j < paramList.Count; j += 3) {
				outputString += "	public " + paramList[j] + " " + paramList[j+1] + "; // " + paramList[j+2] + "\n";
			}
			outputString += "}\n";
			outputString += "\n";
		}
		
		for (int i = 0; i < notifyList.Count; i++) {
			outputString += "[Serializable]\n";
			
			List<string> paramList = notifyList[i];

			outputString += "public class Serialize" + paramList[0] + "Data : BaseSerializeData { \n";

			for (int j = 2; j < paramList.Count; j += 3) {
				outputString += "	public " + paramList[j] + " " + paramList[j+1] + "; // " + paramList[j+2] + "\n";
			}
			outputString += "}\n";
			outputString += "\n";
		}
		
		File.WriteAllText(outputFileName, outputString);
	}
	
	public static void CreateProtocolBaseCs()
	{
		string outputFileName = "Assets/Scripts/Protocol/ProtocolBase.cs";
		string outputString = "";

		outputString += "using System;\n";
		outputString += "using System.Collections;\n";
		outputString += "using System.Collections.Generic;\n";
		outputString += "using UnityEngine;\n";
		outputString += "\n";
		outputString += "public class ProtocolBase\n";
		outputString += "{\n";
		outputString += "	protected bool IsShowLoading;\n";
		outputString += "	protected System.Object TargetObject;\n";
		outputString += "	\n";
		outputString += "	public bool GetShowLoading() {\n";
		outputString += "		return IsShowLoading;\n";
		outputString += "	}\n";
		outputString += "}\n";
		outputString += "\n";
		outputString += "public class SendRecvProtocol : ProtocolBase\n";
		outputString += "{\n";
		outputString += "	virtual public void Initialize(System.Object target, SendParameterBase param, Action<RecieveParameterBase> recieveCallback) {\n";
		outputString += "	}\n";
		outputString += "\n";
		outputString += "	virtual public void Send() {\n";
		outputString += "	}\n";
		outputString += "	\n";
		outputString += "	virtual public void Recieve(BaseSerializeData recvData) {\n";
		outputString += "	}\n";
		outputString += "}\n";
		outputString += "\n";
		outputString += "public class NotifyProtocol : ProtocolBase\n";
		outputString += "{\n";
		outputString += "	virtual public void Initialize(System.Object target, Action<NotifyParameterBase> notifyCallback) {\n";
		outputString += "	}\n";
		outputString += "\n";
		outputString += "	virtual public void Notify(BaseSerializeData notifyData) {\n";
		outputString += "	}\n";
		outputString += "}\n";
		outputString += "\n";
		outputString += "public class SendParameterBase {\n";
		outputString += "	public SendParameterBase() {\n";
		outputString += "		\n";
		outputString += "	}\n";
		outputString += "}\n";
		outputString += "\n";
		outputString += "public class RecieveParameterBase {\n";
		outputString += "	protected ResultCode_ ResultCode;\n";
		outputString += "	public RecieveParameterBase() {\n";
		outputString += "		\n";
		outputString += "	}\n";
		outputString += "\n";
		outputString += "	public ResultCode_ GetResultCode()\n";
		outputString += "	{\n";
		outputString += "		return ResultCode;\n";
		outputString += "	}\n";
		outputString += "}\n";
		outputString += "\n";
		outputString += "public class NotifyParameterBase {\n";
		outputString += "	public NotifyParameterBase() {\n";
		outputString += "		\n";
		outputString += "	}\n";
		outputString += "}\n";
		
		File.WriteAllText(outputFileName, outputString);
	}

	public static void CreateSendRecvProtocolInterfaceCs(List<List<string>> list)
	{
		for (int i = 0; i < list.Count; i++) {
			List<string> paramList = list[i];
			string outputFileName = "Assets/Scripts/Protocol/" + paramList[0] + "ProtocolInterface.cs";
			string outputString = "";

			outputString += "using System;\n";
			outputString += "using System.Collections;\n";
			outputString += "using System.Collections.Generic;\n";
			outputString += "using UnityEngine;\n";
			outputString += "\n";
			outputString += "public class " + paramList[0] + "ProtocolInterface : SendRecvProtocol {\n";
			outputString += "	public class SendParameter : SendParameterBase {\n";

			bool sendFind = false;
			for (int j = 0; j < paramList.Count; j++) {
				if (paramList[j] == "send") {
					sendFind = true;
					break;
				}
			}
			
			int index = 0;
//			bool recvFind = false;
			for (int j = 0; j < paramList.Count; j++) {
				if (paramList[j] == "recv") {
//					recvFind = true;
					break;
				}
				index++;
			}

			if (sendFind == true) {
				for (int j = 2; j < index; j += 3) {
					outputString += "		public " + paramList[j] + " " + paramList[j+1] + " { get; private set; }\n";
				}
				
				outputString += "		public SendParameter(";
				bool isFirst = true;
				for (int j = 2; j < index; j += 3) {
					if (isFirst == true) {
						isFirst = false;
					} else {
						outputString += ", ";
					}
					string name = GetHeadLowerString(paramList[j+1]);
					outputString += paramList[j] + " " + name;
				}
				outputString += ") {\n";

				for (int j = 2; j < index; j += 3) {
					outputString += "			" + paramList[j+1] + " = " + GetHeadLowerString(paramList[j+1]) + ";\n";
				}
				outputString += "		}\n";
			}
			outputString += "	};\n";
			outputString += "\n";

			outputString += "	public class RecieveParameter : RecieveParameterBase {\n";
			for (int j = index+1; j < paramList.Count; j += 3) {
				outputString += "		public " + paramList[j] + " " + paramList[j+1] + " { get; private set; }\n";
			}

			outputString += "		public RecieveParameter(\n";
			outputString += "			ResultCode_ resultCode";
			
			for (int j = index+1; j < paramList.Count; j += 3) {
				outputString += ",\n";
				outputString += "			" + paramList[j] + " " + GetHeadLowerString(paramList[j+1]);
			}

			outputString += "\n		) {\n";
			
			outputString += "			ResultCode = resultCode;\n";
			outputString += "\n";
			for (int j = index+1; j < paramList.Count; j += 3) {
				outputString += "			" + paramList[j+1] + " = " + GetHeadLowerString(paramList[j+1]) + ";\n";
			}
			
			outputString += "		}\n";
			outputString += "	};\n";
			outputString += "\n";

			outputString += "	protected Action<RecieveParameterBase> RecieveCallback = null;\n";
			outputString += "\n";
			outputString += "	protected SendParameterBase _SendParameter = null;\n";
			outputString += "\n";
			
			outputString += "	override public void Initialize(System.Object target, SendParameterBase param, Action<RecieveParameterBase> recieveCallback) {\n";
			outputString += "		_SendParameter = param;\n";
			outputString += "		RecieveCallback = recieveCallback;\n";
			outputString += "		TargetObject = target;\n";
			outputString += "	}\n";
			outputString += "\n";
			
			outputString += "	override public void Send() {\n";
			outputString += "	}\n";
			outputString += "\n";
			
			outputString += "	override public void Recieve(BaseSerializeData recvData) {\n";
			outputString += "	}\n";
			outputString += "}";
			
			File.WriteAllText(outputFileName, outputString);
		}	
	}
	
	public static void CreateNotifyProtocolInterfaceCs(List<List<string>> list)
	{
		for (int i = 0; i < list.Count; i++) {
			List<string> paramList = list[i];
			string outputFileName = "Assets/Scripts/Protocol/" + paramList[0] + "ProtocolInterface.cs";
			string outputString = "";

			outputString += "using System;\n";
			outputString += "using System.Collections;\n";
			outputString += "using System.Collections.Generic;\n";
			outputString += "using UnityEngine;\n";
			outputString += "\n";
			outputString += "public class " + paramList[0] + "ProtocolInterface : NotifyProtocol {\n";
			outputString += "	public class NotifyParameter : NotifyParameterBase {\n";

			for (int j = 2; j < paramList.Count; j += 3) {
				outputString += "		public " + paramList[j] + " " + paramList[j+1] + " { get; private set; }\n";
			}
			
			outputString += "		public NotifyParameter(";
			bool isFirst = true;
			for (int j = 2; j < paramList.Count; j += 3) {
				if (isFirst == true) {
					isFirst = false;
				} else {
					outputString += ", ";
				}
				string name = GetHeadLowerString(paramList[j+1]);
				outputString += paramList[j] + " " + name;
			}
			outputString += ") {\n";

			for (int j = 2; j < paramList.Count; j += 3) {
				outputString += "			" + paramList[j+1] + " = " + GetHeadLowerString(paramList[j+1]) + ";\n";
			}
			outputString += "		}\n";
			outputString += "	};\n";
			outputString += "\n";

			outputString += "	protected Action<NotifyParameterBase> NotifyCallback = null;\n";
			outputString += "\n";
			
			outputString += "	override public void Initialize(System.Object target, Action<NotifyParameterBase> notifyCallback) {\n";
			outputString += "		NotifyCallback = notifyCallback;\n";
			outputString += "		TargetObject = target;\n";
			outputString += "	}\n";
			outputString += "\n";
			
			outputString += "	override public void Notify(BaseSerializeData notifyData) {\n";
			outputString += "	}\n";
			outputString += "\n";
			
			outputString += "}";
			
			File.WriteAllText(outputFileName, outputString);
		}	
	}
	
	public static void CreateNetworkSystemInterfaceCs(List<List<string>> sendrecvList, List<List<string>> notifyList)
	{
		string outputFileName = "Assets/Scripts/Protocol/NetworkSystemInterface.cs";
		string outputString = "";
		
		outputString += "using System;\n";
		outputString += "using System.Collections;\n";
		outputString += "using System.Collections.Generic;\n";
		outputString += "using UnityEngine;\n";
		outputString += "\n";
		outputString += "public class NetworkSystemInterface {\n";
		outputString += "\n";
		outputString += "    protected List<ProtocolBase> RequestQueue = new List<ProtocolBase>();\n";
		outputString += "\n";
		outputString += "    protected List<ProtocolBase> SendRecvProtocolList = new List<ProtocolBase>();\n";
		outputString += "\n";
		outputString += "    protected List<ProtocolBase> NotifyProtocolList = new List<ProtocolBase>();\n";
		outputString += "\n";
		outputString += "    protected int RecieveCount = 0;\n";
		outputString += "\n";
		outputString += "	public NetworkSystemInterface() {\n";
		outputString += "		\n";
		outputString += "	}\n";
		outputString += "\n";
		outputString += "	virtual public void Initialize()\n";
		outputString += "	{\n";
		
		outputString += "		// ProtocolTypeと同じ並びにする必要がある\n";

		for (int i = 0; i < sendrecvList.Count; i++) {
			List<string> paramList = sendrecvList[i];
			outputString += "		SendRecvProtocolList.Add(new " + paramList[0] + "Protocol());\n";
		}
		outputString += "		\n";
		outputString += "		// NotifyProtocolTypeと同じ並びにする必要がある\n";

		for (int i = 0; i < notifyList.Count; i++) {
			List<string> paramList = notifyList[i];
			outputString += "		NotifyProtocolList.Add(new " + paramList[0] + "Protocol());\n";
		}
		outputString += "	}\n";
		outputString += "\n";
		outputString += "	public void SelfUpdate() {\n";
		outputString += "		while (RequestQueue.Count > 0) {\n";
		outputString += "			ProtocolBase protocol = RequestQueue[0];\n";
		outputString += "			RequestQueue.RemoveAt(0);\n";
		outputString += "			Send(protocol);\n";
		outputString += "		}\n";
		outputString += "	}\n";
		outputString += "    \n";
		outputString += "	public void Request(SendRecvProtocolType type, SendParameterBase param, Action<RecieveParameterBase> recieveCallback)\n";
		outputString += "    {\n";
		outputString += "        Request(null, type, param, recieveCallback);\n";
		outputString += "    }\n";
		outputString += "\n";
		outputString += "    public void Request(System.Object target, SendRecvProtocolType type, SendParameterBase param, Action<RecieveParameterBase> recieveCallback) {\n";
		outputString += "		SendRecvProtocol protocol = SendRecvProtocolList[(int)type] as SendRecvProtocol;\n";
		outputString += "        protocol.Initialize(target, param, recieveCallback);\n";
		outputString += "		RequestQueue.Add(protocol);\n";
		outputString += "	}\n";
		outputString += "	\n";
		outputString += "	public void ActivateNotify(System.Object target, NotifyProtocolType type, Action<NotifyParameterBase> notifyCallback) {\n";
		outputString += "		NotifyProtocol protocol = NotifyProtocolList[(int)type] as NotifyProtocol;\n";
		outputString += "        protocol.Initialize(target, notifyCallback);\n";
		outputString += "	}\n";
		outputString += "	\n";
		outputString += "	virtual public void Send(ProtocolBase protocol) {\n";
		outputString += "		//protocol.Send();\n";
		outputString += "	}\n";
		outputString += "	\n";
		outputString += "	virtual public void Recieve(SendRecvProtocolType type, string jsonParam) {\n";
		outputString += "		ProtocolBase protocol = SendRecvProtocolList[(int)type];\n";
		bool isFirst = true;
		for (int i = 0; i < sendrecvList.Count; i++) {
			List<string> paramList = sendrecvList[i];
			if (isFirst == true) {
				isFirst = false;
				outputString += "		if (type == SendRecvProtocolType." + paramList[0] + ") {\n";
			} else {
				outputString += "		} else if (type == SendRecvProtocolType." + paramList[0] + ") {\n";
			}

			outputString += "			Serialize" + paramList[0] + "Data data = JsonUtility.FromJson<Serialize" + paramList[0] + "Data>(jsonParam);\n";
			outputString += "			(protocol as " + paramList[0] + "Protocol).Recieve(data);\n";
		}
		outputString += "		}\n";
		outputString += "	}\n";
		outputString += "\n";
		outputString += "	virtual public void Notify(NotifyProtocolType type, string jsonParam) {\n";
		outputString += "		ProtocolBase protocol = NotifyProtocolList[(int)type];\n";
		
		isFirst = true;
		for (int i = 0; i < notifyList.Count; i++) {
			List<string> paramList = notifyList[i];
			if (isFirst == true) {
				isFirst = false;
				outputString += "		if (type == NotifyProtocolType." + paramList[0] + ") {\n";
			} else {
				outputString += "		} else if (type == NotifyProtocolType." + paramList[0] + ") {\n";
			}

			outputString += "			Serialize" + paramList[0] + "Data data = JsonUtility.FromJson<Serialize" + paramList[0] + "Data>(jsonParam);\n";
			outputString += "			(protocol as " + paramList[0] + "Protocol).Notify(data);\n";
		}
		outputString += "		}\n";
		outputString += "	}\n";
		outputString += "}\n";
		
		File.WriteAllText(outputFileName, outputString);
	}

	public static string GetHeadLowerString(string input) {
		string output = "";
		for (int i = 0; i < input.Length; i++) {
			if (i == 0) {
				output += input[i].ToString().ToLower();
			} else {
				output += input[i];
			}
		}

		return output;
	}
}
