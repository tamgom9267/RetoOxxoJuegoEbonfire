using UnityEngine;
using UnityEngine.SceneManagement;

public class LabGameControl : MonoBehaviour
{
    public int timeElapsed = 0; // Time counter
    public static LabGameControl Instance;
    public int score = 1000; // Starting score

    void Awake()
    {
        if (Instance == null) // Ensure only one instance exists
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            StopAllCoroutines();
            PlayerPrefs.SetInt("score", score);
            PlayerPrefs.SetInt("timeElapsed", 0);
            InvokeRepeating("IncreaseTime", 1f, 1f);
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }


    void IncreaseTime()
    {
        timeElapsed++;
        PlayerPrefs.SetInt("timeElapsed", timeElapsed);
        LabUIController.Instance.UpdateTime(timeElapsed);
    }

    public int GetCurrentScore()
    {
        return PlayerPrefs.GetInt("score", 1000);
    }

    public void DeductPoints()
    {
        int newScore = GetCurrentScore() - 100;
        PlayerPrefs.SetInt("score", newScore);
        LabUIController.Instance.UpdateScore();
        CheckGameOver();
    }

    public void CheckGameOver()
    {
        if (GetCurrentScore() <= 0)
        {
            SceneManager.LoadScene("EndScene");
        }
    }

    public void GotoMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
