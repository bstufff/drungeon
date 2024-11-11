using System.Collections;
using UnityEngine;

public class BlizzardScript : MonoBehaviour
{
    public float damageInterval = 0.5f; // Intervalle de dégâts en secondes
    public int damageAmount = 10; // Montant des dégâts
    public GameObject target;
    private bool isInTrigger = false; // Vérifie si un objet est dans le trigger

    public void OnTriggerEnter2D(Collider2D other)
    {
        target=other.gameObject;
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
            Debug.LogWarning("Inflige des dégâts : " + damageAmount + "Est ralentie");
            //getcomponent<ScriptEnnemie>().speed -= 1;                 
            yield return new WaitForSeconds(damageInterval);
        }
    }
}
