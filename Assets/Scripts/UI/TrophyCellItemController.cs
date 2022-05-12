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
		
		Callback = callback;

		UpdateDisplay(isAchieved);
	}

	public void UpdateDisplay(bool isAchieved) {
		TrophyText.text = string.Format(Data.Detail, Data.CompleteCount);
		AchieveValueText.text = $"{Data.RewardValue}";

		int maxProgressCount = Data.CompleteCount;
		int nowProgressCount = 0;

		if (Data.Type == EnumSelf.TrophyCountType.DeleteEnemy) {
			int id = Data.Parameter;
			nowProgressCount = PlayerPrefsManager.Instance.GetEnemyKillCount(id);
		} else if (Data.Type == EnumSelf.TrophyCountType.HealCount) {
			nowProgressCount = PlayerPrefsManager.Instance.GetHealCount();
		} else if (Data.Type == EnumSelf.TrophyCountType.DiceCostUpCount) {
			nowProgressCount = PlayerPrefsManager.Instance.GetDiceCostUpCount();
		} else if (Data.Type == EnumSelf.TrophyCountType.EraseCount) {
			nowProgressCount = PlayerPrefsManager.Instance.GetEraseCount();
		} else if (Data.Type == EnumSelf.TrophyCountType.FindRarityCard) {
			int rarity = Data.Parameter;
			nowProgressCount = PlayerPrefsManager.Instance.GetFindRarityCardCount(rarity);
		} else if (Data.Type == EnumSelf.TrophyCountType.FindRarityArtifact) {
			int rarity = Data.Parameter;
			nowProgressCount = PlayerPrefsManager.Instance.GetFindRarityArtifactCount(rarity);
		} else if (Data.Type == EnumSelf.TrophyCountType.GetStrength) {
			nowProgressCount = PlayerPrefsManager.Instance.GetStrength();
		} else if (Data.Type == EnumSelf.TrophyCountType.GetShield) {
			nowProgressCount = PlayerPrefsManager.Instance.GetShield();
		} else if (Data.Type == EnumSelf.TrophyCountType.DeckCount) {
			nowProgressCount = PlayerPrefsManager.Instance.GetDeckCount();
		} else if (Data.Type == EnumSelf.TrophyCountType.GiveDamage) {
			nowProgressCount = PlayerPrefsManager.Instance.GetGiveDamage();
		} else if (Data.Type == EnumSelf.TrophyCountType.MaxHp) {
			nowProgressCount = PlayerPrefsManager.Instance.GetMaxHp();
		} else if (Data.Type == EnumSelf.TrophyCountType.HP1Win) {
			nowProgressCount = PlayerPrefsManager.Instance.GetHP1Win();
		} else if (Data.Type == EnumSelf.TrophyCountType.NoDamageBoss) {
			nowProgressCount = PlayerPrefsManager.Instance.GetNoDamageBoss();
		} else if (Data.Type == EnumSelf.TrophyCountType.NoDamageElite) {
			nowProgressCount = PlayerPrefsManager.Instance.GetNoDamageElite();
		} else if (Data.Type == EnumSelf.TrophyCountType.DoubleKO) {
			nowProgressCount = PlayerPrefsManager.Instance.GetDoubleKO();
		}
		
		ProgressText.text = $"{nowProgressCount}/{maxProgressCount}";

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
