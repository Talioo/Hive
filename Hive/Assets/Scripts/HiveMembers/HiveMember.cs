using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiveMember : MonoBehaviour
{
    public MemberType MemberType;
    public MoveType MoveType;
    public HexaCell myCell;

    public virtual void Move(HexaCell newCell)
    {
        if (MoveType == MoveType.Crawl) StartCoroutine(CrawlToTarget(newCell));
    }
    IEnumerator CrawlToTarget(HexaCell newCell)
    {
        Vector3 target = newCell.transform.position;
        target.y = transform.position.y;
        while (transform.position != target)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, 1 * Time.deltaTime);
            yield return null;
        }
        myCell.ColliderActive(true);
        myCell = newCell;
        myCell.ColliderActive(false);
        SOHexaInfo.UncheckCells();
        yield return null;
    }
}
