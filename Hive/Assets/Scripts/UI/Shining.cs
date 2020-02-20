using UnityEngine;
using UnityEngine.UI;

public class Shining : MonoBehaviour
{
    [SerializeField] private Image image;

    private void Awake()
    {
        SOInstances.SetInstance(this);
        Hide();
    }
    public void Hide()
    {
        image.enabled = false;
    }
    public void Show(Vector3 position)
    {
        image.gameObject.transform.position = position;
        image.enabled = true;
    }
}
