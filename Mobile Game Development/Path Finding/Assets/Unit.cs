using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour {
    //public Transform[] target;
    public Transform target;
    NavMeshAgent agent;
    WaitForSeconds delay = new WaitForSeconds(0.3f);
    NavMeshPath path;
    LineRenderer line;

    [SerializeField] Rect rect;
    Vector2 startPoint;
    [SerializeField] Texture tex;
    bool isDrawing = false;

    public Transform formation;

    List<Transform> points;

    [SerializeField] List<GameObject> selectedUnits = new List<GameObject>();

    // Use this for initialization
    void Start() {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(FindPathRoutine());
        line = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        RTScontroller();
        GetCurrentPath();
        //line.positionCount = path.corners.Length; // set size of line points;

        //Vector3[] modifiedCorners = path.corners;
        //for (int i = 0; i < modifiedCorners.Length; i++)
        //{
        //    modifiedCorners[i].y += 1f;
        //}

        //line.SetPositions(modifiedCorners); // pass all corner positions to line points;
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
            agent.SetDestination(target.position);
        }
    }

    //private void OnDrawGizmos()
    //{
    //    if (agent != null && agent.path != null)
    //    {

    //        for (int i = 0; i < path.corners.Length - 1; i++)
    //        {
    //            Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);
    //        }
    //    }
    //}

    void RTScontroller()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDrawing = true;
            startPoint = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            rect = new Rect(
                startPoint.x,
                startPoint.y,
                Input.mousePosition.x - startPoint.x,
                Input.mousePosition.y - startPoint.y
                );
        }
        if (Input.GetMouseButtonUp(0))
        {
            isDrawing = false;
            GameObject[] gos = GameObject.FindGameObjectsWithTag("Unit");
            selectedUnits = new List<GameObject>();

            foreach (GameObject go in gos)
            {
                Vector3 pos = Camera.main.WorldToScreenPoint(go.transform.position);

                if (rect.Contains(pos, true))
                {
                    selectedUnits.Add(go);
                    go.GetComponent<Renderer>().material.color = Color.cyan;
                }
                else
                {
                    go.GetComponent<Renderer>().material.color = Color.white;
                }
            }
        }
    }

    void OnGUI()
    {
        if (isDrawing)
        {
            Rect texRect = rect;
            texRect.Set(rect.x, Screen.height - rect.y, rect.width, -rect.height);
            GUI.DrawTexture(texRect, tex);
        }
    }

    //void testing()
    //{
    //    formation.transform.SetParent(target[i]);
    //    formation.transform.rotation = target[i].transform.rotation;
    //    formation.MakeThemFollowMeh(target[]);

    //    for (int i = 0; i < target.Length; i++)
    //    {
    //        target[i].SetDestination(points[i.position]);
    //    }
    //}


}
