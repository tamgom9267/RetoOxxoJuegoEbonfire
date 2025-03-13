using UnityEngine;
using UnityEngine.UI;

public class MostrarCarta : MonoBehaviour
{
    public GameObject imagen1;
    public GameObject imagen2;
    public GameObject imagen3;

    public void MostrarOcultarImagen()
    {
        imagen1.SetActive(!imagen1.activeSelf);
    }

    public void MostrarOcultarImagen2()
    {
        imagen2.SetActive(!imagen2.activeSelf);
    }

    public void MostrarOcultarImagen3()
    {
        imagen3.SetActive(!imagen3.activeSelf);
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
