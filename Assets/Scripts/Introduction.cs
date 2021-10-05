using UnityEngine;

public class Introcution : MonoBehaviour
{
    public GameObject UI;
    void Start()
    {
        UI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Continue()
    {
        UI.SetActive(false);
        Time.timeScale = 1f;
    }

}
