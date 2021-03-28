using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : SimpleMonoBehaviourSingleton<PlayerDataManager> {
	private Dictionary<int, UniqueItemWrapper> Inventory = new Dictionary<int, UniqueItemWrapper>();

	public void Initialize()
	{
	}

	public void AddItemToInventory(UniqueItemWrapper item) {
		UniqueItemWrapper data = null;
		Inventory.TryGetValue(item.UniqueId, out data);
		if (data != null) {
			Inventory[item.UniqueId] = item;
		} else {
			Inventory.Add(item.UniqueId, item);
		}
	}
	
	public Dictionary<int, UniqueItemWrapper> GetInventoryClone() {
		Dictionary<int, UniqueItemWrapper> clone = new Dictionary<int, UniqueItemWrapper>(Inventory);
		return clone;
	}
	
	public void RemoveItemToInventory(int uniqueId) {
		Inventory.Remove(uniqueId);
	}
}
