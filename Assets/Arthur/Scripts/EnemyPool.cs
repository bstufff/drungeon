using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool  
{
    private List<Enemy> availableEnemies = new List<Enemy>();
    // Prefab contenant tous les éléments d'un ennemi (sprite, pv, mouvement, collider) vides
    private Enemy defaultEnemy; 

    // Constructeur
    public EnemyPool(Enemy defaultEnemy)
    {
        this.defaultEnemy = defaultEnemy;
    }

    // Récupération d'un ennemi de la pool
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
    // Ajoute un ennemi à la pool
    public void ReturnEnemy(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
        availableEnemies.Add(enemy);
    }
}
