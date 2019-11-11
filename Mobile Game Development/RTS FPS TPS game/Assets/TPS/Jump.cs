using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour {
    public bool onFloor= false;
    public bool canJump = false;
    public float jumpHeight = 100f;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update () {     
        playerJump();
	}

    public void onClick()
    {     
        if(onFloor)
        {
            canJump = true;
        }  
      
    }

    void playerJump()
    {     
        if (canJump)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpHeight);
            canJump = false;
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            onFloor = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        onFloor = false;
    }
}
