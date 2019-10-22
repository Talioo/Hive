using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenBee : HiveMember
{
    public override void NewMemberPosition(Vector3 target)
    {
        base.NewMemberPosition(target);
        CheckGameOver();
    }
    void CheckGameOver()
    {
        if (myCell.neighbours.Count == Constants.CellsPerHexaCell)
            SOInstances.GameManager.Lose();
    }
}
