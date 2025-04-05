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
        PlayerPrefs.SetInt("tienda_X", 1);
        PlayerPrefs.Save();
        GameControl.Instance.goBattle();
    }

    public void SeleccionarChocolate()
    {
        PlayerPrefs.SetInt("tienda_X", 2);
        PlayerPrefs.Save();
        GameControl.Instance.goBattle();
    }

    public void SeleccionarBead()
    {
        PlayerPrefs.SetInt("tienda_X", 3);
        PlayerPrefs.Save();
        GameControl.Instance.goBattle();
    }

    public void SeleccionarMilk()
    {
        PlayerPrefs.SetInt("tienda_X", 4);
        PlayerPrefs.Save();
        GameControl.Instance.goBattle();
    }

    public void SeleccionarChips()
    {
        PlayerPrefs.SetInt("tienda_X", 5);
        PlayerPrefs.Save();
        GameControl.Instance.goBattle();
    }


    public void nevermind() {
        infOxxo.SetActive(false);
    }

}
