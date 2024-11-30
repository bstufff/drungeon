using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class MinionScript : Spell
{
    public float moveSpeed = 5f; 
    public int damageAmount = 10; 
    public float damageInterval = 1f; // Intervalle entre chaque attaque

    private Collider2D minionCollider;
    private bool isAttacking = false;
    private GameObject enemyParentObject;
    private GameObject target;
    private int damageCount = 0; // Compteur de dégâts infligés
    

    private void Start()
    {
        minionCollider = GetComponent<Collider2D>();
    }
    public override float ManaCost => 75;

    public override void PlaceSpell(ManaManager manaManager)
    {
        base.PlaceSpell(manaManager); 
        enemyParentObject = FindAnyObjectByType<EnemySpawner>().EnemyParentObject.gameObject;
    }

    private void Update()
    {
        if (minionCollider.isActiveAndEnabled)
        {
            if (target == null)
            {
                FindClosestEnemy();
            }
            {
                MoveTowardsTarget();
            }
        }
    }

    private void FindClosestEnemy()
    {
        Transform[] enemyTransforms = enemyParentObject.GetComponentsInChildren<Transform>();
        if (enemyTransforms.Length > 1)
        {
            GameObject closestEnemy = null;
            float closestDistance = Mathf.Infinity;

            foreach (Transform enemyTransform in enemyTransforms)
            {
                if (enemyTransform.gameObject == enemyParentObject)
                    continue; // Skips the parent transform as it is not an enemy

                float distance = Vector2.Distance(transform.position, enemyTransform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = enemyTransform.gameObject;
                }
            }

            target = closestEnemy;
        }

    }

    private void MoveTowardsTarget()
    {
        if (target != null) // To avoid moving towards a destroyed enemy
        {
            transform.position = Vector2.Lerp(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") & !isAttacking)
        {
            isAttacking = true;
            StartCoroutine(DealDamage(other.gameObject));
        }
    }

    private IEnumerator DealDamage(GameObject target)
    {
        if (isAttacking && damageCount < 10)
        {
            target.TryGetComponent(out HealthManager health);
            health.TakeDamage(damageAmount);
            damageCount++;

            if (damageCount >= 10)
            {
                Destroy(gameObject);
                StopAllCoroutines();
            }

            yield return new WaitForSeconds(damageInterval);
            isAttacking = false;
        }
    }
}
