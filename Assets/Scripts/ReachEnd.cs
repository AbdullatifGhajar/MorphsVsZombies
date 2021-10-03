using UnityEngine;

public class ReachEnd : MonoBehaviour
{

    private SceneFader sceneFader;

    void Start()
    {
        sceneFader = GameObject.Find("SceneFader").GetComponent<SceneFader>();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Menu()
    {
        sceneFader.FadeTo("MainMenu");
    }

}
