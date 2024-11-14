using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int speed = 10;
    public int baseSpeed;
    //public EnemyStatus status;
    public Path path;
    public Enemy(int speed, Path path)
    {
        this.speed = speed;
        this.path = path;
    }
    private void Start()
    {
        baseSpeed = speed;
    }

}
