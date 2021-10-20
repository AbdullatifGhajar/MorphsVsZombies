using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool GameIsOver;

    public static int Level = 1;

    public GameObject pauseMenuUI;
    public GameObject gameOverUI;
    public GameObject completeLevelUI;
    public GameObject reachEndUI;
    public GameObject introduction;
    public bool useIntroduction = true;

    void Start()
    {
        GameIsOver = false;
        if (useIntroduction)
            introduction.SetActive(true);
    }

    bool NoOverlay()
    {
        return !pauseMenuUI.activeSelf && !completeLevelUI.activeSelf && !gameOverUI.activeSelf && !introduction.activeSelf;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && NoOverlay())
            pauseMenuUI.SetActive(true);

        if (GameIsOver)
            return;

        if (PlayerStats.Lives <= 0)
            LoseGame();
    }

    public void LoseGame()
    {
        GameIsOver = true;
        gameOverUI.SetActive(true);
    }

    public void WinLevel()
    {
        GameIsOver = true;
        if(Level == LevelSelector.LevelCount)
        {
            reachEndUI.SetActive(true);
            enabled = false;
        }
        else
            completeLevelUI.SetActive(true);
    }

}
