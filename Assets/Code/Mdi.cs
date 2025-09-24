using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Mdi : MonoBehaviour
{
    [Header("Diálogo")]
    [TextArea(3, 10)]
    public string[] lineas;      // Diálogos a mostrar
    public float velocidad = 0.05f;
    public TMP_Text Dialogo;     // Asigna el TextMeshPro del diálogo

    [Header("Opciones")]
    public string siguienteEscena; // Nombre de la escena a cargar al terminar

    private int indiceLinea = 0;
    private Coroutine escribiendoCoroutine;

    void Start()
    {
        if (lineas.Length > 0)
            EscribirLinea(indiceLinea);
    }

    void Update()
    {
        // Avanzar con Enter
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (escribiendoCoroutine != null)
            {
                // Terminar de escribir la línea instantáneamente
                StopCoroutine(escribiendoCoroutine);
                Dialogo.text = lineas[indiceLinea];
                escribiendoCoroutine = null;
            }
            else
            {
                SiguienteLinea();
            }
        }

        // Volver al menú con Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LoadScene("0");
        }
    }

    void EscribirLinea(int index)
    {
        escribiendoCoroutine = StartCoroutine(EscribirTexto(lineas[index]));
    }

    IEnumerator EscribirTexto(string frase)
    {
        Dialogo.text = "";
        foreach (char letra in frase)
        {
            Dialogo.text += letra;
            yield return new WaitForSeconds(velocidad);
        }
        escribiendoCoroutine = null;
    }

    void SiguienteLinea()
    {
        if (indiceLinea < lineas.Length - 1)
        {
            indiceLinea++;
            EscribirLinea(indiceLinea);
        }
        else
        {
            // Al terminar todos los diálogos, carga la escena definida
            if (!string.IsNullOrEmpty(siguienteEscena))
            {
                LoadScene(siguienteEscena);
            }
            else
            {
                Dialogo.text = ""; // Limpiar texto si no hay escena definida
            }
        }
    }

    // Método público para cargar escenas por nombre
    public void LoadScene(string sceneName)
    {
        if (!string.IsNullOrEmpty(sceneName))
            SceneManager.LoadScene(sceneName);
    }
}
