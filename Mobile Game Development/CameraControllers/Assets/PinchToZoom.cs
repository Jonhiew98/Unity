using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinchToZoom : MonoBehaviour {

    public float perspectiveZoomSpeed = 0.5f;
    Vector2 prevTouchPos1;
    Vector2 prevTouchPos2;
    private float prevTouchMagnitude;
    private float touchMagnitude;
    private float deltaMagnitudeDiff;

	void Update () {
		if(Input.touchCount == 2)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            prevTouchPos1 = touch1.position - touch1.deltaPosition;
            prevTouchPos2 = touch2.position - touch2.deltaPosition;

            prevTouchMagnitude = (prevTouchPos1 - prevTouchPos2).magnitude;
            touchMagnitude = (touch1.position - touch2.position).magnitude;

            deltaMagnitudeDiff = prevTouchMagnitude - touchMagnitude;

            Camera.main.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;
            Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, 0.1f, 179.9f);
            
        }
	}
}
