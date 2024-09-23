using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyMovement : MonoBehaviour
{
    private Enemy enemyComponentReference;
    private List<Vector3> path;
    public int targetIndex = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyComponentReference = GetComponent<Enemy>();
        path = enemyComponentReference.path.path;
        transform.position = path[targetIndex];//Teleports to the start of the path
    }
    void Update()
    {
        if (transform.position == path[targetIndex])
        {
            Debug.Log("target " + targetIndex + " reached");
            targetIndex++;//When the enemy reaches its target, it changes its target to be the next one on the path.
            if (targetIndex >= path.Count) 
            {
                enemyComponentReference.levelManager.Lose("skill issue");
                Destroy(gameObject);
            }
        }
        else
        {
            int speed = enemyComponentReference.speed;
            transform.position = Vector3.MoveTowards(transform.position, path[targetIndex], speed * Time.deltaTime);
        }

    }

    // Update is called once per frame

}
