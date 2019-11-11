using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerMovement : MonoBehaviour {
    public float speed ;
    public Transform BulletPosition;
    public GameObject bullet;
    public AudioClip auClip;

    private float nextFire;
    public float fireRate = 0.5f;

	// Update is called once per frame
	void Update () {
        Movement();
        Attack();
    }

    void Movement()
    {
        float y = Input.GetAxis("Vertical");
        float x = Input.GetAxis("Horizontal");

        Vector2 direction = new Vector2(x, y).normalized;
        Move(direction);
    }

    void Move(Vector2 direction)
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        max.x = max.x - 0.225f;
        min.x = min.x + 0.225f;

        max.y = max.y - 0.285f;
        min.y = min.y + 0.285f;

        Vector2 pos = transform.position;

        pos += direction * speed * Time.deltaTime;

        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);

        transform.position = pos;
    }

    void Attack()
    {
        if (Input.GetKey("space"))
        {
            if(Time.time > nextFire)
            {                      
                nextFire = Time.time + fireRate;
                Instantiate(bullet, BulletPosition.position, transform.rotation);
                AudioSource shoot = GetComponent<AudioSource>();
                shoot.PlayOneShot(auClip,0.7f);
            }
           
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("EnemyBullet"))
        {
            PlayerLives.livesValue -= 1;
            PlayerScores.scoresValue += 10;

            if (PlayerLives.livesValue == 0)
            {
                Destroy(this.gameObject);
                FindObjectOfType<GameManager>().EndGame();
            }     
        }
    }
}
