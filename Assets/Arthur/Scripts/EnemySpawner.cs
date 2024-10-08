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
    public Transform enemyParentObject;
    public Transform[] enemies;

    private Level currentLevel;
    private int currentWave = 0;
    private bool isSpawning;

    public void Spawn(Level level)
    {
        currentLevel = level;
        StartCoroutine(SpawnWave());
    }
    IEnumerator SpawnWave()
    {
        isSpawning = true;

        if (currentWave >= currentLevel.waves.Count)
        {
            yield break;  //No more waves
        }

        // Get the current wave
        Wave wave = currentLevel.waves[currentWave];

        for (int i = 0; i < wave.enemyCount; i++)
        {
            SpawnEnemy(wave.enemyPrefab);
            yield return new WaitForSeconds(wave.spawnRate); //Wait before spawning the next enemy
        }

        isSpawning = false;
        currentWave++;
    }
    void SpawnEnemy(GameObject prefab)
    {
        GameObject instance = Instantiate(prefab, enemyParentObject.transform);
        int pathEnumerator = 0; //path enumerator, splits enemies across all paths equally
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


