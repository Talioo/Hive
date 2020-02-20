using System.Collections.Generic;

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
        SetReadyToUse(cell.availableCells);
    }
    private static void MarkForSpider(HexaCell cell)
    {

    }
    private static void MarkForBeetle(HexaCell cell)
    {
        for (int i = 0; i < cell.neighbours.Count; i++)
            cell.neighbours[i].ReadyToUse(true);
        SetReadyToUse(cell.availableCells);
    }
    private static void MarkForGrasshopper(HexaCell cell)
    {
        var helper = new CellNumerator(cell);
        SetReadyToUse(helper.GrasshoppersTargets());
    }
    private static void MarkForSoldierAnt(HexaCell cell)
    {
        SetReadyToUse(SOInstances.SODuplicateCells.GetAllUniqCells());
    }
    private static void SetReadyToUse(List<EmptyCell> cells)
    {
        for (int i = 0; i < cells.Count; i++)
            cells[i].ReadyToUse(true);
    }
}
