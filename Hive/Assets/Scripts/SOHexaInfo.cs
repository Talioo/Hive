using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Hexa Info", menuName = "Custom/HexaInfo")]
public class SOHexaInfo : ScriptableObject
{
    public GameObject hexaPrefab;

    [SerializeField] private List<HexaCell> cellsOnScene;

    public void AddNewCell(HexaCell cell)
    {
        cellsOnScene.Add(cell);
    }
}
