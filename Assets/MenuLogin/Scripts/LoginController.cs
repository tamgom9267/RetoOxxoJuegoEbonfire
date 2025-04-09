using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginController : MonoBehaviour
{

    public Text nombreReal;
    private string nombre;

    public void GoToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void GoToLaberinto()
    {
        SceneManager.LoadScene("LaberintoScene");
    }

    public void GoToTaberna()
    {
        SceneManager.LoadScene("Taberna Scene");
    }

    public void GoToDecision()
    {
        SceneManager.LoadScene("DecisionScene");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nombre = PlayerPrefs.GetString("Nombre");
        nombreReal.text = "Hola!, " + nombre;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
