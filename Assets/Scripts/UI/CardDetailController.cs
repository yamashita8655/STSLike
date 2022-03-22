using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDetailController : MonoBehaviour
{
	[SerializeField]
	private Image RarityFrameImage = null;
	
	[SerializeField]
	private Image CardImage = null;
	
	[SerializeField]
	private Image UseTypeImage = null;
	
	[SerializeField]
	private Text CardName = null;
	
	[SerializeField]
	private Text CardDetail = null;

	[SerializeField]
	private Text Cost = null;

    private MasterAction2Table.Data Data = null;
    private Action<MasterAction2Table.Data> OnClickDetailBgCallback = null;

    public void Open(MasterAction2Table.Data data, Action<MasterAction2Table.Data> onClickDetailBgCallback) {
        Data = data;
        OnClickDetailBgCallback = onClickDetailBgCallback;

        gameObject.SetActive(true);

		ResourceManager.Instance.RequestExecuteOrder(
			string.Format(Const.RarityFrameImagePath, data.Rarity.ToString()),
			ExecuteOrder.Type.Sprite,
			this.gameObject,
			(rawSprite) => {
				RarityFrameImage.sprite = rawSprite as Sprite;
			}
		);
		
		ResourceManager.Instance.RequestExecuteOrder(
			data.ImagePath,
			ExecuteOrder.Type.Sprite,
			this.gameObject,
			(rawSprite) => {
				CardImage.sprite = rawSprite as Sprite;
			}
		);
		
		if (data.UseType == EnumSelf.UseType.Erase) {
			ResourceManager.Instance.RequestExecuteOrder(
				Const.UseTypeEraseImagePath,
				ExecuteOrder.Type.Sprite,
				this.gameObject,
				(rawSprite) => {
					UseTypeImage.sprite = rawSprite as Sprite;
				}
			);
		} else if (data.UseType == EnumSelf.UseType.Discard) {
			ResourceManager.Instance.RequestExecuteOrder(
				Const.UseTypeDiscardImagePath,
				ExecuteOrder.Type.Sprite,
				this.gameObject,
				(rawSprite) => {
					UseTypeImage.sprite = rawSprite as Sprite;
				}
			);
		} else {
			UseTypeImage.sprite = null;
		}
		

		CardName.text = data.Name;
		Cost.text = data.DiceCost.ToString();
		System.Object[] arguments = new System.Object[data.ActionPackList.Count];
		for (int i = 0; i < data.ActionPackList.Count; i++) {
			arguments[i] = data.ActionPackList[i].Value;
		}
		CardDetail.text = string.Format(data.Detail, arguments);
	}
	
	public void Close() {
		gameObject.SetActive(false);
	}
	
	public void OnClickCloseButton() {
		Close();
	}

    public void OnClickDetailBg()
    {
        OnClickDetailBgCallback(Data);
    }
}
