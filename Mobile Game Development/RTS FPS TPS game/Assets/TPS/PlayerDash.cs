using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour {  
    private bool dashing = false;
    private bool liftTap = false;

    //value for swipe dashing
    private float swipeStart;
    private float dashStart;
    public float turnRate = 5.0f;
    public float swipeDist = 3.0f;
    public float swipeDur = 0.2f;
    public float speed = 2.0f;
    private float dashDur = 0.5f;

    Vector3 tapPos1;
    Vector3 tapPos2;
    Vector3 lookAtTarget;
    Vector3 newDir = Vector3.zero;

    Quaternion playerRoot;
	
	// Update is called once per frame
	void Update () {
        SetTargetPosition();
        Dash();
    }

    void SetTargetPosition()
    {
        if(!dashing)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                playerRoot = transform.rotation;
                tapPos1 = Input.GetTouch(0).position;                
            }

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                tapPos2 = Input.GetTouch(0).position;
                lookAtTarget = new Vector3(tapPos2.x - tapPos1.x, transform.position.y, tapPos2.y - tapPos1.y);
                if (liftTap)
                {
                    swipeStart = Time.time;
                    liftTap = false;
                }
                playerRoot = Quaternion.LookRotation(lookAtTarget);
            }

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                liftTap = true;
                if (Time.time <= swipeStart + swipeDur && !dashing && Vector3.Distance(tapPos1, tapPos2) >= swipeDist)
                {
                    dashing = true;
                    dashStart = Time.time;
                }               
            }
            transform.rotation = Quaternion.Slerp(transform.rotation, playerRoot, turnRate * Time.deltaTime);
        }
    }

    void Dash()
    {
        if(dashing)
        {
            Vector3 dir = tapPos2 - tapPos1;
            newDir.x = dir.x * 100;
            newDir.z = dir.y * 100;

            if(Time.time <= dashStart + dashDur)
            {
                transform.position = Vector3.MoveTowards(transform.position, newDir, speed * Time.deltaTime);
            }
            else
            {
                dashing = false;
            }           
        }       
    }
}
