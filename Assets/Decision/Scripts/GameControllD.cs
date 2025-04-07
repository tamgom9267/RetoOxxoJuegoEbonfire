using UnityEngine;
using UnityEngine.SceneManagement;

// Clase principal que controla el estado del juego
public class GameControllD : MonoBehaviour
{
    public int timeToWin = 70;                     // Tiempo límite para ganar
    public static GameControllD Instance;          // Instancia única
    public UIControllD uiController;               // Referencia al controlador de UI
    
    void Awake()
    {
        Instance = this;
        PlayerPrefs.SetInt("timeToWin", timeToWin);
        DontDestroyOnLoad(this.gameObject);
    }
    
    void Start()
    {
        // Inicia el temporizador si existe el controlador de UI
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
