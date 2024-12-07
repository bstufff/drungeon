using System;
using UnityEngine;

[Serializable]
public class Wave
{
    public EnemyType enemyType; // index du prototype
    public int enemyCount;
    public float spawnRate;
}

