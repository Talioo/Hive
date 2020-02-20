using System.Collections.Generic;
using UnityEngine;

public class EmptyCell : Cell
{
    public int cellNum;
    public List<HexaCell> masterCell;
    public bool ReadyToUseValue { get; private set; }

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
        for (int i = 0; i < SOInstances.SOHexaInfo.hexaCellsOnScene.Count; i++)
        {
            if (Vector3.Distance(SOInstances.SOHexaInfo.hexaCellsOnScene[i].transform.position, transform.position) < Constants.DistanceToHexa)
                return true;
        }
        return false;
    }
    public override void ReadyToUse(bool value)
    {
        if (value && SOInstances.GameManager.selectedHiveMember != null)
        {
            if (WillBreakHive())
                return;
            if (IsHexaCellNowOnMe())
                return;
            if (SOInstances.GameManager.selectedHiveMember.MoveType == MoveType.Crawl)
                if (!SOInstances.SODuplicateCells.CanCrawlOnMe(this))
                    return;
        }
        if (value)
        {
            var uniq = SOInstances.SODuplicateCells.GetUniqCell(this);
            if (uniq == this)
                base.ReadyToUse(value);
            else
                uniq.ReadyToUse(value);
        }
        else
            base.ReadyToUse(value);
        ReadyToUseValue = value;
    }
    [ContextMenu("Test")]
    void Test()
    {
        print("IsHexaCellNowOnMe: " + IsHexaCellNowOnMe());
        print("CheckIsEmpty: " + CheckIsEmpty());
        print("WillBreakHive: " + WillBreakHive());
        print("CanCrawlOnMe: " + SOInstances.SODuplicateCells.CanCrawlOnMe(this));
    }
    bool WillBreakHive()
    {
        if (masterCell.Count > 1)
            return false;
        if (masterCell.Count == 0)
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
