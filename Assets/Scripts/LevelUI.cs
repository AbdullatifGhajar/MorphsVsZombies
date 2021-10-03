using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    private Text levelText;

    void Start()
    {
        levelText = GetComponent<Text>();
    }

    void Update()
    {
        levelText.text = "Level " + GameManager.Level.ToString();
    }
}
