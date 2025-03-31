using UnityEngine;
using UnityEngine.SceneManagement;


public class NewMonoBehaviourScript : MonoBehaviour
{
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
