using System.Collections;
using UnityEngine;

public class MeteorScript : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        
        StartCoroutine(DealDamage());
        gameObject.SetActive(false);
    }
    IEnumerator DealDamage()
    {
        Debug.LogWarning("Inflige des dégâts : " + 10);
        yield return new WaitForSeconds(0.3f);
    }
}
