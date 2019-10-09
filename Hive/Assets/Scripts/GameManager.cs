using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public HiveMember selectedHiveMember;
    private void Awake()
    {
        SOInstances.SetInstance(this);
    }
}
