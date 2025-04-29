using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Text;
using System.Collections;
using Newtonsoft.Json;

public class TabernaAPIManager : MonoBehaviour
{
    private string apiUrl = "https://apideploy-a00838689.replit.app/Taberna";

    void Start()
    {
        StartCoroutine(EnviarDatosTaberna());
    }

    IEnumerator EnviarDatosTaberna()
    {
        int userId = PlayerPrefs.GetInt("UserId");
        int puntosJuego = PlayerPrefs.GetInt("turnos_jugador");
        float tiempoJuegoSegundos = PlayerPrefs.GetFloat("tiempo_total");

        TabernaData datos = new TabernaData
        {
            points = puntosJuego,
            time = TimeSpan.FromSeconds(tiempoJuegoSegundos)
        };

        string json = JsonConvert.SerializeObject(datos);
        Debug.Log("Enviando JSON: " + json);

        var request = new UnityWebRequest($"{apiUrl}/{userId}", "PUT");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.certificateHandler = new ForceAcceptAll(); 

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error al enviar datos: " + request.error);
        }
        else
        {
            Debug.Log("Datos enviados correctamente a la API de taberna.");
        }
    }
}

[Serializable]
public class TabernaData
{
    public int points;
    public TimeSpan time;
}
