using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenBee : HiveMember
{
    [SerializeField] private SOHexaInfo hexaInfo;


    void OnMouseDown()
    {
        //TODO: Use neighbours from #13
    }
    void GetCell()
    {
        //TODO: Use neighbours from #13 to form list
        List<HexaCell> hexaCells = new List<HexaCell>();
        if (hexaCells.Count < 1) return;
        foreach (var item in hexaCells)
        {
            if (item.IsFree) item.ReadyToUse(true);
        }
    }
}
