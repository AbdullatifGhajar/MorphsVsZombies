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
        GameManager.Level = 1;
        sceneFader.FadeTo("Level" + GameManager.Level);
    }

    public void Quit()
    {
        Debug.Log("Exiting...");
        Application.Quit();
    }

}
