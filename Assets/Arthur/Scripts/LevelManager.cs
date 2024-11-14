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
        Level level = levels[levelIndex];
        level.levelPrefab.SetActive(true);
        Camera.main.orthographicSize = level.zoom;
        transform.GetComponent<EnemySpawner>().Spawn(level);
        ingame = true;
    }
    public void Lose(string DeathMessage) 
    {
        Debug.Log(DeathMessage);
    }
    
    
}