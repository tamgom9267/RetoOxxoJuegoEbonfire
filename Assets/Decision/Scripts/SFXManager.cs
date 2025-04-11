using UnityEngine;

// Gestor de efectos de sonido del juego
public class SFXManager : MonoBehaviour
{
    // Referencias a los clips de audio
    public AudioClip Correcto;    // Sonido para respuesta correcta
    public AudioClip Incorrecto;  // Sonido para respuesta incorrecta
    public AudioClip Tiempo;      // Sonido para tiempo bajo

    // Reproduce sonido de respuesta correcta
    public void RespuestaCorrecta()
    {
        AudioSource.PlayClipAtPoint(Correcto,Camera.main.transform.position,0.4f);
    }

    // Reproduce sonido de respuesta incorrecta
    public void RespuestaIncorrecta()
    {
        AudioSource.PlayClipAtPoint(Incorrecto,Camera.main.transform.position,0.3f);
    }

    // Reproduce sonido de poco tiempo
    public void PocoTiempo()
    {
        AudioSource.PlayClipAtPoint(Tiempo,Camera.main.transform.position,0.5f);
    }
}
