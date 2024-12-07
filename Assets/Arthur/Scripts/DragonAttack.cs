using UnityEngine;

public class DragonAttack : MonoBehaviour
{
    [SerializeField] private float _attackDamage;
    private SpriteRenderer _attackSprite;
    private LevelManager _levelManager;

    void Start()
    {
        // Initialisation 
        _attackSprite = GetComponent<SpriteRenderer>();
        _levelManager = FindAnyObjectByType<LevelManager>();
    }
    void Update()
    {
        // Rend l'attaque visible quand LMB est maintenu
        if (Input.GetMouseButton(0) && _levelManager.IsIngame)
        {
            _attackSprite.enabled = true;
        }
        else
        {
            _attackSprite.enabled = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        // Applique des dégats aux ennemis dans la zone de dommages si l'attaque est visible
        if (_attackSprite.enabled)
        {
            collision.GetComponent<HealthManager>().TakeDamage(_attackDamage * Time.deltaTime);
        }
    }
}
