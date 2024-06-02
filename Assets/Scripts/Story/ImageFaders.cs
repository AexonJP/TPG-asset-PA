using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageFaders : MonoBehaviour
{
    public float fadeDuration = 1f; // Durasi fade in dan fade out

    public void FadeIn(Image image)
    {
        StartCoroutine(FadeImage(image, 0f, 1f));
    }

    public void FadeOut(Image image)
    {
        StartCoroutine(FadeImage(image, 1f, 0f));
    }

    private IEnumerator FadeImage(Image image, float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;
        Color color = image.color;
        color.a = startAlpha;
        image.color = color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            image.color = color;
            yield return null;
        }

        color.a = endAlpha;
        image.color = color;
    }
}