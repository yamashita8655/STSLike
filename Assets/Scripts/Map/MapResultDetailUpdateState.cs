using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapResultDetailUpdateState : StateBase {

	/// <summary>
	/// 初期化前処理.
	/// </summary>
	override public bool OnBeforeInit()
	{
		var scene = MapDataCarrier.Instance.Scene as MapScene;

		int index = MapDataCarrier.Instance.SelectTreasureIndex;
		MasterAction2Table.Data data = MapDataCarrier.Instance.TreasureList[index];
		
		scene.TreasureDetailCardName.gameObject.SetActive(true);
		scene.TreasureDetailCardImage.gameObject.SetActive(true);
		scene.TreasureDetailCardDetail.gameObject.SetActive(true);
		scene.TreasureDiceCostText.gameObject.SetActive(true);

		scene.TreasureDetailCardName.text = data.Name;
		scene.TreasureDiceCostText.text = data.DiceCost.ToString();
		ResourceManager.Instance.RequestExecuteOrder(
			data.ImagePath,
			ExecuteOrder.Type.Sprite,
			scene.gameObject,
			(rawSprite) => {
				scene.TreasureDetailCardImage.sprite = rawSprite as Sprite;
			}
		);
		
		ResourceManager.Instance.RequestExecuteOrder(
			string.Format(Const.RarityFrameImagePath, data.Rarity),
			ExecuteOrder.Type.Sprite,
			scene.gameObject,
			(rawSprite) => {
				scene.TreasureRarityFrameImage.sprite = rawSprite as Sprite;
			}
		);

		System.Object[] arguments = new System.Object[data.ActionPackList.Count];
		for (int i = 0; i < data.ActionPackList.Count; i++) {
			arguments[i] = data.ActionPackList[i].Value;
		}
		scene.TreasureDetailCardDetail.text = string.Format(data.Detail, arguments);

		return true;
	}

	/// <summary>
	/// メイン更新処理.
	/// </summary>
	/// <param name="delta">経過時間</param>
	override public void OnUpdateMain(float delta)
	{
		StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.ResultTreasureUserWait);
	}

	/// <summary>
	/// ステート解放時処理.
	/// </summary>
	override public void OnRelease()
	{
	}
}
