using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {
    public float health = 50;
    public Transform player;
    public float speed;
    private float move;

    public float attackRate = 1f;
    private float nextAttack;
    
    private void Update()
    {
        EnemyMove();
    }

    public void Damaged(float amount)
    {
        health -= amount;
        if(health <=0f)
        {
            Killed();
        }
    }

    void Killed()
    {
        Destroy(gameObject);
    }

    void EnemyMove()
    {
        move = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, player.position, move);
    }


    private void OnCollisionStay(Collision collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            Attack();
        }
    }

    void Attack()
    {
        if(Time.time > nextAttack)
        {
            nextAttack = Time.time + attackRate;
            PlayerHealth.curHealth -= 10f;
        }
    }
}
