using UnityEngine;

public class HexaCell : MonoBehaviour
{
    [SerializeField] private SOHexaInfo hexaInfo;
    [SerializeField] private CellParams cellParams;

    private void Start()
    {
        if (!cellParams.IsEmpty) hexaInfo.AddNewCell(this);
    }
    private void OnMouseDown()
    {
        if(cellParams.IsEmpty)
        {
            CreateNewCell();
        }
    }
    private void CreateNewCell()
    {
        Instantiate(hexaInfo.hexaPrefab, transform.position, transform.rotation);
        cellParams.IsEmpty = false;
        hexaInfo.AddNewCell(this);
    }
}
[System.Serializable]
public class CellParams
{
    public bool IsEmpty = true;
}