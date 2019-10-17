using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiveMember : MonoBehaviour
{
    public SOHexaInfo hexaInfo;
    public MemberType MemberType;
    public MoveType MoveType;
    public HexaCell myCell;

    protected float moveSpeed = 1;
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
        {
            if (SOInstances.GameManager.selectedHiveMember is Beetle && SOInstances.GameManager.selectedHiveMember != this)
            {
                if (myCell.readyToUse)
                    SOInstances.GameManager.selectedHiveMember.Move(myCell);
                else
                    Unselect();
            }
            else
                Unselect();
        }
        else 
        if (myCell.CanIMove(this))
            Select();
        
    }
    private void Unselect()
    {
        SOInstances.GameManager.selectedHiveMember = null;
        hexaInfo.TryToRemoveCells();
    }
    private void Select()
    {
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
