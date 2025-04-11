// Clase principal que maneja el sistema de combate por turnos, incluyendo vida, animaciones, preguntas y temporizador
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    // Enum para distinguir el turno actual entre jugador y enemigo
    public enum Turn { Player, Enemy }
    public Turn currentTurn;

    // ===== CONFIGURACIÓN DEL JUGADOR =====
    [Header("Jugador")]
    [SerializeField] private int playerMaxHealth = 16; // Vida máxima del jugador
    [SerializeField] private Sprite[] playerHealthSprites; // Sprites para mostrar la barra de vida
    [SerializeField] private SpriteRenderer playerHealthRenderer; // SpriteRenderer del jugador
    [SerializeField] private Animator playerAnimator; // Animador del jugador
    [SerializeField] private Text actionPreviewText; // Texto para mostrar la acción seleccionada
    [SerializeField] private int playerHealth; // Vida actual del jugador
    private bool playerIsDead = false; // Si el jugador está muerto

    // ===== CONFIGURACIÓN DEL ENEMIGO =====
    [Header("Enemigo")]
    [SerializeField] private int enemyMaxHealth = 7; // Vida máxima del enemigo
    [SerializeField] private Sprite[] enemyHealthSprites; // Sprites para mostrar la vida del enemigo
    [SerializeField] private SpriteRenderer enemyHealthRenderer; // SpriteRenderer del enemigo
    [SerializeField] private Animator enemyAnimator; // Animador del enemigo
    [SerializeField] private int enemyHealth; // Vida actual del enemigo
    private bool enemyIsDead = false; // Si el enemigo está muerto

    // ===== REFERENCIAS EXTERNAS =====
    [Header("UI y Preguntas")]
    public QuestionManager questionManager; // Referencia al manejador de preguntas

    // Posibles acciones del jugador
    private enum PlayerAction { None, AttackA, AttackB, Heal }
    private PlayerAction selectedAction = PlayerAction.None; // Acción actualmente seleccionada

    // Temporizador del juego
    private float gameTimer = 0f;

    // === Se ejecuta antes del Start ===
    void Awake()
    {
        gameTimer = PlayerPrefs.GetFloat("tiempo_total", 0f); // Leer el tiempo acumulado
    }

    // === Inicializa el estado del juego ===
    void Start()
    {
        playerHealth = PlayerPrefs.HasKey("vida_jugador") ? PlayerPrefs.GetInt("vida_jugador") : playerMaxHealth;
        enemyHealth = enemyMaxHealth;

        UpdatePlayerHealthBar();
        UpdateEnemyHealthBar();

        StartPlayerTurn();
    }

    // === Se ejecuta cada frame ===
    void Update()
    {
        gameTimer += Time.deltaTime; // Sumar tiempo
        PlayerPrefs.SetFloat("tiempo_total", gameTimer); // Guardarlo continuamente

        // Cancelar logro si la vida del jugador cae por debajo del 50%
        if (playerHealth < playerMaxHealth / 2)
        {
            PlayerPrefs.SetInt("logro_taberna_vida", 0);
            Debug.Log("Logro cancelado: vida bajó del 50%");
        }
    }

    // === INICIO DEL TURNO DEL JUGADOR ===
    public void StartPlayerTurn()
    {
        if (playerIsDead || enemyIsDead) return;

        selectedAction = PlayerAction.None;
        currentTurn = Turn.Player;
        UpdateActionPreview("Selecciona un movimiento");
        Debug.Log("Turno del jugador");
    }

    // === FIN DEL TURNO DEL JUGADOR ===
    public void EndPlayerTurn()
    {
        Debug.Log("Fin del turno del jugador");

        int turnos_jugador = PlayerPrefs.GetInt("turnos_jugador", 0);
        PlayerPrefs.SetInt("turnos_jugador", turnos_jugador + 1);

        StartCoroutine(StartEnemyTurn());
    }

    // === CORUTINA: TURNO DEL ENEMIGO ===
    private System.Collections.IEnumerator StartEnemyTurn()
    {
        currentTurn = Turn.Enemy;
        Debug.Log("Turno del enemigo");

        yield return new WaitForSeconds(0.5f);

        if (enemyIsDead || playerIsDead) yield break;

        int damage = GetRandomEnemyDamage();
        Debug.Log($"Enemigo ataca con {damage} de daño");

        // Reproducir animación según el daño
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

    // === ACCIONES DEL JUGADOR ===
    public void SelectAttackA() { selectedAction = PlayerAction.AttackA; UpdateActionPreview("Próximo movimiento: Ataque A"); }
    public void SelectAttackB() { selectedAction = PlayerAction.AttackB; UpdateActionPreview("Próximo movimiento: Ataque B"); }
    public void SelectHeal()    { selectedAction = PlayerAction.Heal;    UpdateActionPreview("Próximo movimiento: Curar"); }

    // === CONFIRMAR LA ACCIÓN SELECCIONADA ===
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

    // === RECIBIR RESPUESTA DEL JUGADOR ===
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

    // === GESTIÓN DE VIDA DEL JUGADOR ===
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

    // === ANIMACIÓN DE MUERTE DEL JUGADOR ===
    private System.Collections.IEnumerator HandlePlayerDeath()
    {
        yield return new WaitForSeconds(0.5f);
        playerAnimator.SetTrigger("doDeath");
        yield return new WaitForSeconds(1f);
        PlayerPrefs.SetString("resultado", "derrota");
        PlayerPrefs.Save();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Battlefield_Resultado");
    }

    // === CURACIÓN ===
    private void HealPlayer(int amount)
    {
        playerHealth += amount;
        playerHealth = Mathf.Clamp(playerHealth, 0, playerMaxHealth);
        UpdatePlayerHealthBar();
    }

    // === ACTUALIZAR BARRA DE VIDA DEL JUGADOR ===
    private void UpdatePlayerHealthBar()
    {
        int index = Mathf.Clamp(playerMaxHealth - playerHealth, 0, playerHealthSprites.Length - 1);
        playerHealthRenderer.sprite = playerHealthSprites[index];
    }

    // === GESTIÓN DE VIDA DEL ENEMIGO ===
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

    // === ANIMACIÓN DE MUERTE DEL ENEMIGO ===
    private System.Collections.IEnumerator HandleEnemyDeath()
    {
        yield return new WaitForSeconds(1f);
        enemyAnimator.SetTrigger("doSk_Death");
        yield return new WaitForSeconds(1f);
        PlayerPrefs.SetInt("vida_jugador", playerHealth);
        PlayerPrefs.SetString("resultado", "victoria");
        PlayerPrefs.Save();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Battlefield_Resultado");
    }

    // === ACTUALIZAR BARRA DE VIDA DEL ENEMIGO ===
    private void UpdateEnemyHealthBar()
    {
        int index = Mathf.Clamp(enemyMaxHealth - enemyHealth, 0, enemyHealthSprites.Length - 1);
        enemyHealthRenderer.sprite = enemyHealthSprites[index];
    }

    // === APLICAR DAÑO DESPUÉS DE LA ANIMACIÓN DEL JUGADOR ===
    private System.Collections.IEnumerator ApplyDamageToEnemyAfterAnimation(int amount)
    {
        yield return new WaitForSeconds(0.5f);
        ApplyDamageToEnemy(amount);
        if (!enemyIsDead)
            EndPlayerTurn();
    }

    // === DAÑO ALEATORIO DEL ENEMIGO ===
    private int GetRandomEnemyDamage()
    {
        float r = Random.value * 100f;
        if (r < 60f) return 1;
        else if (r < 99f) return 2;
        else return 3;
    }

    // === ACTUALIZAR TEXTO DE ACCIÓN ===
    private void UpdateActionPreview(string message)
    {
        if (actionPreviewText != null)
            actionPreviewText.text = message;
    }
}
