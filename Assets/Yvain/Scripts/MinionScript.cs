using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class MinionScript : Spell
{
    [SerializeField] private float _moveSpeed = 5f; 
    [SerializeField] private int _damageAmount = 10; 
    [SerializeField] private float _damageInterval = 1f; // Intervalle entre chaque attaque

    private Collider2D _minionCollider;
    private bool _isAttacking = false;
    private GameObject _enemyParentObject;
    private GameObject _target;
    private int _damageCount = 0; // Compteur de dégâts infligés
    

    private void Start()
    {
        _minionCollider = GetComponent<Collider2D>();
    }
    public override float ManaCost => 75;

    public override void PlaceSpell(ManaManager manaManager)
    {
        base.PlaceSpell(manaManager); // Comportement de base du sort

        // Comportement unique au sort
        _enemyParentObject = FindAnyObjectByType<EnemySpawner>().EnemyParentObject.gameObject;
    }

    private void Update()
    {
        if (_minionCollider.isActiveAndEnabled)
            if (_target == null)
                FindClosestEnemy();
            else
                if (_target.activeSelf && _target.CompareTag("Enemy"))
                    MoveTowardsTarget();
                else
                    _target = null;
    }

    private void FindClosestEnemy()
    {
        // Récupère tous les ennemis potentiels 
        Transform[] enemyTransforms = _enemyParentObject.GetComponentsInChildren<Transform>();

        if (enemyTransforms.Length > 1) // Vérifie qu'il y a au moins un ennemi valide
        {
            GameObject closestEnemy = null;
            float closestDistance = Mathf.Infinity;

            foreach (Transform enemyTransform in enemyTransforms)
            {
                if (enemyTransform.gameObject == _enemyParentObject)
                    continue; // Skip l'objet parent car ce n'est pas un ennemi
                
                // Vérifie que l'objet est un ennemi actif
                if (enemyTransform.gameObject.activeSelf && enemyTransform.CompareTag("Enemy")) 
                {
                    // Détermine la distance du minion à l'ennemi

                    float distance = Vector2.Distance(transform.position, enemyTransform.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestEnemy = enemyTransform.gameObject;
                    }
                }

            }

            _target = closestEnemy;
        }

    }

    private void MoveTowardsTarget()
    {
        if (_target != null) // Ne se déplace pas si la cible est invalide
        {
            transform.position = Vector2.Lerp(transform.position, _target.transform.position, _moveSpeed * Time.deltaTime);
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") & !_isAttacking) // Effectue des dégâts au premier ennemi détecté
        {
            _isAttacking = true;
            StartCoroutine(DealDamage(other.gameObject));
        }
    }

    private IEnumerator DealDamage(GameObject target)
    {
        if (_isAttacking && _damageCount < 10)
        {
            // Effectue des dégâts
            _target.TryGetComponent(out HealthManager health);
            health.TakeDamage(_damageAmount);
            _damageCount++;

            if (_damageCount >= 10) // Supprime le minion si il a attaqué plus de 10 fois
            {
                Destroy(gameObject);
                StopAllCoroutines();
            }

            yield return new WaitForSeconds(_damageInterval); // Applique un délai avant de permettre une nouvelle attaque
            _isAttacking = false;
        }
    }
}
