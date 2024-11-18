using UnityEngine;

public class DragonAttack : MonoBehaviour
{
    private SpriteRenderer attackSprite;
    [SerializeField] private float _attackDamage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        attackSprite = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (Input.GetMouseButton(0) && FindAnyObjectByType<LevelManager>().IsIngame)
        {
            attackSprite.enabled = true;
        }
        else
        {
            attackSprite.enabled = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (attackSprite.enabled)
        {
            collision.GetComponent<HealthManager>().TakeDamage(_attackDamage * Time.deltaTime);
        }
    }
}
