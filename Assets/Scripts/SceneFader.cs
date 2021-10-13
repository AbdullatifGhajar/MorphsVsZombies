using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneFader : MonoBehaviour
{
    public Image image;
    public AnimationCurve curve;

    void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void FadeTo(string scene)
    {
        StartCoroutine(FadeOut(scene));
    }

    public void Reload()
    {
        FadeTo(SceneManager.GetActiveScene().name);
    }

    public void FadeToNextLevel()
    {
        GameManager.Level += 1;
        if (PlayerPrefs.GetInt("levelReached", 1) < GameManager.Level)
            PlayerPrefs.SetInt("levelReached", GameManager.Level);

        FadeTo("Level" + GameManager.Level);
    }

    IEnumerator FadeIn()
    {
        float t = 1f;

        while (t > 0f)
        {
            t -= Time.unscaledDeltaTime;
            float a = curve.Evaluate(t);
            image.color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }
    }

    IEnumerator FadeOut(string scene)
    {
        float t = 0f;

        while (t < 1f)
        {
            t += Time.unscaledDeltaTime;
            float a = curve.Evaluate(t);
            image.color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }

        SceneManager.LoadScene(scene);
    }

}
