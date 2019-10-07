using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Hexa Info", menuName = "Custom/HexaInfo")]
public class SOHexaInfo : ScriptableObject
{
    public GameObject hexaPrefab;

    private static List<HexaCell> cellsOnScene;
    private void OnEnable()
    {
        cellsOnScene = new List<HexaCell>();
    }
    public void AddNewCell(HexaCell cell)
    {
        cellsOnScene.Add(cell);
    }
    public List<HexaCell> GetFreeCells()
    {
        List<HexaCell> freeCells = new List<HexaCell>();
        cellsOnScene.ForEach(x => { if (x.IsFree) freeCells.Add(x); });
        return freeCells;
    }
    public static void UncheckCells()
    {
        cellsOnScene.ForEach(x => x.ReadyToUse(false));
    }
}
