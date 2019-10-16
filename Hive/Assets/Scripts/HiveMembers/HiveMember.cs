using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiveMember : MonoBehaviour
{
    public SOHexaInfo hexaInfo;
    public MemberType MemberType;
    public MoveType MoveType;
    public HexaCell myCell;

    protected float moveSpeed = 2;
    protected virtual void Start()
    {
        if (myCell == null)
            myCell = hexaInfo.CreateNewCell(transform.position);
        hexaInfo.duplicateCells.RestructCells();
    }
    public void OnMouseDown()
    {
        hexaInfo.duplicateCells.RestructCells();
        if (SOInstances.GameManager.selectedHiveMember != null)
            return;
        if (!myCell.CanIMove(this))
            return;
        SOInstances.GameManager.selectedHiveMember = this;
        MarkCellsToMove();
    }
    public virtual void Move(Cell newCell)
    {
        if (MoveType == MoveType.Crawl)
            StartCoroutine(CrawlToTarget(newCell));
    }
    public virtual IEnumerator CrawlToTarget(Cell newCell)
    {
        Vector3 target = newCell.transform.position;
        while (transform.position != target)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
            yield return null;
        }
        hexaInfo.TryToRemoveCells();
        yield return null;
        NewMemberPosition(target);
    }
    public virtual void NewMemberPosition(Vector3 target)
    {
        myCell = hexaInfo.CreateNewCell(target);
        SOInstances.GameManager.selectedHiveMember = null;
        hexaInfo.duplicateCells.RestructCells();
    }
    public virtual void MarkCellsToMove()
    {

    }
}
