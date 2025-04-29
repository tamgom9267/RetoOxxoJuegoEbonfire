using UnityEngine.UI;
using UnityEngine;
using System;
using UnityEngine.Networking;
using System.Collections;
using Newtonsoft.Json;

// Gestor del sistema de puntos y su sincronización con el servidor
public class DecisionPointsManager : MonoBehaviour
{
    private const string API_URL = "https://apideploy-a00838689.replit.app/DecisionPoints";
    private float currentPoints;                // Puntos actuales
    private StreakManagerD streakManager;       // Gestor de rachas
    private int userId;                         // ID del usuario
    public Text pointsText;                    // Texto UI para puntos

    // Inicialización y carga de puntos
    void Start()
    {
        userId = PlayerPrefs.GetInt("UserId");
        streakManager = FindFirstObjectByType<StreakManagerD>();
        StartCoroutine(LoadPoints());
        UpdatePointsText();
    }

    // Añade puntos considerando el multiplicador por racha
    public void AddPoints(float points)
    {
        float multiplier = CalculateMultiplier();
        float newPoints = currentPoints + (points * multiplier);
        currentPoints = Mathf.Min(newPoints, 1000f);
        UpdatePointsText();
        StartCoroutine(SavePoints());
    }

    // Calcula el multiplicador basado en la racha actual
    private float CalculateMultiplier()
    {
        int streak = streakManager.GetCurrentStreak();
        if (streak >= 5) return 2.0f;
        if (streak >= 3) return 1.5f;
        return 1.0f;
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

    // Obtiene los puntos actuales
    public float GetCurrentPoints() { return currentPoints; }

    // Actualiza el texto UI de puntos con animación
    private void UpdatePointsText()
    {
        if(pointsText != null)
        {
            pointsText.text = $"{currentPoints}";
            LeanTween.cancel(pointsText.gameObject);
            pointsText.transform.localScale = Vector3.one * 1.2f;
            LeanTween.scale(pointsText.gameObject, Vector3.one, 0.3f)
                .setEase(LeanTweenType.easeOutElastic);
        }
    }

    // Reduce puntos
    public void ReducePoints(float points)
    {
        currentPoints = Mathf.Max(0, currentPoints - points);
        UpdatePointsText();
        StartCoroutine(SavePoints());
    }
}

// Estructura de datos para los puntos
public class DecisionPointsData
{
    public float points;
    public TimeSpan time;
}