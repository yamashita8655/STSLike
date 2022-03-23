using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArtifactDetailController : MonoBehaviour
{
	[SerializeField]
	private Image ArtifactImage = null;
	
	[SerializeField]
	private Text ArtifactName = null;
	
	[SerializeField]
	private Text ArtifactDetail = null;

    private MasterArtifactTable.Data Data = null;
    private Action<MasterArtifactTable.Data> OnClickDetailBgCallback = null;

    public void Open(MasterArtifactTable.Data data, Action<MasterArtifactTable.Data> callback) {
        Data = data;
        OnClickDetailBgCallback = callback;

        gameObject.SetActive(true);

		//ResourceManager.Instance.RequestExecuteOrder(
		//	string.Format(FramePath, data.Rarity.ToString()),
		//	ExecuteOrder.Type.Sprite,
		//	this.gameObject,
		//	(rawSprite) => {
		//		RarityFrameImage.sprite = rawSprite as Sprite;
		//	}
		//);
		
		ResourceManager.Instance.RequestExecuteOrder(
			data.ImagePath,
			ExecuteOrder.Type.Sprite,
			this.gameObject,
			(rawSprite) => {
				ArtifactImage.sprite = rawSprite as Sprite;
			}
		);

		ArtifactName.text = data.Name;
		ArtifactDetail.text = data.Detail;
	}
	
	public void Close() {
		gameObject.SetActive(false);
	}
	
	public void OnClickCloseButton() {
		Close();
	}

    public void OnClickDetailBgButton()
    {
        OnClickDetailBgCallback(Data);
    }
}
