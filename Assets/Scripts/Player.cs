using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    int currentX = 1;
    int currentY = 1;

    Pathfinder pathfinder;

    Transform[] path;

    private void Awake() {
        pathfinder = GetComponent<Pathfinder>();
    }

    public void TravelTo(int targetX, int targetY) {
        path = pathfinder.GetPath(currentX, currentY, targetX, targetY);

        Debug.Log(path);
    }
}
