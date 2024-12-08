using UnityEngine;

public class Enemy : MonoBehaviour 
{   
    // Éléments d'un ennemi qui doivent être mis en place
    public EnemyMovement EnemyMovement;
    [SerializeField] HealthManager _healthManager; 
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] BoxCollider2D _boxCollider2D;

    public void Initialize(EnemyType type)
    {
        foreach (Component component in GetComponents<EnemyStatus>())
        {
            Destroy(component);
        }
        // Permet à n'importe quel ennemi de devenir n'importe quel autre ennemi 
        transform.localScale = new Vector3(type.Scale, type.Scale, type.Scale);
        EnemyMovement.BaseSpeed = type.Speed;
        _healthManager.MaxHealth = type.MaxHealth;
        _spriteRenderer.sprite = type.Sprite;
        _boxCollider2D.size = type.ColliderBounds;
    }

}