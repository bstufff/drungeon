using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySpawner : MonoBehaviour
{
    public Enemy DefaultEnemy; // Liste des prototypes d'ennemis
    public Transform EnemyParentObject;                    // Parent des ennemis dans la hiérarchie
    private int enemiesRemaining = 0;
    public EnemyPool EnemyPool;

    private int _pathEnumerator = 0; // Pour alterner entre les chemins possibles
    private Level _currentLevel;
    private LevelManager _levelManager;

    private void Start()
    {
        // Mise en place
        _levelManager = GetComponent<LevelManager>();
        EnemyPool = new EnemyPool(DefaultEnemy);
    }

    public int EnemiesRemaining
    {
        get { return enemiesRemaining; }
        set { 
                enemiesRemaining = value;
                if (value == 0 && _levelManager.IsIngame)
                    _levelManager.Win();
            }
    }

    // Démarre la séquence de spawn avec un délai initial
    public IEnumerator Spawn(Level level, float gracePeriod)
    {
        // Rénitialisation des variables
        enemiesRemaining = 0;
        _pathEnumerator = 0;
        _currentLevel = level;

        // Compte le total des ennemis parmi toutes les vagues
        foreach (Wave wave in level.Waves)
        {
            EnemiesRemaining += wave.EnemyCount;
        }

        yield return new WaitForSeconds(gracePeriod); // Délai pour que le joueur se prépare
        StartCoroutine(SpawnWaves()); // Apparition des ennemis
    }

    // Ajoute tous les ennemis encore présents dans la scène à la pool
    public void DestroyAllEnemies()
    {
        StopAllCoroutines(); // Désactive tous les processus d'apparition d'ennemis
        foreach (Transform child in EnemyParentObject)
        {
            if (child.CompareTag("Enemy"))
            {
                Debug.Log("recycling truck !");
                EnemyPool.ReturnEnemy(child.GetComponent<Enemy>());
            }

        }
    }

    // Gère l'apparition des vagues d'ennemis
    private IEnumerator SpawnWaves()
    {
        foreach (Wave wave in _currentLevel.Waves)
        {
            for (int i = 0; i < wave.EnemyCount; i++)
            {
                if (!_levelManager.IsIngame)
                {
                    yield break;
                }

                SpawnEnemy(wave.EnemyType); // Utilise un type pour chercher un prototype
                yield return new WaitForSeconds(wave.SpawnRate);
            }
        }
    }

    // Gère le spawn d'un ennemi à partir d'un prototype
    private void SpawnEnemy(EnemyType enemyType)
    {
        Path currentPath = _currentLevel.Paths[_pathEnumerator];

        Enemy enemy = EnemyPool.GetEnemy();

        enemy.Initialize(enemyType);
        enemy.EnemyMovement.Initialize(currentPath.path);
        enemy.transform.SetParent(EnemyParentObject);

        // Alterner entre les chemins
        _pathEnumerator = (_pathEnumerator + 1) % _currentLevel.Paths.Count;
    }
}
