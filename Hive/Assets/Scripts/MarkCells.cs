using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkCells
{
    public static void MarkEmptyCells(HexaCell cell, MemberType member)
    {
        switch (member)
        {
            case MemberType.QueenBee:
                MarkForQueen(cell);
                break;
            case MemberType.Spider:
                MarkForSpider(cell);
                break;
            case MemberType.Beetle:
                MarkForBeetle(cell);
                break;
            case MemberType.Grasshopper:
                MarkForGrasshopper(cell);
                break;
            case MemberType.SoldierAnt:
                MarkForSoldierAnt(cell);
                break;
            default:
                break;
        }
    }
    private static void MarkForQueen(HexaCell cell)
    {
        cell.availableCells.ForEach(x => x.ReadyToUse(true));
    }
    private static void MarkForSpider(HexaCell cell)
    {

    }
    private static void MarkForBeetle(HexaCell cell)
    {
        cell.neighbours.ForEach(x => x.ReadyToUse(true));
        cell.availableCells.ForEach(x => x.ReadyToUse(true));
    }
    private static void MarkForGrasshopper(HexaCell cell)
    {
        var helper = new CellNumerator(cell);
        helper.GrasshoppersTargets().ForEach(x => x.ReadyToUse(true));
    }
    private static void MarkForSoldierAnt(HexaCell cell)
    {
        SOInstances.SODuplicateCells.GetAllUniqCells().ForEach(x => x.ReadyToUse(true));
    }
}
