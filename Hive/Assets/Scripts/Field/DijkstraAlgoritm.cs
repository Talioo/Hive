using System.Collections.Generic;
using UnityEngine;

public class DijkstraAlgoritm : MonoBehaviour
{
    List<EmptyCell> way = new List<EmptyCell>();
    HiveMember movebleMember;
    EmptyCell endPoint;

    public DijkstraAlgoritm(HiveMember hiveMember, EmptyCell targetPoint)
    {
        movebleMember = hiveMember;
        endPoint = targetPoint;
    }
    public List<EmptyCell> FindWay()
    {
        EmptyCell flag = GetNearestEmpty(movebleMember.myCell);
        while (true) 
        {
            way.Add(flag);
            if (flag == endPoint)
                break;
            flag = GetNextFlag(flag);
        }

        return way;
    }
    private EmptyCell GetNextFlag(EmptyCell flag)
    {
        EmptyCell nextFlag = flag;
        foreach (var master in flag.masterCell)
        {
            for (int i = 0; i < master.availableCells.Count; i++)
            {
                if (way.Contains(master.availableCells[i]))
                    continue;
                if (!master.availableCells[i].ReadyToUseValue)
                    continue;
                if (!SOInstances.SODuplicateCells.CanCrawlOnMe(master.availableCells[i]))
                    continue;
                if (Vector3.Distance(master.availableCells[i].transform.position, flag.transform.position) - Constants.DistanceBetweenCells <= 0.1f)
                    nextFlag = master.availableCells[i];
            }
        }
        if (nextFlag == flag)
        {
            Debug.LogError("NextFlag is not found! Way count is " + way.Count);
            return endPoint;
        }
        return nextFlag;
    }
    private EmptyCell GetNearestEmpty(HexaCell hexa)
    {
        List<Cell> uniques = new List<Cell>();
        for (int i = 0; i < hexa.availableCells.Count; i++)
        {
            if (way.Contains(hexa.availableCells[i]))
                continue;
            if (!SOInstances.SODuplicateCells.CanCrawlOnMe(hexa.availableCells[i]))
                continue;
            if (hexa.availableCells[i].IsHexaCellNowOnMe())
                continue;
            uniques.Add(SOInstances.SODuplicateCells.GetUniqCell(hexa.availableCells[i]));
        }
        return GetNearest(uniques) as EmptyCell;
    }
    private HexaCell GetNearestMaster(EmptyCell currentPoint)
    {
        List<Cell> masters = new List<Cell>();
        for (int i = 0; i < currentPoint.masterCell.Count; i++)
        {
            masters.Add(currentPoint.masterCell[i]);
        }
        return GetNearest(masters) as HexaCell;
    }
    private Cell GetNearest(List<Cell> cells)
    {
        Cell nearestOne = null;
        for (int i = 0; i < cells.Count; i++)
        {
            if (nearestOne == null)
                nearestOne = cells[i];
            else
            {
                if (DistanceToEndPoint(nearestOne) > DistanceToEndPoint(cells[i]))
                    nearestOne = cells[i];
            }

        }
        if (nearestOne == null)
            Debug.LogError("Cell is not found!");
        return nearestOne;
    }
    private float DistanceToEndPoint(Cell cell)
    {
        return Vector3.Distance(cell.transform.position, endPoint.transform.position);
    }
}
