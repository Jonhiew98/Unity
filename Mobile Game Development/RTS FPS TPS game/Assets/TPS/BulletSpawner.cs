using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour {
    private float spawnRate;
    private float nextSpawn;

    public GameObject bullet;

    // Update is called once per frame
    void Update () {
		if(Time.time > nextSpawn)
        {
            spawnRate = Random.Range(3, 6);
            nextSpawn = Time.time + spawnRate;
            Instantiate(bullet, transform.position, transform.rotation); 
        }
	}
}
