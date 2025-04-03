using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIControllD : MonoBehaviour
{
    public Text timeText; 
    private int time;
    
    void Start()
    {
        time = GameControllD.Instance.timeToWin;
        UpdateTimeText();
    }

    public void StartTimer()
    {
        StartCoroutine(TimerCountdown());
    }
    
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
    
    void UpdateTimeText()
    {
        timeText.text = "Tiempo Restante: " + time;
    }
}
