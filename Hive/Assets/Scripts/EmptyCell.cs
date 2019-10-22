﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyCell : Cell
{
    public int cellNum;
    public List<HexaCell> masterCell;

    public override void Start()
    {
        base.Start();
    }
    public void AddNewMasterCell(EmptyCell cell)
    {
        cell.masterCell[0].AddEmptyCell(this);
        if (masterCell.Find(x => x == cell) == null)
            masterCell.Add(cell.masterCell[0]);
    }
    public void RemoveMasterCell(HexaCell cell)
    {
        if (masterCell.Find(x => x == cell) != null)
            masterCell.Remove(cell);
    }
    public bool IsHexaCellNowOnMe()
    {
        for (int i = 0; i < hexaInfo.hexaCellsOnScene.Count; i++)
        {
            if (Vector3.Distance(hexaInfo.hexaCellsOnScene[i].transform.position, transform.position) < Constants.DistanceToHexa)
                return true;
        }
        return false;
    }
    public override void ReadyToUse(bool value)
    {
        if (value && WillBreakHive())
            return;
        if (value)
        {
            var uniq = hexaInfo.duplicateCells.GetUniqCell(this);
            if (uniq == this)
                base.ReadyToUse(value);
            else
                uniq.ReadyToUse(value);
        }
        else
            base.ReadyToUse(value);

    }
    bool WillBreakHive()
    {
        if (masterCell.Count > 1)
            return false;
        if (SOInstances.GameManager.selectedHiveMember == null)
            return false;
        if (masterCell[0] != SOInstances.GameManager.selectedHiveMember.myCell)
            return false;
        if (SOInstances.GameManager.selectedHiveMember is Beetle)
            return IsTheBetleUnder();
        return true;
    }
    bool IsTheBetleUnder()
    {
        if (masterCell[0].hiveMembersOnMe[0] != SOInstances.GameManager.selectedHiveMember)
            return false;
        return true;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position + Vector3.down * 0.01f, transform.position + Vector3.up * 0.01f);
    }
}
