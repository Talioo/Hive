using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellNumerator
{
    public int number;
    private HexaCell HexaCell;

    public CellNumerator(HexaCell cell)
    {
        HexaCell = cell;
    }
    //1) найти индекс каждого соседа
    //2) найти в соседе ячейку с индексом
    //3) если пустая - отметить
    //4) если заполнена => п.2
    public List<EmptyCell> GrasshoppersTargets()
    {
        List<EmptyCell> targets = new List<EmptyCell>();
        for (int i = 0; i < HexaCell.neighbours.Count; i++)
        {
            HexaCell currentCell = HexaCell;
            HexaCell currentCellNeighbour = currentCell.neighbours[i];
            int neededIndex = FindNeighbourIndex(currentCellNeighbour);
            while (true)
            {
                var empty = FindEmptyWithIndex(neededIndex, currentCellNeighbour);
                if (!empty.IsHexaCellNowOnMe())
                {
                    targets.Add(empty);
                    break;
                }
                else
                {
                    currentCell = currentCellNeighbour;
                    currentCellNeighbour = FindNeighbourByEmpty(empty, currentCell);
                }
            }
        }
        return targets;
    }
    int FindNeighbourIndex(HexaCell neighbour)
    {
        int index = -1;
        for (int i = 0; i < 6; i++)
        {
            if (Vector3.Distance(HexaCell.availableCells[i].transform.position, neighbour.transform.position) < 0.15f)
                index = HexaCell.availableCells[i].cellNum;
        }
        if(index == -1)
        {
            Debug.LogError("No neighbour on this distance!");
            Debug.Break();
            index = 0;
        }
        return index;
    }
    HexaCell FindNeighbourByEmpty(EmptyCell empty, HexaCell currentCell)
    {
        HexaCell neighbour = null;
        currentCell.neighbours.RemoveAll(x => x == null);
        for (int i = 0; i < currentCell.neighbours.Count; i++)
        {
            if (Vector3.Distance(currentCell.neighbours[i].transform.position, empty.transform.position) < 0.15f)
                neighbour = currentCell.neighbours[i];
        }
        return neighbour;
    }

    EmptyCell FindEmptyWithIndex(int index, HexaCell hexaCell)
    {
        return 
            hexaCell.availableCells.Find(x => x.cellNum == index);
    }
}
