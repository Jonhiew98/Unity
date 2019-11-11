using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class followPlayer : MonoBehaviour {
    public float speed;
    private GameObject player;
    public GameObject Enemy;
    private Vector3 playerPos;
    private Vector3 direction;
    public AudioClip auClip;

    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player");
        playerPos = player.transform.position;
        direction = (playerPos - transform.position).normalized;
    }
	
	// Update is called once per frame
	void Update () {
        EnemyMove();
	}

    void EnemyMove()
    {
        transform.position += direction * speed * Time.deltaTime;
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        if((transform.position.x < min.x) || (transform.position.x > max.x) || (transform.position.y < min.y) || (transform.position.y > max.y))
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

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Bullet"))
        {
            PlayerScores.scoresValue += 15;
            AudioSource explode = GetComponent<AudioSource>();
            explode.Play();
            Invoke("Despawn", 0.35f);

            if (PlayerLives.livesValue == 0)
            {
                explode.Play();
                Invoke("Despawn", 0.35f);
                FindObjectOfType<GameManager>().EndGame();
            }
        }
        else if(other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("EnemyBullet"))
        {
            Physics2D.IgnoreCollision(other.collider, this.gameObject.GetComponent<BoxCollider2D>());
        }
        
    }

    void Despawn()
    {
        Destroy(this.gameObject);
    }
}


