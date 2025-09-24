using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneFader : MonoBehaviour
{
    [Header("Configuración del Fade")]
    public Image fadeImage;          // Image negra que cubre la pantalla
    public float fadeDuration = 1f;  // Duración del fade

    private void Start()
    {
        // Inicia con fade-in al cargar la escena
        if (fadeImage != null)
        {
            StartCoroutine(FadeIn());
        }
        else
        {
            Debug.LogError("Asigna un Image al SceneFader.");
        }
    }

    // Fade-in: de negro a transparente
    public IEnumerator FadeIn()
    {
        float elapsed = 0f;
        Color c = fadeImage.color;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            c.a = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            fadeImage.color = c;
            yield return null;
        }
        c.a = 0f;
        fadeImage.color = c;
    }

    // Fade-out: de transparente a negro
    public IEnumerator FadeOut()
    {
        float elapsed = 0f;
        Color c = fadeImage.color;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            c.a = Mathf.Lerp(0f, 1f, elapsed / fadeDuration);
            fadeImage.color = c;
            yield return null;
        }
        c.a = 1f;
        fadeImage.color = c;
    }

    // Cambiar a otra escena con fade-out
    public void FadeToScene(string sceneName)
    {
        StartCoroutine(FadeAndLoad(sceneName));
    }

    private IEnumerator FadeAndLoad(string sceneName)
    {
        yield return StartCoroutine(FadeOut());
        SceneManager.LoadScene(sceneName);
    }
}
