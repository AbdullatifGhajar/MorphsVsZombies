using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject ui;

    public string menuSceneName = "MainMenu";

    public SceneFader sceneFader;

    void Update()
    {
        // TODO solve conflicts with other menus e.g. CompleteLevel
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
            Toggle();
    }

    public void Toggle()
    {
        if (ui.activeSelf)
            Hide();
        else
            Show();
    }

    public void Show()
    {
        ui.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Hide()
    {
        ui.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Retry()
    {
        Hide();
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        Hide();
        sceneFader.FadeTo(menuSceneName);
    }

}
