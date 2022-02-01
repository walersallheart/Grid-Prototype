using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridHolder : MonoBehaviour
{
    GridPoint[,] gridPoints;
    int columns = 13;
    int rows = 7;

    void Start()
    {
        gridPoints = new GridPoint[columns, rows];

        PopulateGridArray();
        PopulateChildren();
        LabelElements();
        SpaceElements();
    }

    public GridPoint[,] GetGrid(){
        return gridPoints;
    }

    public GridPoint[] GetNeighbors(int column, int row) {
        return null;
    }

    public Transform GetTransform(int x, int y) {
        return gridPoints[x,y].transform;
    }

    public GridPoint GetGridPoint(int x, int y) {
        return gridPoints[x,y];
    }

    void PopulateGridArray() {
        GridPoint[] tempGridPoints = transform.GetComponentsInChildren<GridPoint>();

        int rowIndex = 0;
        int columnIndex = 0;

        foreach (GridPoint gridPoint in tempGridPoints)
        {
            gridPoints[columnIndex,rowIndex] = gridPoint;
            //Debug.Log(gridPoint.gameObject.name);

            columnIndex++;

            if (columnIndex >= columns) {
                columnIndex = 0;
                rowIndex++;
            }
        }
    }

    void PopulateChildren() {
        for (int columnIndex = 0; columnIndex < columns; columnIndex++) {
            for (int rowIndex = 0; rowIndex < rows; rowIndex++) {
                GridPoint gridPoint = gridPoints[columnIndex, rowIndex].GetComponent<GridPoint>();
                gridPoint.SetGridPosition(columnIndex, rowIndex);
            }
        }
    }

    void LabelElements(){
        List<GridPoint> childPoints = new List<GridPoint>();

        for (int columnIndex = 0; columnIndex < columns; columnIndex++) {
            for (int rowIndex = 0; rowIndex < rows; rowIndex++) {
                GridPoint gridPoint = gridPoints[columnIndex, rowIndex].GetComponent<GridPoint>();
                gridPoint.SetGridPosition(columnIndex, rowIndex);
                childPoints.Clear();

                if (columnIndex - 1 > 0) {
                    childPoints.Add(gridPoints[columnIndex-1, rowIndex]);
                }

                if (columnIndex + 1 <columns) {
                    childPoints.Add(gridPoints[columnIndex+1, rowIndex]);
                }

                if (rowIndex - 1 > 0) {
                    childPoints.Add(gridPoints[columnIndex, rowIndex-1]);
                }

                if (rowIndex + 1 <rows) {
                    childPoints.Add(gridPoints[columnIndex, rowIndex + 1]);
                }

                gridPoint.childNodes = childPoints.ToArray();

                /*if (columnIndex == 0 && rowIndex == 0) {
                    Debug.Log("---------------------------");
                    Debug.Log("Node 0,0 neighbors");
                    Debug.Log("---------------------------");
                    Debug.Log("gridPoint.childNodes.Length=>"+gridPoint.childNodes.Length);

                    foreach(GridPoint point in gridPoint.childNodes) {
                        Debug.Log(point.ToString());
                    }
                    Debug.Log("---------------------------");
                }*/
            }
        }
    }

    void SpaceElements(){
        float startingX = gridPoints[0,0].transform.position.x;
        float startingY = gridPoints[0,0].transform.position.y;

        float horizontalSpacing = 1.35f;
        float verticalSpacing = 1.34f;

        float currentX = startingX;
        float currentY = startingY;

        GridPoint currentGridPoint;
        for (int rowIndex = 0; rowIndex < rows; rowIndex++) {
            for (int columnIndex = 0; columnIndex < columns; columnIndex++) {
                currentGridPoint = gridPoints[columnIndex, rowIndex];

                currentGridPoint.GetComponent<RectTransform>().anchoredPosition = new Vector2(currentX, currentY);

                //Debug.Log(new Vector2(currentX, currentY));

                currentX += horizontalSpacing;
            }

            currentX = startingX;
            currentY -= verticalSpacing;
        }
    }
}
