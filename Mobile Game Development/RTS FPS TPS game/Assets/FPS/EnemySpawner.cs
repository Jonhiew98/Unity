using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    public float spawnRate = 2f;
    private float nextSpawn;

    public GameObject Enemy;
    private GameObject clone;

	// Update is called once per frame
	void Update () {
		if(Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnRate;
            Instantiate(Enemy, transform.position, transform.rotation);
        }
	}
}
