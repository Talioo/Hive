using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SOInstances : ScriptableObject
{
    public static UIController UIController { get; private set; }



    public static void SetInstance(UIController _UIController)
    {
        if (UIController == null) UIController = _UIController;
        else Destroy(_UIController.gameObject);
    }

}
