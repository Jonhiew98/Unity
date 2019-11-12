using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class CellManager : MonoBehaviour {

    //grid
    public List<Transform> grid = new List<Transform>();
    public Transform nodePrefab;

    //grid size
    public int cellWidth = 30;
    public int cellHeight = 30;

    //Generator
    public Transform generator;

    //others
    private int counter;

    public static bool isDiagonal = true;

    // Use this for initialization
    public void Start() {
        generateGrid();
        generateNeighbours();
    }

    void generateGrid()
    {
        counter = 0;
        for (int i = 0; i < cellHeight; i++)
        {
            for (int j = 0; j < cellWidth; j++)
            {
                Transform node = Instantiate(nodePrefab, generator);
                node.transform.position = new Vector2(j * node.localScale.x, i * node.localScale.z) - new Vector2(cellWidth * node.localScale.x / 2, cellHeight * node.localScale.z / 2);
                node.name = "node (" + counter + ")";
                grid.Add(node);
                counter++;
            }
        }
    }

    void generateNeighbours()
    {
        if (isDiagonal)
        {
            DiagonalNeighbour();
        }

        else if (!isDiagonal)
        {
            NoDiagonalNeighbour();
        }
    }

    public void DiagonalNeighbour()
    {
        for (int i = 0; i < grid.Count; i++)
        {
            Node currentNode = grid[i].GetComponent<Node>();
            int index = i + 1;

            // For those on the left, with no left neighbours
            if (index % cellWidth == 1)
            {
                // We want the node at the top as long as there is a node.
                if (i + cellWidth < cellWidth * cellHeight)
                {
                    currentNode.addNeighbourNode(grid[i + cellWidth]);   // North node 
                }

                if (i - cellWidth >= 0)
                {
                    currentNode.addNeighbourNode(grid[i - cellWidth]);   // South node
                }

                currentNode.addNeighbourNode(grid[i + 1]);  // East node
            }

            // For those on the right, with no right neighbours
            else if (index % cellWidth == 0)
            {
                // We want the node at the top as long as there is a node.
                if (i + cellWidth < cellWidth * cellHeight)
                {
                    currentNode.addNeighbourNode(grid[i + cellWidth]);   // North node
                }

                if (i - cellWidth >= 0)
                {
                    currentNode.addNeighbourNode(grid[i - cellWidth]);   // South node
                }
                currentNode.addNeighbourNode(grid[i - 1]);     // West node
            }

            else
            {
                // We want the node at the top as long as there is a node.
                if (i + cellWidth < cellWidth * cellHeight)
                {
                    currentNode.addNeighbourNode(grid[i + cellWidth]);   // North node
                    currentNode.addNeighbourNode(grid[(i + cellWidth + 1)]);
                    currentNode.addNeighbourNode(grid[(i + cellWidth - 1)]);
                }

                if (i - cellWidth >= 0)
                {
                    currentNode.addNeighbourNode(grid[i - cellWidth]);   // South node
                    currentNode.addNeighbourNode(grid[(i - cellWidth + 1)]);
                    currentNode.addNeighbourNode(grid[(i - cellWidth - 1)]);
                }
                currentNode.addNeighbourNode(grid[i + 1]);     // East node
                currentNode.addNeighbourNode(grid[i - 1]);     // West node
            }
        }
    }

    public void NoDiagonalNeighbour()
    {
        for (int i = 0; i < grid.Count; i++)
        {
            Node currentNode = grid[i].GetComponent<Node>();
            int index = i + 1;

            // For those on the left, with no left neighbours
            if (index % cellWidth == 1)
            {
                // We want the node at the top as long as there is a node.
                if (i + cellWidth < cellWidth * cellHeight)
                {
                    currentNode.addNeighbourNode(grid[i + cellWidth]);   // North node 
                }

                if (i - cellWidth >= 0)
                {
                    currentNode.addNeighbourNode(grid[i - cellWidth]);   // South node
                }

                currentNode.addNeighbourNode(grid[i + 1]);  // East node
            }

            // For those on the right, with no right neighbours
            else if (index % cellWidth == 0)
            {
                // We want the node at the top as long as there is a node.
                if (i + cellWidth < cellWidth * cellHeight)
                {
                    currentNode.addNeighbourNode(grid[i + cellWidth]);   // North node
                }

                if (i - cellWidth >= 0)
                {
                    currentNode.addNeighbourNode(grid[i - cellWidth]);   // South node
                }
                currentNode.addNeighbourNode(grid[i - 1]);     // West node
            }

            else
            {
                // We want the node at the top as long as there is a node.
                if (i + cellWidth < cellWidth * cellHeight)
                {
                    currentNode.addNeighbourNode(grid[i + cellWidth]);   // North node

                }

                if (i - cellWidth >= 0)
                {
                    currentNode.addNeighbourNode(grid[i - cellWidth]);   // South node

                }
                currentNode.addNeighbourNode(grid[i + 1]);     // East node
                currentNode.addNeighbourNode(grid[i - 1]);     // West node
            }
        }        
    }
}
