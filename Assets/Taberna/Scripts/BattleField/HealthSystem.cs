using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private int maxHealth = 16;
    [SerializeField] private int currentHealth;
    [SerializeField] private Sprite[] healthSprites;
    [SerializeField] private SpriteRenderer healthRenderer;   

    [SerializeField] private Animator playerAnimator;
    private bool isDead = false;


    void Start()
    {
        if (PlayerPrefs.HasKey("vida_jugador"))
        {
            currentHealth = Mathf.Clamp(PlayerPrefs.GetInt("vida_jugador"), 0, maxHealth);
        }
        else
        {
            currentHealth = maxHealth;
        }

        UpdateHealthBar();
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            isDead = true;
            playerAnimator?.SetTrigger("doDeath");

            PlayerPrefs.SetString("resultado", "derrota");
            PlayerPrefs.Save();
            StartCoroutine(LoadAfterDelay());
        }
    }

    private System.Collections.IEnumerator LoadAfterDelay()
    {
        yield return new WaitForSeconds(0.5f); // duración de la animación
        SceneManager.LoadScene("Battlefield_Resultado");
    }


    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        int index = maxHealth - currentHealth;
        index = Mathf.Clamp(index, 0, healthSprites.Length - 1);
        healthRenderer.sprite = healthSprites[index];
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

}
