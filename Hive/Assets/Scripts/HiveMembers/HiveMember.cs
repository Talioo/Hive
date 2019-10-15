using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiveMember : MonoBehaviour
{
    public SOHexaInfo hexaInfo;
    public MemberType MemberType;
    public MoveType MoveType;
    public HexaCell myCell;

    public void OnMouseDown()
    {
        if (SOInstances.GameManager.selectedHiveMember != null)
        {
            hexaInfo.TryToRemoveCells();
            SOInstances.GameManager.selectedHiveMember = null;
        }
        if (!myCell.CanIMove(this))
            return;
        MarkCellsToMove();
        SOInstances.GameManager.selectedHiveMember = this;
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
            transform.position = Vector3.MoveTowards(transform.position, target, 2 * Time.deltaTime);
            yield return null;
        }
        hexaInfo.TryToRemoveCells();
        yield return null;
        hexaInfo.CleanCellsList();
        myCell = hexaInfo.CreateNewCell(target);
        SOInstances.GameManager.selectedHiveMember = null;
        yield return null;
        hexaInfo.CheckEmpties();
    }
    public virtual void MarkCellsToMove()
    {

    }
}
