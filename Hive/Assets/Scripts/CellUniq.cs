using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CellUniq
{
    public EmptyCell uniqueCell { get; private set; }
    public Vector3 uniqPos { get; private set; }
    private List<EmptyCell> duplicatedCellsList;

    public CellUniq(EmptyCell emptyCell)
    {
        duplicatedCellsList = new List<EmptyCell>();
        uniqueCell = emptyCell;
        uniqPos = uniqueCell.transform.position;
        SOInstances.SODuplicateCells.OnRestruct += Restruct;
        SOInstances.SOHexaInfo.OnAddCell += NewHexa;
        SOInstances.SOHexaInfo.OnDestroyCell += RemoveHexa;
    }
    public bool IsOnMyPosition(EmptyCell emptyCell)
    {
        var value = CheckDistance(emptyCell);
        if (value)
        {
            if (uniqueCell == null)
                UpdateUniqueCell(emptyCell);
            else
                UpdateDuplicateCell(emptyCell);
        }
        return value;
    }
    private void NewHexa(HexaCell hexaCell)
    {
        if (!CheckDistance(hexaCell))
            return;
        if (uniqueCell != null)
            uniqueCell.gameObject.SetActive(false);
    }
    private void RemoveHexa(HexaCell hexaCell)
    {
        if (!CheckDistance(hexaCell))
            return;
        if (uniqueCell != null)
            uniqueCell.gameObject.SetActive(true); 
    }
    private bool CheckDistance(Cell cell)
    {
        return Vector3.Distance(uniqPos, cell.transform.position) < Constants.DistanceToCheckMyPos;
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
        uniqueCell.gameObject.SetActive(!uniqueCell.IsHexaCellNowOnMe());
        if (duplicatedCellsList.Count == 0)
            return;
        for (int i = 0; i < duplicatedCellsList.Count; i++)
        {
            if (uniqueCell.masterCell.Find(x => x == duplicatedCellsList[i].masterCell[0]) == null)
                uniqueCell.masterCell.Add(duplicatedCellsList[i].masterCell[0]);
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
        SOInstances.SODuplicateCells.OnRestruct -= Restruct;
        SOInstances.SOHexaInfo.OnAddCell -= NewHexa;
        SOInstances.SOHexaInfo.OnDestroyCell -= RemoveHexa;
    }
}
