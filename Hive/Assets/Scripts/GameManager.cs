using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public HiveMember selectedHiveMember;
    [SerializeField] GameObject loseScreen;
    private void Awake()
    {
        SOInstances.SetInstance(this);
        loseScreen.SetActive(false);
    }

    public void Lose()
    {
        loseScreen.SetActive(true);
    }
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
