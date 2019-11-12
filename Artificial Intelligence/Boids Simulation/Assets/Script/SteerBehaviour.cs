using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteerBehaviour : MonoBehaviour
{
    // vector for boids(seek & flee)
    private Vector3 velocity;
    private Vector3 steer;
    private Vector3 desireVelocity;

    //collision avoidance
    GameObject obstacles;
    private Vector3 avoidance;
    float range = 3f;
    float maxAvoidForce = 20f;

    //seek & flee physics
    public float maxSpeed = 50;
    public float maxVelocity = 10f;
    public float maxForce = 5;
    public float mass = 10;

    //flock radius
    public float boidsRadius = 2f;

    //script
    Spawner spawner;

    // target 
    GameObject target;

    //list for boids & obstacles
    List<GameObject> neighbourBoids = new List<GameObject>();

    void Start()
    {
        spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawner>();
        target = GameObject.FindGameObjectWithTag("Target");
        obstacles = GameObject.FindGameObjectWithTag("Obstacles");
        neighbourBoids = spawner.boids;
    }
    
    void Update()
    {
        SteeringBehaviour();
        boidsFlock();
    }

    void Seek()
    {
        desireVelocity = Vector3.Normalize(target.transform.position - transform.position) * maxVelocity;
        steer = desireVelocity - velocity;

        steer += collisionAvoidance();
        steer = Vector3.ClampMagnitude(steer, maxForce);
        steer = steer / mass;

        velocity = Vector3.ClampMagnitude(velocity + steer * Time.deltaTime, maxSpeed);
        transform.position += velocity;
    }

    void Flee()
    {
        desireVelocity = Vector3.Normalize(transform.position - target.transform.position) * maxVelocity;
        steer = desireVelocity - velocity;

        steer += collisionAvoidance();
        steer = Vector3.ClampMagnitude(steer, maxForce);
        steer = steer / mass;

        velocity = Vector3.ClampMagnitude(velocity + steer * Time.deltaTime, maxSpeed);
        transform.position += velocity;
    }

    void SteeringBehaviour()
    {
        if(target.GetComponent<TargetPos>().isSeek && !target.GetComponent<TargetPos>().isFlee)
        {
            Seek();
        }
        else if(!target.GetComponent<TargetPos>().isSeek && target.GetComponent<TargetPos>().isFlee)
        {
            Flee();
        }
    }

    void boidsFlock()
    {
        Vector3 averagePos = Vector3.zero;
        Vector3 averageDir = Vector3.zero;
        Vector3 averageVelocity;
        Vector3 averageDirection;

        if (neighbourBoids.Count == 0)
        {
            averageDir = Vector3.zero;
        }

        foreach (GameObject flockBoids in neighbourBoids)
        {
            float flockDistance = Vector3.Distance(flockBoids.transform.position, transform.position);
            if (flockDistance < boidsRadius)
            {
                //cohesian
                averagePos += flockBoids.transform.position;
                averagePos /= neighbourBoids.Count;
                averageVelocity = Vector3.Normalize(averagePos - transform.position);

                //alignment
                averageDir +=  velocity;
                averageDir /= neighbourBoids.Count;
                averageDirection = averageDir.normalized;

                // seperation
                desireVelocity = Vector3.Normalize(transform.position - flockBoids.transform.position);
                steer = desireVelocity - velocity;

                steer = Vector3.ClampMagnitude(steer, maxForce);
                steer = steer / mass;

                velocity = Vector3.ClampMagnitude(velocity + steer * Time.deltaTime, maxSpeed);
                transform.position += velocity + (averageVelocity/20) + (averageDirection/5);
            }
        }
    }

    private Vector3 collisionAvoidance()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, range);
        //Debug.DrawRay(transform.position, transform.right * range, Color.green);

        avoidance = Vector3.zero;

        if (hit)
        {           
            if (hit.collider.CompareTag("Obstacles"))
            {
                Vector3 ahead = transform.position + velocity;               
                avoidance = Vector3.Normalize(ahead - hit.collider.transform.position);
                avoidance *= maxAvoidForce;
            }   
        } 
        else
        {
            avoidance *= 0;
        }
     
        return avoidance;
    }



}
