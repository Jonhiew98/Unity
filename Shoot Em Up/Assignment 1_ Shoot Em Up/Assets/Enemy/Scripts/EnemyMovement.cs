using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class EnemyMovement : MonoBehaviour {
    public float enemySpeed;

    public float fireRate;
    private float nextFire;

    public GameObject EnemyBullet;
    public Transform shootPosition;
    public GameObject Player;

	// Update is called once per frame
	void Update () {
        EnemyMove();
        Attack();
    }

    void EnemyMove()
    {
        Vector2 position = transform.position;
        position = new Vector2(position.x, position.y - enemySpeed * Time.deltaTime);
        transform.position = position;

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        if (transform.position.y < min.y)
        {
            PlayerLives.livesValue -= 1;
            Destroy(gameObject);

            if (PlayerLives.livesValue == 0)
            {
                Destroy(GameObject.Find("Player"));
                FindObjectOfType<GameManager>().EndGame();
            }

        }
    }

    void Attack()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(EnemyBullet, shootPosition.position, transform.rotation);
            AudioSource shoot = GetComponent<AudioSource>();
            shoot.Play();
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Bullet"))
        {
            PlayerScores.scoresValue += 20;
            Destroy(this.gameObject);
            if (PlayerLives.livesValue == 0)
            {
                Destroy(GameObject.Find("Player"));
                FindObjectOfType<GameManager>().EndGame();
            }
        }
        if (other.gameObject.CompareTag("EnemyBullet"))
        {
            Physics2D.IgnoreCollision(other.collider, this.gameObject.GetComponent<BoxCollider2D>());
        }
    }
}
