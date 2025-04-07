using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// Clase que controla la interfaz de usuario y el temporizador del juego
public class UIControllD : MonoBehaviour
{
    public Text timeText;      // Referencia al texto que muestra el tiempo
    private int time;          // Contador de tiempo restante
    
    void Start()
    {
        // Inicializa el tiempo desde la instancia del GameControllD
        time = GameControllD.Instance.timeToWin;
        UpdateTimeText();
    }

    // Inicia la cuenta regresiva del temporizador
    public void StartTimer()
    {
        StartCoroutine(TimerCountdown());
    }
    
    // Corutina que maneja la cuenta regresiva
    IEnumerator TimerCountdown()
    {
        yield return new WaitForSeconds(1);    // Espera 1 segundo
        time -= 1;                             // Reduce el tiempo
        UpdateTimeText();                      // Actualiza el texto en pantalla
        
        // Si el tiempo llega a cero, termina el juego
        if(time <= 0)
        {
            GameControllD.Instance.GameOver();
        }
        else
        {
            StartCoroutine(TimerCountdown());  // Continúa la cuenta regresiva
        }
    }
    
    // Actualiza el texto que muestra el tiempo restante
    void UpdateTimeText()
    {
        timeText.text = "Tiempo Restante: " + time;
    }
}
