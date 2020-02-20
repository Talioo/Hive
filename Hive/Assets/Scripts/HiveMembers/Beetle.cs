using System.Collections;
using UnityEngine;

public class Beetle : HiveMember
{
    private Vector3 startPos;
    protected override void Start()
    {
        base.Start();
        startPos = transform.position;
    }
    protected override void Select()
    {
        if(startPos.y == transform.position.y)
        {
            if (!myCell.CanIMove(this))
                return;
        }
        SOInstances.GameManager.selectedHiveMember = this;
        MarkCellsToMove();
    }
    public override IEnumerator JumpToTarget(Cell newCell)
    {
        Vector3 target = newCell.transform.position;
        if (newCell is HexaCell)
            target.y = startPos.y + ((newCell as HexaCell).hiveMembersOnMe.Count * Constants.FigureHeight);
        else
            target.y = startPos.y;
        while (transform.position != target)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
            yield return null;
        }
        target.y = startPos.y;
        SOInstances.SOHexaInfo.TryToRemoveCells();
        yield return null;
        if (newCell is HexaCell)
            NewMemberPosition(target, newCell as HexaCell);
        else
            NewMemberPosition(target);
    }
    public override void NewMemberPosition(Vector3 target)
    {
        myCell = SOInstances.SOHexaInfo.CreateNewCell(target);
        SOInstances.GameManager.selectedHiveMember = null;
        SOInstances.SODuplicateCells.RestructCells();
    }
    public void NewMemberPosition(Vector3 target, HexaCell newCell)
    {
        myCell = newCell;
        SOInstances.GameManager.selectedHiveMember = null;
        SOInstances.SODuplicateCells.RestructCells();
    }
}
