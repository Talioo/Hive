using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private UIMember spawningMember;
    private IEnumerator waitingForAprove;
    public delegate void NewMemberSpawn(bool value);
    public event NewMemberSpawn OnNewMemberSpawning;

    private void Awake()
    {
        SOInstances.SetInstance(this);
    }
    public void SpawnHiveMember(UIMember member)
    {
        if (waitingForAprove != null)
            return;
        OnNewMemberSpawning.Invoke(true);
        spawningMember = member;
        SOInstances.Shining.Show(member.transform.position);
        waitingForAprove = WaitingForAprove();
        StartCoroutine(waitingForAprove);
    }
    public void Aprove(Cell cell)
    {
        if (waitingForAprove == null)
            return;
        StopCoroutine(waitingForAprove);
        Instantiate(spawningMember.spavningPrefab, cell.transform.position, cell.transform.rotation);
        spawningMember.AproveSpawning();
        HideSpawningElements();
    }
    public void HideSpawningElements()
    {
        SOInstances.Shining.Hide();
        waitingForAprove = null;
        OnNewMemberSpawning.Invoke(false);
    }
    IEnumerator WaitingForAprove()
    {
        while (true)
        {
            yield return null;
            if (Input.GetMouseButtonDown(0))
            {
                yield return new WaitForSeconds(0.1f);
                HideSpawningElements();
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
