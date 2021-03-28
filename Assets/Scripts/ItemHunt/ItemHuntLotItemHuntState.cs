using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemHuntLotItemHuntState : StateBase {

	/// <summary>
	/// 初期化前処理.
	/// </summary>
	override public bool OnBeforeInit()
	{
		var scene = ItemHuntDataCarrier.Instance.Scene as ItemHuntScene;

		int itemId = UnityEngine.Random.Range(1, 3+1);

		MasterEquipItemDataTable.Data data = MasterEquipItemDataTable.Instance.GetData(itemId);

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

		Image icon = null;

		EquipItemBase equip = new EquipItemBase(data, p, icon);
        ItemHuntDataCarrier.Instance.HuntedEquipItemList.Add(equip);

        List<EquipItemBase> stackList = new List<EquipItemBase>();
        stackList.Add(equip);
        scene.HuntedItemDialog.AddStackItemList(stackList);

        // 獲得したアイテム情報のセーブ
        string saveString = PlayerPrefsManager.Instance.GetParameter(PlayerPrefsManager.SaveType.HuntedItemList);
        if (string.IsNullOrEmpty(saveString)) {
            saveString += itemId.ToString();
        } else {
            saveString += ("," + itemId.ToString());
        }
        PlayerPrefsManager.Instance.SaveParameter(PlayerPrefsManager.SaveType.HuntedItemList, saveString);

        //int uId = int.Parse(PlayerPrefsManager.Instance.GetParameter(PlayerPrefsManager.SaveType.UniqueIdIndex));
        //UniqueItemWrapper uItem = new UniqueItemWrapper(uId, equip);
        //uId++;

        //PlayerDataManager.Instance.AddItemToInventory(uItem);

        //PlayerPrefsManager.Instance.SaveParameter(PlayerPrefsManager.SaveType.UniqueIdIndex, uId.ToString());

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
