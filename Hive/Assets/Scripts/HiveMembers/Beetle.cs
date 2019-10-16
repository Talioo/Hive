using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beetle : HiveMember
{
    Vector3 startPos;
    private void Start()
    {
        startPos = transform.position;
    }
    public override void MarkCellsToMove()
    {
        myCell.SetReadyToUseToEmpty(true, true);
    }
    public override IEnumerator CrawlToTarget(Cell newCell)
    {
        Vector3 target = newCell.transform.position;
        if (newCell is HexaCell)
            target.y = startPos.y + ((newCell as HexaCell).hiveMembersOnMe.Count/3f);
        else
            target.y = startPos.y;
        while (transform.position != target)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, 2 * Time.deltaTime);
            yield return null;
        }
        target.y = startPos.y;
        hexaInfo.TryToRemoveCells();
        yield return null;
        if (newCell is HexaCell)
            myCell = newCell as HexaCell;
        else
            myCell = hexaInfo.CreateNewCell(target);
        SOInstances.GameManager.selectedHiveMember = null;
        yield return null;
        hexaInfo.duplicateCells.RestructCells();
    }
}
