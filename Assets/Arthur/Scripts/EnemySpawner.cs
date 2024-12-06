using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<Enemy> enemyPrototypes = new List<Enemy>(); // Liste des prototypes d'ennemis
    public Transform EnemyParentObject;                    // Parent des ennemis dans la hiérarchie
    public int EnemiesRemaining = 0;

    private int pathEnumerator = 0; // Pour alterner entre les chemins possibles

    private Level currentLevel;
    private LevelManager levelManager;

    private void Start()
    {
        levelManager = GetComponent<LevelManager>();
    }

    // Démarre la séquence de spawn avec un délai initial
    public IEnumerator Spawn(Level level, float gracePeriod)
    {
        EnemiesRemaining = 0;
        pathEnumerator = 0;
        currentLevel = level;

        foreach (Wave wave in level.waves)
        {
            EnemiesRemaining += wave.enemyCount;
        }

        yield return new WaitForSeconds(gracePeriod); // Délai pour que le joueur se prépare
        StartCoroutine(SpawnWaves());
    }

    // Détruit tous les ennemis encore présents
    public void DestroyAllEnemies()
    {
        StopAllCoroutines();
        foreach (Transform child in EnemyParentObject)
        {
            Destroy(child.gameObject);
        }
    }

    // Gère le spawn des vagues d'ennemis
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
    private void SpawnEnemy(int enemyType)
    {
        if (enemyType < 0 || enemyType >= enemyPrototypes.Count)
        {
            Debug.LogError("Type d'ennemi invalide !");
            return;
        }

        // Clone l'ennemi à partir du prototype
        Enemy enemyInstance = enemyPrototypes[enemyType].Clone();
        enemyInstance.transform.SetParent(EnemyParentObject);

        // Assigner un chemin au nouvel ennemi
        int pathCount = currentLevel.paths.Count;
        enemyInstance.path = currentLevel.paths[pathEnumerator];

        // Alterner entre les chemins
        pathEnumerator = (pathEnumerator + 1) % pathCount;
    }
}
