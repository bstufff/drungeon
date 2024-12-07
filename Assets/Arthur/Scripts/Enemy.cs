using UnityEngine;

public class Enemy : MonoBehaviour //, IPrototype<Enemy>
{
    public EnemyMovement EnemyMovement;
    [SerializeField] HealthManager healthManager; 
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] BoxCollider2D boxCollider2D;

    public void Initialize(EnemyType type)
    {
        transform.localScale = new Vector3(type.scale, type.scale, type.scale);
        EnemyMovement.BaseSpeed = type.speed;
        healthManager.MaxHealth = type.maxHealth;
        healthManager.Heal(type.maxHealth);
        spriteRenderer.sprite = type.sprite;
        boxCollider2D.size = type.colliderBounds;
    }

}