using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class LoginManager : MonoBehaviour
{
    public InputField usernameInput;
    public InputField passwordInput;
    public Button loginButton;
    public Text errorText;
    
    private const string API_URL = "https://apideploy-a00838689.replit.app/Login";

    void Start()
    {
        loginButton.onClick.AddListener(HandleLogin);
        errorText.text = "";
    }

    public void HandleLogin()
    {
        StartCoroutine(Login());
    }

    private IEnumerator Login()
    {
        var loginData = new LoginRequest
        {
            Username = usernameInput.text,
            Password = passwordInput.text
        };

        string json = JsonConvert.SerializeObject(loginData);
        var web = new UnityWebRequest(API_URL, "POST");
        web.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(json));
        web.downloadHandler = new DownloadHandlerBuffer();
        web.SetRequestHeader("Content-Type", "application/json");
        web.certificateHandler = new ForceAcceptAll();

        yield return web.SendWebRequest();

        if (web.result != UnityWebRequest.Result.Success)
        {
            errorText.text = "Error de conexión";
        }
        else
        {
            var response = JsonConvert.DeserializeObject<LoginResponse>(web.downloadHandler.text);
            if (response.Success)
            {
                PlayerPrefs.SetInt("UserId", response.UserId);
                PlayerPrefs.SetString("Username", response.Username);
                PlayerPrefs.SetString("Nombre", response.Nombre);
                PlayerPrefs.Save();
                SceneManager.LoadScene("MenuScene");
            }
            else
            {
                errorText.text = "Usuario o contraseña incorrectos";
            }
        }
    }
}

public class LoginRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}

public class LoginResponse
{
    public bool Success { get; set; }
    public int UserId { get; set; }
    public string Username { get; set; }
    public string Nombre { get; set; }
}
