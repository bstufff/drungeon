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
    private int currentWave = 0;

    public IEnumerator Spawn(Level level, float gracePeriod)
    {
        currentWave = 0;
        EnemiesRemaining = 0;
        pathEnumerator = 0;
        currentLevel = level;
        foreach (Wave wave in level.waves)
        {
            EnemiesRemaining += wave.enemyCount;
        }
        yield return new WaitForSeconds(gracePeriod); // delay for the player to get ready
        StartCoroutine(SpawnWave());

    }

    public void DestroyAllEnemies() // in case enemies are leftover from a previous game
    {
        StopAllCoroutines();
        foreach (Transform child in EnemyParentObject)
        {
            Destroy(child.gameObject);
        }
    }
    IEnumerator SpawnWave()
    {
        if (currentWave >= currentLevel.waves.Count && FindAnyObjectByType<LevelManager>().IsIngame)
        {
            yield break;  
        }

        // Get the current wave
        Wave wave = currentLevel.waves[currentWave];

        for (int i = 0; i < wave.enemyCount; i++)
        {
            SpawnEnemy(wave.enemyPrefab);
            yield return new WaitForSeconds(wave.spawnRate); //Wait before spawning the next enemy
        }

        currentWave++;
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


