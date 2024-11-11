using System.Collections;
using UnityEngine;

public class FlameScript : MonoBehaviour
{
    public float damageInterval = 1f; // Intervalle de dégâts en secondes
    public int damageAmount = 10; // Montant des dégâts
    private bool isInTrigger = false; // Vérifie si un objet est dans le trigger

    public void OnTriggerEnter2D(Collider2D other)
    {
        // Commence à infliger des dégâts si un objet entre dans le trigger
        if (!isInTrigger)
        {
            isInTrigger = true;
            StartCoroutine(DealDamage());
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        // Arrête d'infliger des dégâts si l'objet quitte le trigger
        isInTrigger = false;
    }

    public IEnumerator DealDamage()
    {
        while (isInTrigger)
        {
            Debug.LogWarning("Inflige des dégâts : " + damageAmount);
            yield return new WaitForSeconds(damageInterval);
        }
    }
}
