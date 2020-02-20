using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public HiveMember selectedHiveMember;
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private SOContainer container;
    private void Awake()
    {
        SOInstances.SetInstance(this);
        SOInstances.SetInstance(container);
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
