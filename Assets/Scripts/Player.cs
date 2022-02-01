using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float speed = 3.5f;
    int currentX = 0;
    int currentY = 0;

    Pathfinder pathfinder;

    Transform[] path;
    int currentPoint;

    Vector2 target;
    bool moving = false;

    private void Awake() {
        pathfinder = GetComponent<Pathfinder>();
    }

    public void TravelTo(int targetX, int targetY) {
        Debug.Log("TravelTo("+targetX+","+targetY+")");
        Debug.Log("TravelFrom("+currentX+","+currentY+")");

        path = pathfinder.GetPath(currentX, currentY, targetX, targetY);
        currentX = targetX;
        currentY = targetY;

        currentPoint = 0;
        target = new Vector2(path[currentPoint].position.x, path[currentPoint].position.y);
        moving = true;

        /*Debug.Log(path);*/
        Debug.Log("path.Length->" + path.Length);

        foreach(Transform point in path) {
            Debug.Log(point.GetComponent<GridPoint>().ToString());
            //Debug.Log("X: " + point.position.x + ", Y:" + point.position.y);
        }
    }

    void Update()
    {
        if (moving) {
            float step = speed * Time.deltaTime;

            // move sprite towards the target location
            transform.position = Vector2.MoveTowards(transform.position, target, step);

            float distanceToTarget = Vector2.Distance(target, new Vector2(transform.position.x, transform.position.y));

            if (distanceToTarget < 0.1f) {
                currentPoint++;

                if (currentPoint < path.Length) {
                    target = new Vector2(path[currentPoint].position.x, path[currentPoint].position.y);
                } else {
                    moving = false;
                    pathfinder.ResetVisitedPoints();
                }
            }
        }
    }
}
