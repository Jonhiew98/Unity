using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    //prefabs
    public GameObject boidsPrefab;
    private GameObject flockBoid;

    //spawn num
    public int numBoids = 50;

    private float x, y;
    public int boidsRange;

    public List<GameObject> boids = new List<GameObject>();

    //spawnPos
    private Vector3 boidsPos;

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (boids.Count < numBoids)
            {
                x = Random.Range(-boidsRange, boidsRange);
                y = Random.Range(-boidsRange, boidsRange);
                boidsPos = new Vector3(x, y);
                flockBoid = Instantiate(boidsPrefab, boidsPrefab.transform.position + boidsPos, Quaternion.identity);
                boids.Add(flockBoid);
            }
        }
    }
}
