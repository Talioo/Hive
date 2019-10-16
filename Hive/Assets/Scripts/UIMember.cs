using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMember : MonoBehaviour
{
    [SerializeField] private List<GameObject> images;
    [SerializeField] private Button button;
    public HiveMember spavningPrefab;

    private void Start()
    {
        button.onClick.AddListener(SpawnMe);
    }
    public void SpawnMe()
    {
        print("SpawnMe");
        SOInstances.UIController.SpawnHiveMember(this);
    }
    public void AproveSpawning()
    {
        var lastImage = images[images.Count - 1];
        lastImage.SetActive(false);
        if (lastImage != gameObject)
            images.Remove(lastImage);
    }
}
