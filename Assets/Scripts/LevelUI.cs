using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    public Text levelText;

    void Update()
    {
        levelText.text = "Level " + GameManager.Level.ToString();
    }
}
