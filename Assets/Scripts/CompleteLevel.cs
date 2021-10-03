using UnityEngine;

public class CompleteLevel : MonoBehaviour
{
    private SceneFader sceneFader;

    void Start()
    {
        sceneFader = GameObject.Find("SceneFader").GetComponent<SceneFader>();
    }

    public void Continue()
    {
        GameManager.Level += 1;
        sceneFader.FadeTo("Level" + GameManager.Level);
    }

    public void Menu()
    {
        sceneFader.FadeTo("MainMenu");
    }

}
