using UnityEngine;

public class SOInstances : ScriptableObject
{
    [SerializeField]
    SOContainer container;
    public static UIController UIController { get; private set; }
    public static Shining Shining { get; private set; }
    public static GameManager GameManager { get; private set; }
    public static SOContainer SOContainer { get; private set; }
    public static SOHexaInfo SOHexaInfo => SOContainer.hexaInfo;
    public static SODuplicateCells SODuplicateCells => SOContainer.duplicateCells;

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
    public static void SetInstance(SOContainer _SOContainer)
    {
        if (SOContainer == null)
        {
            SOContainer = _SOContainer;
        }
    }

}
