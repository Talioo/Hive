using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public SOHexaInfo hexaInfo;
    [HideInInspector] public SpriteRenderer sprite;
    [HideInInspector] public bool readyToUse = false;
    Color startColor;
    public virtual void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        startColor = sprite.color;
        hexaInfo.AddNewCell(this);
        hexaInfo.CheckEmpties();
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
            sprite.sortingOrder += 1;
        }
        else
        {
            sprite.color = startColor;
            sprite.sortingOrder -= 1;
        }
    }
    public bool CheckIsEmpty()
    {
        RaycastHit[] raycastHit = Physics.RaycastAll(transform.position + Vector3.down * 5, Vector3.up, 100f);
        foreach (var item in raycastHit)
        {
            print(item.rigidbody.gameObject.name);
        }
        return raycastHit.Length == 0;
    }
    public virtual void OnDestroy()
    {
        hexaInfo.RemoveCell(this);
    }
}
