using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuRegularCardSettingEquipCardDetailOpenState : StateBase {

    /// <summary>
    /// メイン前処理.
    /// 戻り値は、同一フレーム内で次の処理に移行してよければfalse、1フレーム飛ばして欲しい場合はfalse.
    /// </summary>
    override public bool OnBeforeMain()
    {
		var scene = MenuDataCarrier.Instance.Scene as MenuScene;
		scene.CardDetailRoot.SetActive(true);

		var data = MenuDataCarrier.Instance.EquipCardSelectData;
		
		scene.CardDetailNameText.text = data.Name;

		System.Object[] arguments = new System.Object[data.ActionPackList.Count];
		for (int i = 0; i < data.ActionPackList.Count; i++) {
			arguments[i] = data.ActionPackList[i].Value;
		}
		scene.CardDetailDetailText.text = string.Format(data.Detail, arguments);
		
		ResourceManager.Instance.RequestExecuteOrder(
			data.ImagePath,
			ExecuteOrder.Type.Sprite,
			scene.gameObject,
			(rawSprite) => {
				scene.CardDetailImage.sprite = rawSprite as Sprite;
			}
		);
		
		ResourceManager.Instance.RequestExecuteOrder(
			string.Format(Const.RarityFrameImagePath, data.Rarity.ToString()),
			ExecuteOrder.Type.Sprite,
			scene.gameObject,
			(rawSprite) => {
				scene.CardDetailRarityFrameImage.sprite = rawSprite as Sprite;
			}
		);

		scene.CardDetailDetailText.text = string.Format(data.Detail, arguments);
			
		scene.CardDetailUnlockButton.gameObject.SetActive(false);
		scene.CardDetailEquipButton.gameObject.SetActive(false);
		scene.CardDetailCostText.text = data.EquipCost.ToString();

		return false;
    }

    /// <summary>
    /// メイン更新処理.
    /// </summary>
    /// <param name="delta">経過時間</param>
    override public void OnUpdateMain(float delta)
    {
		StateMachineManager.Instance.ChangeState(StateMachineName.Menu, (int)MenuState.RegularCardSettingUserWait);
    }

    /// <summary>
    /// ステート解放時処理.
    /// </summary>
    override public void OnRelease()
    {
    }
}
