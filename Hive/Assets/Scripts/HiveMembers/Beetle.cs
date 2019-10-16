﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beetle : HiveMember
{
    Vector3 startPos;
    protected override void Start()
    {
        base.Start();
        startPos = transform.position;
    }
    public override void MarkCellsToMove()
    {
        myCell.SetReadyToUseToEmpty(true, true);
    }
    public override IEnumerator CrawlToTarget(Cell newCell)
    {
        Vector3 target = newCell.transform.position;
        if (newCell is HexaCell)
            target.y = startPos.y + ((newCell as HexaCell).hiveMembersOnMe.Count/3f);
        else
            target.y = startPos.y;
        while (transform.position != target)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, 2 * Time.deltaTime);
            yield return null;
        }
        target.y = startPos.y;
        hexaInfo.TryToRemoveCells();
        yield return null;
        if (newCell is HexaCell)
            NewMemberPosition(target, newCell as HexaCell);
        else
            NewMemberPosition(target);

    }
    public override void NewMemberPosition(Vector3 target)
    {
        hexaInfo.CleanCellsList();
        myCell = hexaInfo.CreateNewCell(target);
        SOInstances.GameManager.selectedHiveMember = null;
        hexaInfo.CheckEmpties();
    }
    public void NewMemberPosition(Vector3 target, HexaCell newCell)
    {
        hexaInfo.CleanCellsList();
        myCell = newCell;
        SOInstances.GameManager.selectedHiveMember = null;
        hexaInfo.CheckEmpties();
    }
}
