using UnityEngine;

public class HexaCell : MonoBehaviour
{
    [SerializeField] SOHexaInfo hexaInfo;
    [SerializeField] CellParams cellParams;

    private void Start()
    {
        if (!cellParams.IsEmpty) hexaInfo.cellsOnScene.Add(this);
    }
    private void OnMouseDown()
    {
        if(cellParams.IsEmpty)
        {
            Instantiate(hexaInfo.hexaPrefab, transform.position, transform.rotation);
            cellParams.IsEmpty = false;
            hexaInfo.cellsOnScene.Add(this);
        }
    }
}
[System.Serializable]
public class CellParams
{
    public bool IsEmpty = true;
}