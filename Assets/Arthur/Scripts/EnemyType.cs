using UnityEngine;

// Contient toutes les donn�es qui d�finissent un type d'ennemi sous la forme d'un ScriptableObject

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/Enemy", order = 1)]
public class EnemyType : ScriptableObject
{
    public float Scale; // Taille de l'enemi
    public float Speed;
    public int MaxHealth;
    public Sprite Sprite;
    public Vector2 ColliderBounds;
}
