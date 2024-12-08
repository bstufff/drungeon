using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool  
{
    private List<Enemy> _availableEnemies = new List<Enemy>();
    // Prefab contenant tous les �l�ments d'un ennemi (sprite, pv, mouvement, collider) vides
    private Enemy _defaultEnemy; 

    // Constructeur
    public EnemyPool(Enemy defaultEnemy)
    {
        _defaultEnemy = defaultEnemy;
    }

    // R�cup�ration d'un ennemi de la pool
    public Enemy GetEnemy()
    {
        Enemy newEnemy;
        if (_availableEnemies.Count > 0)
        {
            // R�utilise un ennemi de la pool
            newEnemy = _availableEnemies[0];
            _availableEnemies.RemoveAt(0);
            newEnemy.gameObject.SetActive(true);
        }
        else
        {
            // Cr�e un ennemi placeholder si la pool est vide
            newEnemy = UnityEngine.Object.Instantiate(_defaultEnemy);
        }

        return newEnemy;
    }
    // Ajoute un ennemi � la pool
    public void ReturnEnemy(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
        _availableEnemies.Add(enemy);
    }
}
