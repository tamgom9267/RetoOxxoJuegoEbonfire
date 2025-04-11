using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public AudioClip Correcto;
    public AudioClip Incorrecto;
    public AudioClip Tiempo;

    public void RespuestaCorrecta()
    {
        AudioSource.PlayClipAtPoint(Correcto,Camera.main.transform.position,0.4f);
    }
        public void RespuestaIncorrecta()
    {
        AudioSource.PlayClipAtPoint(Incorrecto,Camera.main.transform.position,0.3f);
    }

    public void PocoTiempo()
    {
        AudioSource.PlayClipAtPoint(Tiempo,Camera.main.transform.position,0.5f);
    }

}
