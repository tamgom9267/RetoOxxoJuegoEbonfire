using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public PlayerCombat playerCombat;       
    public EnemyCombat enemyCombat;         

    public enum Turn { Player, Enemy }
    public Turn currentTurn;

    void Start()
    {
        StartPlayerTurn();
    }

    public void StartPlayerTurn()
    {
        currentTurn = Turn.Player;
        Debug.Log("Turno del jugador");
        playerCombat.StartPlayerTurn();     
    }

    public void EndPlayerTurn()
    {
        Debug.Log("Fin del turno del jugador");
        StartCoroutine(StartEnemyTurn());
    }

    private System.Collections.IEnumerator StartEnemyTurn()
    {
        currentTurn = Turn.Enemy;
        Debug.Log("Turno del enemigo");

        yield return new WaitForSeconds(1.5f); // Espera antes de atacar
        enemyCombat.PerformAttack();           // Ataque automático del enemigo

        yield return new WaitForSeconds(1.5f); // Espera antes de regresar al jugador
        StartPlayerTurn();
    }
}
