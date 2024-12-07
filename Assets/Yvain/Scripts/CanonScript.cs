using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CanonScript : Spell
{
    public float rotationSpeed = 10f; // Vitesse de rotation en degrés par seconde
    public GameObject cannonballPrefab; // Préfabriqué du boulet de canon
    public Transform firePoint; // Point d'où le boulet de canon est tiré
    public float fireInterval = 2f; // Intervalle entre chaque tir
    public float cannonballForce = 500f; // Force appliquée au boulet de canon

    private GameObject enemyParentObject;
    private float fireTimer = 0f;
    [SerializeField] private float destroyTimer = 10f;

    public override float ManaCost => 50;

    public override void PlaceSpell(ManaManager manaManager)
    {
        base.PlaceSpell(manaManager);
        enemyParentObject = FindAnyObjectByType<EnemySpawner>().EnemyParentObject.gameObject;
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
        Transform[] enemyTransforms = enemyParentObject.GetComponentsInChildren<Transform>();
        if (enemyTransforms.Length > 1)
        {
            GameObject closestEnemy = null;
            float closestDistance = Mathf.Infinity;

            foreach (Transform enemyTransform in enemyTransforms)
            {
                if (enemyTransform.gameObject == enemyParentObject)
                    continue; // Skips the parent transform as it is not an enemy

                if (enemyTransform.gameObject.activeSelf && enemyTransform.CompareTag("Enemy"))
                {
                    float distance = Vector2.Distance(transform.position, enemyTransform.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestEnemy = enemyTransform.gameObject;
                    }
                }

            }

            return closestEnemy;
        }
        return null;
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
