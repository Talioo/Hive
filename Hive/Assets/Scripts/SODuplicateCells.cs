using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Duplicate Cells", menuName = "Custom/DuplicateCells")]
public class SODuplicateCells : ScriptableObject
{
    [SerializeField] private List<CellUniq> uniqCells;

    public delegate void Restruct();
    public event Restruct OnRestruct;

    private void OnEnable()
    {
        uniqCells = new List<CellUniq>();
    }
    public bool AmISurrounded(HiveMember member)
    {
        if (member.myCell.neighbours.Count <= Constants.CellsPerHexaCell / 2)
            return false;
        var cell = GetUniqCell(member.myCell);
        return IsCellSurrounded(cell);
    }
    public bool CanCrawlOnMe(EmptyCell cell)
    {
        var mainCell = SOInstances.GameManager.selectedHiveMember.myCell;
        var empty = GetUniqCell(cell);
        if (empty.masterCell.Find(x => x == mainCell) != null)
            return CanCrawlOnNearCell(empty, mainCell);
        else
            return !IsCellSurrounded(cell);
    }
    bool CanCrawlOnNearCell(EmptyCell cell, HexaCell mainCell)
    {
        int count = 0;
        for (int i = 0; i < mainCell.neighbours.Count; i++)
        {
            if (cell.masterCell.Find(x => x == mainCell.neighbours[i]) != null)
                count++;
            if (count == 2)
                return false;
        }
        return true;
    }
    bool IsCellSurrounded(EmptyCell cell)
    {
        var mainCells = cell.masterCell;
        if (mainCells.Count >= Constants.CellsPerHexaCell - 1)
            return true;
        if (mainCells.Count <= Constants.CellsPerHexaCell / 2)
            return false;
        List<HexaCell> hexas = new List<HexaCell>();
        hexas.Add(mainCells[0]);
        for (int i = 0; i < mainCells.Count; i++)
        {
            if (hexas.Contains(mainCells[i]))
                continue;
            foreach (var item in hexas)
            {
                if (!item.neighbours.Contains(mainCells[i]))
                    continue;
                hexas.Add(mainCells[i]);
                break;
            }
        }
        return hexas.Count < Constants.CellsPerHexaCell - 2;
    }
    public void AddNewCell(EmptyCell cell)
    {
        for (int i = 0; i < uniqCells.Count; i++)
        {
            if (uniqCells[i].IsOnMyPosition(cell))
                return;
        }
        uniqCells.Add(new CellUniq(cell));
    }
    public EmptyCell GetUniqCell(Cell cell)
    {
        RestructCells();
        return uniqCells.Find(x => Vector3.Distance(x.uniqPos, cell.transform.position) < Constants.DistanceToCheckMyPos).uniqueCell;
    }
    public List<EmptyCell> GetAllUniqCells()
    {
        List<EmptyCell> listUniques = new List<EmptyCell>();
        for (int i = 0; i < uniqCells.Count; i++)
        {
            if (uniqCells[i].uniqueCell != null)
                listUniques.Add(uniqCells[i].uniqueCell);
        }
        return listUniques;
    }
    public void RestructCells()
    {
        if (OnRestruct != null)
            OnRestruct.Invoke();
    }
}
