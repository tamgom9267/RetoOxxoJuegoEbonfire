using UnityEngine;
using UnityEngine.UI;

public class MostrarCarta : MonoBehaviour
{
    public GameObject textPanel; // Panel que contiene el texto
    public Text descriptionText; // Componente Text para mostrar el texto

    void Start()
    {
        // Ocultamos el panel de texto al inicio
        textPanel.SetActive(false);
    }

    public void OnCardClick()
    {
        // Alternar la visibilidad del panel
        textPanel.SetActive(!textPanel.activeSelf);
        
        if(textPanel.activeSelf)
        {
            // Solo actualizar el texto si el panel se está mostrando
            descriptionText.text = "Descripción de la carta ";
        }
    }
}