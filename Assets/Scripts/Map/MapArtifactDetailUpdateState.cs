using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapArtifactDetailUpdateState : StateBase {

    /// <summary>
    /// メイン前処理.
    /// 戻り値は、同一フレーム内で次の処理に移行してよければfalse、1フレーム飛ばして欲しい場合はfalse.
    /// </summary>
    override public bool OnBeforeMain()
    {
		var scene = MapDataCarrier.Instance.Scene as MapScene;
		
		int index = MapDataCarrier.Instance.SelectArtifactIndex;
		MasterArtifactTable.Data data = MapDataCarrier.Instance.ArtifactList[index];
		
		scene.ArtifactNameText.gameObject.SetActive(true);
		scene.ArtifactImage.gameObject.SetActive(true);
		scene.ArtifactDetailText.gameObject.SetActive(true);

		scene.ArtifactNameText.text = data.Name;
		ResourceManager.Instance.RequestExecuteOrder(
			data.ImagePath,
			ExecuteOrder.Type.Sprite,
			scene.gameObject,
			(rawSprite) => {
				scene.ArtifactImage.sprite = rawSprite as Sprite;
			}
		);
		
		ResourceManager.Instance.RequestExecuteOrder(
			string.Format(Const.RarityFrameImagePath, data.Rarity),
			ExecuteOrder.Type.Sprite,
			scene.gameObject,
			(rawSprite) => {
				scene.ArtifactDetailRarityImage.sprite = rawSprite as Sprite;
			}
		);

		scene.ArtifactDetailText.text = data.Detail;
		return true;
    }

    /// <summary>
    /// メイン更新処理.
    /// </summary>
    /// <param name="delta">経過時間</param>
    override public void OnUpdateMain(float delta)
    {
		StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.ArtifactUserWait);
    }

    /// <summary>
    /// ステート解放時処理.
    /// </summary>
    override public void OnRelease()
    {
    }
}
