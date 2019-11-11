using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour {
    public GameObject player;
    Vector3 playerPos;
    public float speed;

	// Use this for initialization
	void Start () {
        playerPos = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        transform.LookAt(playerPos);
    }
	
	// Update is called once per frame
	void Update () {
        transform.position += transform.forward * speed * Time.deltaTime;
        DestroySelf();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            PlayerHealth.curHealth -= 10f;
            Destroy(this.gameObject);
        }
    }

    void DestroySelf()
    {
        Destroy(this.gameObject, 2f);
    }
}
