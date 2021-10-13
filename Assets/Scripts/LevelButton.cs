using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    private SceneFader sceneFader;
    public int level;
    void Start()
    {
        sceneFader = GameObject.Find("SceneFader").GetComponent<SceneFader>();

        Text text = transform.Find("Text").GetComponent<Text>();
        level = int.Parse(text.text);

        Button button = GetComponent<Button>();
        button.onClick.AddListener(SelectLevel);
    }

    public void SelectLevel()
    {
        GameManager.Level = level;
        sceneFader.FadeTo("Level" + level);
    }

}
