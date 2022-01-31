using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValueController : MonoBehaviour
{
	[SerializeField]
	private Image EffectImage = null;
	
	[SerializeField]
	private Text  EffectValue = null;
	
	public void Initialize(ActionPack pack, GameObject attachRoot) {

		string path = ConvertEffectType2Path(pack);

		ResourceManager.Instance.RequestExecuteOrder(
			path,
			ExecuteOrder.Type.Sprite,
			this.gameObject,
			(rawSprite) => {
				EffectImage.sprite = rawSprite as Sprite;
			}
		);

		EffectValue.text = pack.Value.ToString();

		transform.SetParent(attachRoot.transform);
		transform.localPosition = Vector3.zero;
		transform.localScale = Vector3.one;
	}

	private string ConvertEffectType2Path(ActionPack pack) {
		string ret = "";
		if (pack.Effect == EnumSelf.EffectType.Damage) {
			ret = Const.DamageImagePath;
		} else if (pack.Effect == EnumSelf.EffectType.Shield) {
			ret = Const.ShieldImagePath;
		} else if (pack.Effect == EnumSelf.EffectType.Heal) {
			ret = Const.HealImagePath;
		}

		return ret;
	}

}
