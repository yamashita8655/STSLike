using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuData : SceneDataBase
{
	public MasterDungeonTable.Data Data { get; set; }
	public MenuData() {
		Data = null;
	}
}
