using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<Vector3> path = new List<Vector3>();
    public List<Enemy> enemyPrefabs = new List<Enemy>();
    public Transform enemyParentObject;
    public Transform[] enemies;
    public Level currentLevel;


    public void SpawnEnemies(Level level)
    {
        currentLevel = level;
        Enemy instancedEnemy = Instantiate(enemyPrefabs[0], enemyParentObject.transform);
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
        Debug.Log(pathCount);
        instancedEnemy.levelManager = transform.GetComponent<LevelManager>();
        instancedEnemy.GetComponent<Enemy>().path = currentLevel.paths[pathEnumerator];


    }
}

public enum PointType {
    Regular,
    Start,
    End
}

