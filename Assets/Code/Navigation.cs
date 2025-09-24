using UnityEngine;
using UnityEngine.SceneManagement;

public class Navigation : MonoBehaviour
{
    // Carga una escena por nombre (útil para botones que pasan el nombre)
    public void LoadScene(string sceneName)
    {
        if (!string.IsNullOrEmpty(sceneName))
            SceneManager.LoadScene(sceneName);
    }

    // Carga una escena por índice (opcional)
    public void LoadSceneByIndex(int index)
    {
        if (index >= 0 && index < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(index);
    }

    // Va a la siguiente escena según el orden en Build Settings
    public void NextScene()
    {
        int current = SceneManager.GetActiveScene().buildIndex;
        int total = SceneManager.sceneCountInBuildSettings;
        if (current < total - 1)
            SceneManager.LoadScene(current + 1);
        // else: opcionalmente podrías volver a 0 con SceneManager.LoadScene(0);
    }

    // Va a la escena anterior según el orden en Build Settings
    public void PreviousScene()
    {
        int current = SceneManager.GetActiveScene().buildIndex;
        if (current > 0)
            SceneManager.LoadScene(current - 1);
    }

    // Cierra la aplicación (funciona en editor y en build)
    public void QuitApp()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }
}
