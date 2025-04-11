using UnityEngine.UI;
using UnityEngine;
using System;
using UnityEngine.Networking;
using System.Collections;
using Newtonsoft.Json;

// Clase que maneja el sistema de puntos del juego y su sincronización con el servidor
public class DecisionPointsManager : MonoBehaviour
{
    private const string API_URL = "https://localhost:7220/DecisionPoints"; // URL del servidor
    private float currentPoints;                                              // Puntos actuales
    private StreakManagerD streakManager;                                     // Gestor de rachas
    private int userId;                                                       // ID del usuario
    public Text pointsText;                                                  // Texto UI para mostrar puntos

    void Start()
    {
        userId = 1; // ID de usuario hardcodeado
        streakManager = FindFirstObjectByType<StreakManagerD>();
        StartCoroutine(LoadPoints());    // Carga puntos del servidor
        UpdatePointsText();              // Actualiza UI
    }

    // Añade puntos considerando el multiplicador por racha
    public void AddPoints(float points)
    {
        float multiplier = CalculateMultiplier();
        currentPoints += points * multiplier;
        float newPoints = currentPoints + (points * multiplier);
        currentPoints = Mathf.Min(newPoints, 1000f);  // Límite máximo de 1000 puntos
        UpdatePointsText();
        StartCoroutine(SavePoints());
    }

    // Calcula el multiplicador basado en la racha actual
    private float CalculateMultiplier()
    {
        int streak = streakManager.GetCurrentStreak();
        if (streak >= 5) return 2.0f;    // x2 para rachas de 5+
        if (streak >= 3) return 1.5f;    // x1.5 para rachas de 3-4
        return 1.0f;                     // Sin multiplicador
    }

    // Guarda los puntos en el servidor
    private IEnumerator SavePoints()
    {
        var pointsData = new DecisionPointsData
        {
            points = currentPoints,
            time = DateTime.Now.TimeOfDay
        };

        string json = JsonConvert.SerializeObject(pointsData);
        var web = UnityWebRequest.Put($"{API_URL}/{userId}", json);
        web.certificateHandler = new ForceAcceptAll();
        web.SetRequestHeader("Content-Type", "application/json");
        
        yield return web.SendWebRequest();

        if(web.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error API: " + web.error);
        }
    }

    // Carga los puntos desde el servidor
    private IEnumerator LoadPoints()
    {
        UnityWebRequest web = UnityWebRequest.Get($"{API_URL}/{userId}");
        web.certificateHandler = new ForceAcceptAll();
        yield return web.SendWebRequest();

        if(web.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error API: " + web.error);
            currentPoints = 0;
        }
        else
        {
            var pointsData = JsonConvert.DeserializeObject<DecisionPointsData>(web.downloadHandler.text);
            currentPoints = pointsData.points;
            UpdatePointsText();
        }
    }

    // Getters y setters
    public float GetCurrentPoints() { return currentPoints; }

    // Actualiza el texto UI de puntos
    private void UpdatePointsText()
    {
        if(pointsText != null)
        {
            pointsText.text = $"Puntos: {currentPoints}";
        }
    }

    // Reduce puntos (por ejemplo, al comprar items)
    public void ReducePoints(float points)
    {
        currentPoints = Mathf.Max(0, currentPoints - points);
        UpdatePointsText();
        StartCoroutine(SavePoints());
    }
}

public class DecisionPointsData
{
    public float points;
    public TimeSpan time;
}