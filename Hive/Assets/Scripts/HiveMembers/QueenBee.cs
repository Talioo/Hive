using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenBee : HiveMember
{
    [SerializeField] private SOHexaInfo hexaInfo;


    void OnMouseDown()
    {
        //TODO: Move to constants
        GetCell(transform.position + (Vector3.back * 2));
        GetCell(transform.position + (Vector3.back + Vector3.right * 2));
        GetCell(transform.position + (Vector3.back + Vector3.left * 2));
        GetCell(transform.position + (Vector3.forward * 2));
        GetCell(transform.position + (Vector3.forward + Vector3.right * 2));
        GetCell(transform.position + (Vector3.forward + Vector3.left * 2));
    }
    void GetCell(Vector3 position)
    {
        RaycastHit[] raycastHit = Physics.RaycastAll(transform.position, position, 100f);

        print(raycastHit.Length);
        if (raycastHit.Length < 1) return;
        foreach (var item in raycastHit)
        {
            var cell = item.collider.GetComponent<HexaCell>();
            if (cell == null) continue;
            if (cell.IsFree) cell.ReadyToUse(true);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + (Vector3.back * 2));
        Gizmos.DrawLine(transform.position, transform.position + (Vector3.back + Vector3.right * 2));
        Gizmos.DrawLine(transform.position, transform.position + (Vector3.back + Vector3.left * 2));
        Gizmos.DrawLine(transform.position, transform.position + (Vector3.forward * 2));
        Gizmos.DrawLine(transform.position, transform.position + (Vector3.forward + Vector3.right * 2));
        Gizmos.DrawLine(transform.position, transform.position + (Vector3.forward + Vector3.left * 2));
    }
}
