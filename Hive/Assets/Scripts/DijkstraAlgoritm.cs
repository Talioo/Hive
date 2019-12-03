using System.Collections;
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
            foreach (var item in master.availableCells)
            {
                if (way.Contains(item))
                    continue;
                if (!item.ReadyToUseValue)
                    continue;
                if (!SOInstances.SODuplicateCells.CanCrawlOnMe(item))
                    continue;
                if (Vector3.Distance(item.transform.position, flag.transform.position) - Constants.DistanceBetweenCells <= 0.1f)
                    nextFlag = item;
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
        foreach (var item in hexa.availableCells)
        {
            if (way.Contains(item))
                continue;
            if (!SOInstances.SODuplicateCells.CanCrawlOnMe(item))
                continue;
            if (item.IsHexaCellNowOnMe())
                continue;
            uniques.Add(SOInstances.SODuplicateCells.GetUniqCell(item));
        }
        return GetNearest(uniques) as EmptyCell;
    }
    private HexaCell GetNearestMaster(EmptyCell currentPoint)
    {
        List<Cell> masters = new List<Cell>();
        foreach (var item in currentPoint.masterCell)
        {
            masters.Add(item);
        }
        return GetNearest(masters) as HexaCell;
    }
    private Cell GetNearest(List<Cell> cells)
    {
        Cell nearestOne = null;
        foreach (var item in cells)
        {
            if (nearestOne == null)
                nearestOne = item;
            else
            {
                if (DistanceToEndPoint(nearestOne) > DistanceToEndPoint(item))
                    nearestOne = item;
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
