using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private int maxHealth = 16;
    [SerializeField] private int currentHealth;
    [SerializeField] private Sprite[] healthSprites;
    [SerializeField] private SpriteRenderer healthRenderer;   

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
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();
        if (currentHealth <= 0)
        {
            PlayerPrefs.SetString("resultado", "derrota");
            PlayerPrefs.Save();
            SceneManager.LoadScene("Battlefield_Resultado");
        }
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
