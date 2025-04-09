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
        // 1. Guardar los datos antes de borrar
        int userId = PlayerPrefs.GetInt("UserId");
        string username = PlayerPrefs.GetString("Username");
        string nombre = PlayerPrefs.GetString("Nombre");

        // 2. Borrar todo
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        // 3. Restaurar los datos de login
        PlayerPrefs.SetInt("UserId", userId);
        PlayerPrefs.SetString("Username", username);
        PlayerPrefs.SetString("Nombre", nombre);
        PlayerPrefs.Save();
        
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
