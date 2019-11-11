using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {
    public float damage = 10f;
    public float range = 100f;
    public float impactForce = 30f;
    public GameObject hitEffect;
    private GameObject effectClone;

    public float fireRate = 2f;
    private float nextFire;

    public Camera cam;

    private bool isPress = false;

    RaycastHit hit;
	// Update is called once per frame
	void Update () {
        if(isPress && Time.time >= nextFire)
        {
            nextFire = Time.time + 1f / fireRate;
            PlayerShoot();
        }
	}

    public void PlayerShoot()
    {      
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            EnemyBehaviour enemy = hit.transform.GetComponent<EnemyBehaviour>();
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
            if (enemy != null)
            {
                enemy.Damaged(damage);
            }

            effectClone = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(effectClone, 1f);
        }       
    }

    public void onPress()
    {
        isPress = true;
    }

    public void onRelease()
    {
        isPress = false;
    }
}
