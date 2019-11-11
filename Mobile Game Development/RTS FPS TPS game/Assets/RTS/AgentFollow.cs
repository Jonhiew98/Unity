using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentFollow : MonoBehaviour {
    public bool moveSelected = false;
    public static bool followPress = false;
    public static bool patrolPress = false;
    public bool reached = false;
    public Transform endTarget;
    public Transform target;
    Material unit;
    NavMeshAgent agent;
    WaitForSeconds delay = new WaitForSeconds(0.3f);
    NavMeshPath path;
    private int desPoint = 0;

    List<Transform> points;
    Vector3 unitPos;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        unit = GetComponent<Renderer>().material;
        unitPos = new Vector3(agent.transform.position.x, agent.transform.position.y, agent.transform.position.z);
    }

    private void OnMouseDown()
    {
        if (!moveSelected)
        {            
            unit.color = Color.cyan;
            moveSelected = true;
            UnitNumber.unitCount++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetCurrentPath();
        StartCoroutine(FindPathRoutine());
        StartCoroutine(Patrol());
    }

    void GetCurrentPath()
    {
        path = agent.path;
    }

    IEnumerator FindPathRoutine()
    {
        while (true)
        {
            yield return delay; // wait for x second
            //yield return null; // retirm single frame    
            if (moveSelected && followPress)
            {
                patrolPress = false;
                agent.SetDestination(target.position);
            }
        }
    }

    IEnumerator Patrol()
    {
        while(true)
        {
            yield return delay; // wait for x second
            if (moveSelected && patrolPress)
            {
                followPress = false;
                //yield return new WaitForSeconds(5);
                agent.SetDestination(endTarget.position);
                yield return new WaitForSeconds(6f);
                agent.SetDestination(unitPos);
            }
        }
        
    }
}
