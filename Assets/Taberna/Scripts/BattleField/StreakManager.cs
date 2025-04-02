using UnityEngine;

public class StreakManager : MonoBehaviour
{
    public static StreakManager Instance;
    private int correctStreak = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void AddCorrectAnswer()
    {
        correctStreak++;
        if (correctStreak >= 3)
        {
            PlayerPrefs.SetInt("racha", 3);
        }
    }

    public void ResetStreak()
    {
        correctStreak = 0;
        PlayerPrefs.SetInt("racha", 0);
    }

    public float GetCurrentMultiplier()
    {
        return PlayerPrefs.GetInt("racha", 0) >= 3 ? 1.5f : 1f;
    }
}
