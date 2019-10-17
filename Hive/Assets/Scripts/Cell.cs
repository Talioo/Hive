using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public SOHexaInfo hexaInfo;
    public List<HiveMember> hiveMembersOnMe { get; private set; }
    [HideInInspector] public bool readyToUse = false;
    private SpriteRenderer sprite;
    private Color startColor;
    private const float rayLenght = 100f;
    private bool canSpawnOnMe = false;
    public virtual void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        startColor = sprite.color;
        hexaInfo.AddNewCell(this);
        SOInstances.UIController.OnNewMemberSpawning += TryToSpawnOnMe;
        hiveMembersOnMe = new List<HiveMember>();
    }
    public void OnMouseDown()
    {
        if (canSpawnOnMe)
        {
            SOInstances.UIController.Aprove(this);
            hexaInfo.TryToRemoveCells();
        }
        if (readyToUse)
            SOInstances.GameManager.selectedHiveMember.Move(this);
    }
    public virtual void ReadyToUse(bool value)
    {
        readyToUse = value;
        if (readyToUse)
            sprite.color = Color.blue;
        else
            sprite.color = startColor;
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
        RaycastHit[] raycastHit = Physics.RaycastAll(transform.position + Vector3.down, Vector3.up, rayLenght);
        return raycastHit.Length == 0;
    }
    public virtual void OnDestroy()
    {
        hexaInfo.RemoveCell(this);
        hexaInfo.duplicateCells.RestructCells();
        SOInstances.UIController.OnNewMemberSpawning -= TryToSpawnOnMe;
    }
}
