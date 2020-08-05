using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    public CanvasGroup FadeScreen;
    [SerializeField]
    private float fadeTime = 1.0f;

    public static event System.Action<bool> OnShowStartMenu = delegate { };
    public static event System.Action<bool> OnShowIngameUI = delegate { };

    public void StartGame()
    {
        OnShowIngameUI(false);
        StartCoroutine(GameManager.Instance.PrepareGameForStart());
        StartCoroutine(FadeOut());
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private IEnumerator FadeOut()
    {
        while (FadeScreen.alpha < 1.0f)
        {
            FadeScreen.alpha += Time.deltaTime / fadeTime;
            yield return null;
        }

        OnShowStartMenu(false);
        yield return new WaitForSeconds(0.25f);
        OnShowIngameUI(true);
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        while (FadeScreen.alpha > 0.0f)
        {
            FadeScreen.alpha -= Time.deltaTime / fadeTime;
            yield return null;
        }
    }
}
