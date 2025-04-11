using UnityEngine;
using UnityEngine.SceneManagement;

public class OxxoBehaviour : MonoBehaviour
{
    [SerializeField] Debriefing debriefing;
    [SerializeField] GameObject infOxxo;

    [SerializeField] int oxxoID;

    
    [SerializeField] GameObject pointer1;
    [SerializeField] GameObject pointer2;
    [SerializeField] GameObject pointer3;
    [SerializeField] GameObject pointer4;
    [SerializeField] GameObject pointer5;

    [SerializeField] GameObject btnCoffe;
    [SerializeField] GameObject btnChocolate;
    [SerializeField] GameObject btnBead;
    [SerializeField] GameObject btnMilk;
    [SerializeField] GameObject btnChips;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Awake()
    {
        infOxxo.SetActive(false);

        // Apagar pointers si el objeto correspondiente ya fue conseguido
        if (PlayerPrefs.GetInt("objeto_Coffe", 0) == 1)
            pointer1.SetActive(false);

        if (PlayerPrefs.GetInt("objeto_Chocolate_Bar", 0) == 1)
            pointer2.SetActive(false);

        if (PlayerPrefs.GetInt("objeto_Bead", 0) == 1)
            pointer3.SetActive(false);

        if (PlayerPrefs.GetInt("objeto_Milk", 0) == 1)
            pointer4.SetActive(false);

        if (PlayerPrefs.GetInt("objeto_Chips", 0) == 1)
            pointer5.SetActive(false);

        // Validación de que todos los objetos han sido obtenidos
        if (PlayerPrefs.GetInt("objeto_Coffe", 0) == 1 &&
            PlayerPrefs.GetInt("objeto_Chocolate_Bar", 0) == 1 &&
            PlayerPrefs.GetInt("objeto_Bead", 0) == 1 &&
            PlayerPrefs.GetInt("objeto_Milk", 0) == 1 &&
            PlayerPrefs.GetInt("objeto_Chips", 0) == 1)
        {
            PlayerPrefs.SetInt("logro_taberna_completo", 1);
            PlayerPrefs.Save();
            Debug.Log("🏆 Logro activado: juego completado al 100%");
            SceneManager.LoadScene("Taberna_Win");
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Desactiva todos los botones primero
            btnCoffe.SetActive(false);
            btnChocolate.SetActive(false);
            btnBead.SetActive(false);
            btnMilk.SetActive(false);
            btnChips.SetActive(false);

            // Activa el botón correspondiente dependiendo del oxxoID y su punto activo
            if (oxxoID == 1 && pointer1.activeSelf)
            {
                Time.timeScale = 0f; // Pausa el tiempo
                infOxxo.SetActive(true);
                btnCoffe.SetActive(true);
            }
            else if (oxxoID == 2 && pointer2.activeSelf)
            {
                Time.timeScale = 0f; // Pausa el tiempo
                infOxxo.SetActive(true);
                btnChocolate.SetActive(true);
            }
            else if (oxxoID == 3 && pointer3.activeSelf)
            {
                Time.timeScale = 0f; // Pausa el tiempo
                infOxxo.SetActive(true);
                btnBead.SetActive(true);
            }
            else if (oxxoID == 4 && pointer4.activeSelf)
            {
                Time.timeScale = 0f; // Pausa el tiempo
                infOxxo.SetActive(true);
                btnMilk.SetActive(true);
            }
            else if (oxxoID == 5 && pointer5.activeSelf)
            {
                Time.timeScale = 0f; // Pausa el tiempo
                infOxxo.SetActive(true);
                btnChips.SetActive(true);
            }
        }
    }
}


    