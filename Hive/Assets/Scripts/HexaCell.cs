﻿using System.Collections.Generic;
using UnityEngine;

public class HexaCell : MonoBehaviour
{
    [SerializeField] private SOHexaInfo hexaInfo;
    public List<HexaCell> neighbours;
    public bool isMarked = false;

    private void Start()
    {
        hexaInfo.AddNewCell(this);
        Collider collider = new Collider();
    }
    private void OnMouseDown()
    {
        hexaInfo.NeedToRemove(this);
    }
    public void CreateNewCell(EmptyCell emptyCell)
    {
        Instantiate(hexaInfo.hexaPrefab, emptyCell.transform.position, transform.rotation);
    }
    public void RemoveCell()
    {
        if (CheckIsEmpty()) Destroy(gameObject);
    }
    bool CheckIsEmpty()
    {
        RaycastHit[] raycastHit = Physics.RaycastAll(transform.position, Vector3.up, 10f);
        return raycastHit.Length == 0;
    }
    [ContextMenu("FindNeighbour")]
    public void FindNeighbour()
    {
        isMarked = true;
        foreach (var item in neighbours)
        {
            if (!item.isMarked) item.FindNeighbour();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        neighbours.Add(collision.gameObject.GetComponent<HexaCell>());
    }
    public void RemoveMe(HexaCell collision)
    {
        neighbours.Remove(collision);
    }
    private void OnDestroy()
    {
        neighbours.ForEach(x => x.RemoveMe(this));
        hexaInfo.RemoveCell(this);
    }
}