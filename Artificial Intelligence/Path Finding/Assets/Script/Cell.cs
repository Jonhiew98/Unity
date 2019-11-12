using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Cell : MonoBehaviour {
    private Transform node;
    private Transform startNode;
    private Transform endNode;
    private List<Transform> wall = new List<Transform>();

    private bool pressedStart, pressedEnd,pressedWall,removeWall = false;
    public InputField AstarInput;

    SpriteRenderer sr;
    RaycastHit2D hit;

    // Update is called once per frame
    void Update () {
        DetectMousePos();
	}

    void DetectMousePos()
    {
        if (Input.GetMouseButton(0))
        {
            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {              
                if (pressedStart)
                {
                    if (startNode != null)
                    {
                        //remove previous
                        sr = node.GetComponent<SpriteRenderer>();
                        sr.color = Color.white;

                        //change hit colour
                        node = hit.transform;
                        sr = node.GetComponent<SpriteRenderer>();
                        sr.color = Color.green;
                        startNode = node;
                        pressedStart = false;
                    }
                    else
                    {
                        //change hit colour
                        node = hit.transform;
                        sr = node.GetComponent<SpriteRenderer>();
                        sr.color = Color.green;
                        startNode = node;
                        pressedStart = false;
                    }
                    
                }        
                else if(pressedEnd)
                { 
                    if (endNode != null)
                    {
                        //remove previous
                        sr = node.GetComponent<SpriteRenderer>();
                        sr.color = Color.white;

                        //change hit colour
                        node = hit.transform;
                        sr = node.GetComponent<SpriteRenderer>();
                        sr.color = Color.red;
                        endNode = node;
                        pressedEnd = false;
                    }
                    else
                    {
                        //change hit colour
                        node = hit.transform;
                        sr = node.GetComponent<SpriteRenderer>();
                        sr.color = Color.red;
                        endNode = node;
                        pressedEnd = false;
                    }                   
                }
                else if(pressedWall)
                {
                    if (node == null || node != null)
                    {
                        //set colour to black
                        node = hit.transform;
                        sr = node.GetComponent<SpriteRenderer>();
                        sr.color = Color.grey;

                        //disable path / block
                        Node checkNode = node.GetComponent<Node>();
                        checkNode.setPass(false);

                        //add wall
                        wall.Add(node);
                        node = null;
                    }
                }
                else if(removeWall)
                {
                    //Set colour to white
                    node = hit.transform;
                    sr = node.GetComponent<SpriteRenderer>();
                    sr.color = Color.white;

                    // Set selected not to walkable.
                    Node checkNode = node.GetComponent<Node>();
                    checkNode.setPass(true);

                    // Remove Wall
                    wall.Remove(node);
                    node = null;
                }
            }                
        }       
    }

    public void btnNodeStart()
    {
        pressedStart = true;
        pressedEnd = false;
        pressedWall = false;
        removeWall = false;
    }

    public void btnNodeEnd()
    {
        pressedStart = false;
        pressedEnd = true;
        pressedWall = false;
        removeWall = false;
    }

    public void btnAddWall()
    { 
        pressedStart = false;
        pressedEnd = false;
        pressedWall = true;
        removeWall = false;
    }

    public void btnRemoveWall()
    {
        pressedStart = false;
        pressedEnd = false;
        pressedWall = false;
        removeWall = true;
    }

    public void btnFindDiagonalPath()
    {
        CellManager.isDiagonal = true;
        FindDijkstraPath();
    }

    public void btnFindNoDiagonalPath()
    {
        CellManager.isDiagonal = false;
        FindDijkstraPath();
    }

    public void FindDijkstraPath()
    {
        if (startNode != null && endNode != null)
        {
            // Execute Shortest Path.
            FindPath finder = gameObject.GetComponent<FindPath>();
            List<Transform> paths = finder.findDijkstraPath(startNode, endNode);

            foreach (Transform path in paths)
            {
                Renderer sr = path.GetComponent<Renderer>();
                sr.material.color = Color.yellow;
            }
        }
    }

    public void FindAStarPath()
    {
        if (startNode != null && endNode != null)
        {
            int weightValue = int.Parse(AstarInput.text);

            // Execute Shortest Path.
            FindPath finder = gameObject.GetComponent<FindPath>();
            List<Transform> paths = finder.findAstarPath(startNode, endNode,weightValue);

            foreach (Transform path in paths)
            {
                Renderer sr = path.GetComponent<Renderer>();
                sr.material.color = Color.yellow;
            }
        }
    }

    public void btnReset()
    {  
        SceneManager.LoadScene(0);
    }
}
