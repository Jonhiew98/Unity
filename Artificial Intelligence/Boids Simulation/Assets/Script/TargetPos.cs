using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPos : MonoBehaviour {

    private Vector3 targetPos;
    public bool isSeek = false;
    public bool isFlee = false;

	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            isSeek = true;
            isFlee = false;
            targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPos.z = 0;
            transform.position = targetPos;
        }
        else if (Input.GetMouseButtonDown(1))
        {
            isSeek = false;
            isFlee = true;
            targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPos.z = 0;
            transform.position = targetPos;
        }
    }
}
