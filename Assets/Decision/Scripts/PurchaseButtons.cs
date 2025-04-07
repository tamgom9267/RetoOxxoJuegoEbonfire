using UnityEngine;
using UnityEngine.UI;

// Clase que maneja la compra de items en la tienda
public class PurchaseButtons : MonoBehaviour
{
    private DecisionPointsManager pointsManager;    // Referencia al gestor de puntos
    private InventoryManagerD InventoryManager;     // Referencia al gestor de inventario
    public Text pointsDisplayText;                  // Texto UI que muestra los puntos
    
    void Start()
    {
        // Obtiene las referencias necesarias
        pointsManager = FindObjectOfType<DecisionPointsManager>();
        InventoryManager = FindObjectOfType<InventoryManagerD>();
        if (pointsManager == null)
        {
            Debug.LogError("No se encontró DecisionPointsManager en la escena");
            return;
        }
        UpdatePointsDisplay();
    }

    // Actualiza el texto que muestra los puntos disponibles
    private void UpdatePointsDisplay()
    {
        if (pointsDisplayText != null && pointsManager != null)
        {
            pointsDisplayText.text = $"{pointsManager.GetCurrentPoints()}";
        }
        else if (pointsDisplayText == null)
        {
            Debug.LogError("No se ha asignado el Text para mostrar los puntos");
        }
    }

    // Actualiza constantemente el display de puntos
    private void Update()
    {
        UpdatePointsDisplay();
    }

    // Métodos para comprar diferentes items
    // Cada método verifica si hay suficientes puntos y realiza la compra
    public void PurchaseItem1()
    {
        if (pointsManager.GetCurrentPoints() >= 50)
        {
            pointsManager.ReducePoints(50);
            InventoryManager.PurchaseItem(1);
        }
    }

    public void PurchaseItem2()
    {
        if (pointsManager.GetCurrentPoints() >= 100)
        {
            pointsManager.ReducePoints(100);
            InventoryManager.PurchaseItem(2);
        }
    }

    public void PurchaseItem3()
    {
        if (pointsManager.GetCurrentPoints() >= 150)
        {
            pointsManager.ReducePoints(150);
            InventoryManager.PurchaseItem(3);
        }
    }

    public void PurchaseItem4()
    {
        if (pointsManager.GetCurrentPoints() >= 200)
        {
            pointsManager.ReducePoints(200);
            InventoryManager.PurchaseItem(4);
        }
    }
}