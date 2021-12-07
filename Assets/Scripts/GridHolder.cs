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
        LabelElements();
        SpaceElements();
    }

    public GridPoint[,] GetGrid(){
        return gridPoints;
    }

    public GridPoint[] GetNeighbors(int column, int row) {
        return null;
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

    void LabelElements(){
        for (int columnIndex = 0; columnIndex < columns; columnIndex++) {
            for (int rowIndex = 0; rowIndex < rows; rowIndex++) {
                GridPoint gridPoint = gridPoints[columnIndex, rowIndex].GetComponent<GridPoint>();
                gridPoint.SetGridPosition(columnIndex, rowIndex);
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
