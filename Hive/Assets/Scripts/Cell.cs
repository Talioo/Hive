using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public SOHexaInfo hexaInfo;
    public List<HiveMember> hiveMembersOnMe { get; private set; }
    [HideInInspector] public SpriteRenderer sprite;
    [HideInInspector] public bool readyToUse = false;

    private Color startColor;
    private bool canSpawnOnMe = false;
    protected virtual void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        startColor = sprite.color;
        hexaInfo.AddNewCell(this);
        //hexaInfo.CheckEmpties();
        SOInstances.UIController.OnNewMemberSpawning += TryToSpawnOnMe;
        hiveMembersOnMe = new List<HiveMember>();
    }
    public void OnMouseDown()
    {
        if (canSpawnOnMe)
            SOInstances.UIController.Aprove(this);
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
    protected void TryToSpawnOnMe(bool value)
    {
        if (hiveMembersOnMe.Count > 0)
            return;
        if (value)
        {
            sprite.color = Color.green;
            canSpawnOnMe = true;
        }
        else
        {
            sprite.color = startColor;
            canSpawnOnMe = false;
        }
    }
    public bool CheckIsEmpty()
    {
        RaycastHit[] raycastHit = Physics.RaycastAll(transform.position + Vector3.down * 5, Vector3.up, 100f);
        return raycastHit.Length == 0;
    }
    public virtual void OnDestroy()
    {
        hexaInfo.RemoveCell(this);
        SOInstances.UIController.OnNewMemberSpawning -= TryToSpawnOnMe;
    }
}
