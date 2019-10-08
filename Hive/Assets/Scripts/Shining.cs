using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shining : MonoBehaviour
{
    private static Image image;

    void Start()
    {
        image = GetComponent<Image>();
        Hide();
    }

    public static void Hide()
    {
        image.enabled = false;
    }
    public static void Show(Vector3 position)
    {
        image.gameObject.transform.position = position;
        image.enabled = true;
    }
}
