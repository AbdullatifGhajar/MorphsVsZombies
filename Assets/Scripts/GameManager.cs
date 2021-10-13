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

    void Start()
    {
        GameIsOver = false;
        if (introduction)
            introduction.SetActive(true);
    }

    bool NoOverlay()
    {
        return !pauseMenuUI.activeSelf && !reachEndUI.activeSelf && !completeLevelUI.activeSelf && !gameOverUI.activeSelf && !introduction.activeSelf;
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
            reachEndUI.SetActive(true);
        else
            completeLevelUI.SetActive(true);
    }

}
