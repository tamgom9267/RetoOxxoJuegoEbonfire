using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UIController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
