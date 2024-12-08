using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CanonScript : Spell
{
    [SerializeField] private float _rotationSpeed = 10f; // Vitesse de rotation en degrés par seconde
    [SerializeField] private GameObject _cannonballPrefab; // Préfabriqué du boulet de canon
    [SerializeField] private Transform _firePoint; // Point d'où le boulet de canon est tiré
    [SerializeField] private float _fireInterval = 2f; // Intervalle entre chaque tir
    [SerializeField] private float _cannonballForce = 500f; // Force appliquée au boulet de canon
    [SerializeField] private float _destroyTimer = 10f;

    private GameObject _enemyParentObject;
    private float _fireTimer = 0f;


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

    public override float ManaCost => 75; // Définit le côut du sort

    public override void PlaceSpell(ManaManager manaManager)
    {
        base.PlaceSpell(manaManager); // Comportement de base du sort

        // Comportement unique au sort
        _enemyParentObject = FindAnyObjectByType<EnemySpawner>().EnemyParentObject.gameObject;
        Destroy(gameObject, _destroyTimer);
    }

    private GameObject FindClosestEnemy()
    {
        // Récupère tous les ennemis présents dans la scène
        Transform[] enemyTransforms = _enemyParentObject.GetComponentsInChildren<Transform>();

        if (enemyTransforms.Length > 1) // Vérifie qu'il y a au moins un ennemi à cibler
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
                    // Calcule la distance et détermine l'ennemi actif le plus proche
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

    // Se tourne vers l'ennemi
    private void RotateTowardsTarget(Transform target)
    {
        Vector2 direction = (target.position - transform.position).normalized;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetAngle, _rotationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    // Comportement de tir
    private void FireCannonball()
    {
        _fireTimer += Time.deltaTime;

        if (_fireTimer >= _fireInterval)
        {
            // Crée une instance du boulet et le projette dans une direction
            GameObject cannonball = Instantiate(_cannonballPrefab, _firePoint.position, _firePoint.rotation);
            Rigidbody2D rb = cannonball.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.AddForce(_firePoint.right * _cannonballForce);
            }

            _fireTimer = 0f;
        }
    }
}
