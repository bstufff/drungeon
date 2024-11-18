using System.Collections;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class MinionScript : MonoBehaviour, ISpell
{
    public float moveSpeed = 5f; // Vitesse de déplacement
    public int damageAmount = 10; // Montant des dégâts infligés
    public float damageInterval = 1f; // Intervalle entre chaque dégât en secondes

    private GameObject target;
    private int damageCount = 0; // Compteur de dégâts infligés
    private bool isDealingDamage = false; // Indique si le GameObject est en train d'infliger des dégâts

    public void InitializeSpell()
    {
        // placeholder
    }

    private void Update()
    {
        FindClosestEnemy();

        if (target != null)
        {
            MoveTowardsTarget();
        }
    }

    private void FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        target = closestEnemy;
    }

    private void MoveTowardsTarget()
    {
        //Vector2 direction = (target.transform.position - transform.position).normalized;
        //transform.position += (Vector3)direction * moveSpeed * Time.deltaTime;
        transform.position = Vector2.Lerp(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == target && other.CompareTag("Enemy") && !isDealingDamage)
        {
            isDealingDamage = true;
            StartCoroutine(DealDamage(other.gameObject));
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == target && other.CompareTag("Enemy"))
        {
            isDealingDamage = false;
            StopCoroutine(DealDamage(other.gameObject));
        }
    }

    private IEnumerator DealDamage(GameObject target)
    {
        while (isDealingDamage && damageCount < 10)
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
        }
    }
}
