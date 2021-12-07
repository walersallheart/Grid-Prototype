using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GridPoint : MonoBehaviour, IPointerClickHandler
{
    int x;
    int y;
    bool gridPointEnabled = true;

    public bool visited = false;

    Coroutine toggleCoroutine;

    public void SetGridPosition(int x, int y) {
        this.x = x;
        this.y = y;

        GetComponent<GridPointLabel>().SetLabel(x.ToString() + "," + y.ToString());
    }

    public bool IsEnabled() {
        return gridPointEnabled;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        int clickCount = eventData.clickCount;

        //Display the click count.
        Debug.Log(clickCount);

        if (clickCount == 1) {
            toggleCoroutine = StartCoroutine(DetectSingleClick());
        } else {
            StopCoroutine(toggleCoroutine);
            Player player = GameObject.FindObjectOfType<Player>();

            player.TravelTo(x, y);
        }
    }

    IEnumerator DetectSingleClick() {
        yield return new WaitForSeconds(.2f);

        ToggleEnabledState();
    }

    void ToggleEnabledState(){
        gridPointEnabled = !gridPointEnabled;

        GetComponent<SpriteRenderer>().color = gridPointEnabled ? Color.white : Color.grey;
    }
}
