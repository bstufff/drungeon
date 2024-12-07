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
    private int _damageCount = 0; // Compteur de d�g�ts inflig�s
    

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
        // R�cup�re tous les ennemis potentiels 
        Transform[] enemyTransforms = _enemyParentObject.GetComponentsInChildren<Transform>();

        if (enemyTransforms.Length > 1) // V�rifie qu'il y a au moins un ennemi valide
        {
            GameObject closestEnemy = null;
            float closestDistance = Mathf.Infinity;

            foreach (Transform enemyTransform in enemyTransforms)
            {
                if (enemyTransform.gameObject == _enemyParentObject)
                    continue; // Skip l'objet parent car ce n'est pas un ennemi
                
                // V�rifie que l'objet est un ennemi actif
                if (enemyTransform.gameObject.activeSelf && enemyTransform.CompareTag("Enemy")) 
                {
                    // D�termine la distance du minion � l'ennemi

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
        if (_target != null) // Ne se d�place pas si la cible est invalide
        {
            transform.position = Vector2.Lerp(transform.position, _target.transform.position, _moveSpeed * Time.deltaTime);
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") & !_isAttacking) // Effectue des d�g�ts au premier ennemi d�tect�
        {
            _isAttacking = true;
            StartCoroutine(DealDamage(other.gameObject));
        }
    }

    private IEnumerator DealDamage(GameObject target)
    {
        if (_isAttacking && _damageCount < 10)
        {
            // Effectue des d�g�ts
            _target.TryGetComponent(out HealthManager health);
            health.TakeDamage(_damageAmount);
            _damageCount++;

            if (_damageCount >= 10) // Supprime le minion si il a attaqu� plus de 10 fois
            {
                Destroy(gameObject);
                StopAllCoroutines();
            }

            yield return new WaitForSeconds(_damageInterval); // Applique un d�lai avant de permettre une nouvelle attaque
            _isAttacking = false;
        }
    }
}
