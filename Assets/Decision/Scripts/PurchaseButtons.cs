using UnityEngine;
using UnityEngine.UI;

// Clase que maneja los botones de compra en la tienda
public class PurchaseButtons : MonoBehaviour
{
    // Referencias a los gestores necesarios
    private DecisionPointsManager pointsManager;
    private InventoryManagerD inventoryManager;
    private LogrosManager logrosManager;
    private StreakManagerD streakManager;
    public Text pointsDisplayText;
    
    // Inicialización de componentes
    void Start()
    {
        pointsManager = FindFirstObjectByType<DecisionPointsManager>();
        inventoryManager = FindFirstObjectByType<InventoryManagerD>();
        logrosManager = FindFirstObjectByType<LogrosManager>();
        streakManager = FindFirstObjectByType<StreakManagerD>();

        if (pointsManager == null)
        {
            Debug.LogError("No se encontró DecisionPointsManager en la escena");
            return;
        }
        UpdatePointsDisplay();
    }

    // Actualiza el display de puntos
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

    // Actualización continua del display
    private void Update()
    {
        UpdatePointsDisplay();
    }

    // Métodos para comprar items específicos
    public void PurchaseItem1()
    {
        if (!inventoryManager.HasItem(1) && pointsManager.GetCurrentPoints() >= 50)
        {
            pointsManager.ReducePoints(50);
            inventoryManager.PurchaseItem(1);
            inventoryManager.LoadInventory();
            logrosManager.VerificarLogros(
                pointsManager.GetCurrentPoints(),
                streakManager.GetCurrentStreak(),
                true
            );
        }
    }

    public void PurchaseItem2()
    {
        if (!inventoryManager.HasItem(2) && pointsManager.GetCurrentPoints() >= 100)
        {
            pointsManager.ReducePoints(100);
            inventoryManager.PurchaseItem(2);
            inventoryManager.LoadInventory();
            logrosManager.VerificarLogros(
                pointsManager.GetCurrentPoints(),
                streakManager.GetCurrentStreak(),
                true
            );
        }
    }

    public void PurchaseItem3()
    {
        if (!inventoryManager.HasItem(3) && pointsManager.GetCurrentPoints() >= 150)
        {
            pointsManager.ReducePoints(150);
            inventoryManager.PurchaseItem(3);
            inventoryManager.LoadInventory();
            logrosManager.VerificarLogros(
                pointsManager.GetCurrentPoints(),
                streakManager.GetCurrentStreak(),
                true
            );
        }
    }

    public void PurchaseItem4()
    {
        if (!inventoryManager.HasItem(4) && pointsManager.GetCurrentPoints() >= 200)
        {
            pointsManager.ReducePoints(200);
            inventoryManager.PurchaseItem(4);
            inventoryManager.LoadInventory();
            logrosManager.VerificarLogros(
                pointsManager.GetCurrentPoints(),
                streakManager.GetCurrentStreak(),
                true
            );
        }
    }
}