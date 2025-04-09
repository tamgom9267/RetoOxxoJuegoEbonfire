using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Clase que maneja la navegación entre escenas del juego
public class NewMonoBehaviourScript : MonoBehaviour
{
    // Métodos para navegar entre diferentes escenas
    public void BackToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void GoBack()
    {
        SceneManager.LoadScene("DecisionScene");
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("GameSceneDecision");
    }

    public void LoadStore()
    {
        SceneManager.LoadScene("StoreDecision");
    }

    public void LoadInventario()
    {
        SceneManager.LoadScene("InventarioScene");
    }
}

