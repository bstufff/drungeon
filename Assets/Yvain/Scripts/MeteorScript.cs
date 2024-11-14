using System.Collections;
using UnityEngine;

public class MeteorScript : MonoBehaviour
{
    float dmg = 100f;
    float delay = 0.3f;
    public void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(DealDamage(other.gameObject.GetComponent<HealthManager>()));
    }
    IEnumerator DealDamage(HealthManager target)
    {
        target.TakeDamage(dmg);
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}
