using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject pauseButton;
    [SerializeField] GameObject rpgStuff;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pause() {
        pauseMenu.SetActive(true);
        pauseButton.SetActive(false);
        rpgStuff.SetActive(false);
    }

    public void Home() {
        SceneManager.LoadScene("Taberna Scene");
    }

    public void Resume() {
        pauseMenu.SetActive(false);
        pauseButton.SetActive(true);
        rpgStuff.SetActive(true);
    }

    public void Settings() {

    }
}
