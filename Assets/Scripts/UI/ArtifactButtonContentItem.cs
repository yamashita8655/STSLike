using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArtifactButtonContentItem : MonoBehaviour
{
	[SerializeField]
	private Image ArtifactImage = null;

	private readonly string ImagePath = "Image/UI/Artifact/{0}";

	private Action<MasterArtifactTable.Data> Callback = null;

	private MasterArtifactTable.Data Data = null;

	public void Initialize(MasterArtifactTable.Data data, Action<MasterArtifactTable.Data> callback) {

		Data = data;

		ResourceManager.Instance.RequestExecuteOrder(
			string.Format(ImagePath, data.ImagePath),
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
}
