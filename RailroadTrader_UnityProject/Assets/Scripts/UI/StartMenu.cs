using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    public GameObject StartMenuWindow;
    public CanvasGroup FadeScreen;
    [SerializeField]
    private float fadeTime = 1.0f;
    public void StartGame()
    {
        StartCoroutine(GameManager.Instance.PrepareGameForStart());
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        while (FadeScreen.alpha < 1.0f)
        {
            FadeScreen.alpha += Time.deltaTime / fadeTime;
            yield return null;
        }

        StartMenuWindow.SetActive(false);
        yield return new WaitForSeconds(0.25f);
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
