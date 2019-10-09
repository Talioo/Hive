using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyCell : MonoBehaviour
{
    [SerializeField] private HexaCell masterCell;

    private bool readyToUse = false;
    private SpriteRenderer sprite;
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    private void OnMouseDown()
    {
        if (readyToUse) SOInstances.GameManager.selectedHiveMember.Move(this);
    }
    public HexaCell CreateNewCell()
    {
        return Instantiate(masterCell.hexaInfo.hexaPrefab, transform.position, transform.rotation).GetComponent<HexaCell>();
    }
    public void ReadyToUse(bool _readyToUse)
    {
        readyToUse = _readyToUse;
        sprite.color = readyToUse ? Color.blue : Color.white;
    }
}
