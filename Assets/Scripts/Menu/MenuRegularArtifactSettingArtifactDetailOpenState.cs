using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuRegularArtifactSettingArtifactDetailOpenState : StateBase {

    /// <summary>
    /// メイン前処理.
    /// 戻り値は、同一フレーム内で次の処理に移行してよければfalse、1フレーム飛ばして欲しい場合はfalse.
    /// </summary>
    override public bool OnBeforeMain()
    {
		var scene = MenuDataCarrier.Instance.Scene as MenuScene;
		scene.ArtifactDetailRoot.SetActive(true);

		var item = MenuDataCarrier.Instance.SelectArtifactContentItem;
		var data = item.GetData();

		scene.ArtifactDetailNameText.text = data.Name;
		scene.ArtifactDetailDetailText.text = data.Detail;
		
		ResourceManager.Instance.RequestExecuteOrder(
			data.ImagePath,
			ExecuteOrder.Type.Sprite,
			scene.gameObject,
			(rawSprite) => {
				scene.ArtifactDetailImage.sprite = rawSprite as Sprite;
			}
		);
		
		ResourceManager.Instance.RequestExecuteOrder(
			string.Format(Const.RarityFrameImagePath, data.Rarity.ToString()),
			ExecuteOrder.Type.Sprite,
			scene.gameObject,
			(rawSprite) => {
				scene.ArtifactDetailRarityFrameImage.sprite = rawSprite as Sprite;
			}
		);

		if (PlayerPrefsManager.Instance.IsUnlockArtifact(data.Id) == false) {
			// TODO ここで、アンロックしていない時のボタンとか表示を行う
			scene.ArtifactDetailUnlockButton.gameObject.SetActive(true);
			scene.ArtifactDetailEquipButton.gameObject.SetActive(false);
			scene.ArtifactDetailCostText.text = data.UnlockCost.ToString();

			int carryPoint = PlayerPrefsManager.Instance.GetPoint();

			if (carryPoint >= data.UnlockCost) {
				scene.ArtifactDetailUnlockButton.interactable = true;
			} else {
				scene.ArtifactDetailUnlockButton.interactable = false;
			}
		} else {
			scene.ArtifactDetailUnlockButton.gameObject.SetActive(false);
			scene.ArtifactDetailEquipButton.gameObject.SetActive(true);
			scene.ArtifactDetailCostText.text = data.EquipCost.ToString();
			
			int nowCost = scene.GetNowEquipCost();
			int maxCost = scene.GetMaxCost();

			if ((nowCost+data.EquipCost) <= maxCost) {
				scene.ArtifactDetailEquipButton.interactable = true;
			} else {
				scene.ArtifactDetailEquipButton.interactable = false;
			}
		}

		return false;
    }

    /// <summary>
    /// メイン更新処理.
    /// </summary>
    /// <param name="delta">経過時間</param>
    override public void OnUpdateMain(float delta)
    {
		StateMachineManager.Instance.ChangeState(StateMachineName.Menu, (int)MenuState.RegularArtifactSettingUserWait);
    }

    /// <summary>
    /// ステート解放時処理.
    /// </summary>
    override public void OnRelease()
    {
    }
}
