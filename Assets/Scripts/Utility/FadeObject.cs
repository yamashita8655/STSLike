using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeObject : MonoBehaviour {
    [SerializeField]
    private Image image = null;

    public void UpdateMaskCutout(float range)
    {
        image.material.SetFloat("_Range", 1 - range);
    }
}
