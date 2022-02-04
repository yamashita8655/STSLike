using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBattleEnemyLotActionState : StateBase {

    /// <summary>
    /// メイン前処理.
    /// 戻り値は、同一フレーム内で次の処理に移行してよければfalse、1フレーム飛ばして欲しい場合はfalse.
    /// </summary>
    override public bool OnBeforeMain()
    {
		var scene = MapDataCarrier.Instance.Scene as MapScene;

		for (int i = 0; i < MapDataCarrier.Instance.EnemyValueObjects.Count; i++) {
			GameObject.Destroy(MapDataCarrier.Instance.EnemyValueObjects[i]);
		}
		MapDataCarrier.Instance.EnemyValueObjects.Clear();

		// 抽選をして、初めて行動が確定する
		MapDataCarrier.Instance.CuEnemyStatus.LotActionData();
		MasterAction2Table.Data data = MapDataCarrier.Instance.CuEnemyStatus.GetActionData();
		// テキスト表示
		scene.EnemyActionText.text = data.Name;

		// 効果量表示
		var list = data.ActionPackList;
		for (int i = 0; i < list.Count; i++) {
			int index = i;
			ResourceManager.Instance.RequestExecuteOrder(
				Const.ValueItemPath,
				ExecuteOrder.Type.GameObject,
				scene.gameObject,
				(rawObject) => {
					GameObject obj = GameObject.Instantiate(rawObject) as GameObject;
					int val = list[index].Value;
					if (list[index].Effect == EnumSelf.EffectType.Damage) {
						val += MapDataCarrier.Instance.CuEnemyStatus.GetPower().GetParameter(EnumSelf.PowerType.Strength);
					}
					obj.GetComponent<ValueController>().Initialize(list[index].Effect, val, scene.EnemyActionValueRoot);
					MapDataCarrier.Instance.EnemyValueObjects.Add(obj);
				}
			);
		}

		return false;
    }

    /// <summary>
    /// メイン更新処理.
    /// </summary>
    /// <param name="delta">経過時間</param>
    override public void OnUpdateMain(float delta)
    {
		StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattlePlayerTurnStart);
    }

    /// <summary>
    /// ステート解放時処理.
    /// </summary>
    override public void OnRelease()
    {
    }
}
