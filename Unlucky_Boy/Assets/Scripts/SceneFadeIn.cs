using System.Collections;
using UnityEngine;

public class SceneFadeIn : MonoBehaviour
{
    
    public CanvasGroup fadeCanvasGroup;
    public float duracionFade = 1f;

    private void Start()
    {
        if (fadeCanvasGroup != null)
        {
            fadeCanvasGroup.alpha = 1f;
            fadeCanvasGroup.blocksRaycasts = true;
            StartCoroutine(FadeIn());
        }
    }

    private IEnumerator FadeIn()
    {
        float tiempo = 0f;

        while (tiempo < duracionFade)
        {
            tiempo += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Clamp01(1f - (tiempo / duracionFade));
            yield return null;
        }

        fadeCanvasGroup.alpha = 0f;
        fadeCanvasGroup.blocksRaycasts = false;
    }
}