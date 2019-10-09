using UnityEngine;

public class SOInstances : ScriptableObject
{
    public static UIController UIController { get; private set; }
    public static Shining Shining { get; private set; }
    public static GameManager GameManager { get; private set; }

    public static void SetInstance(UIController _UIController)
    {
        if (UIController == null)
        {
            UIController = _UIController;
        }
        else
            Destroy(_UIController.gameObject);
    }
    public static void SetInstance(Shining _Shining)
    {
        if (Shining == null)
        {
            Shining = _Shining;
        }
        else
            Destroy(_Shining.gameObject);
    }
    public static void SetInstance(GameManager _GameManager)
    {
        if (GameManager == null)
        {
            GameManager = _GameManager;
        }
        else
            Destroy(_GameManager.gameObject);
    }

}
