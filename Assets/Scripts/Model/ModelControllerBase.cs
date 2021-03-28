using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelControllerBase : MonoBehaviour
{
	private readonly float MoveTime = 0.1f;
	private List<Vector2> MovePointList = new List<Vector2>();

	private LinearCulc XPosition = new LinearCulc();
	private LinearCulc ZPosition = new LinearCulc();
	
	private float MoveCounter = 999f;

    public void SelfUpdate(float deltaTime)
    {
        if (MoveCounter < MoveTime) {
			MoveCounter += deltaTime;
			if (MoveCounter >= MoveTime) {
				MoveCounter = MoveTime;
			}
			float x = XPosition.GetValue(MoveCounter);
			float z = ZPosition.GetValue(MoveCounter);

			Vector3 currentPosition = gameObject.transform.localPosition;
			gameObject.transform.localPosition = new Vector3(x, currentPosition.y, z);
		} else {
			if (MovePointList.Count > 0) {
				Vector2 nextPos = MovePointList[0];
				MovePointList.RemoveAt(0);
				Vector3 currentPosition = gameObject.transform.localPosition;

				XPosition.SetStartValue(currentPosition.x);
				XPosition.SetEndValue(nextPos.x);
				XPosition.SetEndCount(MoveTime);
				
				ZPosition.SetStartValue(currentPosition.z);
				ZPosition.SetEndValue(nextPos.y);
				ZPosition.SetEndCount(MoveTime);
				MoveCounter = 0f;
			}
		}
    }

	public void AddMovePoint(Vector2 point) {
		MovePointList.Add(point);
	}
	
	public void Clear(Vector2 point) {
		MovePointList.Add(point);
	}
}
