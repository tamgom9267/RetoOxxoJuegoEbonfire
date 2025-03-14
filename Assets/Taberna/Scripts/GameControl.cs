using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    static public GameControl Instance;
    public UIController uiController;
    private Camera mainCamera;

    float FOVmax, FOVmin, FOVnormal;

    public void Awake()
    {
        StopAllCoroutines();

        Instance = this;
        Instance.SetReferences();
        DontDestroyOnLoad(this.gameObject);
    }

    void SetReferences() { //Establecer ciertos objetos, en caso de que haga falta. Aqui tambien iría el SFX
        if(uiController == null) {
            uiController = FindFirstObjectByType<UIController>();
        }

        mainCamera = Camera.main;
    }

    public void goToMenu() {    //Regresar al menu principal
        SceneManager.LoadScene("MenuScene");
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FOVmax = 8.0f;
        FOVnormal = 5.0f;
        FOVmin = 3.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ZoomIn() {
        float current = mainCamera.orthographicSize;

        if(mainCamera.orthographicSize >= FOVnormal) {
            mainCamera.orthographicSize = FOVmin;
        } else {
            mainCamera.orthographicSize = FOVnormal;
        }
    }

    public void ZoomOut() {
        float current = mainCamera.orthographicSize;

        if(mainCamera.orthographicSize <= FOVnormal) {
            mainCamera.orthographicSize = FOVmax;
        } else {
            mainCamera.orthographicSize = FOVnormal;
        }
    }
}
