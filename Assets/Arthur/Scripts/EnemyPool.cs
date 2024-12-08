using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class EnemyPool  
{
    private List<Enemy> _availableEnemies = new List<Enemy>();
    // Prefab contenant tous les éléments d'un ennemi (sprite, pv, mouvement, collider) vides
    private Enemy _defaultEnemy; 

    // Constructeur
    public EnemyPool(Enemy defaultEnemy)
    {
        _defaultEnemy = defaultEnemy;
    }

    // Récupération d'un ennemi de la pool
    public Enemy GetEnemy()
    {
        Enemy newEnemy;
        if (_availableEnemies.Count > 0)
        {
            Debug.Log(_availableEnemies.Count);
            // Réutilise un ennemi de la pool
            newEnemy = _availableEnemies[0];
            _availableEnemies.Remove(_availableEnemies[0]);
            newEnemy.gameObject.SetActive(true);
        }
        else
        {
            // Crée un ennemi placeholder si la pool est vide
            newEnemy = UnityEngine.Object.Instantiate(_defaultEnemy);
        }

        return newEnemy;
    }
    // Ajoute un ennemi à la pool
    public void ReturnEnemy(Enemy enemy)
    {
        if (_availableEnemies.Contains(enemy))
        {
            Debug.Log("already in pool!");
            return;
        } 
        Debug.Log(enemy.name + " was returned !");
        enemy.gameObject.SetActive(false);
        _availableEnemies.Add(enemy);
    }
}
