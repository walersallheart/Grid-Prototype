using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GridPointLabel : MonoBehaviour
{
    [SerializeField] TextMeshPro textField;

    public void SetLabel(string label) {
        textField.text = label;
    }
}