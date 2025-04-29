using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

public class PreguntasTabernaGet : MonoBehaviour
{
    public enum Dificultad { Facil, Dificil }

    [Header("Configuración")]
    public Dificultad dificultad = Dificultad.Facil;

    [HideInInspector] public List<PreguntaTaberna> preguntas = new();
    private string apiUrl = "https://apideploy-a00838689.replit.app/PreguntasTaberna";

    void Start()
    {
        string endpoint = dificultad == Dificultad.Facil ? "faciles" : "dificiles";
        StartCoroutine(ObtenerPreguntas(endpoint));
    }

    IEnumerator ObtenerPreguntas(string tipo)
    {
        UnityWebRequest request = UnityWebRequest.Get($"{apiUrl}/{tipo}");
        request.certificateHandler = new ForceAcceptAll(); // importante si usas localhost
        request.downloadHandler = new DownloadHandlerBuffer();

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error al obtener preguntas: " + request.error);
            yield break;
        }

        PreguntaTaberna[] lista = JsonConvert.DeserializeObject<PreguntaTaberna[]>(request.downloadHandler.text);
        preguntas.AddRange(lista);
    }
}

[System.Serializable]
public class PreguntaTaberna
{
    public int id;
    public string pregunta;
    public string respuesta1;
    public string respuesta2;
    public string respuesta3;
    public string respuesta4;
    public string respuestaCorrecta;
}
