using UnityEngine;

public class HexaCell : MonoBehaviour
{
    [SerializeField] private SOHexaInfo hexaInfo;
    [SerializeField] private CellParams cellParams;
    [SerializeField] private GameManager gameManager;

    public bool IsFree { get { return cellParams.IsEmpty; } }

    private Collider collider;
    private SpriteRenderer sprite;
    private bool readyToUse = false;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider>();
        hexaInfo.AddNewCell(this);
    }
    private void OnMouseDown()
    {
        if (readyToUse) gameManager.selectedHiveMember.Move(this);
    }
    public void ColliderActive(bool active)
    {
        collider.enabled = active;
    }
    public void ReadyToUse(bool _readyToUse)
    {
        readyToUse = _readyToUse;
        sprite.color = readyToUse ? Color.blue : Color.white;
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