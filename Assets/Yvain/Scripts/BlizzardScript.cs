using System.Collections;
using UnityEngine;

public class BlizzardScript : MonoBehaviour
{
    public float damageInterval = 0.5f; // Intervalle de d�g�ts en secondes
    public int damageAmount = 10; // Montant des d�g�ts
    public GameObject target;
    private bool isInTrigger = false; // V�rifie si un objet est dans le trigger

    public void OnTriggerEnter2D(Collider2D other)
    {
        target=other.gameObject;
        // Commence � infliger des d�g�ts si un objet entre dans le trigger
        if (!isInTrigger)
        {
            isInTrigger = true;
            StartCoroutine(DealDamage());
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        // Arr�te d'infliger des d�g�ts si l'objet quitte le trigger
        isInTrigger = false;
    }

    public IEnumerator DealDamage()
    {
        while (isInTrigger)
        {
            Debug.LogWarning("Inflige des d�g�ts : " + damageAmount + "Est ralentie");
            //getcomponent<ScriptEnnemie>().speed -= 1;                 
            yield return new WaitForSeconds(damageInterval);
        }
    }
}
