using UnityEngine;
using UnityEngine.UI;
public class HealthManager : MonoBehaviour
{
    public float CurrentHealth;

    [SerializeField] private Image _healthBar;
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
            CurrentHealth = value;
            RefreshHealthBar();
        }
    }

    public void RefreshHealthBar()
    {
        // Fonction g�n�rale pour afficher et rafraichir la barre de vie
        if (_healthBar == null)
        {
            _healthBar = transform.Find("HealthBar").Find("Health").GetComponent<Image>();
        }
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);
        _healthBar.fillAmount = CurrentHealth / MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        // Applique les d�gats � l'ennemi
        CurrentHealth -= damage;
        RefreshHealthBar();

        if (CurrentHealth <= 0)
        {
            // D�clenche la s�quence de victoire si cet ennemi est le dernier du niveau
            if (_enemySpawner.EnemiesRemaining == 1) 
            {
                _enemySpawner.EnemiesRemaining--;
                Debug.Log("youpla");
                _levelManager.Win(); 
            }
            else
            {
                _enemySpawner.EnemiesRemaining--;
            }

            // Ajoute l'ennemi mort � la pool
            _enemySpawner.EnemyPool.ReturnEnemy(GetComponent<Enemy>());
            Debug.Log("recycling!");
        }
    }
    // Non utilis� dans le jeu, mais bon � avoir au cas o�
    public void Heal(float healingAmount)
    {
        CurrentHealth += healingAmount;
        RefreshHealthBar();
    }
    
}