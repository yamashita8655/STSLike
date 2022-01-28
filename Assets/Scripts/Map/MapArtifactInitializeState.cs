using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapArtifactInitializeState : StateBase {

    /// <summary>
    /// メイン前処理.
    /// 戻り値は、同一フレーム内で次の処理に移行してよければfalse、1フレーム飛ばして欲しい場合はfalse.
    /// </summary>
    override public bool OnBeforeMain()
    {
		var scene = MapDataCarrier.Instance.Scene as MapScene;
		
		scene.ArtifactRoot.SetActive(true);
		scene.MapRoot.SetActive(false);

		scene.ArtifactDecideButton.interactable = false;
		
		scene.ArtifactNameText.gameObject.SetActive(false);
		scene.ArtifactImage.gameObject.SetActive(false);
		scene.ArtifactDetailText.gameObject.SetActive(false);
			
		// TODO Artifactカウント3決め打ち
		MapDataCarrier.Instance.ArtifactList.Clear();
		for (int i = 0; i < 3; i++) {
			// TODO とりあえず、並べるIDは固定値にしておく。
			// 後から、ちゃんと抽選、かつ、同IDは出ないように調整する
			MasterArtifactTable.Data data = MasterArtifactTable.Instance.GetData(i+1);
			MapDataCarrier.Instance.ArtifactList.Add(data);
			scene.ArtifactTexts[i].text = data.Name;
		}

		return true;
    }

	private int LotArtifactId() {
		int id = 0;

		return id;
	}

    /// <summary>
    /// メイン更新処理.
    /// </summary>
    /// <param name="delta">経過時間</param>
    override public void OnUpdateMain(float delta)
    {
		StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.ArtifactDisplay);
    }

    /// <summary>
    /// ステート解放時処理.
    /// </summary>
    override public void OnRelease()
    {
    }
}
