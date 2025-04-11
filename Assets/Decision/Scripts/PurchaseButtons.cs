using UnityEngine;
using UnityEngine.UI;

public class PurchaseButtons : MonoBehaviour
{
    private DecisionPointsManager pointsManager;
    private InventoryManagerD inventoryManager;
    private LogrosManager logrosManager;
    private StreakManagerD streakManager;
    public Text pointsDisplayText;
    
    void Start()
    {
        // Obtiene todas las referencias necesarias una sola vez
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

    private void Update()
    {
        UpdatePointsDisplay();
    }

    public void PurchaseItem1()
    {
        if (pointsManager.GetCurrentPoints() >= 50)
        {
            pointsManager.ReducePoints(50);
            inventoryManager.PurchaseItem(1);
            logrosManager.VerificarLogros(
                pointsManager.GetCurrentPoints(),
                streakManager.GetCurrentStreak(),
                true
            );
        }
    }

    public void PurchaseItem2()
    {
        if (pointsManager.GetCurrentPoints() >= 100)
        {
            pointsManager.ReducePoints(100);
            inventoryManager.PurchaseItem(2);
            logrosManager.VerificarLogros(
                pointsManager.GetCurrentPoints(),
                streakManager.GetCurrentStreak(),
                true
            );
        }
    }

    public void PurchaseItem3()
    {
        if (pointsManager.GetCurrentPoints() >= 150)
        {
            pointsManager.ReducePoints(150);
            inventoryManager.PurchaseItem(3);
            logrosManager.VerificarLogros(
                pointsManager.GetCurrentPoints(),
                streakManager.GetCurrentStreak(),
                true
            );
        }
    }

    public void PurchaseItem4()
    {
        if (pointsManager.GetCurrentPoints() >= 200)
        {
            pointsManager.ReducePoints(200);
            inventoryManager.PurchaseItem(4);
            logrosManager.VerificarLogros(
                pointsManager.GetCurrentPoints(),
                streakManager.GetCurrentStreak(),
                true
            );
        }
    }
}