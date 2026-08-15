using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTrigger : MonoBehaviour
{

    public string siguienteEscena;  
    public float duracionFade = 1f;
    public CanvasGroup fadeCanvasGroup; // arrastra aquí el CanvasGroup de la escena

    private bool activado = false;

    private void Reset()
    {
        var col = GetComponent<Collider>();
        if (col != null) col.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (activado) return;

        if (other.CompareTag("Player"))
        {
            activado = true;
            StartCoroutine(FadeYCambiarNivel());
        }
    }

    private IEnumerator FadeYCambiarNivel()
    {
        if (fadeCanvasGroup != null)
        {
            fadeCanvasGroup.blocksRaycasts = true;
            float tiempo = 0f;

            while (tiempo < duracionFade)
            {
                tiempo += Time.deltaTime;
                fadeCanvasGroup.alpha = Mathf.Clamp01(tiempo / duracionFade);
                yield return null;
            }

            fadeCanvasGroup.alpha = 1f;
        }

        SceneManager.LoadScene(siguienteEscena);
    }
}