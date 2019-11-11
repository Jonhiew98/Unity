using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public Transform player;
    public float offSet = -10f;
    Vector3 cameraPos;

	// Update is called once per frame
	void Update () {
        FollowPlayer();
	}

    void FollowPlayer()
    {
        cameraPos = transform.position;
        cameraPos.x = player.position.x;
        cameraPos.z = player.position.z + offSet;
        transform.position = cameraPos;
    }
}
