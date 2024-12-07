using UnityEngine;
using UnityEngine.UI;
public class HealthManager : MonoBehaviour
{
    public Image healthBar;
    public float currentHealth;

    private float _maxHealth;
    private LevelManager levelManager;
    private EnemySpawner enemySpawner;

    private void Start()
    {
        RefreshManaBar();
        levelManager = FindAnyObjectByType<LevelManager>();
        enemySpawner = FindAnyObjectByType<EnemySpawner>();
    }

    public float MaxHealth
    {
        get { return _maxHealth; }
        set
        {
            _maxHealth = value;
            currentHealth = value;
            RefreshManaBar();
        }
    }

    public void RefreshManaBar()
    {
        if (healthBar == null)
        {
            healthBar = transform.Find("HealthBar").Find("Health").GetComponent<Image>();
        }
        currentHealth = Mathf.Clamp(currentHealth, 0, MaxHealth);
        healthBar.fillAmount = currentHealth / MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        RefreshManaBar();

        if (currentHealth <= 0)
        {
            // Déclenche la séquence de victoire si cet ennemi est le dernier du niveau
            if (enemySpawner.EnemiesRemaining == 1) 
            {
                levelManager.Win(); 
            }
            else
            {
                enemySpawner.EnemiesRemaining--;
            }

            // Ajoute l'ennemi mort à la pool
            enemySpawner.EnemyPool.ReturnEnemy(GetComponent<Enemy>());
        }
    }
    public void Heal(float healingAmount)
    {
        currentHealth += healingAmount;
        RefreshManaBar();
    }
    
}