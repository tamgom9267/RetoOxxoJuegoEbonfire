using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject pauseButton;
    [SerializeField] GameObject rpgStuff;
    [SerializeField] Text turnostxt;
    [SerializeField] private Text tiempotxt;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int turnos_jugador = PlayerPrefs.GetInt("turnos_jugador", 0);

        turnostxt.text = turnos_jugador.ToString();

        float tiempo = PlayerPrefs.GetFloat("tiempo_total", 0f);
        tiempotxt.text = $"{tiempo:F1}s";
    }

    public void Pause() {
        Time.timeScale = 0f; // Pausa el tiempo
        pauseMenu.SetActive(true);
        pauseButton.SetActive(false);
        rpgStuff.SetActive(false);
    }

    public void Home() {
        Time.timeScale = 1f; // Reanuda el tiempo
        SceneManager.LoadScene("Taberna Scene");
    }

    public void Resume() {
        Time.timeScale = 1f; // Reanuda el tiempo
        pauseMenu.SetActive(false);
        pauseButton.SetActive(true);
        rpgStuff.SetActive(true);
    }

    public void Settings() {

    }
}
