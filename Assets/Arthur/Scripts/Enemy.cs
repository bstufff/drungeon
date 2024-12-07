using UnityEngine;

public class Enemy : MonoBehaviour //, IPrototype<Enemy>
{   
    // �l�ments d'un ennemi qui doivent �tre mis en place
    public EnemyMovement EnemyMovement;
    [SerializeField] HealthManager _healthManager; 
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] BoxCollider2D _boxCollider2D;

    public void Initialize(EnemyType type)
    {
        // Permet � n'importe quel ennemi de devenir n'importe quel autre ennemi 
        transform.localScale = new Vector3(type.scale, type.scale, type.scale);
        EnemyMovement.BaseSpeed = type.speed;
        _healthManager.MaxHealth = type.maxHealth;
        _spriteRenderer.sprite = type.sprite;
        _boxCollider2D.size = type.colliderBounds;
    }

}