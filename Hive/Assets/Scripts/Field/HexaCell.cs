using System.Collections.Generic;
using UnityEngine;

public class HexaCell : Cell
{
    #region Parametrs
    public List<EmptyCell> availableCells;
    public List<HexaCell> neighbours;
    public bool isMarked = false;
    public bool IsFree { get { return hiveMembersOnMe.Count == 0; } }
    #endregion
    #region Unity methods
    public override void Start()
    {
        base.Start();
        neighbours = new List<HexaCell>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        var memberOnMe = collision.collider.GetComponent<HiveMember>();
        if (memberOnMe != null)
        {
            memberOnMe.myCell = this;
            hiveMembersOnMe.Add(memberOnMe);
        }
        else
        {
            var neighbour = collision.gameObject.GetComponent<HexaCell>();
            if (neighbour != null)
                neighbours.Add(neighbour);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        var memberOnMe = collision.collider.GetComponent<HiveMember>();
        if (memberOnMe != null)
            hiveMembersOnMe.Remove(memberOnMe);
        RemoveCells();
    }
    public override void OnDestroy()
    {
        DestroyEvent();
        SOInstances.SOHexaInfo.RemoveCell(this);
        base.OnDestroy();
    }
    void DestroyEvent()
    {
        for (int i = 0; i < neighbours.Count; i++)
            neighbours[i].RemoveMe(this);
        for (int i = 0; i < availableCells.Count; i++)
            availableCells[i].RemoveMasterCell(this);
    }
    #endregion
    #region Public methods
    public void SetReadyToUseToEmpty(MemberType member)
    {
        availableCells.RemoveAll(x => x == null);
        neighbours.RemoveAll(x => x == null);
        MarkCells.MarkEmptyCells(this, member);
    }
    public void AddEmptyCell(EmptyCell emptyCell)
    {
        for (int i = 0; i < availableCells.Count; i++)
        {
            if (availableCells[i] == emptyCell)
                return;
        }
        availableCells.Add(emptyCell);
    }
    
    public void FindNeighbour()
    {
        isMarked = true;
        for (int i = 0; i < neighbours.Count; i++)
        {
            if (!neighbours[i].isMarked)
                neighbours[i].FindNeighbour();
        }
    }
    public void RemoveMe(HexaCell collision)
    {
        neighbours.Remove(collision);
    }
    public bool CanIMove(HiveMember member)
    {
        if (!SOInstances.SOHexaInfo.CanRemoveCell(this))
            return false;
        if (hiveMembersOnMe.Count == 0)
            return true;
        return hiveMembersOnMe[hiveMembersOnMe.Count - 1] == member;
    }
    #endregion
    #region Private methods
    protected override void RemoveCells()
    {
        base.RemoveCells();
        availableCells.RemoveAll(x => x == null);
        TryToRemoveCell();
    }
    private void TryToRemoveCell()
    {
        if (hiveMembersOnMe.Count == 0)
            Destroy(gameObject);
    }
    #endregion
}