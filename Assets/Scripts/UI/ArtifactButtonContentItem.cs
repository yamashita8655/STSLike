using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArtifactButtonContentItem : MonoBehaviour
{
	[SerializeField]
	private Image ArtifactImage = null;

	private Action<MasterArtifactTable.Data> Callback = null;

	private MasterArtifactTable.Data Data = null;

	public void Initialize(MasterArtifactTable.Data data, Action<MasterArtifactTable.Data> callback) {

		Data = data;

		ResourceManager.Instance.RequestExecuteOrder(
			data.ImagePath,
			ExecuteOrder.Type.Sprite,
			this.gameObject,
			(rawSprite) => {
				ArtifactImage.sprite = rawSprite as Sprite;
			}
		);
		
		Callback = callback;
	}
	
	public void OnClick() {
		if (Callback != null) {
			Callback(Data);
		}
	}
	
	public MasterArtifactTable.Data GetData() {
		return Data;
	}
}
