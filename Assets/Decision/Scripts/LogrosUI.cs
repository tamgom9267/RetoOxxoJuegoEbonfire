using UnityEngine;
using UnityEngine.UI;

// Clase que maneja la interfaz de usuario para los logros
public class LogrosUI : MonoBehaviour
{
    public GameObject logrosPanel;          // Panel principal de logros
    public Text[] logrosTexts;             // Array de textos para cada logro
    private LogrosManager logrosManager;    // Referencia al gestor de logros
    
    // Inicialización y configuración inicial
    void Start()
    {
        logrosManager = FindFirstObjectByType<LogrosManager>();
        if (logrosManager == null)
        {
            Debug.LogError("LogrosManager no encontrado en la escena");
            return;
        }
        ActualizarUI();
        logrosPanel.SetActive(false);
    }

    // Alterna la visibilidad del panel de logros con animación
    public void ToggleLogrosPanel()
    {
        if (!logrosPanel.activeSelf)
        {
            logrosPanel.SetActive(true);
            logrosPanel.transform.localScale = Vector3.zero;
            LeanTween.scale(logrosPanel, Vector3.one, 0.3f).setEase(LeanTweenType.easeOutBack);
            ActualizarUI();
        }
        else
        {
            LeanTween.scale(logrosPanel, Vector3.zero, 0.2f).setEase(LeanTweenType.easeInBack)
                .setOnComplete(() => logrosPanel.SetActive(false));
        }
    }

    // Actualiza el estado visual de los logros
    void ActualizarUI()
    {
        for (int i = 0; i < logrosTexts.Length; i++)
        {
            bool desbloqueado = logrosManager.IsLogroDesbloqueado(i + 1);
            string estado = desbloqueado ? "✅ ¡Desbloqueado!" : "🔒 Bloqueado";
            logrosTexts[i].text = $"{LogrosManager.logrosDescripciones[i]}\n{estado}";
            logrosTexts[i].color = desbloqueado ? Color.blue : Color.gray;
        }
    }
}
