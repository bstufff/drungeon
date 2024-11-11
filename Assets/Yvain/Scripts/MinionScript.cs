using System.Collections;
using UnityEngine;

public class MinionScript : MonoBehaviour
{
    public float moveSpeed = 5f; // Vitesse de déplacement
    public int damageAmount = 10; // Montant des dégâts infligés
    public float damageInterval = 1f; // Intervalle entre chaque dégât en secondes

    private GameObject targetOpponent;
    private int damageCount = 0; // Compteur de dégâts infligés
    private bool isDealingDamage = false; // Indique si le GameObject est en train d'infliger des dégâts

    private void Update()
    {
        FindClosestOpponent();

        if (targetOpponent != null)
        {
            MoveTowardsTarget();
        }
    }

    private void FindClosestOpponent()
    {
        GameObject[] opponents = GameObject.FindGameObjectsWithTag("Opponent");
        GameObject closestOpponent = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject opponent in opponents)
        {
            float distance = Vector2.Distance(transform.position, opponent.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestOpponent = opponent;
            }
        }

        targetOpponent = closestOpponent;
    }

    private void MoveTowardsTarget()
    {
        Vector2 direction = (targetOpponent.transform.position - transform.position).normalized;
        transform.position += (Vector3)direction * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == targetOpponent && other.CompareTag("Opponent") && !isDealingDamage)
        {
            isDealingDamage = true;
            StartCoroutine(DealDamageOverTime());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == targetOpponent && other.CompareTag("Opponent"))
        {
            isDealingDamage = false;
            StopCoroutine(DealDamageOverTime());
        }
    }

    private IEnumerator DealDamageOverTime()
    {
        while (isDealingDamage && damageCount < 10)
        {
            Debug.Log("Inflige des dégâts : " + damageAmount);
            damageCount++;

            if (damageCount >= 10)
            {
                Destroy(gameObject);
            }

            yield return new WaitForSeconds(damageInterval);
        }
    }
}
