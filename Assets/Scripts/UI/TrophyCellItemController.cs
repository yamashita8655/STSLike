using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrophyCellItemController : MonoBehaviour
{
	[SerializeField]
	private Image AchieveImage = null;

    [SerializeField]
    private Text TrophyText = null;
    
	[SerializeField]
    private Text ProgressText = null;
	
	[SerializeField]
    private Text AchieveValueText = null;
	
	[SerializeField]
    private Button CellButton = null;

    private Action<TrophyCellItemController> Callback = null;

	private MasterTrophyTable.Data Data = null;

	public void Initialize(MasterTrophyTable.Data data, bool isAchieved, Action<TrophyCellItemController> callback) {

		Data = data;

		TrophyText.text = string.Format(data.Detail, data.CompleteCount);
		AchieveValueText.text = $"{data.RewardValue}";

		int maxProgressCount = data.CompleteCount;
		int nowProgressCount = 0;

		if (Data.Type == EnumSelf.TrophyCountType.DeleteEnemy) {
			int id = Data.Parameter;
			nowProgressCount = PlayerPrefsManager.Instance.GetEnemyKillCount(id);
		//} else if (Data.Type == EnumSelf.TrophyCountType.DeleteEnemy) {
		//	
		}
		
		ProgressText.text = $"{nowProgressCount}/{maxProgressCount}";

		if (Data.RewardType == EnumSelf.TrophyRewardType.CardCostUp) {
			ResourceManager.Instance.RequestExecuteOrder(
				Const.CardCostUpImagePath,
				ExecuteOrder.Type.Sprite,
				this.gameObject,
				(rawSprite) => {
					AchieveImage.sprite = rawSprite as Sprite;
				}
			);
		} else if (Data.RewardType == EnumSelf.TrophyRewardType.ArtifactCostUp) {
			ResourceManager.Instance.RequestExecuteOrder(
				Const.CardCostUpImagePath,
				ExecuteOrder.Type.Sprite,
				this.gameObject,
				(rawSprite) => {
					AchieveImage.sprite = rawSprite as Sprite;
				}
			);
		}
		
		if (isAchieved == true) {
			CellButton.interactable = false;
		} else {
			// 達成してるかチェック
			if (nowProgressCount >= maxProgressCount) {
				CellButton.interactable = true;
			} else {
				CellButton.interactable = false;
			}
		}

		Callback = callback;
	}

	public void UpdateCellButtonInteractable(bool interactable) {
		CellButton.interactable = interactable;
	}

	public void OnClick() {
		if (Callback != null) {
			Callback(this);
		}
	}
	
	public MasterTrophyTable.Data GetData() {
		return Data;
	}
}
