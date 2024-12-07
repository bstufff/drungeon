using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySpawner : MonoBehaviour
{
    public Enemy defaultEnemy; // Liste des prototypes d'ennemis
    public Transform EnemyParentObject;                    // Parent des ennemis dans la hiérarchie
    public int EnemiesRemaining = 0;
    public EnemyPool EnemyPool;

    private int pathEnumerator = 0; // Pour alterner entre les chemins possibles
    private Level currentLevel;
    private LevelManager levelManager;

    private void Start()
    {
        // Mise en place
        levelManager = GetComponent<LevelManager>();
        EnemyPool = new EnemyPool(defaultEnemy);
    }

    // Démarre la séquence de spawn avec un délai initial
    public IEnumerator Spawn(Level level, float gracePeriod)
    {
        // Rénitialisation des variables
        EnemiesRemaining = 0;
        pathEnumerator = 0;
        currentLevel = level;

        // Compte le total des ennemis parmi toutes les vagues
        foreach (Wave wave in level.waves)
        {
            EnemiesRemaining += wave.enemyCount;
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
            EnemyPool.ReturnEnemy(child.GetComponent<Enemy>());
        }
    }

    // Gère l'apparition des vagues d'ennemis
    private IEnumerator SpawnWaves()
    {
        foreach (Wave wave in currentLevel.waves)
        {
            for (int i = 0; i < wave.enemyCount; i++)
            {
                if (!levelManager.IsIngame)
                {
                    yield break;
                }

                SpawnEnemy(wave.enemyType); // Utilise un type pour chercher un prototype
                yield return new WaitForSeconds(wave.spawnRate);
            }
        }
    }

    // Gère le spawn d'un ennemi à partir d'un prototype
    private void SpawnEnemy(EnemyType enemyType)
    {
        Path currentPath = currentLevel.paths[pathEnumerator];

        Enemy enemy = EnemyPool.GetEnemy();

        enemy.Initialize(enemyType);
        enemy.EnemyMovement.Initialize(currentPath.path);
        enemy.transform.SetParent(EnemyParentObject);

        // Alterner entre les chemins
        pathEnumerator = (pathEnumerator + 1) % currentLevel.paths.Count;
    }
}
