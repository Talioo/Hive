using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenBee : HiveMember
{
    void OnMouseDown()
    {
        //TODO: Use neighbours from #13
        GetCells();
        SOInstances.GameManager.selectedHiveMember = this;
    }
    void GetCells()
    {
        //TODO: Use neighbours from #13 to form list
        List<EmptyCell> emptyCells = myCell.availableCells;
        if (emptyCells.Count < 1) return;
        foreach (var item in emptyCells)
        {
            item.ReadyToUse(true);
        }
    }
}
