using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellUniq : MonoBehaviour
{
    private EmptyCell uniqueCell;
    private List<EmptyCell> duplicatedCellsList;
    private Vector3 uniqPos;
    private SODuplicateCells duplicateCellsSO;

    private const float distance = 0.2f;

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
        var value = Vector3.Distance(uniqPos, emptyCell.transform.position) < distance;
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
            UpdateUniqueCell(duplicatedCellsList[0]);
            duplicatedCellsList.Remove(duplicatedCellsList[0]);
        }
    }
    private void UpdateUniqueCell(EmptyCell emptyCell)
    {
        uniqueCell = emptyCell;
        uniqueCell.gameObject.SetActive(true);
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
