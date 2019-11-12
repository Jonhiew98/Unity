using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour {
    public Transform mouse;
    public bool isAlive = false;
    private bool tempIsAlive = false;
    SpriteRenderer sr;

    // Use this for initialization
    void Start () {
        mouse = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        sr = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        DetectMousePos();
        ChangeCellColour();
	}

    void DetectMousePos()
    {
        if (mouse.transform.position.x > this.transform.position.x - 0.5f && mouse.transform.position.x < this.transform.position.x + 0.5f &&
            mouse.transform.position.y > this.transform.position.y - 0.5f && mouse.transform.position.y < this.transform.position.y + 0.5f)
        {
            if (Input.GetMouseButton(0))
            {
                if (!isAlive)
                {
                    isAlive = true;
                }                
            }
            if (Input.GetMouseButton(1))
            {
                if(isAlive)
                {
                    isAlive = false;
                }
            }
        }
    }

    void ChangeCellColour()
    {
        if(isAlive)
        {
            sr.color = Color.cyan;
        }
        else
        {
            sr.color = Color.white;
        }
    }
}
