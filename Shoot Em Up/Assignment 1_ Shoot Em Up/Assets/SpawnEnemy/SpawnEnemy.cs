using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour {

    public GameObject[] Enemy;
    float randX;
    Vector2 whereToSpawn;
    float maxSpawnRate = 5f;
    float spawnTime;
    int randEnemy;

	// Use this for initialization
	void Start () {
        Invoke("EnemySpawn", maxSpawnRate);
        InvokeRepeating("IncreaseSpawnRate", 0f, 30f);
	}
	
    void EnemySpawn()
    {
        randEnemy = Random.Range(0, 2);
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        randX = Random.Range(min.x,max.x);
        whereToSpawn = new Vector2(randX, max.y);
        Instantiate(Enemy[randEnemy], whereToSpawn, transform.rotation);

        nextSpawnEnemy();
    }

    void nextSpawnEnemy()
    {
        if(maxSpawnRate > 1f)
        {
            spawnTime = Random.Range(1f, maxSpawnRate);
        }
        else
        {
            spawnTime = 1f;
        }
        Invoke("EnemySpawn", spawnTime);
    }

    void IncreaseSpawnRate()
    {
        if(maxSpawnRate >1f)
        {
            maxSpawnRate--;
        }
        if (maxSpawnRate == 1f)
        {
            CancelInvoke("IncreaseSpawnRate");
        }
    }
}
