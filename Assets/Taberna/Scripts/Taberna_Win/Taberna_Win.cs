using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Taberna_Win : MonoBehaviour
{
    [SerializeField] Text turnostxt;
    [SerializeField] Text tiempotxt;

    [SerializeField] GameObject logrosPanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int turnos_jugador = PlayerPrefs.GetInt("turnos_jugador", 0);

        turnostxt.text = turnos_jugador.ToString();

        float tiempo_total = PlayerPrefs.GetFloat("tiempo_total", 0f);

        tiempotxt.text = tiempo_total.ToString("F1"); // muestra 1 decimal
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Logros()
    {
        logrosPanel.SetActive(true);
    }
    public void Volver()
    {
        logrosPanel.SetActive(false);
    }

    public void GuardarYSalir()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
