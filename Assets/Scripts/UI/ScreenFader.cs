using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFader : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1f;

    private void Start()
    {
        if (fadeImage != null)
        {
            Color color = fadeImage.color;
            color.a = 0f;
            fadeImage.color = color;
        }
    }

    public IEnumerator FadeToBlack()
    {
        fadeImage.enabled = true;
        float timeElapsed = 0f;
        Color color = fadeImage.color;

        while (timeElapsed < fadeDuration)
        {
            color.a = Mathf.Lerp(0f, 1f, timeElapsed / fadeDuration);
            fadeImage.color = color;
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        color.a = 1f;
        fadeImage.color = color;
    }

    public IEnumerator FadeToClear()
    {
        float timeElapsed = 0f;
        Color color = fadeImage.color;

        while (timeElapsed < fadeDuration)
        {
            color.a = Mathf.Lerp(1f, 0f, timeElapsed / fadeDuration);
            fadeImage.color = color;
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        color.a = 0f;
        fadeImage.color = color;
        fadeImage.enabled = false;
    }
}
