using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public GameObject buttonPrefab;

    public int levelCount;

    void Start()
    {
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);

        for (int i = 1; i <= levelCount; i++)
        {
            GameObject buttonGO = Instantiate(buttonPrefab);
            buttonGO.transform.SetParent(transform);

            Text text = buttonGO.transform.Find("Text").GetComponent<Text>();
            text.text = i.ToString();

            Button button = buttonGO.GetComponent<Button>();
            if (i > levelReached)
                button.interactable = false;
        }
    }
}
