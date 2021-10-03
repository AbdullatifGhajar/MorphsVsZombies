using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject UI;

    private SceneFader sceneFader;

    void Start()
    {
        sceneFader = GameObject.Find("SceneFader").GetComponent<SceneFader>();
    }

    void Update()
    {
        // TODO solve conflicts with other menus e.g. CompleteLevel
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
            Toggle();
    }

    public void Toggle()
    {
        if (UI.activeSelf)
            Hide();
        else
            Show();
    }

    public void Show()
    {
        UI.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Hide()
    {
        UI.SetActive(false);
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
        sceneFader.FadeTo("MainMenu");
    }

}
