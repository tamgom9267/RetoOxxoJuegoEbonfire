using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthSystem_Enemy : MonoBehaviour
{
    [SerializeField] private int maxHealth = 7;
    [SerializeField] private int currentHealth;
    [SerializeField] private Sprite[] healthSprites;
    [SerializeField] private SpriteRenderer healthRenderer; 

    [SerializeField] private HealthSystem playerHealth;
    
    void Start()
    {
        UpdateHealthBar();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void UpdateHealthBar()
    {
        int index = maxHealth - currentHealth;
        index = Mathf.Clamp(index, 0, healthSprites.Length - 1);
        healthRenderer.sprite = healthSprites[index];
    }

    private void Die()
    {
        Debug.Log(gameObject.name + " ha muerto.");
        // Animación de muerte, desactivarlo, etc.
        
        // Obtener la vida actual del jugador y guardarla
        int vidaActual = playerHealth.GetCurrentHealth();
        PlayerPrefs.SetInt("vida_jugador", vidaActual);

        PlayerPrefs.SetString("resultado", "victoria");
        PlayerPrefs.Save();
        SceneManager.LoadScene("Battlefield_Resultado");
    }
}
