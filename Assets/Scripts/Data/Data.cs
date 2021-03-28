using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData
{
	public string UniqueId { get; private set; }
	public string ItemId { get; private set; }

	public ItemData(string uniqueId, string itemId)
	{
		UniqueId = uniqueId;
		ItemId = itemId;
	}
}

