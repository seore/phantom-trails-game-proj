using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    public Image fadeImage; 

    private void Awake()
    {
        SetAlpha(0);
        gameObject.SetActive(true);
    }

    public void SetAlpha(float alpha)
    {
        var color = fadeImage.color;
        color.a = Mathf.Clamp01(alpha);
        fadeImage.color = color;
    }

    public IEnumerator FadeIn(float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            SetAlpha(Mathf.Lerp(0, 1, elapsed / duration));
            elapsed += Time.deltaTime;
            yield return null;
        }
        SetAlpha(1);
    }

    public IEnumerator FadeOut(float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            SetAlpha(Mathf.Lerp(1, 0, elapsed / duration));
            elapsed += Time.deltaTime;
            yield return null;
        }
        SetAlpha(0);
    }
}


