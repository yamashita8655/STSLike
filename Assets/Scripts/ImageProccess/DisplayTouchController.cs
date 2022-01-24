using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DisplayTouchController : MonoBehaviour, IPointerDownHandler {

    private Action<Vector2> PointerDownCallback = null;

	public void Initialize(Action<Vector2> pointerDownCallback)
	{
        PointerDownCallback = pointerDownCallback;
	}

	public void OnPointerDown(PointerEventData data)
	{
        PointerDownCallback(data.pressPosition);
    }
}
