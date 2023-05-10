using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEndState : StateBase {

    /// <summary>
    /// メイン前処理.
    /// </summary>
    override public bool OnBeforeMain()
    {
		Debug.Log("MapEndState");

		PlayerPrefsManager.Instance.SetDungeonState(string.Empty);
		PlayerPrefsManager.Instance.SaveHandDifficultList(new List<int>());
		PlayerPrefsManager.Instance.SaveSelectDifficultIndex(0);
		PlayerPrefsManager.Instance.SaveSelectMapType(0);
		PlayerPrefsManager.Instance.SaveNowFloor(0);
		PlayerPrefsManager.Instance.SaveMapTypeList(new List<EnumSelf.MapType>());
		PlayerPrefsManager.Instance.SaveDungeonId("0");
		PlayerPrefsManager.Instance.SaveArtifactList(new List<ArtifactButtonContentItem>());
		PlayerPrefsManager.Instance.SaveOriginalDeckList(new List<MasterAction2Table.Data>());
		PlayerPrefsManager.Instance.SaveDiceCost(0);
		PlayerPrefsManager.Instance.SaveChestList(new List<int>());
		PlayerPrefsManager.Instance.SaveEnemyId(0);
		PlayerPrefsManager.Instance.SaveTreasureList(new List<MasterAction2Table.Data>());
		PlayerPrefsManager.Instance.SaveLotArtifactList(new List<MasterArtifactTable.Data>());
		PlayerPrefsManager.Instance.SaveSaveNowHp(0);
		PlayerPrefsManager.Instance.SaveSaveMaxHp(0);

		return false;
    }

    /// <summary>
    /// メイン更新処理.
    /// </summary>
    /// <param name="delta">経過時間</param>
    override public void OnUpdateMain(float delta)
    {
		FadeManager.Instance.FadeOut(FadeManager.Type.Mask, 0.5f, () => {
			LocalSceneManager.Instance.LoadScene(MapDataCarrier.Instance.NextSceneName, null);
		});
    }

    /// <summary>
    /// ステート解放時処理.
    /// </summary>
    override public void OnRelease()
    {
    }
}
