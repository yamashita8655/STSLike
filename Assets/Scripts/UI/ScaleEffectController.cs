using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleEffectController : MonoBehaviour
{
	[SerializeField]
	private Image ScaleEffectImage = null;

	private LinearCulc XScale = new LinearCulc();
	private LinearCulc YScale = new LinearCulc();
	private LinearCulc Alpha = new LinearCulc();

	private float PassTime = 0f;
	private float Timer = 0f;
	private bool Destroy = false;
	private bool Loaded = false;

	public void Initialize(
		string imagePath,
		GameObject parent,
		float startScale,
		float endScale,
		float time
	) {
		XScale.SetStartValue(startScale);
		XScale.SetEndValue(endScale);
		XScale.SetEndCount(time);
		
		YScale.SetStartValue(startScale);
		YScale.SetEndValue(endScale);
		YScale.SetEndCount(time);
		
		Alpha.SetStartValue(1f);
		Alpha.SetEndValue(0f);
		Alpha.SetEndCount(time);

		Timer = time;

		Loaded = false;

		ResourceManager.Instance.RequestExecuteOrder(
			imagePath,
			ExecuteOrder.Type.Sprite,
			gameObject,
			(rawSprite) => {
				ScaleEffectImage.sprite = rawSprite as Sprite;
				Loaded = true;
			}
		);
		Destroy = false;
		PassTime = 0f;
	}

	public void UpdateSelf(float deltaTime) {
		if (Loaded == true)
		{
			PassTime += deltaTime;
			float xVal = XScale.GetValue(PassTime);
			float yVal = YScale.GetValue(PassTime);
			float alpha = Alpha.GetValue(PassTime);
			gameObject.transform.localScale = new Vector3(xVal, yVal, 1f);
			ScaleEffectImage.color = new Color(1f, 1f, 1f, alpha);
			if (PassTime >= Timer)
			{
				Destroy = true;
			}
		}
	}
	
	public bool CanDestroy() {
		return Destroy;
	}
}
