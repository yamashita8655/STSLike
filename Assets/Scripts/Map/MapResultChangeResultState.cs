using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapResultChangeResultState : StateBase {

	/// <summary>
	/// 初期化前処理.
	/// </summary>
	override public bool OnBeforeInit()
	{
		var scene = MapDataCarrier.Instance.Scene as MapScene;
		scene.ResultRoot.SetActive(false);
		
		int treasureIndex = MapDataCarrier.Instance.SelectTreasureIndex;

		if (treasureIndex == 3) {
			// 3の場合は、スキップが押された
			var player = MapDataCarrier.Instance.CuPlayerStatus;
			if (player.GetParameterListFlag(EnumSelf.ParameterType.Minimalist) == true) {
				player.AddMaxHp(1);
				player.AddNowHp(1);
				scene.UpdateParameterText();
			}
		} else {
			MasterAction2Table.Data data = MapDataCarrier.Instance.TreasureList[treasureIndex];

			MapDataCarrier.Instance.OriginalDeckList.Add(data);
			PlayerPrefsManager.Instance.SaveOriginalDeckList(MapDataCarrier.Instance.OriginalDeckList);
			scene.UpdateOriginalDeckCountText();

			//int changeIndex = MapDataCarrier.Instance.SelectChangeIndex;
			//MapDataCarrier.Instance.CuPlayerStatus.SetActionData(changeIndex, data);

			//scene.PlayerActionNameStrings[changeIndex].text = data.Name;
			//scene.UpdatePlayerValueObject(changeIndex);
		}


		return true;
	}

	/// <summary>
	/// メイン更新処理.
	/// </summary>
	/// <param name="delta">経過時間</param>
	override public void OnUpdateMain(float delta)
	{
		StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.ResultEnd);
	}

	/// <summary>
	/// ステート解放時処理.
	/// </summary>
	override public void OnRelease()
	{
	}
}
