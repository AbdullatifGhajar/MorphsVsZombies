using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class LevelSelector : MonoBehaviour
{
    public GameObject buttonPrefab;

    public static int LevelCount = 6;

    void Start()
    {
        // string AssetsFolderPath = Application.dataPath;
        // string levelFolder = AssetsFolderPath + "/Levels";
        // DirectoryInfo dir = new DirectoryInfo(levelFolder);
        // LevelCount = dir.GetFiles("*.unity").Length;

        int levelReached = PlayerPrefs.GetInt("levelReached", 1);

        for (int i = 1; i <= LevelCount; i++)
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
