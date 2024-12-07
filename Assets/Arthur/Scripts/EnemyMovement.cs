using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyMovement : MonoBehaviour
{
    public List<Transform> path; // Chemin que l'ennemi doit suivre
    
    private float _baseSpeed;
    private float _currentSpeed;
    private int targetIndex = 0; // Progression de l'ennemi
    private LevelManager levelManager;

    private void Awake()
    {
        // Initialisation
        levelManager = FindAnyObjectByType<LevelManager>();
    }
    void Update()
    {
        // D�place l'ennemi uniquement si le jeu est actif 
        if (levelManager.IsIngame)
        {
            // Quand un point du chemin est atteint, passe au prochain
            if (transform.position == path[targetIndex].position)
            {
                targetIndex++;
                if (targetIndex >= path.Count)
                {
                    // Si l'ennemi est � la fin de son chemin, d�clenche la fin de la partie
                    levelManager.Lose(); 
                }
            }
            else
            {
                // D�placement
                transform.position = Vector3.MoveTowards(transform.position, path[targetIndex].position, CurrentSpeed * Time.deltaTime);
            }
        }

    }
    // GET-SET
    public float CurrentSpeed
    {
        get { return _currentSpeed; }
        set { _currentSpeed = value; }
    }

    public float BaseSpeed
    { 
        get { return _baseSpeed; }
        set {
            // BaseSpeed est chang� uniquement lorsque un ennemi est r�activ�, 
            // alors CurrentSpeed est aussi modifi�e
            _currentSpeed = value;
            _baseSpeed = value;
        }
    
    }
    // Mise en place du chemin que doit suivre l'ennemi
    public void Initialize(List<Transform> path)
    {
        this.path = path;
        targetIndex = 0;
        transform.position = path[0].position; // T�l�porte au d�but du chemin
    }

}
