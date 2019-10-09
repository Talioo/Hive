using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Hexa Info", menuName = "Custom/HexaInfo")]
public class SOHexaInfo : ScriptableObject
{
    public GameObject hexaPrefab;

    [SerializeField] private List<HexaCell> cellsOnScene;
    private void OnEnable()
    {
        cellsOnScene = new List<HexaCell>();
    }
    public void NeedToRemove(HexaCell cellToRemove)
    {
        cellToRemove.isMarked = true;
        var startPoint = cellsOnScene.Find(x => !x.isMarked);
        if (startPoint == null) return;
        startPoint.FindNeighbour();
        bool canRemove = true;
        foreach (var item in cellsOnScene)
        {
            if (!item.isMarked)
            {
                canRemove = false;
                break;
            }
        }
        cellsOnScene.ForEach(x => x.isMarked = false);
        if (canRemove) cellToRemove.RemoveCell();
    }
    public void AddNewCell(HexaCell cell)
    {
        cellsOnScene.Add(cell);
    }
    public void RemoveCell(HexaCell cell)
    {
        cellsOnScene.Remove(cell);
    }
    public List<HexaCell> GetFreeCells()
    {
        return cellsOnScene.FindAll(x => x.IsFree);
    }
    public void UncheckCells()
    {
        cellsOnScene.ForEach(x => x.ReadyToUse(false));
    }
}
