using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public int damageAmount = 10; // Montant des d�g�ts inflig�s
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<HealthManager>().TakeDamage(10);
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
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }
}
