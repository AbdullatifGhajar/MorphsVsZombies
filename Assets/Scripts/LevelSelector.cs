using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public SceneFader sceneFader;

    public Button[] levelButtons;

    void Start()
    {
        sceneFader = GameObject.Find("SceneFader").GetComponent<SceneFader>();

        int levelReached = GameManager.Level;
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i + 1 > levelReached)
                levelButtons[i].interactable = false;
        }
    }

    public void Select(string levelName)
    {
        sceneFader.FadeTo(levelName);
    }

}
