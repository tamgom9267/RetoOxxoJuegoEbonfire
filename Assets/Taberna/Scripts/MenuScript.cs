using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartToPlay() {     //Llevar a la pantalla de juego
        SceneManager.LoadScene("GameScene");
    }

    public void ExitGame() {    //Salir de la aplicacion
        UnityEditor.EditorApplication.isPlaying = false;
        //Application.Quit();
    }
}
