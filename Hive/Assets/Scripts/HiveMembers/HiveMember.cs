using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiveMember : MonoBehaviour
{
    public MemberType MemberType;
    public MoveType MoveType;
    public HexaCell myCell;

    protected const float moveSpeed = 0.7f;
    protected virtual void Start()
    {
        if (myCell == null)
            myCell = SOInstances.SOHexaInfo.CreateNewCell(transform.position);
        SOInstances.SODuplicateCells.RestructCells();
    }
    public void OnMouseDown()
    {
        if (SOInstances.GameManager.selectedHiveMember != null)
            return;
        SOInstances.SODuplicateCells.RestructCells();
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
        SOInstances.SOHexaInfo.TryToRemoveCells();
    }
    protected virtual void Select()
    {
        if (!myCell.CanIMove(this))
            return;
        if (SOInstances.SODuplicateCells.AmISurrounded(this))
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
        SOInstances.SOHexaInfo.TryToRemoveCells();
        yield return null;
        NewMemberPosition(target);
    }
    public virtual IEnumerator JumpToTarget(Cell newCell)
    {
        yield return null;
    }
    public virtual void NewMemberPosition(Vector3 target)
    {
        myCell = SOInstances.SOHexaInfo.CreateNewCell(target);
        SOInstances.GameManager.selectedHiveMember = null;
        SOInstances.SODuplicateCells.RestructCells();
    }
    public virtual void MarkCellsToMove()
    {
        myCell.SetReadyToUseToEmpty(MemberType);
    }
}
