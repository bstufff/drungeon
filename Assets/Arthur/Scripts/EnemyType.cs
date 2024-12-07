using UnityEngine;

// Contient toutes les données qui définissent un type d'ennemi sous la forme d'un ScriptableObject

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/Enemy", order = 1)]
public class EnemyType : ScriptableObject
{
    public float scale; // Taille de l'enemi
    public float speed;
    public int maxHealth;
    public Sprite sprite;
    public Vector2 colliderBounds;
}
