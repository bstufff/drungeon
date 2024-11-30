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
    private SpellManager spellManager;
    private EnemySpawner enemySpawner;
    [SerializeField] private ManaManager manaManager;
    private void Start()
    {
        spellManager = GetComponent<SpellManager>();
        enemySpawner = GetComponent<EnemySpawner>();
    }
    public void StartLevel(int levelIndex)
    {
        spellManager.DestroyAllSpells();
        lastLevelPlayed = levelIndex;

        Level level = levels[levelIndex];
        level.levelPrefab.SetActive(true);

        Camera.main.orthographicSize = level.zoom;

        manaManager.ResetMana(level.manaAvailable);

        StartCoroutine(enemySpawner.Spawn(level, 3));

        IsIngame = true;
    }
    public void Lose() 
    {
        spellManager.DestroyAllSpells();
        IsIngame = false;
        gameOverScreen.SetActive(true);
        enemySpawner.DestroyAllEnemies();
    }
    public void Win()
    {
        spellManager.DestroyAllSpells();
        IsIngame = false;
        winScreen.SetActive(true);
        enemySpawner.DestroyAllEnemies();
    }
    public void RetryPreviousLevel()
    {
        StartLevel(lastLevelPlayed);
    }
    public void StartNextLevel()
    {
        levels[lastLevelPlayed].levelPrefab.SetActive(false);
        StartLevel(lastLevelPlayed + 1);
    }


}
