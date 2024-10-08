using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int speed = 10;
    public Path path;
    public Enemy(int speed, Path path)
    {
        this.speed = speed;
        this.path = path;
    }

}
