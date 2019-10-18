using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grasshopper : HiveMember
{
    private Vector3 startPos;
    private const float heigh = 0.1f;
    public List<EmptyCell> possibleCells;
    protected override void Start()
    {
        base.Start();
        startPos = transform.position;
    }
    protected override void Unselect()
    {
        possibleCells.ForEach(x => x.ReadyToUse(false));
        base.Unselect();
    }
    public override IEnumerator JumpToTarget(Cell newCell)
    {
        Vector3 target = newCell.transform.position;
        float distance = Vector3.Distance(transform.position, target);
        float maxY = startPos.y + heigh;
        while (transform.position != target)
        {
            if (Vector3.Distance(transform.position, newCell.transform.position) > distance / 2 && target.y < maxY)
                target.y += 0.01f;
            else
                target.y -= 0.01f;
            if (target.y < startPos.y)
                target.y = startPos.y;
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
            yield return null;
        }
        target.y = startPos.y;
        hexaInfo.TryToRemoveCells();
        yield return null;
        NewMemberPosition(target);
    }
}
