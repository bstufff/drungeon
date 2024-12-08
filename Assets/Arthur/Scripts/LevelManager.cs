using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class LevelManager : MonoBehaviour
{
    public bool IsIngame = false;

    [SerializeField] private List<Level> _levels = new List<Level>();
    [SerializeField] private GameObject _gameOverScreen;
    [SerializeField] private GameObject _winScreen;
    [SerializeField] private ManaManager _manaManager;
    [SerializeField] private SpellManager _spellManager;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private SaveManager _saveManager;

    private int lastLevelPlayed = 0;
    public void StartLevel(int levelIndex)
    {
        // Mise en place du niveau
        lastLevelPlayed = levelIndex;
        Level level = _levels[levelIndex];
        level.levelPrefab.SetActive(true);

        Camera.main.orthographicSize = level.zoom;

        _spellManager.DestroyAllSpells();

        _manaManager.ResetMana(level.manaAvailable);

        StartCoroutine(_enemySpawner.Spawn(level, 3));

        IsIngame = true;
        SaveData save = new SaveData();
        save.SelectedDragon = FindAnyObjectByType<DragonSelector>().SelectedDragon;
        save.SelectedSpells = _spellManager.SpellSelection;
        save.LevelProgressionIndex = lastLevelPlayed;
        _saveManager.SaveGame(save);
    }
    public void ResetLevelElements()
    {
        // Supprime les sorts et les ennemis 
        _spellManager.DestroyAllSpells();
        _enemySpawner.DestroyAllEnemies();
        IsIngame = false;
    }
    public void Lose()
    {
        _gameOverScreen.SetActive(true);
        ResetLevelElements();
    }
    public void Win()
    {
        _winScreen.SetActive(true);
        ResetLevelElements();
    }
    public void RetryPreviousLevel()
    {
        StartLevel(lastLevelPlayed);
    }
    public void StartNextLevel()
    {
        if (lastLevelPlayed + 1 > _levels.Count)
        {
            Debug.Log("There are no more levels !");
            return;
        }
        _levels[lastLevelPlayed].levelPrefab.SetActive(false);
        StartLevel(lastLevelPlayed + 1);
    }

    public void Continue()
    {
        SaveData loadedData = _saveManager.LoadGame();
        if (loadedData != null)
        {
            FindAnyObjectByType<DragonSelector>().SetSelectedDragon(loadedData.SelectedDragon);
            _spellManager.SpellSelection = loadedData.SelectedSpells;
            lastLevelPlayed = loadedData.LevelProgressionIndex;
            _levels[0].levelPrefab.SetActive(false);
            RetryPreviousLevel();
        }
    }


    public void Quit()
    {
        // Cette méthode fonctionne uniquement dans un build et pas dans l'éditeur
        Application.Quit();
    }

}

