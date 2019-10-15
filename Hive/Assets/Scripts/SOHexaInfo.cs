using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Hexa Info", menuName = "Custom/HexaInfo")]
public class SOHexaInfo : ScriptableObject
{
    public GameObject hexaPrefab;

    [SerializeField] private List<HexaCell> hexaCellsOnScene;
    [SerializeField] private List<List<EmptyCell>> emptyCellsOnSceneList;
    private void OnEnable()
    {
        hexaCellsOnScene = new List<HexaCell>();
        emptyCellsOnSceneList = new List<List<EmptyCell>>();
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
            AddToEmtysList(cell as EmptyCell);
    }
    void AddToEmtysList(EmptyCell emptyCell)
    {
        foreach (var item in emptyCellsOnSceneList)
        {
            if (Vector3.Distance(item[0].transform.position, emptyCell.transform.position) < 0.2f)
            {
                item.Add(emptyCell);
                return;
            }
        }
        var newList = new List<EmptyCell>();
        newList.Add(emptyCell);
        emptyCellsOnSceneList.Add(newList);
    }
    public void CheckEmpties()
    {
        foreach (var item in emptyCellsOnSceneList)
        {
            item[0].gameObject.SetActive(true);
            if (item.Count == 1)
                continue;
            for (int i = 1; i < item.Count; i++)
            {
                item[0].AddNewMasterCell(item[i].masterCell[0]);
                item[i].masterCell[0].AddEmptyCell(item[0]);
                item[i].gameObject.SetActive(false);
            }
        }
    }
    public void RemoveCell(Cell cell)
    {
        if (cell is HexaCell)
            hexaCellsOnScene.Remove(cell as HexaCell);
    }
    public void CleanCellsList()
    {
        emptyCellsOnSceneList.RemoveAll(x => x.Count == 1 && x[0] == null);
        foreach (var item in emptyCellsOnSceneList)
        {
            item.RemoveAll(x => x == null);
        }
    }
    public List<HexaCell> GetFreeCells()
    {
        return hexaCellsOnScene.FindAll(x => x.IsFree);
    }
    public void TryToRemoveCells()
    {
        hexaCellsOnScene.ForEach(x => x.RemoveEmptyCell());
        hexaCellsOnScene.ForEach(x => x.SetReadyToUseToEmpty(false));
        hexaCellsOnScene.ForEach(x => x.TryToRemoveCell());
    }
    public HexaCell CreateNewCell(Vector3 position)
    {
        return Instantiate(hexaPrefab, position, hexaPrefab.transform.rotation).GetComponent<HexaCell>();
    }
}
