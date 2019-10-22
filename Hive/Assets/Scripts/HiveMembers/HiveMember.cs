using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiveMember : MonoBehaviour
{
    public SOHexaInfo hexaInfo;
    public MemberType MemberType;
    public MoveType MoveType;
    public HexaCell myCell;

    protected float moveSpeed = 0.5f;
    protected virtual void Start()
    {
        if (myCell == null)
            myCell = hexaInfo.CreateNewCell(transform.position);
        hexaInfo.duplicateCells.RestructCells();
    }
    public void OnMouseDown()
    {
        hexaInfo.duplicateCells.RestructCells();
        ChoseSelected(SOInstances.GameManager.selectedHiveMember);
    }
    void ChoseSelected(HiveMember selectedMember)
    {
        if (selectedMember != null)
        {
            if (selectedMember == this)
            {
                Unselect();
                return;
            }
            if (selectedMember is Beetle)
            {
                if (myCell.readyToUse)
                    selectedMember.Move(myCell);
                else
                {
                    Unselect();
                    Select();
                }
            }
            else
            {
                Unselect();
                Select();
            }
        }
        else
            Select();
    }
    protected virtual void Unselect()
    {
        SOInstances.GameManager.selectedHiveMember = null;
        hexaInfo.TryToRemoveCells();
    }
    protected virtual void Select()
    {
        if (!myCell.CanIMove(this))
            return;
        SOInstances.GameManager.selectedHiveMember = this;
        MarkCellsToMove();
    }
    public virtual void Move(Cell newCell)
    {
        if (MoveType == MoveType.Crawl)
            StartCoroutine(CrawlToTarget(newCell));
        if (MoveType == MoveType.Jump)
            StartCoroutine(JumpToTarget(newCell));
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
    public virtual IEnumerator JumpToTarget(Cell newCell)
    {
        yield return null;
    }
    public virtual void NewMemberPosition(Vector3 target)
    {
        myCell = hexaInfo.CreateNewCell(target);
        SOInstances.GameManager.selectedHiveMember = null;
        hexaInfo.duplicateCells.RestructCells();
    }
    public virtual void MarkCellsToMove()
    {
        myCell.SetReadyToUseToEmpty(MemberType);
    }
}
