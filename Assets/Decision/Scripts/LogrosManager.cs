using UnityEngine;
using System;
using UnityEngine.Networking;
using System.Collections;
using Newtonsoft.Json;

public class LogrosManager : MonoBehaviour
{
    private const string API_URL = "https://localhost:7220/Logros";
    private int userId;
    private bool[] logrosDesbloqueados = new bool[4];
    DecisionPointsManager pointsManager;
    StreakManagerD streakManager;

    public static string[] logrosDescripciones = new string[] {
        "¡Primera Victoria!",
        "Racha de 3 victorias",
        "Comprar primer item",
        "Acumular 500 puntos"
    };

    void Start()
    {
        userId = PlayerPrefs.GetInt("UserId");
        StartCoroutine(LoadLogrosAndVerify());
    }

        private IEnumerator LoadLogrosAndVerify()
    {
        yield return StartCoroutine(LoadLogros());
        
        // Verificar logros después de cargarlos
        pointsManager = FindObjectOfType<DecisionPointsManager>();
        streakManager = FindObjectOfType<StreakManagerD>();
        
        if (pointsManager != null && streakManager != null)
        {
            VerificarLogros(
                pointsManager.GetCurrentPoints(),
                streakManager.GetCurrentStreak(),
                false
            );
        }
    }

    public void DesbloquearLogro(int numeroLogro)
    {
        if (numeroLogro < 1 || numeroLogro > 4) return;
        if (!logrosDesbloqueados[numeroLogro - 1]) {
            StartCoroutine(SaveLogro(numeroLogro));
            MostrarLogro(numeroLogro);
        }
    }

    private void MostrarLogro(int numeroLogro)
    {
        Debug.Log($"¡Logro Desbloqueado: {logrosDescripciones[numeroLogro - 1]}!");
        // Aquí puedes agregar efectos visuales o sonidos
    }

    public void VerificarLogros(float puntos, int racha, bool compraRealizada)
    {
        Debug.Log("Puntos detectados " + puntos.ToString());
        Debug.Log("Racha detectada " + racha.ToString());
        // Logro 1: Primera Victoria
        if (puntos > 0 && !logrosDesbloqueados[0])
            DesbloquearLogro(1);

        // Logro 2: Racha de 3
        if (racha >= 3 && !logrosDesbloqueados[1])
            DesbloquearLogro(2);

        // Logro 3: Primera Compra
        if (compraRealizada && !logrosDesbloqueados[2])
            DesbloquearLogro(3);

        // Logro 4: 500 puntos
        if (puntos >= 500 && !logrosDesbloqueados[3])
            DesbloquearLogro(4);
    }

    private IEnumerator LoadLogros()
    {
        UnityWebRequest web = UnityWebRequest.Get($"{API_URL}/{userId}");
        web.certificateHandler = new ForceAcceptAll();
        yield return web.SendWebRequest();

        if (web.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error API: " + web.error);
        }
        else
        {
            var logrosData = JsonConvert.DeserializeObject<LogrosData>(web.downloadHandler.text);
            logrosDesbloqueados[0] = logrosData.logro_dec1;
            logrosDesbloqueados[1] = logrosData.logro_dec2;
            logrosDesbloqueados[2] = logrosData.logro_dec3;
            logrosDesbloqueados[3] = logrosData.logro_dec4;
        }
    }

    private IEnumerator SaveLogro(int numeroLogro)
    {
        UnityWebRequest web = UnityWebRequest.Put($"{API_URL}/{userId}/{numeroLogro}", "");
        web.certificateHandler = new ForceAcceptAll();
        yield return web.SendWebRequest();

        if (web.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error API: " + web.error);
        }
        else
        {
            logrosDesbloqueados[numeroLogro - 1] = true;
            Debug.Log($"Logro {numeroLogro} desbloqueado!");
        }
    }

    public bool IsLogroDesbloqueado(int numeroLogro)
    {
        if (numeroLogro < 1 || numeroLogro > 4) return false;
        return logrosDesbloqueados[numeroLogro - 1];
    }
}

[Serializable]
public class LogrosData
{
    public bool logro_dec1;
    public bool logro_dec2;
    public bool logro_dec3;
    public bool logro_dec4;
}
