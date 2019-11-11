using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelCollider : MonoBehaviour {
    [HideInInspector] public SpriteRenderer spr;
    [SerializeField] private PixelCollider other;

    bool hasCollision = false;
    bool stayCollision = false;
 
    public Rect preview = new Rect();
    public Color[] colorPreview = new Color[1000];

    private float startStay = 0;

	// Use this for initialization
	void Start () {
        this.spr = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        CheckCollision();
        ShowFeedback();		
	}

    void CheckCollision()
    {
        if(this.spr.bounds.Intersects(other.spr.bounds))
        {
            //1.Get global intersection
            Rect globalInt = new Rect();
            globalInt.xMin = Mathf.Max(this.spr.bounds.min.x, other.spr.bounds.min.x);
            globalInt.xMax = Mathf.Min(this.spr.bounds.max.x, other.spr.bounds.max.x);
            globalInt.yMin = Mathf.Max(this.spr.bounds.min.y, other.spr.bounds.min.y);
            globalInt.yMax = Mathf.Min(this.spr.bounds.max.y, other.spr.bounds.max.y);

            preview = globalInt;

            //2.Get this sprite's local intersection
            Rect localSect = new Rect();
            localSect.xMin = globalInt.xMin - this.spr.bounds.min.x;
            localSect.yMin = globalInt.yMin - this.spr.bounds.min.y;
            localSect.width = globalInt.width;
            localSect.height = globalInt.height;

            //3.Get OTHER sprite's local intersection
            Rect otherSect = new Rect();
            otherSect.xMin = globalInt.xMin - other.spr.bounds.min.x;
            otherSect.yMin = globalInt.yMin - other.spr.bounds.min.y;
            otherSect.width = globalInt.width;
            otherSect.height = globalInt.height;

            //4.Get THIS sprite's COLOR ARRAY
            Color[] localColor = this.spr.sprite.texture.GetPixels(
            Mathf.RoundToInt(localSect.xMin),
            Mathf.RoundToInt(localSect.yMin),
            Mathf.RoundToInt(localSect.width),
            Mathf.RoundToInt(localSect.height)
            );

            this.colorPreview = localColor;

            //5.Get OTHER sprite's COLOR ARRAY
            Color[] otherColor = other.spr.sprite.texture.GetPixels(
            Mathf.RoundToInt(otherSect.xMin),
            Mathf.RoundToInt(otherSect.yMin),
            Mathf.RoundToInt(otherSect.width),
            Mathf.RoundToInt(otherSect.height)
             );

            //6.Compare both color Arrays in a FOR LOOP
            for(int i=0; i< localColor.Length && i< otherColor.Length; i++)
            {
                //7.CHECK for collisions!
                if(localColor[i].a ==1f && otherColor[i].a ==1f)
                {
                    hasCollision = true;
                    return;
                }
            }
            hasCollision = false;
        }
        else
        {
            this.hasCollision = false;
        }

        spr.sprite.texture.GetPixels();
    }

    void ShowFeedback()
    {
        if (this.hasCollision)
        {
            this.spr.color = Color.red;
            stayCollision = true;
            if (stayCollision)
            {
                startStay += Time.deltaTime;
                if (startStay >= 0.5)
                {
                    this.spr.color = Color.yellow;
                }
            }
        }
        else
        {
            this.spr.color = Color.white;
            stayCollision = false;
            startStay = 0;   
        }
    }
}
