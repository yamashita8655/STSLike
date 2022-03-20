using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuRegularArtifactSettingArtifactDetailEquipState : StateBase {

    /// <summary>
    /// メイン前処理.
    /// 戻り値は、同一フレーム内で次の処理に移行してよければfalse、1フレーム飛ばして欲しい場合はfalse.
    /// </summary>
    override public bool OnBeforeMain()
    {
		var scene = MenuDataCarrier.Instance.Scene as MenuScene;

		var item = MenuDataCarrier.Instance.SelectArtifactContentItem;
		var data = item.GetData();

		// TODO この辺、処理負荷高かったら、都度作り直しじゃなくて、オブジェクト再利用ロジックに変えてもいいかもね。
		ResourceManager.Instance.RequestExecuteOrder(
			Const.RegularArtifactButtonItemPath,
			ExecuteOrder.Type.GameObject,
			scene.gameObject,
			(rawObject) => {
				GameObject obj = GameObject.Instantiate(rawObject) as GameObject;
				obj.transform.SetParent(scene.CarryEquipArtifactContentRoot.transform);
				obj.transform.localPosition = Vector3.zero;
				obj.transform.localScale = Vector3.one;
				RegularArtifactButtonController ctrl = obj.GetComponent<RegularArtifactButtonController>();
				ctrl.Initialize(
					data,
					scene.OnClickCarryArtifactButton,
					scene.OnClickCarryArtifactDetailButton
				);
				MenuDataCarrier.Instance.RegularArtifactButtonControllers.Add(ctrl);
			}
		);

		PlayerPrefsManager.Instance.AddRegularSettingArtifactId(data.Id);

		scene.UpdateEquipArtifactCostText();

		return false;
    }

    /// <summary>
    /// メイン更新処理.
    /// </summary>
    /// <param name="delta">経過時間</param>
    override public void OnUpdateMain(float delta)
    {
		StateMachineManager.Instance.ChangeState(StateMachineName.Menu, (int)MenuState.RegularArtifactSettingArtifactDetailClose);
    }

    /// <summary>
    /// ステート解放時処理.
    /// </summary>
    override public void OnRelease()
    {
    }
}
