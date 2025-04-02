using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public HealthSystem playerHealth; // Asignar al jugador en el Inspector
    public TurnManager turnManager;

    public void PerformAttack()
    {
        int damage = GetRandomDamage();
        Debug.Log($"El enemigo ataca con {damage} de daño");

        if (playerHealth != null)
            playerHealth.TakeDamage(damage);

        turnManager.StartPlayerTurn();
    }

    private int GetRandomDamage()
    {
        float random = Random.value * 100f;

        if (random < 60f)
            return 1; // 60% chance
        else if (random < 99f)
            return 2; // 39% chance
        else
            return 3; // 1% chance
    }
}
