using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FlickController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    public enum FlickResult {
		Success,
        Failure
    };

    private readonly float FlickReceptTime = 0.5f;
    private readonly float FlickReceptLength = 100f;

    private bool IsDownStart = false;
    private float FlickReceptCounter = 0f;
    private Action<FlickResult, float, float> FlickResultCallback = null;

    private Vector2 DownPosition = new Vector2();

    public void Initialize(Action<FlickResult, float, float> flickResultCallback)
    {
        IsDownStart = false;
        FlickReceptCounter = 0f;
        FlickResultCallback = flickResultCallback;
    }

    private void Update()
    {
        if (IsDownStart == true) {
            FlickReceptCounter += Time.deltaTime;
        }
    }

    public void OnPointerDown(PointerEventData data)
    {
        IsDownStart = true;
        FlickReceptCounter = 0f;

        DownPosition = data.pressPosition;
    }

    public void OnPointerUp(PointerEventData data)
    {
        IsDownStart = false;
        FlickResult flickResult = FlickResult.Failure;
        Vector2 pressPotision = data.pressPosition;
        Vector2 upPotision = data.position;
        float horizontalDistance = pressPotision.x - upPotision.x;
        float verticalDistance = pressPotision.y - upPotision.y;

        if (FlickReceptCounter < FlickReceptTime) {
            // とりあえず、X方向の長さだけ条件で見ているが、必要があればY方向も見る
            if (Math.Abs(horizontalDistance) >= FlickReceptLength) {
				flickResult = FlickResult.Success;
            }
            
			if (Math.Abs(verticalDistance) >= FlickReceptLength) {
				flickResult = FlickResult.Success;
            }
        }

        FlickResultCallback(flickResult, horizontalDistance, verticalDistance);
    }
}
