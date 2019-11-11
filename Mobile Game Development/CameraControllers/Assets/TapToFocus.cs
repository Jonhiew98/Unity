using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapToFocus : MonoBehaviour {
    Ray ray;
    Ray centerRay;
    RaycastHit hit;
    RaycastHit centerHit;
    private Vector3 targetPos;
    private Vector3 offSet;
    private Vector3 centerPoint;


    void Start()
    {
        centerPoint = Vector3.zero;
        centerPoint.x = Screen.width / 2;
        centerPoint.y = Screen.height / 2;
        centerRay = Camera.main.ScreenPointToRay(centerPoint);
        if (Physics.Raycast(centerRay, out centerHit))
        {
            offSet = transform.position - centerHit.point;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            FocusObject();
        }
        else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            DragCamera();
        }
    }

    void FocusObject()
    {
        ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        if (Physics.Raycast(ray, out hit))
        {
            targetPos = hit.point + offSet;
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 100f);
        }
    }

    void DragCamera()
    {
       Vector3 direction = new Vector3(Input.GetTouch(0).deltaPosition.x, 0f, Input.GetTouch(0).deltaPosition.y);
       transform.Translate(direction,Space.World);
    }
}
