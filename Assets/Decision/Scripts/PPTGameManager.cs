using UnityEngine;
using UnityEngine.UI;

// Clase que maneja la lógica del minijuego Piedra, Papel o Tijera
public class PPTGameManager : MonoBehaviour
{
    private StreakManagerD streakManager;        // Gestor de racha de victorias
    private DecisionPointsManager pointsManager; // Gestor de puntos
    private MostrarCarta mostrarCarta;           //Gestor de cartas

    // Referencias a elementos de la interfaz
    public GameObject pptPanel;     // Panel del juego
    public Text resultadoText;      // Texto que muestra el resultado

    void Start()
    {
        // Obtiene referencias a los gestores necesarios
        streakManager = FindObjectOfType<StreakManagerD>();
        pointsManager = FindObjectOfType<DecisionPointsManager>();
        mostrarCarta = FindObjectOfType<MostrarCarta>();
        
        // Oculta el panel del juego al inicio
        if (pptPanel) pptPanel.SetActive(false);
    }

    // Métodos para manejar las elecciones del jugador
    // 0 = Piedra, 1 = Papel, 2 = Tijera
    public void OnPiedraClick() { HandlePlayerChoice(0); }
    public void OnPapelClick() { HandlePlayerChoice(1); }
    public void OnTijeraClick() { HandlePlayerChoice(2); }
    public void OnCerrarClick() { CerrarPanel(); }

    // Inicia el juego mostrando el panel
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

        // Lógica para determinar el ganador y asignar puntos
        if (playerChoice == computerChoice)
        {
            resultado += "¡Empate!";
            pointsManager.AddPoints(5f);
            streakManager.IncrementStreak();
            mostrarCarta.UpdateStreakText();
        }
        else if ((playerChoice == 0 && computerChoice == 2) || 
                 (playerChoice == 1 && computerChoice == 0) || 
                 (playerChoice == 2 && computerChoice == 1))
        {
            resultado += "¡Ganaste!";
            streakManager.IncrementStreak();
            mostrarCarta.UpdateStreakText();
            pointsManager.AddPoints(10f);
        }
        else
        {
            resultado += "¡Perdiste!";
            pointsManager.ReducePoints(5f);
            streakManager.ResetStreak();
            mostrarCarta.UpdateStreakText();
        }

        if (resultadoText) resultadoText.text = resultado;
    }

    // Oculta el panel del juego
    private void CerrarPanel()
    {
        if (pptPanel) pptPanel.SetActive(false);
    }
}