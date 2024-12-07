using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool  
{
    private List<Enemy> availableEnemies = new List<Enemy>();
    private Enemy defaultEnemy;

    public EnemyPool(Enemy defaultEnemy)
    {
        this.defaultEnemy = defaultEnemy;
    }

    public Enemy GetEnemy()
    {
        Enemy newEnemy;
        if (availableEnemies.Count > 0)
        {
            // Réutilise un ennemi de la pool
            newEnemy = availableEnemies[0];
            availableEnemies.RemoveAt(0);
            newEnemy.gameObject.SetActive(true);
        }
        else
        {
            // Crée un ennemi placeholder si la pool est vide
            newEnemy = UnityEngine.Object.Instantiate(defaultEnemy);
        }

        return newEnemy;
    }
    public void ReturnEnemy(Enemy enemy)
    {
        // Reset enemy state (if necessary)
        enemy.gameObject.SetActive(false);
        availableEnemies.Add(enemy);
    }
}
