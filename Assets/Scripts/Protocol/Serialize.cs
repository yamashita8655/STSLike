using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSerializeData {
	public string ResultCode;
}

[Serializable]
public class SerializeLoginData : BaseSerializeData { 
	public string UniqueId; // ユニークID
	public string Name; // 名前
	public string ModelId; // モデルID
}

[Serializable]
public class SerializeSignUpData : BaseSerializeData { 
	public string UserId; // あ
	public string UniqueId; // あ
	public string Name; // あ
	public string ModelId; // あ
}

[Serializable]
public class SerializeSendChatMessageData : BaseSerializeData { 
}

[Serializable]
public class SerializeGetFishData : BaseSerializeData { 
	public string UniqueId; // 取得した魚のユニークID。0だと釣りに失敗している
	public string ItemId; // 取得した魚識別用のID
}

[Serializable]
public class SerializeItemSellData : BaseSerializeData { 
}

[Serializable]
public class SerializeChangeMapData : BaseSerializeData { 
	public string MapId; // マップのベースID
	public int HomeLevel; // ホームのレベル
	public string HomeModelId; // 家モデルID
	public string HomeMapId; // 家のマップID
	public string MapEventPlaceJson; // マップのイベント情報Json文字列
	public string HomeEventPlaceJson; // ホームのイベント情報Json文字列
}

[Serializable]
public class SerializeHomeUpgradeData : BaseSerializeData { 
}

[Serializable]
public class SerializeSendMailData : BaseSerializeData { 
}

[Serializable]
public class SerializeGetMailData : BaseSerializeData { 
	public string[] MailUniqueIds; // メール識別ID
	public string[] Froms; // 差出人名
	public string[] Subjects; // 件名
	public string[] Bodys; // 本文
	public string[] Dates; // 日付
	public int[] MailTypes; // メールのタイプ
	public int[] Unreads; // 既読かどうか（1が未読）
}

[Serializable]
public class SerializeSendMailReadData : BaseSerializeData { 
}

[Serializable]
public class SerializeGetPurchaseListData : BaseSerializeData { 
	public int[] PurchasePrices; // メール識別ID
}

[Serializable]
public class SerializeSearchUserData : BaseSerializeData { 
	public string[] UniqueIds; // ユニークID
	public string[] Names; // 名前
	public string[] Images; // 画像を表示する為の何か。これ、サーバーに置くと思うから、バイトを送ってもらうのが良い気がする。
}

[Serializable]
public class SerializeSpawnCharacterData : BaseSerializeData { 
	public string UniqueId; // あ
	public string Name; // あ
	public string ModelId; // あ
}

[Serializable]
public class SerializeNotifyChatMessageData : BaseSerializeData { 
	public string Type; // あ
	public string UniqueId; // あ
	public string Name; // あ
	public string Body; // あ
}

[Serializable]
public class SerializePlayerInventoryData : BaseSerializeData { 
	public string[] UniqueIds; // ユニークIDのリスト
	public string[] ItemIds; // アイテムIDのリスト
}

[Serializable]
public class SerializePlayerMoneyData : BaseSerializeData { 
	public int Money; // 所持金
}

[Serializable]
public class SerializeNotifyMailData : BaseSerializeData { 
	public int UnreadCount; // 未読件数
}

