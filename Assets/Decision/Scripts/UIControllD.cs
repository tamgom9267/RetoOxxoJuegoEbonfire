using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// Controlador de la interfaz de usuario y temporizador
public class UIControllD : MonoBehaviour
{
    public Text timeText;           // Texto que muestra el tiempo
    private int time;               // Contador de tiempo restante
    private SFXManager sfxmanager;  // Gestor de efectos de sonido
    
    // Inicialización de componentes
    void Start()
    {
        sfxmanager = FindFirstObjectByType<SFXManager>();
        time = GameControllD.Instance.timeToWin;
        UpdateTimeText();
    }

    // Inicia la cuenta regresiva
    public void StartTimer()
    {
        StartCoroutine(TimerCountdown());
    }
    
    // Corutina que maneja la cuenta regresiva
    IEnumerator TimerCountdown()
    {
        yield return new WaitForSeconds(1);
        time -= 1;
        UpdateTimeText();
        
        if(time <= 0)
        {
            GameControllD.Instance.GameOver();
        }
        else
        {
            StartCoroutine(TimerCountdown());
        }
    }
    
    // Actualiza el texto del temporizador con efectos visuales
    void UpdateTimeText()
    {
        timeText.text = "Tiempo Restante: " + time;
        
        if(time <= 15)
        {
            timeText.transform.localScale = Vector3.one * 1.2f;
            LeanTween.scale(timeText.gameObject, Vector3.one, 0.5f)
                .setEase(LeanTweenType.easeOutBounce);
            timeText.color = Color.red;
            sfxmanager.PocoTiempo();
        }
        else
        {
            timeText.color = Color.white;
        }
    }
}