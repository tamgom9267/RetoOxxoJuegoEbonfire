using UnityEngine;
using UnityEngine.UI;

public class MostrarCarta : MonoBehaviour
{
    public GameObject textPanel;
    public Text descriptionText;
    public Text streakText;
    private StreakManagerD streakManager;
    
    private string[] textosPredefinidos = new string[]
    {
        "Esta es la primera descripción de la carta",
        "Esta es la segunda descripción diferente",
        "Una tercera descripción muy interesante",
        "La cuarta descripción de la carta",
        "Y esta es la quinta descripción"
    };

    private bool[] respuestasCorrectas = new bool[]
    {
        true,   
        false,  
        true,   
        false,  
        true    
    };

    private int indiceActual;

    void Start()
    {
        textPanel.SetActive(false);
        streakManager = FindObjectOfType<StreakManagerD>();
        
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
        textPanel.SetActive(!textPanel.activeSelf);
        
        if(textPanel.activeSelf)
        {
            indiceActual = Random.Range(0, textosPredefinidos.Length);
            descriptionText.text = textosPredefinidos[indiceActual];
        }
    }

    private void UpdateStreakText()
    {
        if (streakText != null && streakManager != null)
        {
            streakText.text = "🔥 " + streakManager.GetCurrentStreak().ToString();
            streakText.text = streakManager.GetCurrentStreak() >= 2 ? "🔥 " + streakManager.GetCurrentStreak().ToString() : "";
        }
    }

    private void VerificarRespuesta(bool respuestaUsuario)
    {
        if(respuestaUsuario == respuestasCorrectas[indiceActual])
        {
            Debug.Log("¡Correcto!");
            streakManager.IncrementStreak();
            UpdateStreakText();
            textPanel.SetActive(false);
        }
        else
        {
            Debug.Log("Incorrecto");
            streakManager.ResetStreak();
            UpdateStreakText();
            textPanel.SetActive(false);
        }
    }
}