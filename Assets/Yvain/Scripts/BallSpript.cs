using System.Collections;
using UnityEngine;

public class BallSpript : MonoBehaviour
{
    public int damageAmount = 10; // Montant des dégâts infligés

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Opponent"))
        {
            Debug.Log("Inflige des dégâts : " + damageAmount);
        }

        // Dans tous les cas, détruit le GameObject après la collision
        Destroy(gameObject);
    }
}
