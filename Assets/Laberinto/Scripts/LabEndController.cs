using UnityEngine;
using UnityEngine.SceneManagement;

public class LabEndController : MonoBehaviour
{
    public void GoToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void GoToLab()
    {
        SceneManager.LoadScene("LaberintoScene");
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
