using UnityEngine;
using UnityEngine.UI;

public class LogrosUI : MonoBehaviour
{
    public GameObject logrosPanel;          // Panel principal de logros
    public Text[] logrosTexts;             // Array de textos para cada logro
    private LogrosManager logrosManager;    // Referencia al gestor de logros
    
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

    public void ToggleLogrosPanel()
    {
        logrosPanel.SetActive(!logrosPanel.activeSelf);
        if (logrosPanel.activeSelf)
        {
            ActualizarUI();
        }
    }

    void ActualizarUI()
    {
        for (int i = 0; i < logrosTexts.Length; i++)
        {
            bool desbloqueado = logrosManager.IsLogroDesbloqueado(i + 1);
            string estado = desbloqueado ? "✅ ¡Desbloqueado!" : "🔒 Bloqueado";
            logrosTexts[i].text = $"{LogrosManager.logrosDescripciones[i]}\n{estado}";
            logrosTexts[i].color = desbloqueado ? Color.green : Color.gray;
        }
    }
}
