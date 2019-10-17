using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyCell : Cell
{
    public List<HexaCell> masterCell;
    private bool alreadyTaken = false;

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
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.GetComponent<EmptyCell>() == null)
            alreadyTaken = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.GetComponent<EmptyCell>() == null)
            alreadyTaken = false;
    }
    public override void ReadyToUse(bool value)
    {
        //if (alreadyTaken && value)
        //    return;
        base.ReadyToUse(value);
    }
}
