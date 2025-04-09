using UnityEngine;
using UnityEngine.UI;
public class UIController : MonoBehaviour
{
    [SerializeField] Text turnostxt;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int turnos_jugador = PlayerPrefs.GetInt("turnos_jugador", 0);

        turnostxt.text = turnos_jugador.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void returnToMenu() {    //Regresar al menu principal
        GameControl.Instance.goToMenu();
    }

    public void Zoom() {
        GameControl.Instance.ZoomIn();
    }

    public void Zoomnt() {
        GameControl.Instance.ZoomOut();
    }
}
