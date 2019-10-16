using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private UIMember spawningMember;
    IEnumerator waitingForAprove;
    private void Awake()
    {
        SOInstances.SetInstance(this);
    }
    public void SpawnHiveMember(UIMember member)
    {
        if (waitingForAprove != null)
            return;
        spawningMember = member;
        SOInstances.Shining.Show(member.transform.position);
        waitingForAprove = WaitingForAprove();
        StartCoroutine(waitingForAprove);
    }
    public void Aprove()
    {
        if (waitingForAprove == null)
            return;
        StopCoroutine(waitingForAprove);
        waitingForAprove = null;
        spawningMember.AproveSpawning();
    }
    public void Cancel()
    {
        SOInstances.Shining.Hide();
        waitingForAprove = null;

    }
    IEnumerator WaitingForAprove()
    {
        while (true)
        {
            yield return null;
            if (Input.GetMouseButtonDown(0))
            {
                yield return new WaitForSeconds(0.1f);
                Cancel();
                break;
            }
        }
    }
    bool TouchUI()
    {
        GraphicRaycaster gr = GetComponent<GraphicRaycaster>();
        PointerEventData ped = new PointerEventData(null);
        ped.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        gr.Raycast(ped, results);
        return results.Count > 0;
    }
}
