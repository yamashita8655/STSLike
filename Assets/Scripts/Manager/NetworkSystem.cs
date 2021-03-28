using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkSystem : NetworkSystemInterface {
	public NetworkSystem() {
		
	}

	override public void Initialize()
	{
		// 要は初期化されてるかどうかだけなので、この数値だけみればいい
		if (SendRecvProtocolList.Count != 0) {
			LogManager.Instance.Log("NetworkManager.Initialize Count > 0");
		}

		base.Initialize();
	}

	// 仮
	int TestStartUniqueId = 100;
	override public void Send(ProtocolBase protocol) {
		base.Send(protocol);
		// 多分この辺で、サーバー通信のモジュールのSendを呼び出す事になると思う
		// その為のパラメータを、ProtocolBaseから継承したデータクラスのデータを用いて、
		// 送信パラメータを作成する事になると思う

		// 本番は、恐らくReceiveやNotifyがコールバックで勝手に呼び出されるフローになる

		if (protocol.GetShowLoading() == true) {
			if (RecieveCount == 0) {
				SystemDialogManager.Instance.OpenLoading();
			}
			RecieveCount++;
		}

		// TODO ここは仮。
		// サーバーからプロトコル識別番号を送ってもらって、
		// それで、どのインスタンスを取得するか判断する
		string jsonParam = "";
		if ((protocol as LoginProtocol) != null) {
			//protocol.Send();
			
			DebugAfterLogin();

			jsonParam = "{ \"ResultCode\": \"0\", \"UniqueId\": \"1\", \"Name\": \"MyPlayer\", \"ModelId\": \"Prefab/Model/Avater/BodyBuidler\" }";
			Recieve(SendRecvProtocolType.Login, jsonParam);
		} else if ((protocol as SignUpProtocol) != null) {
			DebugAfterLogin();

			jsonParam = "{ \"ResultCode\": \"0\", \"UserId\": \"1\", \"UniqueId\": \"1\", \"Name\": \"MyPlayer\", \"ModelId\": \"Prefab/Model/Avater/BodyBuidler\" }";
			Recieve(SendRecvProtocolType.SignUp, jsonParam);
		} else if ((protocol as SendChatMessageProtocol) != null) {
			jsonParam = "{ \"ResultCode\": \"0\" }";
			Recieve(SendRecvProtocolType.SendChatMessage, jsonParam);
		} else if ((protocol as GetFishProtocol) != null) {
			jsonParam = "{ \"ResultCode\": \"0\", \"UniqueId\": \"" + TestStartUniqueId + "\", \"ItemId\": \"1\" }";
			TestStartUniqueId++;
			Recieve(SendRecvProtocolType.GetFish, jsonParam);
		} else if ((protocol as ItemSellProtocol) != null) {
			jsonParam = "{ \"ResultCode\": \"0\" }";
			Recieve(SendRecvProtocolType.ItemSell, jsonParam);
		} else if ((protocol as ChangeMapProtocol) != null) {
			if ((protocol as ChangeMapProtocol).DebugGetSendUniqueId() == "0") {
				jsonParam = "{ \"ResultCode\": \"0\", \"MapId\": \"1000\", \"HomeMapId\": \"2000\", \"HomeLevel\": 1, \"HomeModelId\": \"2000\",";
				jsonParam += " \"MapEventPlaceJson\" : \"";
				jsonParam += "{";
				jsonParam += "\\\"MapPlaceId\\\":[";
				jsonParam += "\\\"1000\\\",";
				jsonParam += "\\\"1000\\\",";
				jsonParam += "\\\"1000\\\",";
				jsonParam += "\\\"1000\\\"";
				jsonParam += "],";
				jsonParam += "";
				jsonParam += "\\\"Path\\\":[";
				jsonParam += "\\\"Prefab/Model/Map/homemanager\\\",";
				jsonParam += "\\\"Prefab/Model/Map/mailbox\\\",";
				jsonParam += "\\\"Prefab/Model/Map/lakewater\\\",";
				jsonParam += "\\\"\\\"";
				jsonParam += "],";
				jsonParam += "";
				jsonParam += "\\\"X\\\":[";
				jsonParam += "7,";
				jsonParam += "1,";
				jsonParam += "10,";
				jsonParam += "3";
				jsonParam += "],";
				jsonParam += "";
				jsonParam += "\\\"Z\\\":[";
				jsonParam += "4,";
				jsonParam += "1,";
				jsonParam += "10,";
				jsonParam += "1";
				jsonParam += "],";
				jsonParam += "";
				jsonParam += "\\\"Event\\\":[";
				jsonParam += "\\\"HomeUpgrade\\\",";
				jsonParam += "\\\"MailBox\\\",";
				jsonParam += "\\\"Fishing\\\",";
				jsonParam += "\\\"WarpHome\\\"";
				jsonParam += "],";
				jsonParam += "";
				jsonParam += "\\\"ScaleX\\\":[";
				jsonParam += "100,";
				jsonParam += "100,";
				jsonParam += "10,";
				jsonParam += "1";
				jsonParam += "],";
				jsonParam += "";
				jsonParam += "\\\"ScaleY\\\":[";
				jsonParam += "100,";
				jsonParam += "100,";
				jsonParam += "10,";
				jsonParam += "1";
				jsonParam += "],";
				jsonParam += "";
				jsonParam += "\\\"ScaleZ\\\":[";
				jsonParam += "100,";
				jsonParam += "100,";
				jsonParam += "10,";
				jsonParam += "1";
				jsonParam += "]";
				jsonParam += "}";
				jsonParam += "\", ";
				
				jsonParam += " \"HomeEventPlaceJson\" : \"";
				jsonParam += "{";
				jsonParam += "\\\"MapPlaceId\\\":[";
				jsonParam += "\\\"2000\\\"";
				jsonParam += "],";
				jsonParam += "";
				jsonParam += "\\\"Path\\\":[";
				jsonParam += "\\\"\\\"";
				jsonParam += "],";
				jsonParam += "";
				jsonParam += "\\\"X\\\":[";
				jsonParam += "3";
				jsonParam += "],";
				jsonParam += "";
				jsonParam += "\\\"Z\\\":[";
				jsonParam += "3";
				jsonParam += "],";
				jsonParam += "";
				jsonParam += "\\\"Event\\\":[";
				jsonParam += "\\\"WarpField\\\"";
				jsonParam += "],";
				jsonParam += "";
				jsonParam += "\\\"ScaleX\\\":[";
				jsonParam += "1";
				jsonParam += "],";
				jsonParam += "";
				jsonParam += "\\\"ScaleY\\\":[";
				jsonParam += "1";
				jsonParam += "],";
				jsonParam += "";
				jsonParam += "\\\"ScaleZ\\\":[";
				jsonParam += "1";
				jsonParam += "]";
				jsonParam += "}";
				jsonParam += "\"";
				jsonParam += "}";
			} else {
				jsonParam = "{ \"ResultCode\": \"0\", \"MapId\": \"1000\", \"HomeMapId\": \"2000\", \"HomeLevel\": 2, \"HomeModelId\": \"2000\",";
				jsonParam += " \"MapEventPlaceJson\" : \"";
				jsonParam += "{";
				jsonParam += "\\\"MapPlaceId\\\":[";
				jsonParam += "\\\"1000\\\",";
				jsonParam += "\\\"1000\\\",";
				jsonParam += "\\\"1000\\\",";
				jsonParam += "\\\"1000\\\"";
				jsonParam += "],";
				jsonParam += "";
				jsonParam += "\\\"Path\\\":[";
				jsonParam += "\\\"Prefab/Model/Map/homemanager\\\",";
				jsonParam += "\\\"Prefab/Model/Map/mailbox\\\",";
				jsonParam += "\\\"Prefab/Model/Map/lakewater\\\",";
				jsonParam += "\\\"\\\"";
				jsonParam += "],";
				jsonParam += "";
				jsonParam += "\\\"X\\\":[";
				jsonParam += "7,";
				jsonParam += "8,";
				jsonParam += "10,";
				jsonParam += "3";
				jsonParam += "],";
				jsonParam += "";
				jsonParam += "\\\"Z\\\":[";
				jsonParam += "4,";
				jsonParam += "1,";
				jsonParam += "10,";
				jsonParam += "1";
				jsonParam += "],";
				jsonParam += "";
				jsonParam += "\\\"Event\\\":[";
				jsonParam += "\\\"HomeUpgrade\\\",";
				jsonParam += "\\\"MailBox\\\",";
				jsonParam += "\\\"Fishing\\\",";
				jsonParam += "\\\"WarpHome\\\"";
				jsonParam += "],";
				jsonParam += "";
				jsonParam += "\\\"ScaleX\\\":[";
				jsonParam += "100,";
				jsonParam += "100,";
				jsonParam += "10,";
				jsonParam += "1";
				jsonParam += "],";
				jsonParam += "";
				jsonParam += "\\\"ScaleY\\\":[";
				jsonParam += "100,";
				jsonParam += "100,";
				jsonParam += "10,";
				jsonParam += "1";
				jsonParam += "],";
				jsonParam += "";
				jsonParam += "\\\"ScaleZ\\\":[";
				jsonParam += "100,";
				jsonParam += "100,";
				jsonParam += "10,";
				jsonParam += "1";
				jsonParam += "]";
				jsonParam += "}";
				jsonParam += "\", ";
				
				jsonParam += " \"HomeEventPlaceJson\" : \"";
				jsonParam += "{";
				jsonParam += "\\\"MapPlaceId\\\":[";
				jsonParam += "\\\"2000\\\"";
				jsonParam += "],";
				jsonParam += "";
				jsonParam += "\\\"Path\\\":[";
				jsonParam += "\\\"\\\"";
				jsonParam += "],";
				jsonParam += "";
				jsonParam += "\\\"X\\\":[";
				jsonParam += "3";
				jsonParam += "],";
				jsonParam += "";
				jsonParam += "\\\"Z\\\":[";
				jsonParam += "3";
				jsonParam += "],";
				jsonParam += "";
				jsonParam += "\\\"Event\\\":[";
				jsonParam += "\\\"WarpField\\\"";
				jsonParam += "],";
				jsonParam += "";
				jsonParam += "\\\"ScaleX\\\":[";
				jsonParam += "1";
				jsonParam += "],";
				jsonParam += "";
				jsonParam += "\\\"ScaleY\\\":[";
				jsonParam += "1";
				jsonParam += "],";
				jsonParam += "";
				jsonParam += "\\\"ScaleZ\\\":[";
				jsonParam += "1";
				jsonParam += "]";
				jsonParam += "}";
				jsonParam += "\"";
				jsonParam += "}";
			}
			//jsonParam = "{ \"ResultCode\": \"0\",";
			//jsonParam += " \"MapEventPlaceJson\" : \"{ \\\"MapPlaceId\\\" : [\\\"1000\\\", \\\"1000\\\"]}\",\n";
			//jsonParam += " \"HomeEventPlaceJson\" : \"\"\n";
			//jsonParam += "}";


			Recieve(SendRecvProtocolType.ChangeMap, jsonParam);
		} else if ((protocol as HomeUpgradeProtocol) != null) {
			jsonParam = "{ \"ResultCode\": \"0\" }";
			Recieve(SendRecvProtocolType.HomeUpgrade, jsonParam);
		} else if ((protocol as SendMailProtocol) != null) {
			jsonParam = "{ \"ResultCode\": \"0\" }";
			Recieve(SendRecvProtocolType.SendMail, jsonParam);
		} else if ((protocol as GetMailProtocol) != null) {
			jsonParam = "{ \"ResultCode\": \"0\", ";
			jsonParam += " \"MailUniqueIds\" : [";
			jsonParam += "\"1\",";
			jsonParam += "\"2\",";
			jsonParam += "\"3\",";
			jsonParam += "\"4\",";
			jsonParam += "\"5\",";
			jsonParam += "\"6\",";
			jsonParam += "\"7\",";
			jsonParam += "\"8\",";
			jsonParam += "\"9\",";
			jsonParam += "\"10\"";
			jsonParam += "],";

			jsonParam += " \"Froms\" : [";
			jsonParam += "\"あ\",";
			jsonParam += "\"い\",";
			jsonParam += "\"う\",";
			jsonParam += "\"え\",";
			jsonParam += "\"お\",";
			jsonParam += "\"か\",";
			jsonParam += "\"き\",";
			jsonParam += "\"く\",";
			jsonParam += "\"け\",";
			jsonParam += "\"こ\"";
			jsonParam += "],";
			
			jsonParam += " \"Subjects\" : [";
			jsonParam += "\"ああ\",";
			jsonParam += "\"いい\",";
			jsonParam += "\"うう\",";
			jsonParam += "\"ええ\",";
			jsonParam += "\"おお\",";
			jsonParam += "\"かか\",";
			jsonParam += "\"きき\",";
			jsonParam += "\"くく\",";
			jsonParam += "\"けけ\",";
			jsonParam += "\"ここ\"";
			jsonParam += "],";
			
			jsonParam += " \"Bodys\" : [";
			jsonParam += "\"ああああ\",";
			jsonParam += "\"いいいい\",";
			jsonParam += "\"うううう\",";
			jsonParam += "\"ええええ\",";
			jsonParam += "\"おおおお\",";
			jsonParam += "\"かかかか\",";
			jsonParam += "\"きききき\",";
			jsonParam += "\"くくくく\",";
			jsonParam += "\"けけけけ\",";
			jsonParam += "\"ここここ\"";
			jsonParam += "],";
			
			jsonParam += " \"Dates\" : [";
			jsonParam += "\"2020/12/1 20:00\",";
			jsonParam += "\"2020/12/2 20:00\",";
			jsonParam += "\"2020/12/3 20:00\",";
			jsonParam += "\"2020/12/4 20:00\",";
			jsonParam += "\"2020/12/5 20:00\",";
			jsonParam += "\"2020/12/6 20:00\",";
			jsonParam += "\"2020/12/7 20:00\",";
			jsonParam += "\"2020/12/8 20:00\",";
			jsonParam += "\"2020/12/9 20:00\",";
			jsonParam += "\"2020/12/10 20:00\"";
			jsonParam += "],";
			
			jsonParam += " \"MailTypes\" : [";
			jsonParam += "0,";
			jsonParam += "0,";
			jsonParam += "0,";
			jsonParam += "0,";
			jsonParam += "0,";
			jsonParam += "0,";
			jsonParam += "0,";
			jsonParam += "0,";
			jsonParam += "0,";
			jsonParam += "0";
			jsonParam += "],";
			
			jsonParam += " \"Unreads\" : [";
			jsonParam += "1,";
			jsonParam += "1,";
			jsonParam += "1,";
			jsonParam += "1,";
			jsonParam += "1,";
			jsonParam += "1,";
			jsonParam += "1,";
			jsonParam += "1,";
			jsonParam += "1,";
			jsonParam += "0";
			jsonParam += "]";
			jsonParam += "}";
			Recieve(SendRecvProtocolType.GetMail, jsonParam);
		} else if ((protocol as SendMailReadProtocol) != null) {
			jsonParam = "{ \"ResultCode\": \"0\" }";
			Recieve(SendRecvProtocolType.SendMailRead, jsonParam);
		} else if ((protocol as GetPurchaseListProtocol) != null) {
			jsonParam = "{ \"ResultCode\": \"0\", ";
			jsonParam += " \"PurchasePrices\" : [";
			jsonParam += "\"100\",";
			jsonParam += "\"500\",";
			jsonParam += "\"1000\",";
			jsonParam += "\"3000\",";
			jsonParam += "\"5000\",";
			jsonParam += "\"10000\",";
			jsonParam += "\"100000\"";
			jsonParam += "]";
			jsonParam += "}";
			Recieve(SendRecvProtocolType.GetPurchaseList, jsonParam);
		} else if ((protocol as SearchUserProtocol) != null) {
			jsonParam = "{ \"ResultCode\": \"0\", ";

			jsonParam += " \"UniqueIds\" : [";
			jsonParam += "\"1\",";
			jsonParam += "\"2\",";
			jsonParam += "\"3\",";
			jsonParam += "\"4\",";
			jsonParam += "\"5\",";
			jsonParam += "\"6\",";
			jsonParam += "\"7\"";
			jsonParam += "],";

			jsonParam += " \"Names\" : [";
			jsonParam += "\"あ\",";
			jsonParam += "\"い\",";
			jsonParam += "\"う\",";
			jsonParam += "\"え\",";
			jsonParam += "\"お\",";
			jsonParam += "\"か\",";
			jsonParam += "\"き\"";
			jsonParam += "],";
			
			jsonParam += " \"Images\" : [";
			jsonParam += "\"\",";
			jsonParam += "\"\",";
			jsonParam += "\"\",";
			jsonParam += "\"\",";
			jsonParam += "\"\",";
			jsonParam += "\"\",";
			jsonParam += "\"\"";
			jsonParam += "]";

			jsonParam += "}";
			Recieve(SendRecvProtocolType.SearchUser, jsonParam);
		}
	}
	
	override public void Recieve(SendRecvProtocolType type, string jsonParam) {
		NetworkManager.Instance.StartCoroutine(CoRecieve(type, jsonParam));
	} 
	public IEnumerator CoRecieve(SendRecvProtocolType type, string jsonParam) {
		// TODO 疑似遅延
		yield return new WaitForSeconds(1);
		
		RecieveCount--;
		if (RecieveCount <= 0) {
			RecieveCount = 0;
			SystemDialogManager.Instance.CloseLoading();
		}

		base.Recieve(type, jsonParam);
	}
	
	override public void Notify(NotifyProtocolType type, string jsonParam) {
		NetworkManager.Instance.StartCoroutine(CoNotify(type, jsonParam));
	}

	public IEnumerator CoNotify(NotifyProtocolType type, string jsonParam) {
		// TODO 疑似遅延
		yield return new WaitForSeconds(1);
		base.Notify(type, jsonParam);
	}

	private void DebugAfterLogin() {
		// ログイン済んだら、ここでプレイヤーに送るべき情報を通知する
		SerializePlayerInventoryData inventory = new SerializePlayerInventoryData();
		inventory.UniqueIds = new string[9] { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
		inventory.ItemIds = new string[9] { "1", "2", "3", "1", "2", "3", "1", "2", "3" };
		LocalServerManager.Instance.CallPlayerInventoryNotify(inventory);
		
		SerializePlayerMoneyData money = new SerializePlayerMoneyData();
		money.Money = 10000;
		LocalServerManager.Instance.CallPlayerMoneyNotify(money);
	}
}
