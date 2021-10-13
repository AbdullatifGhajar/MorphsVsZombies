using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private SceneFader sceneFader;

    void Start()
    {
        sceneFader = GameObject.Find("SceneFader").GetComponent<SceneFader>();
    }

    public void Play()
    {
        sceneFader.FadeTo("LevelSelect");
    }

    public void Quit()
    {
        Debug.Log("Exiting...");
        Application.Quit();
    }

}
