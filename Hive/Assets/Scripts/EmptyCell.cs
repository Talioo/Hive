using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyCell : MonoBehaviour
{
    [SerializeField] private HexaCell masterCell;
    private void OnMouseDown()
    {
        masterCell.CreateNewCell(this);
    }
}
