using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBattleHandSelectEndState : StateBase {

    /// <summary>
    /// メイン前処理.
    /// 戻り値は、同一フレーム内で次の処理に移行してよければfalse、1フレーム飛ばして欲しい場合はfalse.
    /// </summary>
    override public bool OnBeforeMain()
    {
		var scene = MapDataCarrier.Instance.Scene as MapScene;
		var player = MapDataCarrier.Instance.CuPlayerStatus;

		MasterAction2Table.Data data = MapDataCarrier.Instance.SelectBattleCardData;
		
		int count = MapDataCarrier.Instance.ActionPackCount;
		ActionPack pack = data.ActionPackList[count]; 

		var ctrls = MapDataCarrier.Instance.BattleCardButtonControllers;
		var deckList = MapDataCarrier.Instance.BattleDeckList;
		var trashList = MapDataCarrier.Instance.TrashList;
		var discardList = MapDataCarrier.Instance.DiscardList;

		for (int i = 0; i < ctrls.Count; i++) {
			// ついでなので、非表示にする
			ctrls[i].UpdateToggleActive(false);

			if (ctrls[i].IsSelect() == true) {
				if (pack.Effect == EnumSelf.EffectType.Hand2DeckTop) {
					deckList.Insert(0, ctrls[i].GetData());
				}

				if (pack.Effect == EnumSelf.EffectType.Hand2Discard) {
					discardList.Add(ctrls[i].GetData());
				}

				if (pack.Effect == EnumSelf.EffectType.Hand2Trash) {
					trashList.Add(ctrls[i].GetData());
				}
			
				ctrls[i].gameObject.SetActive(false);
				ctrls[i].ResetToggle();
			}
		}

		scene.UpdateCardListCountText();

		scene.HandCardSelectRoot.SetActive(false);
		scene.HandCardSelectDecideButton.gameObject.SetActive(false);
		return false;
    }

    /// <summary>
    /// メイン更新処理.
    /// </summary>
    /// <param name="delta">経過時間</param>
    override public void OnUpdateMain(float delta)
    {
		StateMachineManager.Instance.ChangeState(StateMachineName.Map, (int)MapState.BattleCheck);
    }

    /// <summary>
    /// ステート解放時処理.
    /// </summary>
    override public void OnRelease()
    {
    }
}
