using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerController : MonoBehaviour
{
	[SerializeField]
	private Image PowerImage = null;
	
	[SerializeField]
	private Text PowerValue = null;
	
	public void Initialize(EnumSelf.PowerType type, int val, GameObject attachRoot) {

		string path = ConvertType2Path(type);

		ResourceManager.Instance.RequestExecuteOrder(
			path,
			ExecuteOrder.Type.Sprite,
			this.gameObject,
			(rawSprite) => {
				PowerImage.sprite = rawSprite as Sprite;
			}
		);

		PowerValue.text = val.ToString();

		transform.SetParent(attachRoot.transform);
		transform.localPosition = Vector3.zero;
		transform.localScale = Vector3.one;
	}

	private string ConvertType2Path(EnumSelf.PowerType type) {
		string ret = "";
		if (type == EnumSelf.PowerType.Strength) {
			ret = Const.StrengthImagePath;
		}

		return ret;
	}

}
