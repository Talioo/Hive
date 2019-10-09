using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiveMember : MonoBehaviour
{
    public SOHexaInfo hexaInfo;
    public MemberType MemberType;
    public MoveType MoveType;
    public HexaCell myCell;

    public virtual void Move(EmptyCell newCell)
    {
        if (MoveType == MoveType.Crawl)
            StartCoroutine(CrawlToTarget(newCell));
    }
    IEnumerator CrawlToTarget(EmptyCell newCell)
    {
        Vector3 target = newCell.transform.position;
        target.y = transform.position.y;
        while (transform.position != target)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, 1 * Time.deltaTime);
            yield return null;
        }
        myCell = newCell.CreateNewCell();
        hexaInfo.UncheckCells();
        yield return null;
    }
}
