using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapData : SceneDataBase
{
	public MasterDungeonTable.Data Data { get; set; }
	public MapData() {
		Data = null;
	}
}
