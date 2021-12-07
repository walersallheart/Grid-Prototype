using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    GridHolder gridHolder;

    private void Awake() {
        gridHolder = GameObject.FindObjectOfType<GridHolder>();
    }

    public Transform[] GetPath(int fromX, int fromY, int toX, int toY){
        if (fromX == toX && fromY == toY) {
            return null;
        }

        Transform[] path = new Transform[1];

        return path;
    }

    void ResetVisitedPoints(){
        foreach (GridPoint gridPoint in gridHolder.GetGrid()) {
            gridPoint.visited = false;
        }
    }
}
