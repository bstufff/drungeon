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
    private int _targetIndex = 0; // Progression de l'ennemi
    private LevelManager _levelManager;

    private void Awake()
    {
        // Initialisation
        _levelManager = FindAnyObjectByType<LevelManager>();
    }
    void Update()
    {
        // Déplace l'ennemi uniquement si le jeu est actif 
        if (_levelManager.IsIngame)
        {
            // Quand un point du chemin est atteint, passe au prochain
            if (transform.position == path[_targetIndex].position)
            {
                _targetIndex++;
                if (_targetIndex >= path.Count)
                {
                    // Si l'ennemi est à la fin de son chemin, déclenche la fin de la partie
                    _levelManager.Lose(); 
                }
            }
            else
            {
                // Déplacement
                transform.position = Vector3.MoveTowards(transform.position, path[_targetIndex].position, CurrentSpeed * Time.deltaTime);
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
            // BaseSpeed est changé uniquement lorsque un ennemi est réactivé, 
            // alors CurrentSpeed est aussi modifiée
            _currentSpeed = value;
            _baseSpeed = value;
        }
    
    }
    // Mise en place du chemin que doit suivre l'ennemi
    public void Initialize(List<Transform> path)
    {
        this.path = path;
        _targetIndex = 0;
        transform.position = path[0].position; // Téléporte au début du chemin
    }

}
