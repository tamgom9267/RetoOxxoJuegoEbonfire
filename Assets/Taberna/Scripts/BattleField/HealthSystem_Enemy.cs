using UnityEngine;

public class HealthSystem_Enemy : MonoBehaviour
{
    public int maxHealth = 7;
    public int currentHealth = 7;

    public Sprite[] healthSprites;           // 8 sprites: del lleno al vacío
    public SpriteRenderer healthRenderer;    // SpriteRenderer del objeto de barra de vida del enemigo

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
        // Aquí puedes reproducir una animación de muerte, desactivarlo, etc.
        gameObject.SetActive(false); // Por ahora, lo ocultamos
    }
}
