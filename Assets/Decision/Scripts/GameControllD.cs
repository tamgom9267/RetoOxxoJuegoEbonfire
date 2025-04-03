using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControllD : MonoBehaviour
{
    public int timeToWin = 70;
    public static GameControllD Instance;
    public UIControllD uiController;
    
    void Awake()
    {
        Instance = this;
        PlayerPrefs.SetInt("timeToWin", timeToWin);
        DontDestroyOnLoad(this.gameObject);
    }
    
    void Start()
    {
        if(uiController != null)
        {
            uiController.StartTimer();
        }
    }
    
    public void GameOver()
    {
        SceneManager.LoadScene("DecisionScene");
    }
}
