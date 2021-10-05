using UnityEngine;

public class OverlayMenu : MonoBehaviour
{
    void OnEnable()
    {
        Time.timeScale = 0f;
    }

    public void OnDisable()
    {
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && gameObject.activeSelf)
            gameObject.SetActive(false);
    }
}
