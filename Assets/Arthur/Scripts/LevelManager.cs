using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class LevelManager : MonoBehaviour
{
    public List<Level> levels = new List<Level>();
    public bool IsIngame = false;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject winScreen;
    private int lastLevelPlayed = 0;
    public void StartLevel(int levelIndex)
    {
        GetComponent<SpellManager>().DestroyAllSpells();
        lastLevelPlayed = levelIndex;
        gameOverScreen.SetActive(false);

        Level level = levels[levelIndex];

        level.levelPrefab.SetActive(true);
        Camera.main.orthographicSize = level.zoom;
        transform.GetComponent<EnemySpawner>().Spawn(level);

        IsIngame = true;
    }
    public void Lose() 
    {
        GetComponent<SpellManager>().DestroyAllSpells();
        IsIngame = false;
        gameOverScreen.SetActive(true);
        transform.GetComponent<EnemySpawner>().DestroyAllEnemies();
    }
    public void Win()
    {
        GetComponent<SpellManager>().DestroyAllSpells();
        IsIngame = false;
        winScreen.SetActive(true);
        transform.GetComponent<EnemySpawner>().DestroyAllEnemies();
    }
    public void RetryPreviousLevel()
    {
        StartLevel(lastLevelPlayed);
    }
    public void StartNextLevel()
    {
        StartLevel(lastLevelPlayed + 1);
    }


}

public interface ISpell {
    public void InitializeSpell();
}