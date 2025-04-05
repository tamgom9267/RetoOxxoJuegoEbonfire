using UnityEngine;

public class Debriefing : MonoBehaviour
{
    [SerializeField] GameObject infOxxo;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SeleccionarCoffe()
    {
        Time.timeScale = 1f; // Reanuda el tiempo
        PlayerPrefs.SetInt("tienda_X", 1);
        PlayerPrefs.Save();
        GameControl.Instance.goBattle();
    }

    public void SeleccionarChocolate()
    {
        Time.timeScale = 1f; // Reanuda el tiempo
        PlayerPrefs.SetInt("tienda_X", 2);
        PlayerPrefs.Save();
        GameControl.Instance.goBattle();
    }

    public void SeleccionarBead()
    {
        Time.timeScale = 1f; // Reanuda el tiempo
        PlayerPrefs.SetInt("tienda_X", 3);
        PlayerPrefs.Save();
        GameControl.Instance.goBattle();
    }

    public void SeleccionarMilk()
    {
        Time.timeScale = 1f; // Reanuda el tiempo
        PlayerPrefs.SetInt("tienda_X", 4);
        PlayerPrefs.Save();
        GameControl.Instance.goBattle();
    }

    public void SeleccionarChips()
    {
        Time.timeScale = 1f; // Reanuda el tiempo
        PlayerPrefs.SetInt("tienda_X", 5);
        PlayerPrefs.Save();
        GameControl.Instance.goBattle();
    }


    public void nevermind() {
        Time.timeScale = 1f; // Reanuda el tiempo
        infOxxo.SetActive(false);
    }

}
