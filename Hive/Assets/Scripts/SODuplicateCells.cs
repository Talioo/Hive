using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Duplicate Cells", menuName = "Custom/DuplicateCells")]
public class SODuplicateCells : ScriptableObject
{
    private List<CellUniq> uniqCells;

    public delegate void Restruct();
    public event Restruct OnRestruct;
    private void OnEnable()
    {
        uniqCells = new List<CellUniq>();
    }
    public void AddNewCell(EmptyCell cell)
    {
        foreach (var item in uniqCells)
        {
            if (item.IsOnMyPosition(cell))
                return;
        }
        uniqCells.Add(new CellUniq(cell, this));
    }
    public void RestructCells()
    {
        if (OnRestruct != null)
            OnRestruct.Invoke();
    }
}
