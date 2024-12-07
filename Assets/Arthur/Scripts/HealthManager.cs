using UnityEngine;
using UnityEngine.UI;
public class HealthManager : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    public float currentHealth;

    private float _maxHealth;
    private LevelManager _levelManager;
    private EnemySpawner _enemySpawner;

    private void Start()
    {
        // Initialisation
        RefreshHealthBar();
        _levelManager = FindAnyObjectByType<LevelManager>();
        _enemySpawner = FindAnyObjectByType<EnemySpawner>();
    }

    public float MaxHealth
    {
        get { return _maxHealth; }
        set
        {
            // MaxHealth change uniquement si un ennemi est "transform�" en un autre,
            // alors on en profite pour remettre en place les autres variables
            _maxHealth = value;
            currentHealth = value;
            RefreshHealthBar();
        }
    }

    public void RefreshHealthBar()
    {
        // Fonction g�n�rale pour afficher et rafraichir la barre de vie
        if (healthBar == null)
        {
            healthBar = transform.Find("HealthBar").Find("Health").GetComponent<Image>();
        }
        currentHealth = Mathf.Clamp(currentHealth, 0, MaxHealth);
        healthBar.fillAmount = currentHealth / MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        // Applique les d�gats � l'ennemi
        currentHealth -= damage;
        RefreshHealthBar();

        if (currentHealth <= 0)
        {
            // D�clenche la s�quence de victoire si cet ennemi est le dernier du niveau
            if (_enemySpawner.EnemiesRemaining == 1) 
            {
                Debug.Log("youpla");
                _levelManager.Win(); 
            }
            else
            {
                _enemySpawner.EnemiesRemaining--;
            }

            // Ajoute l'ennemi mort � la pool
            _enemySpawner.EnemyPool.ReturnEnemy(GetComponent<Enemy>());
        }
    }
    // Non utilis� dans le jeu, mais bon � avoir au cas o�
    public void Heal(float healingAmount)
    {
        currentHealth += healingAmount;
        RefreshHealthBar();
    }
    
}