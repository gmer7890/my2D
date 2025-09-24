using UnityEngine;
using TMPro;

public class TextoGlitch : MonoBehaviour
{
    public TMP_Text texto;
    public float velocidad = 10f;

    void Update()
    {
        // Hace que el texto desaparezca y reaparezca aleatoriamente
        if (Random.value > 0.9f)
        {
            texto.enabled = !texto.enabled;
        }

        // Efecto de cambio de color
        float t = Mathf.PingPong(Time.time * velocidad, 1);
        texto.color = Color.Lerp(Color.red, Color.white, t);
    }
}
