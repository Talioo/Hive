using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenBee : HiveMember
{
    public override void MarkCellsToMove()
    {
        myCell.SetReadyToUseToEmpty(true);
    }
}
