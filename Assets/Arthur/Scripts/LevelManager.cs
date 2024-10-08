using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class LevelManager : MonoBehaviour
{
    public List<Level> levels = new List<Level>();
    private bool ingame = false;
    public void StartLevel(int levelIndex)
    {
        transform.GetComponent<EnemySpawner>().Spawn(levels[levelIndex]);
        ingame = true;
    }
    public void Lose(string DeathMessage) 
    {
        Debug.Log(DeathMessage);

    }
    
    
}