public enum SendRecvProtocolType : int {
	Login = 0,
	SignUp,
	SendChatMessage,
	GetFish,
	ItemSell,
	ChangeMap,
	HomeUpgrade,
	SendMail,
	GetMail,
	SendMailRead,
	GetPurchaseList,
	SearchUser,
}

public enum NotifyProtocolType : int {
	SpawnCharacter = 0,
	NotifyChatMessage,
	PlayerInventory,
	PlayerMoney,
	NotifyMail,
}
