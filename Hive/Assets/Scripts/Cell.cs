using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [HideInInspector] public bool readyToUse = false;
    public List<HiveMember> hiveMembersOnMe { get; private set; }
    public SpriteRenderer sprite { get; private set; }
    private Color startColor;
    private bool canSpawnOnMe = false;
    public virtual void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        startColor = sprite.color;
        SOInstances.SOHexaInfo.AddNewCell(this);
        SOInstances.UIController.OnNewMemberSpawning += TryToSpawnOnMe;
        hiveMembersOnMe = new List<HiveMember>();
        SOInstances.SOHexaInfo.OnRemoveCells += RemoveCells;
    }
    public void OnMouseDown()
    {
        if (canSpawnOnMe)
        {
            SOInstances.UIController.Aprove(this);
            SOInstances.SOHexaInfo.TryToRemoveCells();
        }
        if (readyToUse)
        {
            SOInstances.GameManager.selectedHiveMember.Move(this);
            SOInstances.SOHexaInfo.TryToRemoveCells();
        }
    }
    public virtual void ReadyToUse(bool value)
    {
        readyToUse = value;
        if (readyToUse)
            sprite.color = Color.blue;
        else
            sprite.color = startColor;
    }
    protected virtual void RemoveCells()
    {
        ReadyToUse(false);
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
        RaycastHit[] raycastHit = Physics.RaycastAll(transform.position + Vector3.down, Vector3.up, Constants.RayLenght);
        return raycastHit.Length == 0;
    }
    public virtual void OnDestroy()
    {
        SOInstances.SODuplicateCells.RestructCells();
        SOInstances.UIController.OnNewMemberSpawning -= TryToSpawnOnMe;
        SOInstances.SOHexaInfo.OnRemoveCells -= RemoveCells;
    }
}
