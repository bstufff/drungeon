using System.Collections;
using UnityEngine;

public class CanonScript : MonoBehaviour, ISpell
{
    public float rotationSpeed = 10f; // Vitesse de rotation en degr�s par seconde
    public GameObject cannonballPrefab; // Pr�fabriqu� du boulet de canon
    public Transform firePoint; // Point d'o� le boulet de canon est tir�
    public float fireInterval = 2f; // Intervalle entre chaque tir
    public float cannonballForce = 500f; // Force appliqu�e au boulet de canon

    private float fireTimer = 0f;
    [SerializeField] private float destroyTimer = 10f;

    public void InitializeSpell()
    {
        Destroy(gameObject, destroyTimer);
    }
    private void Update()
    {
        if (GetComponent<Collider2D>().isActiveAndEnabled) // Active 
        {
            GameObject closestEnemy = FindClosestEnemy();

            if (closestEnemy != null)
            {
                RotateTowardsTarget(closestEnemy.transform);
                FireCannonball();
            }
        }
    }

    private GameObject FindClosestEnemy()
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
        return closestEnemy;
    }

    private void RotateTowardsTarget(Transform target)
    {
        Vector2 direction = (target.position - transform.position).normalized;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetAngle, rotationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void FireCannonball()
    {
        fireTimer += Time.deltaTime;

        if (fireTimer >= fireInterval)
        {
            GameObject cannonball = Instantiate(cannonballPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = cannonball.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.AddForce(firePoint.right * cannonballForce);
            }

            fireTimer = 0f;
        }
    }
}
