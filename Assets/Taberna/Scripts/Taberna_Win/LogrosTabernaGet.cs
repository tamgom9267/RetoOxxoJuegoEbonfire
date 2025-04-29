using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using Newtonsoft.Json;

public class LogrosTabernaGet : MonoBehaviour
{
    [Header("Paneles de logros")]
    [SerializeField] private GameObject logro1Panel;
    [SerializeField] private GameObject logro2Panel;
    [SerializeField] private GameObject logro3Panel;
    [SerializeField] private GameObject logro4Panel;

    private string apiUrl = "https://apideploy-a00838689.replit.app/LogrosControllerTaberna";

    void Start()
    {
        int userId = PlayerPrefs.GetInt("UserId", -1);
        if (userId != -1)
            StartCoroutine(ObtenerLogros(userId));
        else
            Debug.LogError("No se encontró el ID de usuario en PlayerPrefs");
    }

    IEnumerator ObtenerLogros(int userId)
    {
        UnityWebRequest request = UnityWebRequest.Get($"{apiUrl}/{userId}");
        request.certificateHandler = new ForceAcceptAll(); // omitir errores de certificado si es local

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error al obtener logros: " + request.error);
            yield break;
        }

        LogrosTabernaResponse logros = JsonConvert.DeserializeObject<LogrosTabernaResponse>(request.downloadHandler.text);

        // Activar paneles si el valor es 1
        logro1Panel.SetActive(logros.logro_tab1 == true);
        logro2Panel.SetActive(logros.logro_tab2 == true);
        logro3Panel.SetActive(logros.logro_tab3 == true);
        logro4Panel.SetActive(logros.logro_tab4 == true);
    }
}

[System.Serializable]
public class LogrosTabernaResponse
{
    public bool logro_tab1;
    public bool logro_tab2;
    public bool logro_tab3;
    public bool logro_tab4;
}
