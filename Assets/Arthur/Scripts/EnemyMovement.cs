using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyMovement : MonoBehaviour
{
    private Enemy enemyComponentReference;
    private List<Transform> path;
    public int targetIndex = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyComponentReference = GetComponent<Enemy>();
        path = enemyComponentReference.path.path;//Gets the path to follow from the Enemy Component
        transform.position = path[targetIndex].position;//Teleports to the start of the path
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position == path[targetIndex].position)
        {
            Debug.Log("target " + targetIndex + " reached");
            targetIndex++;//When the enemy reaches its target, it changes its target to be the next one on the path.
            if (targetIndex >= path.Count) 
            {
                GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>().Lose("you lost haha");//when reaching the end, calls the lose method in LevelManager and destroys the enemy
                Destroy(gameObject);
            }
        }
        else
        {
            int speed = enemyComponentReference.speed;
            transform.position = Vector3.MoveTowards(transform.position, path[targetIndex].position, speed * Time.deltaTime);
        }

    }

}
