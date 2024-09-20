using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class LevelManager : MonoBehaviour
{
    public List<Level> levels = new List<Level>();
    private void Start()
    {
        StartLevel(levels[0]);
    }
    public void StartLevel(Level level)
    {
        transform.GetComponent<EnemySpawner>().SpawnEnemies(level);
    }
    public void Lose(string DeathMessage) 
    {
        Debug.Log(DeathMessage);

    }
    public void EnemyDeath()
    {
        //Placeholder, ne sert à rien
    }
    
}