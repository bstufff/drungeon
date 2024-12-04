using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 10;
    public float baseSpeed;

    public Path path;
    public Enemy(float speed, Path path)
    {
        this.speed = speed;
        this.path = path;
    }
    private void Start()
    {
        baseSpeed = speed;
    }

}
