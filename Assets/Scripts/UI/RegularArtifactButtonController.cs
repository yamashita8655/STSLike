using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegularArtifactButtonController : MonoBehaviour
{
    [SerializeField]
    private Image ArtifactImage = null;

    [SerializeField]
    private Text EquipCostText = null;

    private MasterArtifactTable.Data Data = null;

	private Action<MasterArtifactTable.Data> ClickCallback = null;
	private Action<RegularArtifactButtonController> DetailClickCallback = null;

	public void Initialize(
		MasterArtifactTable.Data data,
		Action<MasterArtifactTable.Data> clickCallback,
		Action<RegularArtifactButtonController> detailClickCallback
	) {
		Data = data;
		ClickCallback = clickCallback;
		DetailClickCallback = detailClickCallback;

		UpdateDisplay();
	}

	private void UpdateDisplay() {
		ResourceManager.Instance.RequestExecuteOrder(
			string.Format(Data.ImagePath),
			ExecuteOrder.Type.Sprite,
			this.gameObject,
			(rawSprite) => {
                ArtifactImage.sprite = rawSprite as Sprite;
			}
		);

        EquipCostText.text = Data.EquipCost.ToString();
	}

	public void OnClick() {
		if (ClickCallback != null) {
			ClickCallback(Data);
		}
	}
	
	public void OnDetailClick() {
		if (DetailClickCallback != null) {
			DetailClickCallback(this);
		}
	}

	public MasterArtifactTable.Data GetData() {
		return Data;
	}
}
