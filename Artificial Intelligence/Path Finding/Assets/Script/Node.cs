using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {
    [SerializeField] private float weight = int.MaxValue;
    [SerializeField] private float cost = int.MaxValue;
    [SerializeField] private float heuristic = 0;
    [SerializeField] private Transform parentNode = null;
    [SerializeField] private List<Transform> neighbourNode;
    [SerializeField] private bool pass = true;

    public void resetNode()
    {
        weight = int.MaxValue;
        cost = int.MaxValue;
        heuristic = 0;
        parentNode = null;
    }

    public void setWeight(float value)
    {
        weight = value;
    }

    public float getWeight()
    {
        float result = weight;
        return result;
    }

    public void setCost(float value)
    {
        cost = value;
    }

    public float getCost()
    {
        float result = cost;
        return result;
    }

    public void setHeuristic(float value)
    {
        heuristic = value;
    }

    public float getHeuristic()
    {
        float result = heuristic;
        return result;
    }

    public void setParentNode(Transform node)
    {
        parentNode = node;
    }

    public void addNeighbourNode(Transform node)
    {
        this.neighbourNode.Add(node);
    }

    public List<Transform> getNeighbourNode()
    {
        List<Transform> result = this.neighbourNode;
        return result;
    }

    public Transform getParentNode()
    {
        Transform result = this.parentNode;
        return result;
    }

    public void setPass(bool value)
    {
        pass = value;
    }

    public bool canPass()
    {
        bool result = pass;
        return result;
    }
}
