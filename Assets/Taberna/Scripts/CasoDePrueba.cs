using UnityEngine;
using UnityEngine.SceneManagement;

public class CasoDePrueba : MonoBehaviour
{
    void Update()
    {
        // Opción 1: Ctrl + 1
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Alpha1))
        {
            ActivarDebug(427.53f, 23, 5);
        }

        // Opción 2: Ctrl + 2
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Alpha2))
        {
            ActivarDebug(175.31f, 8, 16);
        }
    }

    void ActivarDebug(float tiempoTotal, int turnosJugador, int vidaJugador)
    {
        // Activar 4 de los 5 objetos (dejando 'objeto_Chips' sin activar)
        PlayerPrefs.SetInt("objeto_Coffe", 1);
        PlayerPrefs.SetInt("objeto_Chocolate_Bar", 1);
        PlayerPrefs.SetInt("objeto_Bead", 1);
        PlayerPrefs.SetInt("objeto_Milk", 1);
        // No tocar objeto_Chips

        // Definir los nuevos valores extra
        PlayerPrefs.SetFloat("tiempo_total", tiempoTotal);
        PlayerPrefs.SetInt("turnos_jugador", turnosJugador);
        PlayerPrefs.SetInt("vida_jugador", vidaJugador);

        PlayerPrefs.Save(); // Guardar cambios

        // Recargar escena para aplicar cambios
        SceneManager.LoadScene("Taberna Scene");
    }
}
