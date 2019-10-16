using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMember : MonoBehaviour
{
    [SerializeField] private UIController uiController;
    [SerializeField] private List<GameObject> images;
    [SerializeField] private Button button;


    private void Start()
    {
        button.onClick.AddListener(SpawnMe);
    }
    void SpawnMe()
    {
        uiController.SpawnHiveMember(this);
    }
    public void AproveSpawning()
    {
        var lastImage = images[images.Count - 1];
        lastImage.SetActive(false);
        if (lastImage != gameObject)
            images.Remove(lastImage);
    }
}
