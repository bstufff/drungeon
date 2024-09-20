using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp = 100;
    public int maxHP = 100;
    public int speed = 10;
    public Path path;
    public LevelManager levelManager;
    public Enemy(int hp, int maxHP, int speed, Path path, LevelManager levelManager)
    {
        this.hp = hp;
        this.maxHP = maxHP;
        this.speed = speed;
        this.path = path;
        this.levelManager = levelManager;
    }
    public void TakeDamage(int dmg)
    {
        if (hp - dmg <= 0)
        {
            levelManager.EnemyDeath();
        }
        else
        {
            hp -= dmg;
        }
    }

}
