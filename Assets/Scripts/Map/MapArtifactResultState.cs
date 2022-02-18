using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapArtifactResultState : StateBase {

    /// <summary>
    /// メイン前処理.
    /// 戻り値は、同一フレーム内で次の処理に移行してよければfalse、1フレーム飛ばして欲しい場合はfalse.
    /// </summary>
    override public bool OnBeforeMain()
    {
		var scene = MapDataCarrier.Instance.Scene as MapScene;
		
		int artifactIndex = MapDataCarrier.Instance.SelectArtifactIndex;
		
		MasterArtifactTable.Data data = MapDataCarrier.Instance.ArtifactList[artifactIndex];

		// TODO ここで、アーティファクト管理に獲得したアーティファクトを加える
		MapDataCarrier.Instance.CarryArtifactList.Add(data);

		scene.AddArtifactObject(data);

		//ResourceManager.Instance.RequestExecuteOrder(
		//	ArtifactButtonPath,
		//	ExecuteOrder.Type.GameObject,
		//	scene.gameObject,
		//	(rawObj) => {
		//		GameObject obj = GameObject.Instantiate(rawObj) as GameObject;
		//		obj.transform.SetParent(scene.ArtifactContentRoot.transform);
		//		obj.transform.localPosition = Vector3.zero;
		//		obj.transform.localScale = Vector3.one;

		//		obj.GetComponent<ArtifactButtonContentItem>().Initialize(
		//			data,
		//			(d) => {
		//				scene.OnClickCarryArtifactButton(d);
		//			}
		//		);
		//	}
		//);

		return true;
    }

    /// <summary>
    /// メイン更新処理.
    /// </summary>
    /// <param name="delta">経過時間</param>
    override public void OnUpdateMain(float delta)
    {
		StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.ArtifactEnd);
    }

    /// <summary>
    /// ステート解放時処理.
    /// </summary>
    override public void OnRelease()
    {
    }
}
