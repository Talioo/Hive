using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ant : HiveMember
{
    public override IEnumerator CrawlToTarget(Cell newCell)
    {
        List<Vector3> target = FindMyTarget(newCell);
        for (int i = 0; i < target.Count; i++)
        {
            yield return StartCoroutine(MoveToTarget(target[i]));
            yield return null;
        }
        
        SOInstances.SOHexaInfo.TryToRemoveCells();
        yield return null;
        NewMemberPosition(target[target.Count - 1]);
    }
    IEnumerator MoveToTarget(Vector3 target)
    {
        while (transform.position != target)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
            yield return null;
        }
        yield return null;
    }
    List<Vector3> FindMyTarget(Cell lastCell)
    {
        List<Vector3> target = new List<Vector3>();
        DijkstraAlgoritm algoritm = new DijkstraAlgoritm(this, SOInstances.SODuplicateCells.GetUniqCell(lastCell as EmptyCell));
        foreach (var item in algoritm.FindWay())
        {
            target.Add(item.transform.position);
        }
        return target;
    }
}
