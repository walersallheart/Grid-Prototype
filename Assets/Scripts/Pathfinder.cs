using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    GridHolder gridHolder;

    private void Awake()
    {
        gridHolder = GameObject.FindObjectOfType<GridHolder>();
    }

    public Transform[] GetPath(int fromX, int fromY, int toX, int toY)
    {
        Queue<GridPoint> queue = new Queue<GridPoint>();

        GridPoint start = gridHolder.GetGridPoint(fromX, fromY);
        GridPoint end = gridHolder.GetGridPoint(toX, toY);

        start.visited = true;

        /*Debug.ClearDeveloperConsole();
        Debug.Log("Start:" + start.ToString());
        Debug.Log("End:" + end.ToString());*/

        queue.Enqueue(start);

        // While there is node to be handled in the queue
        while (queue.Count != 0)
        {
            // Handle the node in the front of the lineand get unvisited neighbors
            GridPoint curNode = queue.Dequeue();

            // Terminate if the goal is reached
            if (curNode == end) {
                break;
            }

            // Take neighbors, set its parent, mark as visited, and add to the queue
            GridPoint[] neighbors = GetUnvisitedNeighbors(curNode);

            if (curNode.GetX() == 6 && curNode.GetY() == 3) {
                Debug.Log("curNode->"+curNode.ToString());
                Debug.Log("neighbors.Length=>" + neighbors.Length);
                Debug.Log(neighbors[0].ToString());
            }

            for (int i = 0; i < neighbors.Length; i++){
                neighbors[i].visited = true;
                neighbors[i].parentNode = curNode;
                queue.Enqueue(neighbors[i]);
            }
        }

        Transform[] pathArray = GetPathToDestination(end);
        Array.Reverse(pathArray);

        string debugPath = "";

        foreach(Transform path in pathArray) {
            debugPath += path.GetComponent<GridPoint>().ToString() + "====";
        }

        Debug.Log(debugPath);

        return pathArray;
    }

    Transform[] GetPathToDestination(GridPoint endPoint) {
        List<Transform> path = new List<Transform>();

        GridPoint currentPoint = endPoint;

        path.Add(currentPoint.GetComponent<Transform>());

        while(currentPoint.parentNode != null) {
            currentPoint = currentPoint.parentNode;
            path.Add(currentPoint.GetComponent<Transform>());
        }

        return path.ToArray();
    }

    GridPoint[] GetUnvisitedNeighbors(GridPoint gridPoint) {
        List<GridPoint> unvisitedNeighbors = new List<GridPoint>();

        if (gridPoint.GetX() == 6 && gridPoint.GetY() == 3) {
            Debug.Log("gridPoint.childNodes.Length->"+gridPoint.childNodes.Length);
        }

        foreach(GridPoint neighbor in gridPoint.childNodes){
            if (neighbor.GetX() == 6 && neighbor.GetY() == 3) {
                Debug.Log("gridPoint.childNodes.Length->"+gridPoint.childNodes.Length);
                Debug.Log("neighbor.visited->"+neighbor.visited);
                Debug.Log("neighbor.IsEnabled->"+neighbor.IsEnabled());
            }

            if (!neighbor.visited && neighbor.IsEnabled()) {
                unvisitedNeighbors.Add(neighbor);
            }
        }

        if (gridPoint.GetX() == 6 && gridPoint.GetY() == 3) {
            Debug.Log("unvisitedNeighbors.Count=>" + unvisitedNeighbors.Count);
        }

        return unvisitedNeighbors.ToArray();
    }

    public void ResetVisitedPoints()
    {
        foreach (GridPoint gridPoint in gridHolder.GetGrid())
        {
            gridPoint.visited = false;
            gridPoint.parentNode = null;
        }
    }
}
