using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPath : MonoBehaviour {
    private GameObject[] nodes;

    public List<Transform> findDijkstraPath(Transform start, Transform end)
    {
        nodes = GameObject.FindGameObjectsWithTag("Cell");

        List<Transform> result = new List<Transform>();
        Transform node = DijkstrasAlgo(start, end);

        // While there's still previous node, we will continue.
        while (node != null)
        {
            result.Add(node);
            Node currentNode = node.GetComponent<Node>();
            node = currentNode.getParentNode();
        }

        // Reverse the list so that it will be from start to end.
        result.Reverse();
        return result;
    }

    public List<Transform> findAstarPath(Transform start, Transform end,int weight)
    {
        nodes = GameObject.FindGameObjectsWithTag("Cell");

        List<Transform> result = new List<Transform>();
        Transform node = AStarAlgo(start, end, weight);

        // While there's still previous node, we will continue.
        while (node != null)
        {
            result.Add(node);
            Node currentNode = node.GetComponent<Node>();
            node = currentNode.getParentNode();
        }

        // Reverse the list so that it will be from start to end.
        result.Reverse();
        return result;
    }

    private Transform DijkstrasAlgo(Transform start, Transform end)
    {
        List<Transform> path = new List<Transform>();

        // We add all the nodes we found into unexplored.
        foreach (GameObject obj in nodes)
        {
            Node nodeObj = obj.GetComponent<Node>();
            if(nodeObj.canPass())
            {
                nodeObj.resetNode();
                path.Add(obj.transform);
            }              
        }

        Node startNode = start.GetComponent<Node>();
        startNode.setWeight(0);

        while (path.Count != 0)
        {
            path.Sort((x, y) => x.GetComponent<Node>().getWeight().CompareTo(y.GetComponent<Node>().getWeight()));

            Transform current = path[0];
            path.Remove(current);

            if(current == end)
            {
                return end;
            }

            Node currentNode = current.GetComponent<Node>();
            List<Transform> neighbours = currentNode.getNeighbourNode();
            foreach (Transform neighNode in neighbours)
            {
                Node node = neighNode.GetComponent<Node>();
                if (path.Contains(neighNode) && node.canPass())
                {
                    float distance = Vector2.Distance(neighNode.position, current.position);
                    distance = currentNode.getWeight() + distance;

                    if (distance < node.getWeight())
                    {                       
                        node.setWeight(distance);                      
                        node.setParentNode(current);
                        node.transform.GetComponent<Renderer>().material.color = Color.cyan;
                    }                 
                }
            }
        }
        return end;
    }

    private Transform AStarAlgo(Transform start, Transform end,int weight)
    {
        List<Transform> path = new List<Transform>();

        // We add all the nodes we found into unexplored.
        foreach (GameObject obj in nodes)
        {
            Node nodeObj = obj.GetComponent<Node>();
            if (nodeObj.canPass())
            {
                nodeObj.resetNode();
                path.Add(obj.transform);
            }
        }

        Node startNode = start.GetComponent<Node>();
        startNode.setWeight(0);
        startNode.setCost(startNode.getWeight() + startNode.getHeuristic());

        while (path.Count != 0)
        {
            path.Sort((x, y) => x.GetComponent<Node>().getCost().CompareTo(y.GetComponent<Node>().getCost()));

            Transform current = path[0];
            
            if (current == end)
            {
                return end;
            }

            path.Remove(current);

            Node currentNode = current.GetComponent<Node>();
            List<Transform> neighbours = currentNode.getNeighbourNode();
            foreach (Transform neighNode in neighbours)
            {
                Node node = neighNode.GetComponent<Node>();
                if (path.Contains(neighNode) && node.canPass())
                {
                    float distance = Vector2.Distance(neighNode.position, current.position);
                    node.setHeuristic(Vector2.Distance(neighNode.position, end.position) * weight);
                    distance = currentNode.getWeight() + distance;
                    
                    if (distance < node.getWeight())
                    {
                        node.setWeight(distance);
                        node.setCost(node.getHeuristic() + node.getWeight());
                        node.setParentNode(current);                        
                        node.transform.GetComponent<Renderer>().material.color = Color.cyan;
                    }
                }
            }
        }
        return end;
    }
}
