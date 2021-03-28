using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultData : SceneDataBase
{
	public LocalSceneManager.SceneName InGameType { get; set; }
	public int Score { get; set; }
	public ResultData() {
		InGameType = LocalSceneManager.SceneName.None;
		Score = 0;
	}
}
