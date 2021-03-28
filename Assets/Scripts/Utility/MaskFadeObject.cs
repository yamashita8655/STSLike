using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaskFadeObject : FadeObjectBase {

    override public void UpdateFade(float range)
    {
        FadeImage.material.SetFloat("_Range", 1 - range);
    }
}
