using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class LogrosTabernaManager : MonoBehaviour
{
    private int logroCompleto;
    private int logroTiempo;
    private int logroTurnos;
    private int logroVida;

    private string apiUrl = "https://localhost:7220/LogrosControllerTaberna";

    void Start()
    {
        int userId = PlayerPrefs.GetInt("UserId");

        // Leer logros desde PlayerPrefs
        logroCompleto = PlayerPrefs.GetInt("logro_taberna_completo", 0);
        logroTiempo   = PlayerPrefs.GetInt("logro_taberna_tiempo", 0);
        logroTurnos   = PlayerPrefs.GetInt("logro_taberna_turnos", 0);
        logroVida     = PlayerPrefs.GetInt("logro_taberna_vida", 1); // 1 por defecto

        // Validar y enviar solo los que estén en 1
        if (logroCompleto == 1) StartCoroutine(EnviarLogro(userId, 1));
        if (logroTiempo   == 1) StartCoroutine(EnviarLogro(userId, 2));
        if (logroTurnos   == 1) StartCoroutine(EnviarLogro(userId, 3));
        if (logroVida     == 1) StartCoroutine(EnviarLogro(userId, 4));
    }

    IEnumerator EnviarLogro(int userId, int logroNum)
    {
        string url = $"{apiUrl}/{userId}/{logroNum}";
        UnityWebRequest request = UnityWebRequest.Put(url, "");
        request.SetRequestHeader("Content-Type", "application/json");
        request.certificateHandler = new ForceAcceptAll(); // Por si usas certificados locales

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"Error al enviar logro {logroNum}: {request.error}");
        }
        else
        {
            Debug.Log($"Logro {logroNum} enviado correctamente para usuario {userId}");
        }
    }
}
