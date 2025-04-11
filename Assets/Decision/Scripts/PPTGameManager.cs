using UnityEngine;
using UnityEngine.UI;

// Clase que maneja el minijuego de Piedra, Papel o Tijera
public class PPTGameManager : MonoBehaviour
{
    // Referencias a otros gestores necesarios
    private StreakManagerD streakManager;        // Gestor de racha de victorias
    private DecisionPointsManager pointsManager; // Gestor de puntos del juego
    private MostrarCarta mostrarCarta;          // Gestor de cartas de preguntas

    // Referencias a elementos de UI
    public GameObject pptPanel;    // Panel principal del juego
    public Text resultadoText;     // Texto que muestra el resultado
    public GameObject boton;       // Botón para cerrar el panel

    // Inicialización de componentes
    void Start()
    {
        streakManager = FindFirstObjectByType<StreakManagerD>();
        pointsManager = FindFirstObjectByType<DecisionPointsManager>();
        mostrarCarta = FindFirstObjectByType<MostrarCarta>();
        
        // Oculta elementos UI al inicio
        if (pptPanel) pptPanel.SetActive(false);
        boton.SetActive(false);
    }

    // Métodos para manejar las elecciones del jugador (0=Piedra, 1=Papel, 2=Tijera)
    public void OnPiedraClick() { HandlePlayerChoice(0); }
    public void OnPapelClick() { HandlePlayerChoice(1); }
    public void OnTijeraClick() { HandlePlayerChoice(2); }
    public void OnCerrarClick() { CerrarPanel(); }

    // Muestra el panel del juego
    public void PlayGame()
    {
        if (pptPanel)
        {
            pptPanel.SetActive(true);
            if (resultadoText) resultadoText.text = "¡Elige tu jugada!";
        }
    }

    // Procesa la elección del jugador y determina el resultado
    private void HandlePlayerChoice(int playerChoice)
    {
        int computerChoice = Random.Range(0, 3);
        string[] opciones = { "Piedra", "Papel", "Tijera" };
        string resultado = "La computadora eligió " + opciones[computerChoice] + ".\n";

        // Lógica para determinar el ganador
        if (playerChoice == computerChoice)
        {
            resultado += "¡Empate!";
            pointsManager.AddPoints(5f);
            streakManager.IncrementStreak();
            mostrarCarta.UpdateStreakText();
            boton.SetActive(true);
        }
        else if ((playerChoice == 0 && computerChoice == 2) || 
                 (playerChoice == 1 && computerChoice == 0) || 
                 (playerChoice == 2 && computerChoice == 1))
        {
            resultado += "¡Ganaste!";
            streakManager.IncrementStreak();
            mostrarCarta.UpdateStreakText();
            pointsManager.AddPoints(10f);
            boton.SetActive(true);
        }
        else
        {
            resultado += "¡Perdiste!";
            pointsManager.ReducePoints(5f);
            streakManager.ResetStreak();
            mostrarCarta.UpdateStreakText();
            boton.SetActive(true);
        }

        if (resultadoText) resultadoText.text = resultado;
    }

    // Oculta el panel del juego
    private void CerrarPanel()
    {
        if (pptPanel) pptPanel.SetActive(false);
        boton.SetActive(false);
    }
}