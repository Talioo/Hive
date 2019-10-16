using System.Collections.Generic;
using UnityEngine;

public class HexaCell : Cell
{
    #region Parametrs
    [SerializeField] private List<HexaCell> neighbours;
    [SerializeField] public List<EmptyCell> availableCells;
    public bool isMarked = false;
    public bool IsFree { get { return hiveMembersOnMe.Count == 0; } }
    #endregion
    #region Unity methods
    public override void Start()
    {
        base.Start();
        hexaInfo.OnRemoveCells += RemoveCells;
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
    }
    public override void OnDestroy()
    {
        neighbours.ForEach(x => x.RemoveMe(this));
        availableCells.ForEach(x => x.RemoveMasterCell(this));
        hexaInfo.OnRemoveCells -= RemoveCells;
        base.OnDestroy();
    }
    #endregion
    #region Public methods
    public void SetReadyToUseToEmpty(bool value, bool isBeetle = false)
    {
        if (!value || isBeetle)
        {
            neighbours.ForEach(x => x.ReadyToUse(value));
        }
        availableCells.ForEach(x => x.ReadyToUse(value));
    }
    public void AddEmptyCell(EmptyCell emptyCell)
    {
        foreach (var item in availableCells)
        {
            if (item == emptyCell)
                return;
        }
        availableCells.Add(emptyCell);
    }
    
    public void FindNeighbour()
    {
        isMarked = true;
        foreach (var item in neighbours)
        {
            if (!item.isMarked)
                item.FindNeighbour();
        }
    }
    public void RemoveMe(HexaCell collision)
    {
        neighbours.Remove(collision);
    }
    public bool CanIMove(HiveMember member)
    {
        //@TODO: Add better algoritm to find if cell cen be removed
        //if (!hexaInfo.CanRemoveCell(this))
        //    return false;
        if (hiveMembersOnMe.Count == 0)
            return true;
        return hiveMembersOnMe[hiveMembersOnMe.Count - 1] == member;
    }
    #endregion
    #region Private methods
    private void RemoveCells()
    {
        availableCells.RemoveAll(x => x == null);
        SetReadyToUseToEmpty(false);
        TryToRemoveCell();
    }
    private void TryToRemoveCell()
    {
        if (hiveMembersOnMe.Count == 0)
            Destroy(gameObject);
    }
    #endregion
}