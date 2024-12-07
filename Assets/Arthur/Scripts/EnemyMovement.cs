using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyMovement : MonoBehaviour
{
    public List<Transform> path;
    
    private float _baseSpeed;
    private float _currentSpeed;
    private int targetIndex = 0;
    private LevelManager levelManager;

    private void Awake()
    {
        levelManager = FindAnyObjectByType<LevelManager>();
    }
    void Update()
    {
        if (levelManager.IsIngame)
        {
            if (transform.position == path[targetIndex].position)
            {
                targetIndex++;
                if (targetIndex >= path.Count)
                {
                    levelManager.Lose(); 
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, path[targetIndex].position, CurrentSpeed * Time.deltaTime);
            }
        }

    }

    public float CurrentSpeed
    {
        get { return _currentSpeed; }
        set {  
            _currentSpeed = value;
        }
    }

    public float BaseSpeed
    { 
        get { return _baseSpeed; }
        set {
            _currentSpeed = value;
            _baseSpeed = value;
        }
    
    }

    public void Initialize(List<Transform> path)
    {
        this.path = path;
        targetIndex = 0;
        transform.position = path[0].position; //Teleports to the start of the path
    }

}
