using UnityEngine;
using System;
using UnityEngine.Networking;
using System.Collections;
using Newtonsoft.Json;
using Unity.Android.Gradle.Manifest;

// Gestor del sistema de rachas y su sincronización con el servidor
public class StreakManagerD : MonoBehaviour
{
    private const string API_URL = "https://localhost:7220/Streaks";
    public int currentStreak;   // Racha actual del jugador
    private int userId;         // ID del usuario
    LogrosManager logros;
    
    // Inicialización y carga de racha
    void Start()
    {
        userId = PlayerPrefs.GetInt("UserId");
        logros = FindFirstObjectByType<LogrosManager>();
        StartCoroutine(LoadStreak());
    }

    // Incrementa la racha actual
    public void IncrementStreak()
    {
        currentStreak++;
        StartCoroutine(SaveStreak());
    }
    
    // Reinicia la racha a cero
    public void ResetStreak()
    {
        currentStreak = 0;
        StartCoroutine(SaveStreak());
    }

    // Obtiene la racha actual
    public void getStreak()
    {
        StartCoroutine(LoadStreak());
    }
    
    // Guarda la racha en el servidor
    private IEnumerator SaveStreak()
    {
        UnityWebRequest web = UnityWebRequest.Put($"{API_URL}/{userId}", 
            JsonConvert.SerializeObject(new StreakData { userId = userId, streak = currentStreak }));
        web.certificateHandler = new ForceAcceptAll();
        web.SetRequestHeader("Content-Type", "application/json");
        
        yield return web.SendWebRequest();

        if(web.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error API: " + web.error);
        }
        if(currentStreak >= 3)
        {
            logros.DesbloquearLogro(3);
        }
    }
    
    // Carga la racha desde el servidor
    public IEnumerator LoadStreak()
    {
        string JSONurl = $"{API_URL}/{userId}";
        UnityWebRequest web = UnityWebRequest.Get(JSONurl);
        web.certificateHandler = new ForceAcceptAll();
        yield return web.SendWebRequest();

        if(web.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error API: " + web.error);
            currentStreak = 0;
        }
        else
        {
            StreakData streakData = JsonConvert.DeserializeObject<StreakData>(web.downloadHandler.text);
            currentStreak = streakData.streak;
            Debug.Log(currentStreak);
        }
    }

    // Retorna la racha actual
    public int GetCurrentStreak()
    {
        return currentStreak;
    }
}

// Estructura de datos para la racha
public class StreakData
{
    public int userId;   // ID del usuario
    public int streak;   // Valor de la racha
}

