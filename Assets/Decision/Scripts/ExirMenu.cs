using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Clase que maneja la navegación entre diferentes escenas del juego
public class NewMonoBehaviourScript : MonoBehaviour
{
    // Método para volver al menú principal
    public void BackToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    // Método para volver a la escena de decisión
    public void GoBack()
    {
        SceneManager.LoadScene("DecisionScene");
    }

    // Método para cargar la escena principal del juego
    public void LoadGame()
    {
        SceneManager.LoadScene("GameSceneDecision");
    }

    // Método para cargar la tienda
    public void LoadStore()
    {
        SceneManager.LoadScene("StoreDecision");
    }

    // Método para cargar el inventario
    public void LoadInventario()
    {
        SceneManager.LoadScene("InventarioScene");
    }
}

