using UnityEngine;
using UnityEngine.UI;
public class HealthManager : MonoBehaviour
{
    public Image healthBar;
    public float currentHealth;
    public float maxHealth;
    private void Start()
    {
        healthBar = transform.Find("HealthBar").Find("Health").GetComponent<Image>();
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthBar.fillAmount = currentHealth / maxHealth;
        if (currentHealth <= 0)
        {
            if (FindAnyObjectByType<EnemySpawner>().EnemiesRemaining == 1) // Si cet ennemi est le dernier du niveau
            {
                FindAnyObjectByType<LevelManager>().Win(); // Déclenche la séquence de victoire
            }
            Destroy(gameObject);
        }
    }
    public void Heal(float healingAmount)
    {
        currentHealth += healingAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthBar.fillAmount = currentHealth / maxHealth;
    }
}