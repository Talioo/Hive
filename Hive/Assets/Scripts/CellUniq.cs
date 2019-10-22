using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellUniq
{
    public EmptyCell uniqueCell { get; private set; }
    public Vector3 uniqPos { get; private set; }
    private List<EmptyCell> duplicatedCellsList;
    private SODuplicateCells duplicateCellsSO;

    public CellUniq(EmptyCell emptyCell, SODuplicateCells duplicateCells)
    {
        duplicatedCellsList = new List<EmptyCell>();
        uniqueCell = emptyCell;
        uniqPos = uniqueCell.transform.position;
        duplicateCellsSO = duplicateCells;
        duplicateCellsSO.OnRestruct += Restruct;
    }
    public bool IsOnMyPosition(EmptyCell emptyCell)
    {
        var value = Vector3.Distance(uniqPos, emptyCell.transform.position) < Constants.DistanceToCheckMyPos;
        if (value)
        {
            if (uniqueCell == null)
                UpdateUniqueCell(emptyCell);
            else
                UpdateDuplicateCell(emptyCell);
        }
        return value;
    }
    private void Restruct()
    {
        if (uniqueCell != null)
            return;
        duplicatedCellsList.RemoveAll(x => x == null);
        if (duplicatedCellsList.Count > 0)
        {
            var newEmpty = duplicatedCellsList[0];
            duplicatedCellsList.Remove(newEmpty);
            UpdateUniqueCell(newEmpty);
        }
    }
    private void UpdateUniqueCell(EmptyCell emptyCell)
    {
        uniqueCell = emptyCell;
        uniqueCell.gameObject.SetActive(true);
        if (duplicatedCellsList.Count == 0)
            return;
        foreach (var item in duplicatedCellsList)
        {
            if (uniqueCell.masterCell.Find(x => x == item.masterCell[0]) == null)
                uniqueCell.masterCell.Add(item.masterCell[0]);
        }
    }
    private void UpdateDuplicateCell(EmptyCell emptyCell)
    {
        uniqueCell.AddNewMasterCell(emptyCell);
        emptyCell.gameObject.SetActive(false);
        duplicatedCellsList.Add(emptyCell);
    }
    private void OnDisable()
    {
        duplicateCellsSO.OnRestruct -= Restruct;
    }
}
