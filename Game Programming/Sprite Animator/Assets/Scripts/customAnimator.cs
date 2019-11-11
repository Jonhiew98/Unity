using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class customAnimator : MonoBehaviour {

    [SerializeField] int curFrame = 0;
	[SerializeField] int fps = 30;
	[SerializeField] int rowCount = 2;
	[SerializeField] int colCount = 8;

	float spriteWidth = 0f;
    float spriteHeight = 0f;
    Renderer rend;
	Vector2 offSet = Vector2.zero;
	float timeBetweenFrames;
	float currentTime = 0f;

    int select = 0;
    bool Reversing = false;


	// Use this for initialization
	void Start () {
		rend  = this.GetComponent<Renderer> ();

		spriteWidth = 1f / (float)colCount;
        spriteHeight = 1f / (float)rowCount;
    }
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            select = 1;
            Break();
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            select = 2;
            Break();
        }

        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            select = 3;
            Break();
        }

        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            select = 4;
            Break();
        }

        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            select = 5;
            Break();
        }

        if(select == 1)
        {
            Loop();
        }
        else if( select == 2)
        {
            OneShot();
        }
        else if( select == 3)
        {
            Reverse();
        }
        else if( select == 4)
        {
            PingPong();
        }
        else if (select == 5)
        {
            MultipleRows();
        }
    }

    void Loop()
    {
        timeBetweenFrames = 1f / (float)fps;
        if (currentTime < timeBetweenFrames)
        {
            currentTime += Time.deltaTime;
        }
        else
        {
            currentTime = 0f;
            curFrame++;
        }

        offSet.x = curFrame * spriteWidth;
        rend.material.SetTextureOffset("_MainTex", offSet);
    }

    void OneShot()
    {
        timeBetweenFrames = 1f / (float)fps;

        if (currentTime < timeBetweenFrames)
        {
            currentTime += Time.deltaTime;
        }
        else
        {
            if (curFrame <= colCount)
            {
                currentTime = 0f;
                curFrame++;
            }
        }

        offSet.x = curFrame * spriteWidth;
        rend.material.SetTextureOffset("_MainTex", offSet);
    }

    void Reverse()
    {
        timeBetweenFrames = 1f / (float)fps;

        if (currentTime < timeBetweenFrames)
        {
            currentTime += Time.deltaTime;
        }
        else
        {
            currentTime = 0f;
            curFrame--;
        }

        offSet.x = curFrame * spriteWidth;
        rend.material.SetTextureOffset("_MainTex", offSet);
    }

    void PingPong()
    {
        timeBetweenFrames = 1f / (float)fps;

        if (currentTime < timeBetweenFrames)
        {
            currentTime += Time.deltaTime;
        }
        else
        {
            if (!Reversing)
            {
                if (curFrame >= colCount)
                {
                    Reversing = true;
                }
                currentTime = 0f;
                curFrame++;
            }
            else if (Reversing)
            {
                if (curFrame <= 0)
                {
                    Reversing = false;
                }
                currentTime = 0f;
                curFrame--;
            }
        }
        offSet.x = curFrame * spriteWidth;
        rend.material.SetTextureOffset("_MainTex", offSet);
    }

    void MultipleRows()
    {
        timeBetweenFrames = 1f / (float)fps;

        if (currentTime < timeBetweenFrames)
        {
            currentTime += Time.deltaTime;
        }
        else
        {
            currentTime = 0f;
            curFrame++;
        }
        
        if (curFrame >= colCount)
        {
            curFrame = 0;
            offSet.y += spriteHeight;
        }
        offSet.x = curFrame * spriteWidth;
        rend.material.SetTextureOffset("_MainTex", offSet);
    }

    void Break()
    {
        curFrame = 0;
        Vector2 offset = Vector2.zero;
        currentTime = 0f;
    }
}
