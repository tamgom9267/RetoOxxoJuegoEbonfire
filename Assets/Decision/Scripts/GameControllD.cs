using UnityEngine;
using UnityEngine.SceneManagement;

// Controlador principal del juego que maneja el estado global
public class GameControllD : MonoBehaviour
{
    public int timeToWin = 70;                // Tiempo límite para ganar
    public static GameControllD Instance;     
    public UIControllD uiController;          // Referencia al controlador de UI
    
    // Inicialización y configuración inicial
    void Awake()
    {
        Instance = this;
        PlayerPrefs.SetInt("timeToWin", timeToWin);
        DontDestroyOnLoad(this.gameObject);
    }
    
    // Inicia el temporizador si existe el controlador de UI
    void Start()
    {
        if(uiController != null)
        {
            uiController.StartTimer();
        }
    }
    
    // Método llamado cuando se acaba el tiempo
    public void GameOver()
    {
        SceneManager.LoadScene("DecisionScene");
    }
}
