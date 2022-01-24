using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsHitComponent : MonoBehaviour
{
    private Action<Collision> OnCollisionCallback = null;

    // Start is called before the first frame update
    
    /// <summary>
    /// オブジェクトとの判定をとりたい時
    /// </summary>
    /// <param name="callback">コールバック関数</param>
    public void CollisionInitilize(Action<Collision> callback)
    {
        OnCollisionCallback = callback;
    }

    void OnCollisionEnter(Collision collision)
    {
        OnCollisionCallback(collision);
    }
}
