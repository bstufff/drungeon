using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySpawner : MonoBehaviour
{
    public Enemy defaultEnemy; // Liste des prototypes d'ennemis
    public Transform EnemyParentObject;                    // Parent des ennemis dans la hi�rarchie
    public int EnemiesRemaining = 0;
    public EnemyPool EnemyPool;

    private int pathEnumerator = 0; // Pour alterner entre les chemins possibles
    private Level currentLevel;
    private LevelManager levelManager;

    private void Start()
    {
        levelManager = GetComponent<LevelManager>();
        EnemyPool = new EnemyPool(defaultEnemy);
    }

    // D�marre la s�quence de spawn avec un d�lai initial
    public IEnumerator Spawn(Level level, float gracePeriod)
    {
        EnemiesRemaining = 0;
        pathEnumerator = 0;
        currentLevel = level;

        foreach (Wave wave in level.waves)
        {
            EnemiesRemaining += wave.enemyCount;
        }

        yield return new WaitForSeconds(gracePeriod); // D�lai pour que le joueur se pr�pare
        StartCoroutine(SpawnWaves());
    }

    // D�truit tous les ennemis encore pr�sents
    public void DestroyAllEnemies()
    {
        StopAllCoroutines();
        foreach (Transform child in EnemyParentObject)
        {
            EnemyPool.ReturnEnemy(child.GetComponent<Enemy>());
        }
    }

    // G�re le spawn des vagues d'ennemis
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

    // G�re le spawn d'un ennemi � partir d'un prototype
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
