using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemHuntInitializeState : StateBase {

	/// <summary>
	/// 初期化前処理.
	/// </summary>
	override public bool OnBeforeInit()
	{
		var scene = ItemHuntDataCarrier.Instance.Scene as ItemHuntScene;
        ItemHuntDataCarrier.Instance.HuntedEquipItemList.Clear();

        // 獲得していない分のアイテム表示
        string saveString = PlayerPrefsManager.Instance.GetParameter(PlayerPrefsManager.SaveType.HuntedItemList);
        if (string.IsNullOrEmpty(saveString) == false) {
            string[] split = saveString.Split(',');
            List<EquipItemBase> stackList = new List<EquipItemBase>();
            for (int i = 0; i < split.Length; i++) {
                int itemId = int.Parse(split[i]);
                MasterEquipItemDataTable.Data data = MasterEquipItemDataTable.Instance.GetData(itemId);
                Image icon = null;
                Parameter p = new Parameter(
                        0,
                        0,
                        0,
                        0,
                        0,
                        0,
                        0,
                        0,
                        0,
                        0,
                        0,
                        0,
                        0,
                        0,
                        0,
                        0,
                        0,
                        0
                    );
                EquipItemBase equip = new EquipItemBase(data, p, icon);
                stackList.Add(equip);
            }
            scene.HuntedItemDialog.AddStackItemList(stackList);
        }

        return true;
	}

	/// <summary>
	/// メイン更新処理.
	/// </summary>
	/// <param name="delta">経過時間</param>
	override public void OnUpdateMain(float delta)
	{
		StateMachineManager.Instance.ChangeState(StateMachineName.ItemHunt, (int)ItemHuntState.SetHuntTimer);
	}

	/// <summary>
	/// ステート解放時処理.
	/// </summary>
	override public void OnRelease()
	{
	}
}
