using UnityEngine;

public class ReachEnd : MonoBehaviour
{

    public SceneFader sceneFader;

    public void Quit()
    {
        Application.Quit();
    }

    public void Menu()
    {
        sceneFader.FadeTo("MainMenu");
    }

}
