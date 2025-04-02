using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public int maxHealth = 16;
    public int currentHealth = 16;

    public Sprite[] healthSprites;           
    public SpriteRenderer healthRenderer;    

    void Start()
    {
        UpdateHealthBar();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();
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
}
