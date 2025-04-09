using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    public enum Turn { Player, Enemy }
    public Turn currentTurn;

    [Header("Jugador")]
    [SerializeField] private int playerMaxHealth = 16;
    [SerializeField] private Sprite[] playerHealthSprites;
    [SerializeField] private SpriteRenderer playerHealthRenderer;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Text actionPreviewText;

    [SerializeField] private int playerHealth;
    private bool playerIsDead = false;

    [Header("Enemigo")]
    [SerializeField] private int enemyMaxHealth = 7;
    [SerializeField] private Sprite[] enemyHealthSprites;
    [SerializeField] private SpriteRenderer enemyHealthRenderer;
    [SerializeField] private Animator enemyAnimator;

    [SerializeField] private int enemyHealth;
    private bool enemyIsDead = false;

    [Header("UI y Preguntas")]
    public QuestionManager questionManager;

    private enum PlayerAction { None, AttackA, AttackB, Heal }
    private PlayerAction selectedAction = PlayerAction.None;

    private float gameTimer = 0f;


    void Awake()
    {
        gameTimer = PlayerPrefs.GetFloat("tiempo_total", 0f);
    }
    
    void Start()
    {
        playerHealth = PlayerPrefs.HasKey("vida_jugador") ? PlayerPrefs.GetInt("vida_jugador") : playerMaxHealth;
        enemyHealth = enemyMaxHealth;

        UpdatePlayerHealthBar();
        UpdateEnemyHealthBar();

        StartPlayerTurn();
    }

    void Update()
    {
        gameTimer += Time.deltaTime;
        PlayerPrefs.SetFloat("tiempo_total", gameTimer);
    }


    // ========== TURNOS ==========
    public void StartPlayerTurn()
    {
        if (playerIsDead || enemyIsDead) return;

        selectedAction = PlayerAction.None;
        currentTurn = Turn.Player;
        UpdateActionPreview("Selecciona un movimiento");
        Debug.Log("Turno del jugador");
    }

    public void EndPlayerTurn()
    {
        Debug.Log("Fin del turno del jugador");

        int turnos_jugador = PlayerPrefs.GetInt("turnos_jugador", 0);
        PlayerPrefs.SetInt("turnos_jugador", turnos_jugador + 1);

        StartCoroutine(StartEnemyTurn());
    }

    private System.Collections.IEnumerator StartEnemyTurn()
    {
        currentTurn = Turn.Enemy;
        Debug.Log("Turno del enemigo");

        yield return new WaitForSeconds(0.5f);

        if (enemyIsDead || playerIsDead) yield break;

        int damage = GetRandomEnemyDamage();
        Debug.Log($"Enemigo ataca con {damage} de daño");

        if (damage == 1)
            enemyAnimator.SetTrigger("doSk_Attack");
        else if (damage == 2)
            enemyAnimator.SetTrigger("doSk_Attack2");
        else
            enemyAnimator.SetTrigger("doSk_Attack3");

        yield return new WaitForSeconds(0.5f);

        ApplyDamageToPlayer(damage);

        if (!playerIsDead)
            StartPlayerTurn();
    }

    // ========== ACCIONES DEL JUGADOR ==========

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
        bool isHard = selectedAction == PlayerAction.AttackB;
        if (selectedAction == PlayerAction.Heal)
            isHard = Random.value > 0.5f;

        questionManager.ShowQuestion(this, isHard, selectedAction == PlayerAction.Heal);
    }

    public void ReceiveAnswer(bool correct, bool isHealing, bool isHard)
    {
        if (correct)
        {
            if (isHealing)
            {
                int healAmount = isHard ? 3 : 1;
                Debug.Log($"Curando {healAmount} puntos de vida");
                HealPlayer(healAmount);
                EndPlayerTurn();
            }
            else
            {
                int damage = selectedAction == PlayerAction.AttackA ? 1 : 3;

                if (selectedAction == PlayerAction.AttackA)
                    playerAnimator.SetTrigger("doDashAttack");
                else
                    playerAnimator.SetTrigger("doAttack");

                StartCoroutine(ApplyDamageToEnemyAfterAnimation(damage));
            }
        }
        else
        {
            Debug.Log("Respuesta incorrecta. No se aplica efecto.");
            EndPlayerTurn();
        }
    }

    // ========== VIDA JUGADOR ==========
    private void ApplyDamageToPlayer(int amount)
    {
        if (playerIsDead) return;

        playerHealth -= amount;
        playerHealth = Mathf.Clamp(playerHealth, 0, playerMaxHealth);
        UpdatePlayerHealthBar();

        if (playerHealth <= 0)
        {
            playerIsDead = true;
            StartCoroutine(HandlePlayerDeath());
        }
    }

    private System.Collections.IEnumerator HandlePlayerDeath()
    {
        yield return new WaitForSeconds(0.5f); // Delay antes de animación
        playerAnimator.SetTrigger("doDeath");
        yield return new WaitForSeconds(1f); // Esperar que la animación se muestre
        PlayerPrefs.SetString("resultado", "derrota");
        PlayerPrefs.Save();
        yield return new WaitForSeconds(1f); // Esperar antes de cambiar de escena
        SceneManager.LoadScene("Battlefield_Resultado");
    }


    private void HealPlayer(int amount)
    {
        playerHealth += amount;
        playerHealth = Mathf.Clamp(playerHealth, 0, playerMaxHealth);
        UpdatePlayerHealthBar();
    }

    private void UpdatePlayerHealthBar()
    {
        int index = Mathf.Clamp(playerMaxHealth - playerHealth, 0, playerHealthSprites.Length - 1);
        playerHealthRenderer.sprite = playerHealthSprites[index];
    }

    // ========== VIDA ENEMIGO ==========
    private void ApplyDamageToEnemy(int amount)
    {
        if (enemyIsDead) return;

        enemyHealth -= amount;
        enemyHealth = Mathf.Clamp(enemyHealth, 0, enemyMaxHealth);
        UpdateEnemyHealthBar();

        if (enemyHealth <= 0)
        {
            enemyIsDead = true;
            StartCoroutine(HandleEnemyDeath());
        }
    }

    private System.Collections.IEnumerator HandleEnemyDeath()
    {
        yield return new WaitForSeconds(1f); // Delay antes de animación
        enemyAnimator.SetTrigger("doSk_Death");
        yield return new WaitForSeconds(1f); // Esperar que la animación se muestre
        PlayerPrefs.SetInt("vida_jugador", playerHealth);
        PlayerPrefs.SetString("resultado", "victoria");
        PlayerPrefs.Save();
        yield return new WaitForSeconds(1f); // Esperar antes de cambiar de escena
        SceneManager.LoadScene("Battlefield_Resultado");
    }


    private void UpdateEnemyHealthBar()
    {
        int index = Mathf.Clamp(enemyMaxHealth - enemyHealth, 0, enemyHealthSprites.Length - 1);
        enemyHealthRenderer.sprite = enemyHealthSprites[index];
    }

    private System.Collections.IEnumerator ApplyDamageToEnemyAfterAnimation(int amount)
    {
        yield return new WaitForSeconds(0.5f);
        ApplyDamageToEnemy(amount);
        if (!enemyIsDead)
            EndPlayerTurn();
    }

    private int GetRandomEnemyDamage()
    {
        float r = Random.value * 100f;
        if (r < 60f) return 1;
        else if (r < 99f) return 2;
        else return 3;
    }

    private void UpdateActionPreview(string message)
    {
        if (actionPreviewText != null)
            actionPreviewText.text = message;
    }
}
