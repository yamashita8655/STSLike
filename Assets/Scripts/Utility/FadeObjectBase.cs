using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeObjectBase : MonoBehaviour {
    [SerializeField]
    private Image CuFadeImage = null;
	protected Image FadeImage => CuFadeImage;
    
	virtual public void Initialize()
    {
    }

    virtual public void UpdateFade(float range)
    {
        //image.material.SetFloat("_Range", 1 - range);
    }
}
