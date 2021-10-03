using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private SceneFader sceneFader;

    void Start()
    {
        sceneFader = GameObject.Find("SceneFader").GetComponent<SceneFader>();
    }

    public void Retry()
    {
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        sceneFader.FadeTo("MainMenu");
    }

}
