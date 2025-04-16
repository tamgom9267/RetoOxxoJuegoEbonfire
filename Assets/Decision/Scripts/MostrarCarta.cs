using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;
using Newtonsoft.Json;

public class MostrarCarta : MonoBehaviour
{
    public GameObject textPanel;
    public Text descriptionText;
    public Text streakText;
    private StreakManagerD streakManager;
    private DecisionPointsManager pointsManager;
    private SFXManager sfxmanager;
    private List<Pregunta> preguntas;
    private Pregunta preguntaActual;
    private const string API_URL = "https://localhost:7220/Preguntas";

    void Start()
    {
        textPanel.SetActive(false);
        sfxmanager = FindFirstObjectByType<SFXManager>();
        streakManager = FindFirstObjectByType<StreakManagerD>();
        pointsManager = FindFirstObjectByType<DecisionPointsManager>();
        if (streakManager == null)
        {
            Debug.LogError("No se encontró StreakManagerD en la escena.");
            return;
        }
        StartCoroutine(CargarPreguntas());
        UpdateStreakText();
    }

    private IEnumerator CargarPreguntas()
    {
        UnityWebRequest web = UnityWebRequest.Get(API_URL);
        web.certificateHandler = new ForceAcceptAll();
        yield return web.SendWebRequest();

        if (web.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error al cargar preguntas: " + web.error);
        }
        else
        {
            preguntas = JsonConvert.DeserializeObject<List<Pregunta>>(web.downloadHandler.text);
            Debug.Log($"Se cargaron {preguntas.Count} preguntas");
        }
    }

    public void OnBotonVerdaderoClick()
    {
        VerificarRespuesta(true);
    }

    public void OnBotonFalsoClick()
    {
        VerificarRespuesta(false);
    }

    public void OnCardClick()
    {
        if(!textPanel.activeSelf && preguntas != null && preguntas.Count > 0)
        {
            textPanel.SetActive(true);
            textPanel.transform.localScale = Vector3.zero;
            LeanTween.scale(textPanel, Vector3.one, 0.3f).setEase(LeanTweenType.easeOutBack);
            
            int indiceAleatorio = Random.Range(0, preguntas.Count);
            preguntaActual = preguntas[indiceAleatorio];
            descriptionText.text = preguntaActual.TextoPregunta;
        }
        else
        {
            LeanTween.scale(textPanel, Vector3.zero, 0.2f).setEase(LeanTweenType.easeInBack).setOnComplete(() => {
                textPanel.SetActive(false);
            });
        }
    }

    public void UpdateStreakText()
    {
        if (streakText != null && streakManager != null)
        {
            streakText.text = streakManager.GetCurrentStreak() >= 2 ? "🔥 " + streakManager.GetCurrentStreak().ToString() : "";
        }
    }

    private void VerificarRespuesta(bool respuestaUsuario)
    {
        if(preguntaActual == null) return;

        if(respuestaUsuario == preguntaActual.RespuestaCorrecta)
        {
            sfxmanager.RespuestaCorrecta();
            pointsManager.AddPoints(10f);
            UpdateStreakText();
            Debug.Log("¡Correcto!");
            if (Random.value <= 0.5f)
            {
                var pptGame = FindFirstObjectByType<PPTGameManager>();
                if (pptGame != null)
                {
                    pptGame.PlayGame();
                    textPanel.SetActive(false);
                }
            }
            else 
            {
                streakManager.IncrementStreak();
                pointsManager.AddPoints(10f);
                UpdateStreakText();
                textPanel.SetActive(false);
            }
        }
        else
        {
            sfxmanager.RespuestaIncorrecta();
            Debug.Log("Incorrecto");
            streakManager.ResetStreak();
            pointsManager.ReducePoints(5f);
            UpdateStreakText();
            textPanel.SetActive(false);
        }
    }

    [System.Serializable]
    private class Pregunta
    {
        public int Id { get; set; }
        public string TextoPregunta { get; set; }
        public bool RespuestaCorrecta { get; set; }
    }
}