using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualPadController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler {

	private Vector2 StartPotision = Vector3.zero;

	private float Degree = 0f;

    private Action PointerDownCallback = null;
    private Action<float> MoveCallback = null;
	private Action PointerUpCallback = null;

	public void Initialize(Action pointerDownCallback, Action<float> callback, Action pointerUpCallback)
	{
        MoveCallback = callback;
		PointerUpCallback = pointerUpCallback;
        PointerDownCallback = pointerDownCallback;
        Degree = 0f;
	}

	private void Update()
	{
	}

	public void OnPointerDown(PointerEventData data)
	{
		StartPotision = data.pressPosition;
        PointerDownCallback();
    }

	public void OnDrag(PointerEventData data)
	{
		Vector2 now = data.position;

		float x = StartPotision.x - now.x;
		float y = StartPotision.y - now.y;

		float radian = Mathf.Atan2(x, y);
		Degree = radian * 180f / 3.1415f;
		if (Degree < 0f) {
			Degree += 360f;
		}

		MoveCallback(Degree);
	}

	public void OnPointerUp(PointerEventData data)
	{
		PointerUpCallback();
	}
}
