using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultadoManager : MonoBehaviour
{
    [SerializeField] GameObject Coffe;
    [SerializeField] GameObject Chocolate_Bar;
    [SerializeField] GameObject Bead;
    [SerializeField] GameObject Milk;
    [SerializeField] GameObject Chips;

    public GameObject canvasWin;
    public GameObject canvasLose;

    void Start()
    {
        string resultado = PlayerPrefs.GetString("resultado", "derrota");

        canvasWin.SetActive(resultado == "victoria");
        canvasLose.SetActive(resultado == "derrota");
        
        // Desactivar todos los objetos por defecto
        Coffe.SetActive(false);
        Chocolate_Bar.SetActive(false);
        Bead.SetActive(false);
        Milk.SetActive(false);
        Chips.SetActive(false);

        int tiendaID = PlayerPrefs.GetInt("tienda_X", -1);

        if (tiendaID == 1)
        {
            PlayerPrefs.SetInt("objeto_Coffe", 1);
            Coffe.SetActive(true);
        }
        else if (tiendaID == 2)
        {
            PlayerPrefs.SetInt("objeto_Chocolate_Bar", 1);
            Chocolate_Bar.SetActive(true);
        }
        else if (tiendaID == 3)
        {
            PlayerPrefs.SetInt("objeto_Bead", 1);
            Bead.SetActive(true);
        }
        else if (tiendaID == 4)
        {
            PlayerPrefs.SetInt("objeto_Milk", 1);
            Milk.SetActive(true);
        }
        else if (tiendaID == 5)
        {
            PlayerPrefs.SetInt("objeto_Chips", 1);
            Chips.SetActive(true);
        }
        else
        {
            canvasWin.SetActive(false);
            canvasLose.SetActive(true);
        }
    }

    public void ObtenerObjeto()
    {
        PlayerPrefs.Save();
        SceneManager.LoadScene("Taberna Scene");
    }


    public void ReiniciarJuego()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        SceneManager.LoadScene("Taberna Scene");
    }
}
