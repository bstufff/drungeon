using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<Enemy> enemyPrefabs = new List<Enemy>();
    public Transform EnemyParentObject;
    public Transform[] enemies;
    public int EnemiesRemaining = 0;
    private int pathEnumerator = 0; // pour alterner entre les path possibles

    private Level currentLevel;
    private LevelManager levelManager;

    private void Start()
    {
        levelManager = GetComponent<LevelManager>();
    }

    public IEnumerator Spawn(Level level, float gracePeriod)
    {
        EnemiesRemaining = 0;
        pathEnumerator = 0;
        currentLevel = level;
        foreach (Wave wave in level.waves)
        {
            EnemiesRemaining += wave.enemyCount;
        }
        yield return new WaitForSeconds(gracePeriod); // delay for the player to get ready
        StartCoroutine(SpawnWaves());

    }

    public void DestroyAllEnemies() // in case enemies are leftover from a previous game
    {
        StopAllCoroutines();
        foreach (Transform child in EnemyParentObject)
        {
            Destroy(child.gameObject);
        }
    }
    IEnumerator SpawnWaves()
    {
        foreach (Wave wave in currentLevel.waves)
        {
            for (int i = 0; i < wave.enemyCount; i++)
            {
                if (!levelManager.IsIngame)
                {
                    yield break;
                }
                SpawnEnemy(wave.enemyPrefab);
                yield return new WaitForSeconds(wave.spawnRate); //Wait before spawning the next enemy
            }
        }
    }
    void SpawnEnemy(GameObject prefab)
    {
        GameObject instance = Instantiate(prefab, EnemyParentObject.transform);

        int pathCount = currentLevel.paths.Count;

        if (pathEnumerator == pathCount - 1)
        {
            pathEnumerator = 0;
        }
        else
        {
            pathEnumerator++;
        }
        instance.GetComponent<Enemy>().path = currentLevel.paths[pathEnumerator];
    }
}


