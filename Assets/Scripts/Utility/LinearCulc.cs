using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearCulc {
	private float EndCount;
	private float StartValue;
	private float EndValue;

	public LinearCulc(float endCount, float startValue, float endValue) {
		EndCount = endCount;
		StartValue = startValue;
		EndValue = endValue;
	}
	
	public LinearCulc() {
	}

	public void SetEndCount(float endCount) {
		EndCount = endCount;
	}
	
	public void SetStartValue(float val) {
		StartValue = val;
	}
	
	public void SetEndValue(float val) {
		EndValue = val;
	}

	public float GetValue(float nowCount) {
        float ratio = 1f;
        if (EndCount > 0f) {
            ratio = nowCount / EndCount;
        }
		float val = (EndValue-StartValue)*ratio;
		val = StartValue + val;

		return val;
	}
}
