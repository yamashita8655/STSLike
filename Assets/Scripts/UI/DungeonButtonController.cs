using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonButtonController : MonoBehaviour
{
	[SerializeField]
	private Text DungeonName = null;

	private Action<MasterDungeonTable.Data> Callback = null;

	private MasterDungeonTable.Data Data = null;

	public void Initialize(MasterDungeonTable.Data data, Action<MasterDungeonTable.Data> callback) {
		Data = data;
		DungeonName.text = Data.Name;
		Callback = callback;
	}
	
	public void OnClick() {
		if (Callback != null) {
			Callback(Data);
		}
	}
}
