using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using Newtonsoft.Json;

public class TabernaGetAPI : MonoBehaviour
{
    private string apiUrl = "https://localhost:7220/Taberna";

    public int turnosPrevios;
    public TimeSpan tiempoPrevio;

    [Header("UI")]
    public GameObject ContinueCanvas;
    public Text textoResumen;

    public void Start()
    {
        if (PlayerPrefs.GetInt("JuegoIniciado", 0) == 1)
        {
            StartCoroutine(ObtenerDatosTaberna());
        }
    }

    IEnumerator ObtenerDatosTaberna()
    {
        int userId = PlayerPrefs.GetInt("UserId");

        string url = $"{apiUrl}/{userId}";
        var request = UnityWebRequest.Get(url);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.certificateHandler = new ForceAcceptAll();

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error al obtener datos: " + request.error);
        }
        else
        {
            string json = request.downloadHandler.text;
            TabernaGetData datos = JsonConvert.DeserializeObject<TabernaGetData>(json);

            turnosPrevios = (int)datos.points;

            if (TimeSpan.TryParse(datos.time, out TimeSpan ts))
            {
                tiempoPrevio = ts;
            }
            else
            {
                tiempoPrevio = TimeSpan.Zero;
            }

            // Mostrar los datos en el canvas y pausar el tiempo
            ContinueCanvas.SetActive(true);
            textoResumen.text = $"Turnos: {turnosPrevios}   Tiempo: {Mathf.RoundToInt((float)tiempoPrevio.TotalSeconds)}s";
            Time.timeScale = 0f; // Pausar tiempo
        }
    }

    public void OnContinuar()
    {
        ContinueCanvas.SetActive(false);
        PlayerPrefs.SetInt("JuegoIniciado", 0);
        PlayerPrefs.Save();
        Time.timeScale = 1f; // Reanudar tiempo
    }

    public void OnVolver()
    {
        Debug.Log("Botón Volver presionado.");
        Time.timeScale = 1f; // Reanudar tiempo antes de cambiar de escena
        SceneManager.LoadScene("MenuScene");
    }
}

[Serializable]
public class TabernaGetData
{
    public float points;
    public string time;
}
