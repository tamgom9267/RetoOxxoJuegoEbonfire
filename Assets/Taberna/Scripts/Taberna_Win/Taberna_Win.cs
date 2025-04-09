using UnityEngine;
using UnityEngine.UI;

public class Taberna_Win : MonoBehaviour
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
}
