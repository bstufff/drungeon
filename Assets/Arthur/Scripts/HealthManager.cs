using UnityEngine;
using UnityEngine.UI;
public class HealthManager : MonoBehaviour
{
    public Image healthBar;
    public float currentHealth;
    public float maxHealth;
    private LevelManager levelManager;
    private EnemySpawner enemySpawner;
    private void Start()
    {
        healthBar = transform.Find("HealthBar").Find("Health").GetComponent<Image>();
        levelManager = FindAnyObjectByType<LevelManager>();
        enemySpawner = FindAnyObjectByType<EnemySpawner>();
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthBar.fillAmount = currentHealth / maxHealth;
        if (currentHealth <= 0)
        {
            if (enemySpawner.EnemiesRemaining == 1) // Si cet ennemi est le dernier du niveau
            {
                levelManager.Win(); // Déclenche la séquence de victoire
            }
            else
            {
                enemySpawner.EnemiesRemaining--;
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