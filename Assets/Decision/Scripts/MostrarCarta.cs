using UnityEngine;
using UnityEngine.UI;

// Clase que maneja la lógica de mostrar y responder cartas con preguntas
public class MostrarCarta : MonoBehaviour
{
    // Referencias a elementos de UI
    public GameObject textPanel;        // Panel que muestra el texto
    public Text descriptionText;        // Texto de la descripción
    public Text streakText;            // Texto que muestra la racha actual
    private StreakManagerD streakManager;    // Gestor de rachas
    private DecisionPointsManager pointsManager;  // Gestor de puntos
    private SFXManager sfxmanager;

    // Array de textos predefinidos para las cartas
    private string[] textosPredefinidos = new string[]
    {
        "Esta es la primera descripción de la carta",
        "Esta es la segunda descripción diferente",
        "Una tercera descripción muy interesante",
        "La cuarta descripción de la carta",
        "Y esta es la quinta descripción"
    };

    // Array de respuestas correctas correspondientes a cada texto
    private bool[] respuestasCorrectas = new bool[]
    {
        true,   // Respuesta correcta para la primera carta
        false,  // Respuesta correcta para la segunda carta
        true,   // Respuesta correcta para la tercera carta
        false,  // Respuesta correcta para la cuarta carta
        true    // Respuesta correcta para la quinta carta
    };

    private int indiceActual;  // Índice de la carta actual

    void Start()
    {
        textPanel.SetActive(false);
        sfxmanager = FindFirstObjectByType<SFXManager>();
        streakManager = FindFirstObjectByType<StreakManagerD>();
        pointsManager = FindFirstObjectByType<DecisionPointsManager>();
        if (streakManager == null)
        {
            Debug.LogError("No se encontró StreakManagerD en la escena. Asegúrate de que existe en la escena.");
            return;
        }
        UpdateStreakText();
    }

    // Maneja el clic en el botón "Visita Presencial"
    public void OnBotonVerdaderoClick()
    {
        VerificarRespuesta(true);
    }

    // Maneja el clic en el botón "Visita Virtual"
    public void OnBotonFalsoClick()
    {
        VerificarRespuesta(false);
    }

    // Maneja el clic en la carta
    public void OnCardClick()
    {
        if(!textPanel.activeSelf)
        {
            textPanel.SetActive(true);
            textPanel.transform.localScale = Vector3.zero;
            LeanTween.scale(textPanel, Vector3.one, 0.3f).setEase(LeanTweenType.easeOutBack);
            indiceActual = Random.Range(0, textosPredefinidos.Length);
            descriptionText.text = textosPredefinidos[indiceActual];
        }
        else
        {
            LeanTween.scale(textPanel, Vector3.zero, 0.2f).setEase(LeanTweenType.easeInBack).setOnComplete(() => {
                textPanel.SetActive(false);
            });
        }
    }

    // Actualiza el texto de la racha actual
    public void UpdateStreakText()
    {
        if (streakText != null && streakManager != null)
        {
            streakText.text = "🔥 " + streakManager.GetCurrentStreak().ToString();
            streakText.text = streakManager.GetCurrentStreak() >= 2 ? "🔥 " + streakManager.GetCurrentStreak().ToString() : "";
        }
    }

    // Verifica si la respuesta del usuario es correcta
    private void VerificarRespuesta(bool respuestaUsuario)
    {
        if(respuestaUsuario == respuestasCorrectas[indiceActual])
        {
            sfxmanager.RespuestaCorrecta();
            pointsManager.AddPoints(10f);
            UpdateStreakText();
            Debug.Log("¡Correcto!");
            if (Random.value <= 0.5f) // 50% de probabilidad
            {
                // Inicia el minijuego de Piedra, Papel o Tijera
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
            pointsManager.ReducePoints(5f); // Reduce 5 puntos por respuesta incorrecta
            UpdateStreakText();
            textPanel.SetActive(false);
        }
    }
}