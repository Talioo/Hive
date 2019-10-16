using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Hexa Info", menuName = "Custom/HexaInfo")]
public class SOHexaInfo : ScriptableObject
{
    public GameObject hexaPrefab;
    public SODuplicateCells duplicateCells;

    [SerializeField] private List<HexaCell> hexaCellsOnScene;
    public delegate void RemoveCells();
    public event RemoveCells OnRemoveCells;
    private void OnEnable()
    {
        hexaCellsOnScene = new List<HexaCell>();
    }
    public bool CanRemoveCell(HexaCell cellToRemove)
    {
        cellToRemove.isMarked = true;
        var startPoint = hexaCellsOnScene.Find(x => !x.isMarked);
        if (startPoint == null)
            return false;
        startPoint.FindNeighbour();
        bool canRemove = true;
        foreach (var item in hexaCellsOnScene)
        {
            if (!item.isMarked)
            {
                canRemove = false;
                break;
            }
        }
        hexaCellsOnScene.ForEach(x => x.isMarked = false);
        return canRemove;
    }
    public void AddNewCell(Cell cell)
    {
        if(cell is HexaCell)
            hexaCellsOnScene.Add(cell as HexaCell);
        if (cell is EmptyCell)
            duplicateCells.AddNewCell(cell as EmptyCell);
    }
    public void RemoveCell(Cell cell)
    {
        if (cell is HexaCell)
            hexaCellsOnScene.Remove(cell as HexaCell);
    }
    public List<HexaCell> GetFreeCells()
    {
        return hexaCellsOnScene.FindAll(x => x.IsFree);
    }
    public void TryToRemoveCells()
    {
        OnRemoveCells.Invoke();
    }
    public HexaCell CreateNewCell(Vector3 position)
    {
        return Instantiate(hexaPrefab, position, hexaPrefab.transform.rotation).GetComponent<HexaCell>();
    }
}
