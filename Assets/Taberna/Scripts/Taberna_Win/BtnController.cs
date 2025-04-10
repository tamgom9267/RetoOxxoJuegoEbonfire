using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnController : MonoBehaviour
{
    public void Logros()
    {
        SceneManager.LoadScene("Taberna_Logros");
    }

    public void GuardarYSalir()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
