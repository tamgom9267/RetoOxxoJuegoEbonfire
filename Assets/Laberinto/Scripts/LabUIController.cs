using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class LabUIController : MonoBehaviour
{
    public static LabUIController Instance;
    public Text timeText;
    public Text pointsText;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateScore();
    }

    public void UpdateTime(int timeElapsed)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(timeElapsed);
        timeText.text = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
    }

    public void UpdateScore()
    {
        int currentScore = LabGameControl.Instance.GetCurrentScore();
        pointsText.text = "Puntos: " + currentScore;
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void GoToWinScreen()
    {
        SceneManager.LoadScene("LaberintoFin");
    }
}
