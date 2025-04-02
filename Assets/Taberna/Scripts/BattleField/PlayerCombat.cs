using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{
    public HealthSystem playerHealth;
    public HealthSystem_Enemy enemyHealth;
    public QuestionManager questionManager;

    public TurnManager turnManager;
    public Text actionPreviewText; 

    private enum PlayerAction { None, AttackA, AttackB, Heal }
    private PlayerAction selectedAction = PlayerAction.None;


    public void StartPlayerTurn()
    {
        selectedAction = PlayerAction.None;
        UpdateActionPreview("Selecciona un movimiento");
    }

    public void SelectAttackA()
    {
        selectedAction = PlayerAction.AttackA;
        UpdateActionPreview("Próximo movimiento: Ataque A");
    }

    public void SelectAttackB()
    {
        selectedAction = PlayerAction.AttackB;
        UpdateActionPreview("Próximo movimiento: Ataque B");
    }

    public void SelectHeal()
    {
        selectedAction = PlayerAction.Heal;
        UpdateActionPreview("Próximo movimiento: Curar");
    }

    public void ConfirmAction()
    {
        if (selectedAction == PlayerAction.None)
        {
            UpdateActionPreview("Selecciona un movimiento antes de prepararte.");
            return;
        }

        Debug.Log("Preparado. Ejecutando acción: " + selectedAction);
        bool isHard = false;

        if (selectedAction == PlayerAction.AttackB)
        {
            isHard = true;
        }
        else if (selectedAction == PlayerAction.Heal)
        {
            isHard = Random.value > 0.5f; // 50% chance de que sea difícil
        }

        questionManager.ShowQuestion(this, isHard, selectedAction == PlayerAction.Heal);

    }

    private void UpdateActionPreview(string message)
    {
        if (actionPreviewText != null)
            actionPreviewText.text = message;
    }
    public void ReceiveAnswer(bool correct, bool isHealing, bool isHard)
    {
        float rachaMod = StreakManager.Instance != null ? StreakManager.Instance.GetCurrentMultiplier() : 1f;

        if (correct)
        {
            if (isHealing)
            {
                int healAmount = isHard ? 3 : 1;
                int totalHeal = Mathf.RoundToInt(healAmount * rachaMod);

                Debug.Log($"Curando {totalHeal} puntos de vida");
                playerHealth?.Heal(totalHeal);
            }
            else
            {
                // Ataque A: daño 1, Ataque B: daño 3
                int baseDamage = (selectedAction == PlayerAction.AttackA) ? 1 : 3;
                int totalDamage = Mathf.RoundToInt(baseDamage * rachaMod);

                Debug.Log($"Atacando enemigo con {totalDamage} de daño");
                if (enemyHealth != null)
                    enemyHealth.TakeDamage(totalDamage);
            }

            StreakManager.Instance?.AddCorrectAnswer();
        }
        else
        {
            Debug.Log("Respuesta incorrecta. No se aplica efecto.");
            StreakManager.Instance?.ResetStreak();
        }

        turnManager.EndPlayerTurn();
    }

}
