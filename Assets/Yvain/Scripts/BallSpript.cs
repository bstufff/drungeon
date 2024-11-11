using System.Collections;
using UnityEngine;

public class BallSpript : MonoBehaviour
{
    public int damageAmount = 10; // Montant des d�g�ts inflig�s

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Opponent"))
        {
            Debug.Log("Inflige des d�g�ts : " + damageAmount);
        }

        // Dans tous les cas, d�truit le GameObject apr�s la collision
        Destroy(gameObject);
    }
}
