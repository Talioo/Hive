using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public SOHexaInfo hexaInfo;
    [HideInInspector] public bool readyToUse = false;
    private SpriteRenderer sprite;
    private Color startColor;
    private const float rayLenght = 100f;
    public virtual void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        startColor = sprite.color;
        hexaInfo.AddNewCell(this);
    }
    public void OnMouseDown()
    {
        if (readyToUse)
            SOInstances.GameManager.selectedHiveMember.Move(this);
    }
    public virtual void ReadyToUse(bool value)
    {
        readyToUse = value;
        if (readyToUse)
        {
            sprite.color = Color.blue;
            sprite.sortingOrder++;
        }
        else
        {
            sprite.color = startColor;
            sprite.sortingOrder--;
        }
    }
    public bool CheckIsEmpty()
    {
        RaycastHit[] raycastHit = Physics.RaycastAll(transform.position + Vector3.down, Vector3.up, rayLenght);
        return raycastHit.Length == 0;
    }
    public virtual void OnDestroy()
    {
        hexaInfo.RemoveCell(this);
    }
}
