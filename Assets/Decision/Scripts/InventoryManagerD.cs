using UnityEngine;
using System;
using UnityEngine.Networking;
using System.Collections;
using Newtonsoft.Json;

// Clase que gestiona el inventario del jugador, permitiendo cargar, actualizar y verificar items
public class InventoryManagerD : MonoBehaviour
{
    // Instancia única para acceder desde otros scripts
    public static InventoryManagerD Instance { get; private set; }
    
    // URL base de la API para gestionar el inventario
    private const string API_URL = "https://10.227.1.80:7220/Inventory";
    private string userId;
    public InventoryItem currentInventory;

    // Referencias a los objetos que representan items en el juego
    public GameObject item1Object;
    public GameObject item2Object;
    public GameObject item3Object;
    public GameObject item4Object;

    // Clase que define la estructura de datos del inventario
    [Serializable]
    public class InventoryItem
    {
        public int userId;      // ID del usuario
        public bool item_1;     // Estado del item 1
        public bool item_2;     // Estado del item 2
        public bool item_3;     // Estado del item 3
        public bool item_4;     // Estado del item 4
    }

    void Start()
    {
        //userId = PlayerPrefs.GetString();
        userId = "1";
        StartCoroutine(LoadInventory());
    }

    // Carga el inventario del jugador desde la API
    public IEnumerator LoadInventory()
    {
        string JSONurl = $"{API_URL}/{userId}";
        UnityWebRequest web = UnityWebRequest.Get(JSONurl);
        web.certificateHandler = new ForceAcceptAll();
        yield return web.SendWebRequest();

        if(web.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"Error API: {web.error}");
            Debug.LogError($"Response Code: {web.responseCode}");
            Debug.LogError($"Response: {web.downloadHandler.text}");
        }
        else
        {
            currentInventory = JsonConvert.DeserializeObject<InventoryItem>(web.downloadHandler.text);
            UpdateInventoryObjects();
            Debug.Log("Inventory loaded and objects updated");
        }
    }

    // Método para comprar un item específico
    public void PurchaseItem(int itemNumber)
    {
        StartCoroutine(UpdateItem(itemNumber, true));
    }

    // Actualiza el estado de un item en la API
    private IEnumerator UpdateItem(int itemNumber, bool value)
    {
        string JSONurl = $"{API_URL}/{userId}/{itemNumber}";
        UnityWebRequest web = UnityWebRequest.Put(JSONurl, JsonConvert.SerializeObject(value));
        web.certificateHandler = new ForceAcceptAll();
        web.SetRequestHeader("Content-Type", "application/json");

        yield return web.SendWebRequest();

        if(web.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"Error API: {web.error}");
            Debug.LogError($"Response Code: {web.responseCode}");
            Debug.LogError($"Response: {web.downloadHandler.text}");
        }
    }

    // Verifica si el jugador tiene un item específico
    public bool HasItem(int itemNumber)
    {
        switch(itemNumber)
        {
            case 1: return currentInventory.item_1;
            case 2: return currentInventory.item_2;
            case 3: return currentInventory.item_3;
            case 4: return currentInventory.item_4;
            default: return false;
        }
    }

    // Actualiza la visibilidad de los objetos según el inventario
    public void UpdateInventoryObjects()
    {
        if(item1Object != null) item1Object.SetActive(currentInventory.item_1);
        if(item2Object != null) item2Object.SetActive(currentInventory.item_2);
        if(item3Object != null) item3Object.SetActive(currentInventory.item_3);
        if(item4Object != null) item4Object.SetActive(currentInventory.item_4);
    }
}