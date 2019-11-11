using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMoving : MonoBehaviour
{
    public GameObject[] Star;
    public int MaxStars;
    private float speed;
    float randX;
    float randY;
    Vector2 whereToSpawn;

    int randStar;

    // Use this for initialization
    void Start()
    {
        Invoke("SpawnStar",0f);
    }

    void SpawnStar()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        for (int i = 0; i < MaxStars; i++)
        {
            randStar = Random.Range(0, 4);
            GameObject star = Instantiate(Star[randStar]);
            randX = Random.Range(min.x, max.x);
            randY = Random.Range(min.y, max.y);
            whereToSpawn = new Vector2(randX, randY);
            star.transform.position = whereToSpawn;
            starMovement.speed = (1f * Random.value + 0.5f);
            star.transform.parent = transform;
        }
    }
}
