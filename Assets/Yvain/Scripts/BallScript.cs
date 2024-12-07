using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    [SerializeField] private int _damageAmount = 10; // Montant des dégâts infligés
    private void OnCollisionEnter2D(Collision2D other)
    {
        // Vérifie que la collision est un ennemi
        if (other.gameObject.CompareTag("Enemy"))
        {
            // Applique les dégâts et supprime le projectile
            other.gameObject.GetComponent<HealthManager>().TakeDamage(_damageAmount);
            StopCoroutine(DeleteProjectile());
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        StartCoroutine(DeleteProjectile());
    }
    private IEnumerator DeleteProjectile()
    {
        // Supprime le projectile si il ne touche aucune cible 
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }
}
